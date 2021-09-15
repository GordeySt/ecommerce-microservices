import { all, call, put, select, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { PaginatedResult, PagingParams } from '../../../../common/models/pagination';
import { IProduct } from '../../../../common/models/product';
import { setErrors } from '../../../../common/state/actions/errorActions';
import {
    getProductsFailure,
    getProductsSuccess,
    loadMoreProductsFailure,
    loadMoreProductsSuccess,
    ProductActions,
    resetProducts,
    setPagination,
    setProducts,
} from '../actions/actions';
import { IPredicate } from '../reducers/productsReducer';
import { getPagingParams, getPredicates } from '../selectors/productsSelectors';

export function* getProducts() {
    try {
        yield put(resetProducts());
        const params = new URLSearchParams();
        const pagingParams: PagingParams = yield select(getPagingParams);
        const predicates: IPredicate[] = yield select(getPredicates);
        params.append('pageSize', pagingParams.pageSize.toString());
        params.append('pageNumber', pagingParams.pageNumber.toString());
        predicates.map((predicate) => {
            return params.append(predicate.key, predicate.value);
        });
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, params);
        yield all([put(getProductsSuccess()), put(setProducts(result.data)), put(setPagination(result.pagination))]);
    } catch (error) {
        yield all([setErrors(error), put(getProductsFailure(error))]);
    }
}

export function* loadMoreProducts() {
    try {
        const params = new URLSearchParams();
        const pagingParams: PagingParams = yield select(getPagingParams);
        const predicates: IPredicate[] = yield select(getPredicates);
        params.append('pageSize', pagingParams.pageSize.toString());
        params.append('pageNumber', pagingParams.pageNumber.toString());
        predicates.map((predicate) => {
            return params.append(predicate.key, predicate.value);
        });
        const result: PaginatedResult<IProduct[]> = yield call(catalogApi.loadProducts, params);
        yield put(loadMoreProductsSuccess(result));
    } catch (error) {
        yield put(loadMoreProductsFailure(error));
    }
}

export default function* productRootSaga() {
    yield takeEvery(ProductActions.GET_PRODUCTS_REQUEST, getProducts);
    yield takeEvery(ProductActions.LOAD_MORE_PRODUCTS_REQUEST, loadMoreProducts);
}
