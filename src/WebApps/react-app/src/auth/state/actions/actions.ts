import { IUserFormValues } from '../../../common/models/user'

export const AuthActions = {
    SIGNUP_REQUEST: 'SIGNUP_REQUEST',
    SIGNUP_SUCCESS: 'SIGNUP_SUCCESS',
    SIGNUP_FAILURE: 'SIGNUP_FAILURE',
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
