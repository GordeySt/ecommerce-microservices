import { RootState } from '../store/commonStore';

export const getLoadingStatus = (state: RootState) => state.loader.loading;
