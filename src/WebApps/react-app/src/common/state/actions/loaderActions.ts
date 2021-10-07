import { LoaderActions } from '../types/loaderTypes';

export const showLoader = () => ({
    type: LoaderActions.SHOW_LOADER,
});

export const hideLoader = () => ({
    type: LoaderActions.HIDE_LOADER,
});
