import {
    AuthActions,
    resendEmailVerificationRequest,
    signUpUserFailure,
    signUpUserRequest,
    signUpUserSuccess,
    verifyEmailRequest,
} from '../../components/auth/state/actions/actions';

describe('Auth Action Creators Tests', () => {
    it(`Should create ${AuthActions.SIGNUP_REQUEST} action`, () => {
        // Arrange
        const signUpValues = {
            email: 'email@test.com',
            password: 'password1',
        };

        // Act
        const action = signUpUserRequest(signUpValues);

        // Assert
        expect(action).toEqual({
            type: AuthActions.SIGNUP_REQUEST,
            payload: signUpValues,
        });
    });

    it(`Should create ${AuthActions.SIGNUP_SUCCESS} action`, () => {
        // Act
        const action = signUpUserSuccess();

        // Assert
        expect(action).toEqual({
            type: AuthActions.SIGNUP_SUCCESS,
        });
    });

    it(`Should create ${AuthActions.SIGNUP_FAILURE} action`, () => {
        // Arrange
        const error = new Error('test error');

        // Act
        const action = signUpUserFailure(error);

        // Assert
        expect(action).toEqual({
            type: AuthActions.SIGNUP_FAILURE,
            payload: error,
        });
    });

    it(`Should create ${AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST} action`, () => {
        // Arrange
        const email = 'email@test.com';

        // Act
        const action = resendEmailVerificationRequest(email);

        // Assert
        expect(action).toEqual({
            type: AuthActions.RESEND_EMAIL_VERIFICATION_REQUEST,
            payload: email,
        });
    });

    it(`Should create ${AuthActions.VERIFY_EMAIL_REQUEST} action`, () => {
        // Arrange
        const email = 'email@test.com';
        const token = 'test-token';

        // Act
        const action = verifyEmailRequest(email, token);

        // Assert
        expect(action).toEqual({
            type: AuthActions.VERIFY_EMAIL_REQUEST,
            payload: {
                email,
                token,
            },
        });
    });
});
