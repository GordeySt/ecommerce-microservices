import { push } from 'connected-react-router';
import { toast } from 'react-toastify';
import { all, call, put, takeEvery } from 'redux-saga/effects';
import { authApi } from '../../../../common/api/authApi';
import { AuthRoutes } from '../../../../common/constants/routeConstants';
import { setErrors } from '../../../../common/state/actions/errorActions';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import {
    AuthActions,
    resendEmailVerificationFailure,
    resendEmailVerificationSuccess,
    signUpUserFailure,
    signUpUserSuccess,
    verifyEmailFailure,
    verifyEmailSuccess,
} from '../actions/actions';
import { ResendEmailVerificationRequestType, SignUpUserRequestType, VerifyEmailRequestType } from '../actions/types';

export function* signUpUser({ payload }: SignUpUserRequestType) {
    try {
        yield put(showLoader());
        yield call(authApi.signUp, payload);
        yield all([
            put(signUpUserSuccess()),
            put(hideLoader()),
            put(push(AuthRoutes.signUpSuccessRoute + `?email=${payload.email}`)),
        ]);
    } catch (error) {
        yield all([put(hideLoader()), put(setErrors(error)), put(signUpUserFailure(error))]);
    }
}

export function* resendEmailVerification({ payload }: ResendEmailVerificationRequestType) {
    try {
        yield put(showLoader());
        yield call(authApi.resendEmailVerification, payload);
        yield all([put(hideLoader()), put(resendEmailVerificationSuccess())]);
        toast.success('Resend successfully');
    } catch (error) {
        toast.error('Problem resending email verification');
        yield all([put(hideLoader()), put(resendEmailVerificationFailure(error))]);
    }
}

export function* verifyEmail({ payload }: VerifyEmailRequestType) {
    try {
        yield put(showLoader());
        yield call(authApi.verifyEmail, payload);
        yield all([put(hideLoader()), put(verifyEmailSuccess())]);
        toast.success('Verifiend successfully');
    } catch (error) {
        toast.error('Problem verifying email');
        yield all([put(hideLoader()), put(verifyEmailFailure(error))]);
    }
}

export default function* authRootSaga() {
    yield takeEvery(AuthActions.SIGNUP_REQUEST, signUpUser);
    yield takeEvery(AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST, resendEmailVerification);
    yield takeEvery(AuthActions.VERIFY_EMAIL_REQUEST, verifyEmail);
}
