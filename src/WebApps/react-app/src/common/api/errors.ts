import { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { CommonRoutes } from '../constants/routeConstants';
import { history } from '../state/store/commonStore';
import { StatusCodes } from 'http-status-codes';

export const ErrorsHandler = {
    handleNetworkError: (error: AxiosError) =>
        error.message === 'Network Error' && !error.response && toast.error('Network error - make sure API is running'),
    handle404Error: (response: AxiosResponse) =>
        response.status === StatusCodes.NOT_FOUND && history.push(CommonRoutes.notFoundRoute),
    handle400Error: (response: AxiosResponse) =>
        response.status === StatusCodes.BAD_REQUEST &&
        response.config.method === 'get' &&
        response.data.errors.hasOwnProperty('id') &&
        history.push(CommonRoutes.notFoundRoute),
    handle500Error: (response: AxiosResponse) =>
        response.status === StatusCodes.INTERNAL_SERVER_ERROR &&
        toast.error('Server error - check the terminal for more info!'),
    handle401Error: (response: AxiosResponse) => {
        if (
            response.status === StatusCodes.UNAUTHORIZED &&
            response.headers['www-authenticate'].startsWith('Bearer error="invalid_token"')
        ) {
            window.localStorage.removeItem('jwt');
            history.push('/');
            toast.info('Your session has expired, please login again');
        }
    },
};
