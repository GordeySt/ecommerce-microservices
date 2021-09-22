import * as actions from '../actions/productActions';

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
    | ReturnType<typeof actions.getProductByIdSuccess>
    | ReturnType<typeof actions.resetProducts>;
export type GetProductsRequestType = ReturnType<typeof actions.getProductsRequest>;
export type LoadMoreProductsRequestType = ReturnType<typeof actions.loadMoreProductsRequest>;
export type GetProductsByIdRequestType = ReturnType<typeof actions.getProductByIdRequest>;
