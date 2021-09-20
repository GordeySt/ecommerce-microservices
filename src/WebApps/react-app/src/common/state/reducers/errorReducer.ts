import { ERROR_ANY } from '../../models/anyAliases';
import { ErrorActions } from '../actions/errorActions';
import { ErrorActionTypes } from '../types/errorTypes';

export interface IErrorState {
    error: ERROR_ANY;
}

const initialState: IErrorState = {
    error: null as ERROR_ANY,
};

export const errorReducer = (state = initialState, action: ErrorActionTypes) => {
    switch (action.type) {
        case ErrorActions.SET_ERRORS:
            return { ...state, error: action.payload };
        default:
            return state;
    }
};
