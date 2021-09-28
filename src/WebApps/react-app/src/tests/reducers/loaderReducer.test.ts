import { hideLoader, showLoader } from '../../common/state/actions/loaderActions';
import { loaderReducer } from '../../common/state/reducers/loaderReducer';
import { LoaderActions } from '../../common/state/types/loaderTypes';

const initialState = {
    loading: false,
};

describe('LoaderReducer action type responses', () => {
    it(`Should update loading status to true on ${LoaderActions.SHOW_LOADER}`, () => {
        // Arrange
        const action = showLoader();
        const newState = loaderReducer(initialState, action);

        // Act and Assert
        expect(newState.loading).toEqual(true);
    });

    describe(`Should update loading status to false on ${LoaderActions.HIDE_LOADER}`, () => {
        // Arrange
        const action = hideLoader();
        const newState = loaderReducer(initialState, action);

        // Act and Assert
        expect(newState.loading).toEqual(false);
    });
});
