export const LoaderActions = {
    SHOW_LOADER: 'SHOW_LOADER',
    HIDE_LOADER: 'HIDE_LOADER',
} as const;

export const showLoader = () => ({
    type: LoaderActions.SHOW_LOADER,
});

export const hideLoader = () => ({
    type: LoaderActions.HIDE_LOADER,
});
