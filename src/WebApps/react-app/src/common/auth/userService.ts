import { UserManager, UserManagerSettings } from 'oidc-client';
import { setAccessTokenHeader, setIdTokenHeader, setUserId } from './authHeaders';
import { SignoutRedirectArgsType } from './types';

const userManagerSettings: UserManagerSettings = {
    client_id: 'spa',
    redirect_uri: 'http://localhost:8080/signin-oidc',
    response_type: 'code',
    scope: 'openid profile roles catalogapi identityapi basketapi',
    authority: 'https://localhost:5011/',
    post_logout_redirect_uri: 'http://localhost:8080/signout-oidc',
};

const userManager = new UserManager(userManagerSettings);

export async function loadUser() {
    const user = await userManager.getUser();
    console.log('User: ', user);
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
