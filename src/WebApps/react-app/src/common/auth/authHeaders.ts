﻿import { LocalStorageConstants } from '../constants/localStorageConstants';

export const setAccessTokenHeader = (token: string | null | undefined) =>
    localStorage.setItem(LocalStorageConstants.accessToken, token ? token : '');
export const setIdTokenHeader = (token: string | null | undefined) =>
    localStorage.setItem(LocalStorageConstants.idToken, token ? token : '');
export const setUserId = (id: string | null | undefined) =>
    localStorage.setItem(LocalStorageConstants.userId, id ? id : '');
export const getAccessToken = () => localStorage.getItem(LocalStorageConstants.accessToken);
export const getIdToken = () => localStorage.getItem(LocalStorageConstants.idToken);
export const getUserId = () => localStorage.getItem(LocalStorageConstants.userId);
