import { ERROR_ANY } from '../../../../common/models/anyAliases';
import { IPagination, PaginatedResult } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../types/productTypes';

export const getProductsRequest = () => ({
    type: ProductActions.GET_PRODUCTS_REQUEST,
});

export const getProductsSuccess = () => ({
    type: ProductActions.GET_PRODUCTS_SUCCESS,
});

export const getProductsFailure = (error: ERROR_ANY) => ({
    type: ProductActions.GET_PRODUCTS_FAILURE,
    paylod: error,
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
    type: ProductActions.GET_PRODUCT_BY_ID_REQUEST,
    payload: id,
});

export const getProductByIdSuccess = (product: IProduct) => ({
    type: ProductActions.GET_PRODUCT_BY_ID_SUCCESS,
    paylod: product,
});

export const getProductByIdFailure = (error: ERROR_ANY) => ({
    type: ProductActions.GET_PRODUCT_BY_ID_FAILURE,
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
