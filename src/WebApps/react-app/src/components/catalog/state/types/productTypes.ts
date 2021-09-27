import { InferValueTypes } from '../../../../common/state/types/commonTypes';
import * as actions from '../actions/productActions';

export const ProductActions = {
    GET_PRODUCTS_REQUEST: 'GET_PRODUCTS_REQUEST',
    GET_PRODUCTS_SUCCESS: 'GET_PRODUCTS_SUCCESS',
    GET_PRODUCTS_FAILURE: 'GET_PRODUCTS_FAILURE',
    LOAD_MORE_PRODUCTS_REQUEST: 'LOAD_MORE_PRODUCTS_REQUEST',
    LOAD_MORE_PRODUCTS_SUCCESS: 'LOAD_MORE_PRODUCTS_SUCCESS',
    LOAD_MORE_PRODUCTS_FAILURE: 'LOAD_MORE_PRODUCTS_FAILURE',
    GET_PRODUCTS_BY_ID_REQUEST: 'GET_PRODUCTS_BY_ID_REQUEST',
    GET_PRODUCTS_BY_ID_SUCCESS: 'GET_PRODUCTS_BY_ID_SUCCESS',
    GET_PRODUCTS_BY_ID_FAILURE: 'GET_PRODUCTS_BY_ID_FAILURE',
    SET_PRODUCTS: 'SET_PRODUCTS',
    SET_PAGINATION: 'SET_PAGINATION',
    SET_END_STATUS: 'SET_END_STATUS',
    RESET_PRODUCTS: 'RESET_PRODUCTS',
} as const;

export type ProductsActionType = ReturnType<InferValueTypes<typeof actions>>;

export type GetProductsRequestType = ReturnType<typeof actions.getProductsRequest>;
export type LoadMoreProductsRequestType = ReturnType<typeof actions.loadMoreProductsRequest>;
export type GetProductsByIdRequestType = ReturnType<typeof actions.getProductByIdRequest>;
