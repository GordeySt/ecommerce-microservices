import * as actions from '../actions/loaderActions'

export type LoaderActionTypes = ReturnType<typeof actions.showLoader> | ReturnType<typeof actions.hideLoader>
