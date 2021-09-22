import * as actions from './actions';

export type ProductsActionType =
    | ReturnType<typeof actions.setProducts>
    | ReturnType<typeof actions.loadMoreProductsRequest>
    | ReturnType<typeof actions.loadMoreProductsSuccess>
    | ReturnType<typeof actions.loadMoreProductsFailure>
    | ReturnType<typeof actions.setEndStatusLoadMoreProducts>
    | ReturnType<typeof actions.setPagination>;
export type AddRatingRequestType = ReturnType<typeof actions.addRatingRequest>;
export type ChangeRatingRequestType = ReturnType<typeof actions.changeRatingRequest>;
export type GetProductsRequestType = ReturnType<typeof actions.getProductsRequest>;
export type LoadMoreProductsRequestType = ReturnType<typeof actions.loadMoreProductsRequest>;
