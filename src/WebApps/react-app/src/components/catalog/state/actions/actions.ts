﻿import { ERROR_ANY } from '../../../../common/models/anyAliases';
import { IPagination, PaginatedResult, PagingParams } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';

export const ProductActions = {
    GET_PRODUCTS_REQUEST: 'GET_PRODUCTS_REQUEST',
    GET_PRODUCTS_SUCCESS: 'GET_PRODUCTS_SUCCESS',
    GET_PRODUCTS_FAILURE: 'GET_PRODUCTS_FAILURE',
    LOAD_MORE_PRODUCTS_REQUEST: 'LOAD_MORE_PRODUCTS_REQUEST',
    LOAD_MORE_PRODUCTS_SUCCESS: 'LOAD_MORE_PRODUCTS_SUCCESS',
    LOAD_MORE_PRODUCTS_FAILURE: 'LOAD_MORE_PRODUCTS_FAILURE',
    SET_PRODUCTS: 'SET_PRODUCTS',
    SET_PAGINATION: 'SET_PAGINATION',
    SET_END_STATUS: 'SET_END_STATUS',
    RESET_PRODUCTS: 'RESET_PRODUCTS',
    SET_PAGING_PARAMS: 'SET_PAGING_PARAMS',
    SET_PREDICATES: 'SET_PREDICATES',
    RESET_PREDICATES: 'RESET_PREDICATES',
    RESET_SORTING_PREDICATES: 'RESET_SORTING_PREDICATES',
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

export const setPagingParams = (pagingParams: PagingParams) => ({
    type: ProductActions.SET_PAGING_PARAMS,
    payload: pagingParams,
});

export const setPredicates = (key: string, value: string) => ({
    type: ProductActions.SET_PREDICATES,
    payload: {
        key,
        value,
    },
});

export const resetPredicates = () => ({
    type: ProductActions.RESET_PREDICATES,
});

export const resetSortingPredicates = () => ({
    type: ProductActions.RESET_SORTING_PREDICATES,
});
