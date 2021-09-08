import { ERROR_ANY } from '../../models/anyAliases';
import { ICurrentUser } from '../../models/user';

export const UserActions = {
    GET_USER_BY_ID_REQUEST: 'GET_USER_BY_ID_REQEUST',
    GET_USER_BY_ID_SUCCESS: 'GET_USER_BY_ID_SUCCESS',
    GET_USER_BY_ID_FAILURE: 'GET_USER_BY_ID_FAILURE',
    SET_USER: 'SET_USER',
};

export interface IGetUserByIdRequest {
    type: typeof UserActions.GET_USER_BY_ID_REQUEST;
    payload: string;
}

export type SetUserActionType = ReturnType<typeof setUser>;

export const getUserByIdRequest = (id: string): IGetUserByIdRequest => ({
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

export const setUser = (user: ICurrentUser) => ({
    type: UserActions.SET_USER,
    payload: user,
});
