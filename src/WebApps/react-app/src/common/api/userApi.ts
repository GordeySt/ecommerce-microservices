import { getUserId } from '../auth/authHeaders';
import { UserApiUrls } from '../constants/routeConstants';
import { ICurrentUser, IRatingUser } from '../models/user';
import { requests } from './baseApi';

export const userApi = {
    getUserById: (id: string | null) => requests.get<IRatingUser>(UserApiUrls.getUserByIdUrl + id),
    getCurrentUser: () => requests.get<ICurrentUser>(UserApiUrls.getCurrentUser),
    createUser: () => requests.post<void>(UserApiUrls.createUser + getUserId(), {}),
};
