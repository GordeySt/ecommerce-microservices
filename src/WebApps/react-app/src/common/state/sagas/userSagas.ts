import { push } from 'connected-react-router';
import { StatusCodes } from 'http-status-codes';
import { all, call, put, takeEvery } from 'redux-saga/effects';
import { userApi } from '../../api/userApi';
import { CommonRoutes } from '../../constants/routeConstants';
import { ICurrentUser, IRatingUser } from '../../models/user';
import { setErrors } from '../actions/errorActions';
import { hideLoader, showLoader } from '../actions/loaderActions';
import {
    getCurrentUserFailure,
    getCurrentUserSuccess,
    getUserByIdFailure,
    getUserByIdRequest,
    getUserByIdSuccess,
    IGetUserByIdRequest,
    setUser,
    UserActions,
} from '../actions/userActions';

export function* getUserById({ payload }: IGetUserByIdRequest) {
    try {
        yield put(showLoader());
        const user: IRatingUser = yield call(userApi.getUserById, payload);
        yield all([put(getUserByIdSuccess()), put(setUser(user)), put(hideLoader())]);
    } catch (error: any) {
        if (error.status === StatusCodes.NOT_FOUND) {
            yield call(createUser, payload);
        }
        yield all([put(hideLoader()), setErrors(error), put(getUserByIdFailure(error))]);
    }
}

export function* createUser(payload: string | null) {
    try {
        yield all([put(push(CommonRoutes.welcomePageRoute)), put(showLoader())]);
        yield call(userApi.createUser);
        const user: IRatingUser = yield call(userApi.getUserById, payload);
        yield all([put(getUserByIdSuccess()), put(setUser(user)), put(hideLoader())]);
    } catch (error) {
        yield all([put(hideLoader()), put(getUserByIdFailure(error))]);
    }
}

export function* getCurrentUser() {
    try {
        yield put(showLoader());
        const currentUser: ICurrentUser = yield call(userApi.getCurrentUser);
        const ratingUser: IRatingUser = yield call(getUserById, getUserByIdRequest(currentUser.id));
        yield all([put(getCurrentUserSuccess()), put(setUser(ratingUser)), put(hideLoader())]);
    } catch (error: any) {
        yield all([put(hideLoader()), put(getCurrentUserFailure(error))]);
    }
}

export default function* userRootSaga() {
    yield takeEvery(UserActions.GET_USER_BY_ID_REQUEST, getUserById);
    yield takeEvery(UserActions.GET_CURRENT_USER_REQUEST, getCurrentUser);
}
