﻿import * as actions from './actions';

export type ProductsActionType =
    | ReturnType<typeof actions.setProducts>
    | ReturnType<typeof actions.loadMoreProductsRequest>
    | ReturnType<typeof actions.loadMoreProductsSuccess>
    | ReturnType<typeof actions.loadMoreProductsFailure>
    | ReturnType<typeof actions.setEndStatusLoadMoreProducts>
    | ReturnType<typeof actions.setPagination>
    | ReturnType<typeof actions.getProductsRequest>
    | ReturnType<typeof actions.getProductsSuccess>
    | ReturnType<typeof actions.getProductsFailure>
    | ReturnType<typeof actions.resetProducts>
    | ReturnType<typeof actions.setPagingParams>
    | ReturnType<typeof actions.setPredicates>
    | ReturnType<typeof actions.resetPredicates>
    | ReturnType<typeof actions.resetSortingPredicates>;
export type AddRatingRequestType = ReturnType<typeof actions.addRatingRequest>;
export type ChangeRatingRequestType = ReturnType<typeof actions.changeRatingRequest>;
export type GetProductsRequestType = ReturnType<typeof actions.getProductsRequest>;
export type LoadMoreProductsRequestType = ReturnType<typeof actions.loadMoreProductsRequest>;
