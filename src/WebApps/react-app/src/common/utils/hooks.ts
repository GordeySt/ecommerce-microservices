import { TypedUseSelectorHook, useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';
import { RootState } from '../state/store/commonStore';

export const useQuery = () => new URLSearchParams(useLocation().search);

export const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector;
