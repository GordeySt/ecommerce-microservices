import { UserManager, UserManagerSettings } from 'oidc-client';
import { setAccessTokenHeader, setIdTokenHeader, setUserId } from './authHeaders';
import { SignoutRedirectArgsType } from './types';

const userManagerSettings: UserManagerSettings = {
    client_id: process.env.CLIENT_ID,
    redirect_uri: process.env.REDIRECT_URI,
    response_type: process.env.RESPONSE_TYPE,
    scope: process.env.SCOPE,
    authority: process.env.AUTHORITY,
    post_logout_redirect_uri: process.env.POST_LOGOUT_REDIRECT_URI,
};

const userManager = new UserManager(userManagerSettings);

export async function loadUser() {
    const user = await userManager.getUser();
    const accessToken = user?.access_token;
    const idToken = user?.id_token;
    const userId = user?.profile.sub;
    setAccessTokenHeader(accessToken);
    setIdTokenHeader(idToken);
    setUserId(userId);
}

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => userManager.signinRedirectCallback();

export const signoutRedirect = (args?: SignoutRedirectArgsType) => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirect(args);
};

export const signoutRedirectCallback = () => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
};

export default userManager;
