import { IUserFormValues } from '../../../../common/models/user';

export const AuthActions = {
    SIGNUP_REQUEST: 'SIGNUP_REQUEST',
    SIGNUP_SUCCESS: 'SIGNUP_SUCCESS',
    SIGNUP_FAILURE: 'SIGNUP_FAILURE',
    RESEND_EMAIL_VERIFICATION_REQUEST: 'RESEND_EMAIL_VERIFICATION_REQUEST',
    RESEND_EMAIL_VERIFICATION_SUCCESS: 'RESEND_EMAIL_VERIFICATION_SUCCESS',
    RESEND_EMAIL_VERIFICATION_FAILURE: 'RESEND_EMAIL_VERIFICATION_FAILURE',
    VERIFY_EMAIL_REQUEST: 'VERIFY_EMAIL_REQUEST',
    VERIFY_EMAIL_SUCCESS: 'VERIFY_EMAIL_SUCCESS',
    VERIFY_EMAIL_FAILURE: 'VERIFY_EMAIL_FAILURE',
} as const;

export const signUpUserRequest = (signUpValues: IUserFormValues) => ({
    type: AuthActions.SIGNUP_REQUEST,
    payload: signUpValues,
});

export const signUpUserSuccess = () => ({
    type: AuthActions.SIGNUP_SUCCESS,
});

export const signUpUserFailure = (error: Error) => ({
    type: AuthActions.SIGNUP_FAILURE,
    payload: error,
});

export const resendEmailVerificationRequest = (email: string) => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST,
    payload: email,
});

export const resendEmailVerificationSuccess = () => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_SUCCESS,
});

export const resendEmailVerificationFailure = (error: Error) => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_FAILURE,
    payload: error,
});

export const verifyEmailRequest = (email: string, token: string) => ({
    type: AuthActions.VERIFY_EMAIL_REQUEST,
    payload: {
        email,
        token,
    },
});

export const verifyEmailSuccess = () => ({
    type: AuthActions.VERIFY_EMAIL_SUCCESS,
});

export const verifyEmailFailure = (error: Error) => ({
    type: AuthActions.VERIFY_EMAIL_FAILURE,
    payload: error,
});
