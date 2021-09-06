import { AuthApiUrls } from '../constants/routeConstants';
import { IUserFormValues } from '../models/user';
import { requests } from './baseApi';

type VerifyDataType = {
    email: string;
    token: string;
};

export const authApi = {
    signUp: (user: IUserFormValues): Promise<void> => requests.post<void>(AuthApiUrls.signUpUrl, user),
    resendEmailVerification: (email: string): Promise<void> =>
        requests.get<void>(AuthApiUrls.resendEmailVerificationUrl + email),
    verifyEmail: (verifyData: VerifyDataType) => requests.post<void>(AuthApiUrls.verifyEmailUrl, verifyData),
};
