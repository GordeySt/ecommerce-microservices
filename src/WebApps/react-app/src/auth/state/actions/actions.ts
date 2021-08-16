import { IUserFormValues } from '../../../common/models/user'

export const AuthActions = {
    SIGNUP_REQUEST: 'SIGNUP_REQUEST',
    SIGNUP_SUCCESS: 'SIGNUP_SUCCESS',
    SIGNUP_FAILURE: 'SIGNUP_FAILURE',
}

export function signUpUserRequest(signUpValues: IUserFormValues): any {
    return {
        type: AuthActions.SIGNUP_REQUEST,
        payload: signUpValues,
    }
}

export function signUpUserSuccess(): any {
    return {
        type: AuthActions.SIGNUP_SUCCESS,
    }
}

export function signUpUserFailure(error: Error): any {
    return {
        type: AuthActions.SIGNUP_FAILURE,
        payload: error,
    }
}
