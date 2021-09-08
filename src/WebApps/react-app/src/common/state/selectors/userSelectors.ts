import { RootState } from '../store/commonStore';

export const getCurrentUser = (state: RootState) => state.user.user;
