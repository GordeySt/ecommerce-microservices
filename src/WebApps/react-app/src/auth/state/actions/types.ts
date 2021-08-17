import * as actions from './actions'

export type SignUpUserRequestType = ReturnType<typeof actions.signUpUserRequest>
export type ResendEmailVerificationRequestType = ReturnType<typeof actions.resendEmailVerificationRequest>
export type VerifyEmailRequestType = ReturnType<typeof actions.verifyEmailRequest>
