import { LoaderActions } from '../actions/loaderActions';
import { LoaderActionTypes } from '../types/loaderTypes';

const initialState = {
    loading: false,
};

export const loaderReducer = (state = initialState, action: LoaderActionTypes) => {
    switch (action.type) {
        case LoaderActions.SHOW_LOADER:
            return { ...state, loading: true };
        case LoaderActions.HIDE_LOADER:
            return { ...state, loading: false };
        default:
            return state;
    }
};
