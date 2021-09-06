import { push } from 'connected-react-router';
import { expectSaga } from 'redux-saga-test-plan';
import { call } from 'redux-saga-test-plan/matchers';
import { throwError } from 'redux-saga-test-plan/providers';
import { authApi } from '../../common/api/authApi';
import { AuthRoutes } from '../../common/constants/routeConstants';
import { setErrors } from '../../common/state/actions/errorActions';
import { hideLoader, showLoader } from '../../common/state/actions/loaderActions';
import { errorReducer } from '../../common/state/reducers/errorReducer';
import {
    AuthActions,
    resendEmailVerificationFailure,
    resendEmailVerificationRequest,
    resendEmailVerificationSuccess,
    signUpUserFailure,
    signUpUserRequest,
    signUpUserSuccess,
    verifyEmailFailure,
    verifyEmailRequest,
    verifyEmailSuccess,
} from '../../components/auth/state/actions/actions';
import { resendEmailVerification, signUpUser, verifyEmail } from '../../components/auth/state/sagas/authSagas';

describe('Auth Sagas Tests', () => {
    it(`Should sign up user successfuly on ${AuthActions.SIGNUP_REQUEST}`, () => {
        // Arrange
        const signUpValues = {
            email: 'email@test.com',
            password: 'password1',
        };

        const action = signUpUserRequest(signUpValues);

        // Act and Assert
        return expectSaga(signUpUser, action)
            .put(showLoader())
            .provide([[call(authApi.signUp, signUpValues), null]])
            .put(signUpUserSuccess())
            .put(hideLoader())
            .put(push(AuthRoutes.signUpSuccessRoute + `?email=${signUpValues.email}`))
            .run();
    });

    it(`Should throw error on signing up user on ${AuthActions.SIGNUP_REQUEST}`, () => {
        // Arrange
        const signUpValues = {
            email: 'email@test.com',
            password: 'password1',
        };
        const error = new Error('test error');

        const action = signUpUserRequest(signUpValues);

        // Act and Assert
        return expectSaga(signUpUser, action)
            .withReducer(errorReducer)
            .put(showLoader())
            .provide([[call(authApi.signUp, signUpValues), throwError(error)]])
            .put(hideLoader())
            .put(setErrors(error))
            .put(signUpUserFailure(error))
            .hasFinalState({
                error,
            })
            .run();
    });

    it(`Should resend email verification successfuly on ${AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST}`, () => {
        // Arrange
        const email = 'email@test.com';
        const action = resendEmailVerificationRequest(email);

        // Act and Assert
        return expectSaga(resendEmailVerification, action)
            .put(showLoader())
            .provide([[call(authApi.resendEmailVerification, email), null]])
            .put(hideLoader())
            .put(resendEmailVerificationSuccess())
            .run();
    });

    it(`Should throw error on resend email verification on ${AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST}`, () => {
        // Arrange
        const email = 'email@test.com';
        const error = new Error('test error');
        const action = resendEmailVerificationRequest(email);

        // Act and Assert
        return expectSaga(resendEmailVerification, action)
            .put(showLoader())
            .provide([[call(authApi.resendEmailVerification, email), throwError(error)]])
            .put(hideLoader())
            .put(resendEmailVerificationFailure(error))
            .run();
    });

    it(`Should verify email successfuly on ${AuthActions.VERIFY_EMAIL_REQUEST}`, () => {
        // Arrange
        const email = 'email@test.com';
        const token = 'test-token';
        const error = new Error('test error');
        const action = verifyEmailRequest(email, token);

        // Act and Assert
        return expectSaga(verifyEmail, action)
            .put(showLoader())
            .provide([[call(authApi.verifyEmail, { email, token }), throwError(error)]])
            .put(hideLoader())
            .put(verifyEmailFailure(error))
            .run();
    });

    it(`Should throw error on verify email on ${AuthActions.VERIFY_EMAIL_REQUEST}`, () => {
        // Arrange
        const email = 'email@test.com';
        const token = 'test-token';
        const action = verifyEmailRequest(email, token);

        // Act and Assert
        return expectSaga(verifyEmail, action)
            .put(showLoader())
            .provide([[call(authApi.verifyEmail, { email, token }), null]])
            .put(hideLoader())
            .put(verifyEmailSuccess())
            .run();
    });
});
