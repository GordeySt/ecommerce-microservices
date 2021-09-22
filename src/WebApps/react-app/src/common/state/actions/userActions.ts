import { ERROR_ANY } from '../../models/anyAliases';
import { IRatingUser } from '../../models/user';

export const UserActions = {
    GET_USER_BY_ID_REQUEST: 'GET_USER_BY_ID_REQEUST',
    GET_USER_BY_ID_SUCCESS: 'GET_USER_BY_ID_SUCCESS',
    GET_USER_BY_ID_FAILURE: 'GET_USER_BY_ID_FAILURE',
    GET_CURRENT_USER_REQUEST: 'GET_CURRENT_USER_REQUEST',
    GET_CURRENT_USER_SUCCESS: 'GET_CURRENT_USER_SUCCESS',
    GET_CURRENT_USER_FAILURE: 'GET_CURRENT_USER_FAILURE',
    SET_USER: 'SET_USER',
} as const;

export interface IGetUserByIdRequest {
    type: typeof UserActions.GET_USER_BY_ID_REQUEST;
    payload: string | null;
}

export type SetUserActionType = ReturnType<typeof setUser>;

export const getUserByIdRequest = (id: string | null): IGetUserByIdRequest => ({
    type: UserActions.GET_USER_BY_ID_REQUEST,
    payload: id,
});

export const getUserByIdSuccess = () => ({
    type: UserActions.GET_USER_BY_ID_SUCCESS,
});

export const getUserByIdFailure = (error: ERROR_ANY) => ({
    type: UserActions.GET_USER_BY_ID_FAILURE,
    payload: error,
});

export const getCurrentUserRequest = () => ({
    type: UserActions.GET_CURRENT_USER_REQUEST,
});

export const getCurrentUserSuccess = () => ({
    type: UserActions.GET_CURRENT_USER_SUCCESS,
});

export const getCurrentUserFailure = (error: ERROR_ANY) => ({
    type: UserActions.GET_CURRENT_USER_FAILURE,
    payload: error,
});

export const setUser = (user: IRatingUser) => ({
    type: UserActions.SET_USER,
    payload: user,
});
