import axios from 'axios';
import { CatalogApiUrls } from '../constants/routeConstants';
import { IProduct } from '../models/product';
import { ICurrentUser } from '../models/user';
import { requests, responseBody, sleep } from './baseApi';

type AddRatingData = {
    id: string;
    ratingCount: number | null;
};

export const catalogApi = {
    loadProducts: () => axios.get<IProduct[]>(CatalogApiUrls.loadCatalogUrl).then(sleep(1000)).then(responseBody),
    getUserById: (id: string) => requests.get<ICurrentUser>(CatalogApiUrls.getUserByIdUrl + id),
    addRating: (addRatingData: AddRatingData) => {
        requests.post<void>(CatalogApiUrls.addRatingUrl, addRatingData);
    },
    changeRating: (changeRatingData: AddRatingData) => {
        requests.post<void>(CatalogApiUrls.changeRatingUrl, changeRatingData);
    },
};
