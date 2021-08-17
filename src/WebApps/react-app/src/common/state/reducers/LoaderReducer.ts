import { LoaderActions } from '../actions/loaderActions'

const initialState: any = {
    loading: false,
}

export const loaderReducer = (state: any = initialState, action: any) => {
    switch (action.type) {
        case LoaderActions.SHOW_LOADER:
            return { ...state, loading: true }
        case LoaderActions.HIDE_LOADER:
            return { ...state, loading: false }
        default:
            return state
    }
}
