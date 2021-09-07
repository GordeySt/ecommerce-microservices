import { all, call, spawn } from 'redux-saga/effects';
import authRootSaga from '../../../components/auth/state/sagas/authSagas';
import productRootSaga from '../../../components/catalog/state/sagas/productSagas';

export default function* rootSaga() {
    const sagas = [authRootSaga, productRootSaga];

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
