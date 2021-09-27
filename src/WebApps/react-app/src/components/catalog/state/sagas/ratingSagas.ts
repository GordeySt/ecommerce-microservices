import { toast } from 'react-toastify';
import { all, call, put, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import { addRatingFailure, addRatingSuccess, changeRatingSuccess, RatingActions } from '../actions/ratingActions';
import { AddRatingRequestType, ChangeRatingRequestType } from '../types/ratingTypes';

export function* addRating({ payload }: AddRatingRequestType) {
    try {
        yield put(showLoader());
        yield call(catalogApi.addRating, payload);
        yield all([put(addRatingSuccess()), put(hideLoader())]);
        toast.success(`Your rating's ${payload.ratingCount}. Thx for your feedback`);
    } catch (error) {
        yield all([put(hideLoader()), put(addRatingFailure(error))]);
        toast.error('Problem giving rating. Please try again!');
    }
}

export function* changeRating({ payload }: ChangeRatingRequestType) {
    try {
        yield put(showLoader());
        yield call(catalogApi.changeRating, payload);
        yield all([put(changeRatingSuccess()), put(hideLoader())]);
        toast.success(`Your rating has been changed successfully to ${payload.ratingCount}. Thx for your feedback`);
    } catch (error) {
        yield all([put(hideLoader()), put(hideLoader())]);
        toast.error('Problem changing rating. Please try again!');
    }
}

export default function* ratingRootSaga() {
    yield takeEvery(RatingActions.ADD_RATING_REQUEST, addRating);
    yield takeEvery(RatingActions.CHANGE_RATING_REQUEST, changeRating);
}
