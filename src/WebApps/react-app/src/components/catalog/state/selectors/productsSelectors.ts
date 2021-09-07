import { RootState } from '../../../../common/state/store/commonStore';

export const getProducts = (state: RootState) => state.products.products;
