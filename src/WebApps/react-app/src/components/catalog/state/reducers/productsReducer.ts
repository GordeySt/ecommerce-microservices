import { IPagination, PagingParams } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../actions/actions';
import { ProductsActionType } from '../actions/types';

export interface IPredicate {
    key: string;
    value: string;
}

const initialState = {
    products: [] as IProduct[],
    pagination: {} as IPagination,
    pagingParams: {} as PagingParams,
    predicates: [] as IPredicate[],
    isLoadingMore: false,
    isLoadingProducts: false,
};

export const productsReducer = (state = initialState, action: ProductsActionType) => {
    switch (action.type) {
        case ProductActions.GET_PRODUCTS_REQUEST:
            return { ...state, isLoadingProducts: true };
        case ProductActions.GET_PRODUCTS_SUCCESS:
            return { ...state, isLoadingProducts: false };
        case ProductActions.GET_PRODUCTS_FAILURE:
            return { ...state, isLoadingProducts: false };
        case ProductActions.LOAD_MORE_PRODUCTS_REQUEST:
            return { ...state, isLoadingMore: true };
        case ProductActions.LOAD_MORE_PRODUCTS_SUCCESS:
            const { products } = state;
            return {
                ...state,
                isLoadingMore: false,
                pagination: action.payload.pagination,
                products: [...products, ...action.payload.data],
            };
        case ProductActions.LOAD_MORE_PRODUCTS_FAILURE:
            return { ...state, isLoadingMore: false };
        case ProductActions.SET_PRODUCTS:
            return { ...state, products: action.payload };
        case ProductActions.SET_PAGINATION:
            return { ...state, pagination: action.payload };
        case ProductActions.RESET_PRODUCTS:
            return { ...state, products: [] };
        case ProductActions.SET_PAGING_PARAMS:
            return { ...state, pagingParams: action.payload };
        case ProductActions.SET_PREDICATES:
            const { predicates } = state;
            return { ...state, predicates: [...predicates, action.payload] };
        case ProductActions.RESET_PREDICATES:
            return { ...state, predicates: [] };
        case ProductActions.RESET_SORTING_PREDICATES:
            const newPredicates = state.predicates.filter((predicate) => predicate.key === 'minimumAge');

            return { ...state, predicates: newPredicates };
        default:
            return state;
    }
};
