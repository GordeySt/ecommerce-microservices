import { all, call, spawn } from 'redux-saga/effects';
import authRootSaga from '../../../components/auth/state/sagas/authSagas';
import productRootSaga from '../../../components/catalog/state/sagas/productSagas';
import ratingRootSaga from '../../../components/catalog/state/sagas/ratingSagas';
import userRootSaga from './userSagas';

export default function* rootSaga() {
    const sagas = [authRootSaga, productRootSaga, ratingRootSaga, userRootSaga];

    yield all(
        sagas.map((saga) =>
            spawn(function* () {
                while (true) {
                    try {
                        yield call(saga);
                        break;
                    } catch (e) {
                        console.error(e);
                    }
                }
            })
        )
    );
}
