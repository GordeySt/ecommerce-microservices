import { PaginatedResult } from '../../common/models/pagination';
import { IProduct } from '../../common/models/product';
import {
    getProductByIdSuccess,
    getProductsRequest,
    loadMoreProductsSuccess,
    ProductActions,
    resetProducts,
} from '../../components/catalog/state/actions/productActions';
import { IProductsState, productsReducer } from '../../components/catalog/state/reducers/productsReducer';
import { data, pagination, product } from '../../common/utils/testData/catalogTestData';

const initialState = {
    products: [] as IProduct[],
    product: {} as IProduct,
    pagination: null,
    isLoadingMore: false,
    isLoadingProducts: false,
} as IProductsState;

describe('ProductsReducer action type responses for', () => {
    describe(`${ProductActions.GET_PRODUCTS_REQUEST}`, () => {
        // Arrange
        const action = getProductsRequest();
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are got', () => {
            expect(newState.isLoadingProducts).toEqual(true);
        });
    });

    describe(`${ProductActions.LOAD_MORE_PRODUCTS_SUCCESS}`, () => {
        // Arrange
        const payload = new PaginatedResult<IProduct[]>(data, pagination);
        const action = loadMoreProductsSuccess(payload);
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are loaded', () => {
            expect(newState.pagination).toEqual(pagination);
            expect(newState.products).toEqual(data);
            expect(newState.isLoadingMore).toEqual(false);
        });
    });

    describe(`${ProductActions.RESET_PRODUCTS}`, () => {
        // Arrange
        const action = resetProducts();
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are got', () => {
            expect(newState.products).toEqual([]);
        });
    });

    describe(`${ProductActions.RESET_PRODUCTS}`, () => {
        // Arrange
        const action = resetProducts();
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are got', () => {
            expect(newState.products).toEqual([]);
        });
    });

    describe(`${ProductActions.GET_PRODUCTS_BY_ID_SUCCESS}`, () => {
        // Arrange
        const action = getProductByIdSuccess(product);
        const newState = productsReducer(initialState, action);

        // Act and Assert
        it('Products are got', () => {
            expect(newState.product).toEqual(product);
        });
    });
});
