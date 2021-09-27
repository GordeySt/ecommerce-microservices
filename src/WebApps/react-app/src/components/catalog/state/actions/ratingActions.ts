import { ERROR_ANY } from '../../../../common/models/anyAliases';

export const RatingActions = {
    ADD_RATING_REQUEST: 'ADD_RATING_REQUEST',
    ADD_RATING_SUCCESS: 'ADD_RATING_SUCCESS',
    ADD_RATING_FAILURE: 'ADD_RATING_FAILURE',
    CHANGE_RATING_REQUEST: 'CHANGE_RATING_REQUEST',
    CHANGE_RATING_SUCCESS: 'CHANGE_RATING_SUCCESS',
    CHANGE_RATING_FAILURE: 'CHANGE_RATING_FAILURE',
} as const;

export const addRatingRequest = (id: string, ratingCount: number | null) => ({
    type: RatingActions.ADD_RATING_REQUEST,
    payload: {
        id,
        ratingCount,
    },
});

export const addRatingSuccess = () => ({
    type: RatingActions.ADD_RATING_SUCCESS,
});

export const addRatingFailure = (error: ERROR_ANY) => ({
    type: RatingActions.ADD_RATING_FAILURE,
    payload: error,
});

export const changeRatingRequest = (id: string, ratingCount: number | null) => ({
    type: RatingActions.CHANGE_RATING_REQUEST,
    payload: {
        id,
        ratingCount,
    },
});

export const changeRatingSuccess = () => ({
    type: RatingActions.CHANGE_RATING_SUCCESS,
});

export const changeRatingFailure = (error: ERROR_ANY) => ({
    type: RatingActions.CHANGE_RATING_FAILURE,
    payload: error,
});
