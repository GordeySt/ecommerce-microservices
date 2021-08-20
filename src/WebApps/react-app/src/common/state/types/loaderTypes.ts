import * as loaderActions from '../actions/loaderActions';

export type LoaderActionTypes =
    | ReturnType<typeof loaderActions.showLoader>
    | ReturnType<typeof loaderActions.hideLoader>;
