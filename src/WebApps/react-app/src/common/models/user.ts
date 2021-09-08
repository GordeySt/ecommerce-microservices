import { IProductRating } from './rating';

export interface IUserFormValues {
    email: string;
    password: string;
}

export interface ICurrentUser {
    id: string;
    userName: string;
    ratings: IProductRating[];
}
