import { PagingParams } from '../../../../common/models/pagination';
import { FilteringActions } from '../types/filteringTypes';

export const setPagingParams = (pagingParams: PagingParams) => ({
    type: FilteringActions.SET_PAGING_PARAMS,
    payload: pagingParams,
});

export const setPredicates = (key: string, value: string) => ({
    type: FilteringActions.SET_PREDICATES,
    payload: {
        key,
        value,
    },
});

export const resetPredicates = () => ({
    type: FilteringActions.RESET_PREDICATES,
});

export const resetSortingPredicates = () => ({
    type: FilteringActions.RESET_SORTING_PREDICATES,
});
