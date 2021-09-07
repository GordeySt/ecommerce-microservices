import { IProduct } from '../../../../common/models/product';
import { ProductActions } from '../actions/actions';
import { SetProductsType } from '../actions/types';

const initialState = {
    products: [] as IProduct[],
};

export const productsReducer = (state = initialState, action: SetProductsType) => {
    switch (action.type) {
        case ProductActions.SET_PRODUCTS:
            return { ...state, products: action.payload };
        default:
            return state;
    }
};
