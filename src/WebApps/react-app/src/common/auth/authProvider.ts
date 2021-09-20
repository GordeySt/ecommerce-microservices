import React, { FC, useEffect, useRef } from 'react';
import { User, UserManager } from 'oidc-client';
import { setAccessTokenHeader, setIdTokenHeader, setUserId } from './authHeaders';
import { useDispatch } from 'react-redux';
import { getUserByIdRequest } from '../state/actions/userActions';

interface IAuthProviderProps {
    userManager: UserManager;
}

const AuthProvider: FC<IAuthProviderProps> = ({ userManager: manager, children }): any => {
    const userManager = useRef<UserManager>();
    const dispatch = useDispatch();

    useEffect(() => {
        userManager.current = manager;
        const onUserLoaded = (user: User) => {
            if (process.env.name === 'dev') {
                console.log('User loaded: ', user);
            }

            setAccessTokenHeader(user.access_token);
            setIdTokenHeader(user.id_token);
            setUserId(user.profile.sub);
            dispatch(getUserByIdRequest(user.profile.sub));
        };
        const onUserUnloaded = () => {
            setAccessTokenHeader(null);
            setIdTokenHeader(null);
        };

        userManager.current.events.addUserLoaded(onUserLoaded);
        userManager.current.events.addUserUnloaded(onUserUnloaded);

        return () => {
            if (userManager && userManager.current) {
                userManager.current.events.removeUserLoaded(onUserLoaded);
                userManager.current.events.removeUserUnloaded(onUserUnloaded);
            }
        };
    }, [manager, dispatch]);

    return React.Children.only(children);
};

export default AuthProvider;
