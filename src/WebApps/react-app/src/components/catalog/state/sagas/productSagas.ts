import { all, call, put, select, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { PaginatedResult, PagingParams } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { setErrors } from '../../../../common/state/actions/errorActions';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import { formUrlSearchParams } from '../../../../common/utils/functions';
import {
    getProductByIdFailure,
    getProductByIdSuccess,
    getProductsFailure,
    getProductsSuccess,
    loadMoreProductsFailure,
    loadMoreProductsSuccess,
    resetProducts,
    setPagination,
    setProducts,
} from '../actions/productActions';
import { getPagingParams, getPredicates } from '../selectors/productsSelectors';
import { IPredicate } from '../types/filteringTypes';
import { GetProductsByIdRequestType, ProductActions } from '../types/productTypes';

export function* getProducts() {
    try {
        yield put(resetProducts());
        const pagingParams: PagingParams = yield select(getPagingParams);
        const predicates: IPredicate[] = yield select(getPredicates);
        const params = formUrlSearchParams(pagingParams, predicates);
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, params);
        yield all([put(getProductsSuccess()), put(setProducts(result.data)), put(setPagination(result.pagination))]);
    } catch (error) {
        yield all([setErrors(error), put(getProductsFailure(error))]);
    }
}

export function* loadMoreProducts() {
    try {
        const pagingParams: PagingParams = yield select(getPagingParams);
        const predicates: IPredicate[] = yield select(getPredicates);
        const params = formUrlSearchParams(pagingParams, predicates);
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, params);
        yield put(loadMoreProductsSuccess(result));
    } catch (error) {
        yield put(loadMoreProductsFailure(error));
    }
}

export function* getProductById({ payload }: GetProductsByIdRequestType) {
    try {
        yield put(showLoader());
        const product: IProduct = yield call(catalogApi.getProductById, payload);
        yield all([put(hideLoader()), put(getProductByIdSuccess(product))]);
        yield;
    } catch (error) {
        yield put(getProductByIdFailure(error));
    }
}

export default function* productRootSaga() {
    yield takeEvery(ProductActions.GET_PRODUCTS_REQUEST, getProducts);
    yield takeEvery(ProductActions.LOAD_MORE_PRODUCTS_REQUEST, loadMoreProducts);
    yield takeEvery(ProductActions.GET_PRODUCTS_BY_ID_REQUEST, getProductById);
}
