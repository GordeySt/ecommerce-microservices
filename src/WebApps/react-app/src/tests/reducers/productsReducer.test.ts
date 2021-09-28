import { PaginatedResult } from '../../common/models/pagination';
import { IProduct } from '../../common/models/product';
import {
    getProductByIdSuccess,
    getProductsRequest,
    loadMoreProductsSuccess,
    resetProducts,
} from '../../components/catalog/state/actions/productActions';
import { IProductsState, productsReducer } from '../../components/catalog/state/reducers/productsReducer';
import { data, pagination, product } from '../../common/utils/testData/catalogTestData';
import { ProductActions } from '../../components/catalog/state/types/productTypes';

const initialState = {
    products: [] as IProduct[],
    product: {} as IProduct,
    pagination: null,
    isLoadingMore: false,
    isLoadingProducts: false,
} as IProductsState;

describe('ProductsReducer action type response', () => {
    it(`Should update isLoadingProducts to true on ${ProductActions.GET_PRODUCTS_REQUEST}`, () => {
        // Arrange
        const action = getProductsRequest();
        const newState = productsReducer(initialState, action);

        // Act and Assert
        expect(newState.isLoadingProducts).toEqual(true);
    });

    it(`Should update pagination, products and isLoadingMore status on ${ProductActions.LOAD_MORE_PRODUCTS_SUCCESS}`, () => {
        // Arrange
        const payload = new PaginatedResult<IProduct[]>(data, pagination);
        const action = loadMoreProductsSuccess(payload);
        const newState = productsReducer(initialState, action);

        // Act and Assert
        expect(newState.pagination).toEqual(pagination);
        expect(newState.products).toEqual(data);
        expect(newState.isLoadingMore).toEqual(false);
    });

    it(`Should reset products array on ${ProductActions.RESET_PRODUCTS}`, () => {
        // Arrange
        const action = resetProducts();
        const newState = productsReducer(initialState, action);

        // Act and Assert
        expect(newState.products).toEqual([]);
    });

    it(`Should update product on ${ProductActions.GET_PRODUCT_BY_ID_SUCCESS}`, () => {
        // Arrange
        const action = getProductByIdSuccess(product);
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are got', () => {
            expect(newState.product).toEqual(product);
        });
    });
});
