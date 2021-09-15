import { RootState } from '../../../../common/state/store/commonStore';

export const getProducts = (state: RootState) => state.products.products;
export const getPagination = (state: RootState) => state.products.pagination;
export const getLoadMoreLoadingStatus = (state: RootState) => state.products.isLoadingMore;
export const getLoadingProductsStatus = (state: RootState) => state.products.isLoadingProducts;
