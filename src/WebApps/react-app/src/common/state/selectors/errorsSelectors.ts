import { RootState } from '../store/commonStore';

export const getErrors = (state: RootState) => state.errors.error;
