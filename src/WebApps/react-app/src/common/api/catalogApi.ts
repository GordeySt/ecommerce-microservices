import axios from 'axios';
import { CatalogApiUrls } from '../constants/routeConstants';
import { PaginatedResult, PagingParams } from '../models/pagination';
import { IProduct } from '../models/product';
import { requests, responseBody, sleep } from './baseApi';

type AddRatingData = {
    id: string;
    ratingCount: number | null;
};

export const catalogApi = {
    loadProducts: (pagingParams: PagingParams) =>
        axios
            .get<PaginatedResult<IProduct[]>>(
                CatalogApiUrls.loadCatalogUrl +
                    `?pageNumber=${pagingParams.pageNumber}` +
                    `&pageSize=${pagingParams.pageSize}`
            )
            .then(sleep(1000))
            .then(responseBody),
    addRating: (addRatingData: AddRatingData) => requests.post<void>(CatalogApiUrls.addRatingUrl, addRatingData),
    changeRating: (changeRatingData: AddRatingData) =>
        requests.post<void>(CatalogApiUrls.changeRatingUrl, changeRatingData),
};
