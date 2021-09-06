import { authApi } from '../../common/api/authApi';
import { requests } from '../../common/api/baseApi';
import { AuthApiUrls } from '../../common/constants/routeConstants';

describe('Auth Api Tests', () => {
    beforeEach(() => {
        requests.get = jest.fn();
        requests.post = jest.fn();
    });

    it('SignUpUser API Test', () => {
        const signUpValues = {
            email: 'email@test.com',
            password: 'password1',
        };

        authApi.signUp(signUpValues);
        expect(requests.post).toHaveBeenCalledWith(AuthApiUrls.signUpUrl, signUpValues);
    });

    it('ResendEmailVerification API Test', () => {
        const email = 'email@test.com';

        authApi.resendEmailVerification(email);
        expect(requests.get).toHaveBeenCalledWith(AuthApiUrls.resendEmailVerificationUrl + email);
    });

    it('VerifyEmail API Test', () => {
        const verifyData = {
            email: 'email@test.com',
            token: 'test-token',
        };

        authApi.verifyEmail(verifyData);
        expect(requests.post).toHaveBeenCalledWith(AuthApiUrls.verifyEmailUrl, verifyData);
    });
});
