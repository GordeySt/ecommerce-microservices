import { toast } from 'react-toastify';
import { all, call, put, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import { addRatingFailure, addRatingSuccess, ProductActions } from '../actions/actions';
import { AddRatingRequestType } from '../actions/types';

export function* addRating(action: AddRatingRequestType) {
    try {
        yield put(showLoader());
        yield call(catalogApi.addRating, action.payload);
        yield all([put(addRatingSuccess()), put(hideLoader())]);
        toast.success(`Your rating's ${action.payload.ratingCount}. Thx for your feedback`);
    } catch (error) {
        yield all([put(hideLoader()), put(addRatingFailure(error))]);
        toast.error('Problem giving rating. Please try again!');
    }
}

export default function* ratingRootSaga() {
    yield takeEvery(ProductActions.ADD_RATING_REQUEST, addRating);
}
