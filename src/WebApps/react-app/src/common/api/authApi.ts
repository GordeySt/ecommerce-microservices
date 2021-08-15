import { IUserFormValues } from '../models/user'
import { requests } from './baseApi'

export const authApi = {
    signUp: (user: IUserFormValues): Promise<void> => requests.post<void>('/auth/signup', user),
}
