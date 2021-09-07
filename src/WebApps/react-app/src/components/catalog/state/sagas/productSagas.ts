import { all, call, put, takeEvery } from 'redux-saga/effects';
import { catalogApi } from '../../../../common/api/catalogApi';
import { IProduct } from '../../../../common/models/product';
import { setErrors } from '../../../../common/state/actions/errorActions';
import { hideLoader, showLoader } from '../../../../common/state/actions/loaderActions';
import { getProductsFailure, getProductsSuccess, ProductActions, setProducts } from '../actions/actions';

export function* getProducts() {
    try {
        yield put(showLoader());
        const products: IProduct[] = yield call(catalogApi.loadProducts);
        console.log(products);
        yield all([put(getProductsSuccess()), put(setProducts(products)), put(hideLoader())]);
    } catch (error) {
        yield all([put(hideLoader()), setErrors(error), put(getProductsFailure(error))]);
    }
}

export default function* productRootSaga() {
    yield takeEvery(ProductActions.GET_PRODUCTS_REQUEST, getProducts);
}
