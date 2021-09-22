import { IPagination } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../actions/productActions';
import { ProductsActionType } from '../types/productTypes';

export interface IPredicate {
    key: string;
    value: string;
}

export interface IProductsState {
    products: IProduct[];
    pagination: IPagination | null;
    isLoadingMore: boolean;
}

const initialState = {
    products: [] as IProduct[],
    pagination: null,
    isLoadingMore: false,
} as IProductsState;

export const productsReducer = (state = initialState, action: ProductsActionType): IProductsState => {
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
                products: [...products, ...data],
            };
        case ProductActions.LOAD_MORE_PRODUCTS_FAILURE:
            return { ...state, isLoadingMore: false };
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
