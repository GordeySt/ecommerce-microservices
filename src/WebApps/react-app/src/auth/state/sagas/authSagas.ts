import { push } from 'connected-react-router'
import { toast } from 'react-toastify'
import { all, call, put, takeEvery } from 'redux-saga/effects'
import { authApi } from '../../../common/api/authApi'
import { hideLoader, showLoader } from '../../../common/state/actions/loaderActions'
import {
    AuthActions,
    resendEmailVerificationFailure,
    resendEmailVerificationSuccess,
    signUpUserFailure,
    signUpUserSuccess,
} from '../actions/actions'
import { resendEmailVerificationRequestType, SignUpUserRequestType } from '../actions/types'

function* signUpUser(action: SignUpUserRequestType) {
    try {
        yield put(showLoader())
        yield call(authApi.signUp, action.payload)
        yield all([
            put(signUpUserSuccess()),
            put(hideLoader()),
            put(push(`/auth/signUpSuccess?email=${action.payload.email}`)),
        ])
    } catch (error) {
        toast.error('Problem signing up')
        yield all([put(hideLoader()), put(signUpUserFailure(error))])
    }
}

function* resendEmailVerification(action: resendEmailVerificationRequestType) {
    try {
        yield put(showLoader())
        yield call(authApi.resendEmailVerification, action.payload)
        yield all([put(hideLoader()), resendEmailVerificationSuccess()])
        toast.success('Resend successfully')
    } catch (error) {
        toast.error('Problem resending email verification')
        yield all([put(hideLoader()), put(resendEmailVerificationFailure(error))])
    }
}

export default function* authRootSaga() {
    yield takeEvery(AuthActions.SIGNUP_REQUEST, signUpUser)
    yield takeEvery(AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST, resendEmailVerification)
}
