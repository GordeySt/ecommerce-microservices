import { all, call, spawn } from 'redux-saga/effects'
import authRootSaga from '../../../components/auth/state/sagas/authSagas'

export default function* rootSaga() {
    const sagas = [authRootSaga]

    yield all(
        sagas.map((saga) =>
            spawn(function* () {
                while (true) {
                    try {
                        yield call(saga)
                        break
                    } catch (e) {
                        console.error(e)
                    }
                }
            })
        )
    )
}
