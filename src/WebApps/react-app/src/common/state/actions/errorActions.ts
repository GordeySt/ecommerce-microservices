import { ERROR_ANY } from '../../models/anyAliases';

export const ErrorActions = {
    SET_ERRORS: 'SET_ERRORS',
} as const;

export const setErrors = (error: ERROR_ANY) => ({
    type: ErrorActions.SET_ERRORS,
    payload: error,
});
