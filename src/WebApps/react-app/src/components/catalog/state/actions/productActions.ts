﻿import { ERROR_ANY } from '../../../../common/models/anyAliases';
import { IPagination, PaginatedResult } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';

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

export const loadMoreProductsRequest = () => ({
    type: ProductActions.LOAD_MORE_PRODUCTS_REQUEST,
});

export const loadMoreProductsSuccess = (result: PaginatedResult<IProduct[]>) => ({
    type: ProductActions.LOAD_MORE_PRODUCTS_SUCCESS,
    payload: result,
});

export const loadMoreProductsFailure = (error: ERROR_ANY) => ({
    type: ProductActions.LOAD_MORE_PRODUCTS_FAILURE,
    payload: error,
});

export const getProductByIdRequest = (id: string) => ({
    type: ProductActions.GET_PRODUCTS_BY_ID_REQUEST,
    payload: id,
});

export const getProductByIdSuccess = (product: IProduct) => ({
    type: ProductActions.GET_PRODUCTS_BY_ID_SUCCESS,
    payload: product,
});

export const getProductByIdFailure = (error: ERROR_ANY) => ({
    type: ProductActions.GET_PRODUCTS_BY_ID_FAILURE,
    payload: error,
});

export const setProducts = (products: IProduct[]) => ({
    type: ProductActions.SET_PRODUCTS,
    payload: products,
});

export const setEndStatusLoadMoreProducts = () => ({
    type: ProductActions.SET_END_STATUS,
});

export const setPagination = (pagination: IPagination) => ({
    type: ProductActions.SET_PAGINATION,
    payload: pagination,
});

export const resetProducts = () => ({
    type: ProductActions.RESET_PRODUCTS,
});
