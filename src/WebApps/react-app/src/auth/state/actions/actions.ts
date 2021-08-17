import { IUserFormValues } from '../../../common/models/user'

export const AuthActions = {
    SIGNUP_REQUEST: 'SIGNUP_REQUEST',
    SIGNUP_SUCCESS: 'SIGNUP_SUCCESS',
    SIGNUP_FAILURE: 'SIGNUP_FAILURE',
    RESEND_EMAIL_VERIFICATION_REQUEST: 'RESEND_EMAIL_VERIFICATION_REQUEST',
    RESEND_EMAIL_VERIFICATION_SUCCESS: 'RESEND_EMAIL_VERIFICATION_REQUEST_SUCCESS',
    RESEND_EMAIL_VERIFICATION_FAILURE: 'RESEND_EMAIL_VERIFICATION_REQUEST_FAILURE',
} as const

export const signUpUserRequest = (signUpValues: IUserFormValues) => ({
    type: AuthActions.SIGNUP_REQUEST,
    payload: signUpValues,
})

export const signUpUserSuccess = () => ({
    type: AuthActions.SIGNUP_SUCCESS,
})

export const signUpUserFailure = (error: Error) => ({
    type: AuthActions.SIGNUP_FAILURE,
    payload: error,
})

export const resendEmailVerificationRequest = (email: string) => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST,
    payload: email,
})

export const resendEmailVerificationSuccess = () => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_SUCCESS,
})

export const resendEmailVerificationFailure = (error: Error) => ({
    type: AuthActions.RESEND_EMAIL_VERIFICATION_FAILURE,
    payload: error,
})
