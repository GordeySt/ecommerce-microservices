import * as actions from '../actions/loaderActions';
import { InferValueTypes } from './commonTypes';

export const LoaderActions = {
    SHOW_LOADER: 'SHOW_LOADER',
    HIDE_LOADER: 'HIDE_LOADER',
} as const;

export type LoaderActionTypes = ReturnType<InferValueTypes<typeof actions>>;
