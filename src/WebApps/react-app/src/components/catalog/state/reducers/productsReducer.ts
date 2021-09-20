﻿import { IPagination } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../actions/actions';
import { ProductsActionType } from '../actions/types';

export interface IProductsState {
    products: IProduct[];
    pagination: IPagination;
    isLoadingMore: boolean;
}

const initialState: IProductsState = {
    products: [] as IProduct[],
    pagination: {} as IPagination,
    isLoadingMore: false,
};

export const productsReducer = (state = initialState, action: ProductsActionType) => {
    switch (action.type) {
        case ProductActions.LOAD_MORE_PRODUCTS_REQUEST:
            return { ...state, isLoadingMore: true };
        case ProductActions.LOAD_MORE_PRODUCTS_SUCCESS:
            const { products } = state;
            const { data, pagination } = action.payload;
            return {
                ...state,
                isLoadingMore: false,
                pagination: pagination,
                products: [...products, data],
            };
        case ProductActions.LOAD_MORE_PRODUCTS_FAILURE:
            return { ...state, isLoadingMore: false };
        case ProductActions.SET_PRODUCTS:
            return { ...state, products: action.payload };
        case ProductActions.SET_PAGINATION:
            return { ...state, pagination: action.payload };
        default:
            return state;
    }
};
