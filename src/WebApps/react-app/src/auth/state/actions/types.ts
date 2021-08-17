import * as actions from './actions'

export type SignUpUserRequestType = ReturnType<typeof actions.signUpUserRequest>
export type resendEmailVerificationRequestType = ReturnType<typeof actions.resendEmailVerificationRequest>
export type verifyEmailRequestType = ReturnType<typeof actions.verifyEmailRequest>
