import { all, call, put, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../api/catalogApi';
import { ICurrentUser } from '../../models/user';
import { setErrors } from '../actions/errorActions';
import { hideLoader, showLoader } from '../actions/loaderActions';
import {
    getUserByIdFailure,
    getUserByIdSuccess,
    IGetUserByIdRequest,
    setUser,
    UserActions,
} from '../actions/userActions';

export function* getUserById(action: IGetUserByIdRequest) {
    try {
        yield put(showLoader());
        const user: ICurrentUser = yield call(catalogApi.getUserById, action.payload);
        yield all([put(getUserByIdSuccess()), put(setUser(user)), put(hideLoader())]);
    } catch (error) {
        yield all([put(hideLoader()), setErrors(error), put(getUserByIdFailure(error))]);
    }
}

export default function* userRootSaga() {
    yield takeEvery(UserActions.GET_USER_BY_ID_REQUEST, getUserById);
}
