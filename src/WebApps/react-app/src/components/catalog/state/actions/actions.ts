import { ERROR_ANY } from '../../../../common/models/anyAliases';
import { IProduct } from '../../../../common/models/product';

export const ProductActions = {
    GET_PRODUCTS_REQUEST: 'GET_PRODUCTS_REQUEST',
    GET_PRODUCTS_SUCCESS: 'GET_PRODUCTS_SUCCESS',
    GET_PRODUCTS_FAILURE: 'GET_PRODUCTS_FAILURE',
    SET_PRODUCTS: 'SET_PRODUCTS',
    ADD_RATING_REQUEST: 'ADD_RATING_REQUEST',
    ADD_RATING_SUCCESS: 'ADD_RATING_SUCCESS',
    ADD_RATING_FAILURE: 'ADD_RATING_FAILURE',
} as const;

export const getProductsRequest = () => ({
    type: ProductActions.GET_PRODUCTS_REQUEST,
});

export const getProductsSuccess = () => ({
    type: ProductActions.GET_PRODUCTS_SUCCESS,
});

export const getProductsFailure = (error: ERROR_ANY) => ({
    type: ProductActions.GET_PRODUCTS_FAILURE,
    payload: error,
});

export const setProducts = (products: IProduct[]) => ({
    type: ProductActions.SET_PRODUCTS,
    payload: products,
});

export const addRatingRequest = (id: string, ratingCount: number | null) => ({
    type: ProductActions.ADD_RATING_REQUEST,
    payload: {
        id,
        ratingCount,
    },
});

export const addRatingSuccess = () => ({
    type: ProductActions.ADD_RATING_SUCCESS,
});

export const addRatingFailure = (error: ERROR_ANY) => ({
    type: ProductActions.ADD_RATING_FAILURE,
    payload: error,
});
