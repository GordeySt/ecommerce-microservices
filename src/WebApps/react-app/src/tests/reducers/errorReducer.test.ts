import { ERROR_ANY } from '../../common/models/anyAliases';
import { ErrorActions, setErrors } from '../../common/state/actions/errorActions';
import { errorReducer } from '../../common/state/reducers/errorReducer';

const initialState = {
    error: null as ERROR_ANY,
};

describe('ErrorReducer action type responses for', () => {
    it(`Should update error on ${ErrorActions.SET_ERRORS}`, () => {
        // Arrange
        const error = new Error('test error');
        const action = setErrors(error);
        const newState = errorReducer(initialState, action);

        // Act and Assert
        expect(newState.error).toEqual(error);
    });
});
