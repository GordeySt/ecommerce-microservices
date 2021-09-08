import { ICurrentUser } from '../../models/user';
import { SetUserActionType, UserActions } from '../actions/userActions';

const initialState = {
    user: {} as ICurrentUser,
};

export const userReducer = (state = initialState, action: SetUserActionType) => {
    switch (action.type) {
        case UserActions.SET_USER:
            return { ...state, user: action.payload };
        default:
            return state;
    }
};
