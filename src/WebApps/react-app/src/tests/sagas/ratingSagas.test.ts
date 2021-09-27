import { expectSaga } from 'redux-saga-test-plan';
import { call } from 'redux-saga-test-plan/matchers';
import { throwError } from 'redux-saga-test-plan/providers';
import { catalogApi } from '../../common/api/catalogApi';
import { hideLoader, showLoader } from '../../common/state/actions/loaderActions';
import {
    addRatingFailure,
    addRatingRequest,
    addRatingSuccess,
    RatingActions,
} from '../../components/catalog/state/actions/ratingActions';
import { addRating } from '../../components/catalog/state/sagas/ratingSagas';

describe('Rating Sagas Tests', () => {
    it(`Should add rating successfuly on ${RatingActions.ADD_RATING_REQUEST}`, () => {
        // Arrange
        const id = 'testid';
        const ratingCount = 4;

        const action = addRatingRequest(id, ratingCount);

        // Act and Assert
        return expectSaga(addRating, action)
            .put(showLoader())
            .provide([[call(catalogApi.addRating, action.payload), null]])
            .put(addRatingSuccess())
            .put(hideLoader())
            .run();
    });

    it(`Should throw error on adding rating on ${RatingActions.ADD_RATING_REQUEST}`, () => {
        // Arrange
        const error = new Error('test error');
        const id = 'testid';
        const ratingCount = 4;

        const action = addRatingRequest(id, ratingCount);

        // Act and Assert
        return expectSaga(addRating, action)
            .put(showLoader())
            .provide([[call(catalogApi.addRating, action.payload), throwError(error)]])
            .put(addRatingFailure(error))
            .put(hideLoader())
            .run();
    });
});
