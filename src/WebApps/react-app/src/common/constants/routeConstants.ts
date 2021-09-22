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

export const CatalogRoutes = {
    catalogPageRoute: '/catalog',
};

export const AuthApiUrls = {
    signUpUrl: '/auth/signup',
    resendEmailVerificationUrl: '/auth/resend-email-verification?email=',
    verifyEmailUrl: '/auth/verify-email',
};

export const CatalogApiUrls = {
    loadCatalogUrl: '/catalog',
    getProductByIdUrl: '/catalog/id/',
    addRatingUrl: '/productratings/add-ratings',
    changeRatingUrl: '/productratings/change-ratings',
};

export const UserApiUrls = {
    getUserByIdUrl: '/users/id/',
    getCurrentUser: '/users/current-user',
    createUser: '/users/create-user/id/',
};
