import { all, call, put, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { PaginatedResult } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { setErrors } from '../../../../common/state/actions/errorActions';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import {
    getProductsFailure,
    getProductsSuccess,
    loadMoreProductsFailure,
    loadMoreProductsSuccess,
    ProductActions,
    setPagination,
    setProducts,
} from '../actions/actions';
import { GetProductsRequestType, LoadMoreProductsRequestType } from '../actions/types';

export function* getProducts({ payload }: GetProductsRequestType) {
    try {
        yield put(showLoader());
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, payload);
        yield all([
            put(getProductsSuccess()),
            put(setProducts(result.data)),
            put(setPagination(result.pagination)),
            put(hideLoader()),
        ]);
    } catch (error) {
        yield all([put(hideLoader()), setErrors(error), put(getProductsFailure(error))]);
    }
}

export function* loadMoreProducts({ payload }: LoadMoreProductsRequestType) {
    try {
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, payload);
        yield put(loadMoreProductsSuccess(result));
    } catch (error) {
        yield put(loadMoreProductsFailure(error));
    }
}

export default function* productRootSaga() {
    yield takeEvery(ProductActions.GET_PRODUCTS_REQUEST, getProducts);
    yield takeEvery(ProductActions.LOAD_MORE_PRODUCTS_REQUEST, loadMoreProducts);
}
