import axios, { AxiosResponse } from 'axios';
import { LocalStorageConstants } from '../constants/localStorageConstants';
import { PaginatedResult } from '../models/pagination';
import { ErrorsHandler } from './errors';

const delayValue = 1000;

axios.defaults.baseURL = 'http://localhost:5015/';

axios.interceptors.request.use(
    (config) => {
        const token = window.localStorage.getItem(LocalStorageConstants.accessToken);

        if (token) config.headers.Authorization = `Bearer ${token}`;

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

axios.interceptors.response.use(
    async (response) => {
        const pagination = response.headers['pagination'];
        if (pagination) {
            response.data = new PaginatedResult(response.data, JSON.parse(pagination));

            return response as AxiosResponse<PaginatedResult<never>>;
        }

        return response;
    },
    (er) => {
        ErrorsHandler.handleNetworkError(er);
        ErrorsHandler.handle404Error(er.response);
        ErrorsHandler.handle400Error(er.response);
        ErrorsHandler.handle500Error(er.response);
        ErrorsHandler.handle401Error(er.response);

        throw er.response;
    }
);

export const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>((resolve) => setTimeout(() => resolve(response), ms));

export const responseBody = <T>(response: AxiosResponse<T>) => response.data;

export const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(sleep(delayValue)).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(sleep(delayValue)).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(sleep(delayValue)).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(sleep(delayValue)).then(responseBody),
};
