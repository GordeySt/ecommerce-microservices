import { push } from 'connected-react-router'
import { all, call, put, takeEvery } from 'redux-saga/effects'
import { authApi } from '../../../common/api/authApi'
import { hideLoader, showLoader } from '../../../common/state/actions/loaderActions'
import { AuthActions, signUpUserFailure, signUpUserSuccess } from '../actions/actions'

function* signUpUser(action: any) {
    try {
        yield put(showLoader())
        yield call(authApi.signUp, action.payload)
        yield all([put(signUpUserSuccess()), put(hideLoader()), put(push('/'))])
    } catch (error) {
        window.alert('Problem submiting data')
        yield all([put(hideLoader()), put(signUpUserFailure(error))])
    }
}

export default function* authRootSaga() {
    yield takeEvery(AuthActions.SIGNUP_REQUEST, signUpUser)
}
