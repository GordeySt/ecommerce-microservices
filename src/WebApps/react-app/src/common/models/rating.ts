import { IProduct } from './product';

export interface IProductRating {
    rating: number | null;
    product?: IProduct;
}
