export const CommonRoutes = {
    defaultRoute: '/',
    notFoundRoute: '/notfound',
    welcomePageRoute: '/welcome-page',
};

export const AuthRoutes = {
    verifyEmailRoute: '/auth/verify-email',
    signUpSuccessRoute: '/auth/signup-success',
    signInOidcRoute: '/signin-oidc',
    signOutOidcRoute: '/signout-oidc',
};

export const AuthApiUrls = {
    signUpUrl: '/auth/signup',
    resendEmailVerificationUrl: '/auth/resend-email-verification?email=',
    verifyEmailUrl: '/auth/verify-email',
};
