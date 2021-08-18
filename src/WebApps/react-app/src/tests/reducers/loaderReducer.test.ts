import { hideLoader, showLoader } from '../../common/state/actions/loaderActions'
import { loaderReducer } from '../../common/state/reducers/loaderReducer'

const initialState = {
    loading: false,
}

describe('LoaderReducer action type responses for', () => {
    describe('SHOW_LOADER', () => {
        // Arrange
        const action = showLoader()
        const newState = loaderReducer(initialState, action)

        // Act and Assert
        it('Error is set', () => {
            expect(newState.loading).toEqual(true)
        })
    })

    describe('HIDE_LOADER', () => {
        // Arrange
        const action = hideLoader()
        const newState = loaderReducer(initialState, action)

        // Act and Assert
        it('Error is set', () => {
            expect(newState.loading).toEqual(false)
        })
    })
})
