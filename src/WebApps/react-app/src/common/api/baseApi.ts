import axios, { AxiosResponse } from 'axios'
import { ErrorsHandler } from './errors'

axios.defaults.baseURL = 'http://localhost:5015/'

axios.interceptors.response.use(
    async (response) => {
        return response
    },
    (er) => {
        ErrorsHandler.handleNetworkError(er)
        ErrorsHandler.handle404Error(er.response)
        ErrorsHandler.handle400Error(er.response)
        ErrorsHandler.handle500Error(er.response)
        ErrorsHandler.handle401Error(er.response)

        throw er.response
    }
)

const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>((resolve) => setTimeout(() => resolve(response), ms))

const responseBody = <T>(response: AxiosResponse<T>) => response.data

export const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(sleep(1000)).then(responseBody),
    post: <T>(url: string, body: Record<string, never>) =>
        axios.post<T>(url, body).then(sleep(1000)).then(responseBody),
    put: <T>(url: string, body: Record<string, never>) => axios.put<T>(url, body).then(sleep(1000)).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(sleep(1000)).then(responseBody),
}
