import { IPagination } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../actions/productActions';
import { ProductsActionType } from '../types/productTypes';

export interface IProductsState {
    products: IProduct[];
    pagination: IPagination | null;
    isLoadingMore: boolean;
    isLoadingProducts: boolean;
    product: IProduct;
}

const initialState = {
    products: [] as IProduct[],
    product: {} as IProduct,
    pagination: null,
    isLoadingMore: false,
    isLoadingProducts: false,
} as IProductsState;

export const productsReducer = (state = initialState, action: ProductsActionType): IProductsState => {
    switch (action.type) {
        case ProductActions.GET_PRODUCTS_REQUEST:
            return { ...state, isLoadingProducts: true };
        case ProductActions.GET_PRODUCTS_SUCCESS:
        case ProductActions.GET_PRODUCTS_FAILURE:
            return { ...state, isLoadingProducts: false };
        case ProductActions.LOAD_MORE_PRODUCTS_REQUEST:
            return { ...state, isLoadingMore: true };
        case ProductActions.LOAD_MORE_PRODUCTS_SUCCESS:
            const { products } = state;
            const { data, pagination } = action.payload;
            return {
                ...state,
                isLoadingMore: false,
                pagination: pagination,
                products: [...products, ...data],
            };
        case ProductActions.LOAD_MORE_PRODUCTS_FAILURE:
            return { ...state, isLoadingMore: false };
        case ProductActions.GET_PRODUCTS_BY_ID_SUCCESS:
            return { ...state, product: action.payload };
        case ProductActions.SET_PRODUCTS:
            return { ...state, products: action.payload };
        case ProductActions.SET_PAGINATION:
            return { ...state, pagination: action.payload };
        case ProductActions.RESET_PRODUCTS:
            return { ...state, products: [] };
        default:
            return state;
    }
};
