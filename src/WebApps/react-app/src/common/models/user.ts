import { IProductRating } from './rating';

export interface IUserFormValues {
    email: string;
    password: string;
}

export interface IAppUserRoles {
    id: string;
    name: string;
}

export interface IRatingUser {
    id: string;
    userName: string;
    ratings: IProductRating[];
}

export interface ICurrentUser {
    id: string;
    email: string;
    appUserRoles: IAppUserRoles[];
}
