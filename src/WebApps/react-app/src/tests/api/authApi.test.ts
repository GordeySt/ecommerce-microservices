import { authApi } from '../../common/api/authApi';
import { requests } from '../../common/api/baseApi';
import { AuthApiUrls } from '../../common/constants/routeConstants';

describe('Auth Api Tests', () => {
    beforeEach(() => {
        requests.get = jest.fn();
        requests.post = jest.fn();
    });

    describe('SignUpUser API Test', () => {
        const signUpValues = {
            email: 'email@test.com',
            password: 'password1',
        };

        it('httpClient is called as expected', () => {
            authApi.signUp(signUpValues);
            expect(requests.post).toHaveBeenCalledWith(AuthApiUrls.signUpUrl, signUpValues);
        });
    });

    describe('ResendEmailVerification API Test', () => {
        const email = 'email@test.com';

        it('httpClient is called as expected', () => {
            authApi.resendEmailVerification(email);
            expect(requests.get).toHaveBeenCalledWith(AuthApiUrls.resendEmailVerificationUrl + email);
        });
    });

    describe('VerifyEmail API Test', () => {
        const verifyData = {
            email: 'email@test.com',
            token: 'test-token',
        };

        it('httpClient is called as expected', () => {
            authApi.verifyEmail(verifyData);
            expect(requests.post).toHaveBeenCalledWith(AuthApiUrls.verifyEmailUrl, verifyData);
        });
    });
});
