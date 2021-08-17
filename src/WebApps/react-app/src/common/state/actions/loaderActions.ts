export const LoaderActions = {
    SHOW_LOADER: 'SHOW_LOADER',
    HIDE_LOADER: 'HIDE_LOADER',
}

export function showLoader(): any {
    return {
        type: LoaderActions.SHOW_LOADER,
    }
}

export function hideLoader(): any {
    return {
        type: LoaderActions.HIDE_LOADER,
    }
}
