import { ERROR_ANY } from '../../../../common/models/anyAliases';
import { IProduct } from '../../../../common/models/product';

export const ProductActions = {
    GET_PRODUCTS_REQUEST: 'GET_PRODUCTS_REQUEST',
    GET_PRODUCTS_SUCCESS: 'GET_PRODUCTS_SUCCESS',
    GET_PRODUCTS_FAILURE: 'GET_PRODUCTS_FAILURE',
    SET_PRODUCTS: 'SET_PRODUCTS',
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
