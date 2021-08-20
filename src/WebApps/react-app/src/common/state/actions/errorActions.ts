export const ErrorActions = {
    SET_ERRORS: 'SET_ERRORS',
} as const

export const setErrors = (error: Error) => ({
    type: ErrorActions.SET_ERRORS,
    payload: error,
})
