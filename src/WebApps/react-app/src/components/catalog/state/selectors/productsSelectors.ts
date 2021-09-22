﻿import { RootState } from '../../../../common/state/store/commonStore';

export const getProducts = (state: RootState) => state.products.products;
export const getProduct = (state: RootState) => state.products.product;
export const getPagination = (state: RootState) => state.products.pagination;
export const getLoadMoreLoadingStatus = (state: RootState) => state.products.isLoadingMore;
export const getLoadingProductsStatus = (state: RootState) => state.products.isLoadingProducts;
export const getPagingParams = (state: RootState) => state.filtering.pagingParams;
export const getPredicates = (state: RootState) => state.filtering.predicates;
