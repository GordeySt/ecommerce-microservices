import { push } from 'connected-react-router'
import { call, put, takeEvery } from 'redux-saga/effects'
import { authApi } from '../../../common/api/authApi'
import { hideLoader, showLoader } from '../../../common/state/actions/loaderActions'
import { AuthActions, signUpUserFailure } from '../actions/actions'

function* signUpUser(action: any) {
    try {
        yield put(showLoader())
        yield call(authApi.signUp, action.payload)
        yield put(hideLoader())
        yield put(push('/'))
    } catch (error) {
        yield put(hideLoader())
        yield put(signUpUserFailure(error))
    }
}

export default function* authRootSaga() {
    yield takeEvery(AuthActions.SIGNUP_REQUEST, signUpUser)
}
