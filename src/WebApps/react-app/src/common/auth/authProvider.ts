﻿import React, { FC, useEffect, useRef } from 'react';
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
            console.log('User loaded: ', user);
            setAccessTokenHeader(user.access_token);
            setIdTokenHeader(user.id_token);
            setUserId(user.profile.sub);
            dispatch(getUserByIdRequest(user.profile.sub));
        };
        const onUserUnloaded = () => {
            setAccessTokenHeader(null);
            setIdTokenHeader(null);
            console.log('User unloaded');
        };
        const onAccessTokenExpiring = () => {
            console.log('User token expiring');
        };
        const onAccessTokenExpired = () => {
            console.log('User token expired');
        };
        const onUserSignedOut = () => {
            console.log('User signed out');
        };

        userManager.current.events.addUserLoaded(onUserLoaded);
        userManager.current.events.addUserUnloaded(onUserUnloaded);
        userManager.current.events.addAccessTokenExpiring(onAccessTokenExpiring);
        userManager.current.events.addAccessTokenExpired(onAccessTokenExpired);
        userManager.current.events.addUserSignedOut(onUserSignedOut);

        return () => {
            if (userManager && userManager.current) {
                userManager.current.events.removeUserLoaded(onUserLoaded);
                userManager.current.events.removeUserUnloaded(onUserUnloaded);
                userManager.current.events.removeAccessTokenExpiring(onAccessTokenExpiring);
                userManager.current.events.removeAccessTokenExpired(onAccessTokenExpired);
                userManager.current.events.removeUserSignedOut(onUserSignedOut);
            }
        };
    }, [manager, dispatch]);

    return React.Children.only(children);
};

export default AuthProvider;
