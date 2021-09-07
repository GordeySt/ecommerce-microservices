import axios from 'axios';
import { IProduct } from '../models/product';
import { responseBody, sleep } from './baseApi';

export const catalogApi = {
    loadProducts: () => axios.get<IProduct[]>('/catalog').then(sleep(1000)).then(responseBody),
};
