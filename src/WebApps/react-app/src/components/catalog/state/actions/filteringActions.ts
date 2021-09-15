import { PagingParams } from '../../../../common/models/pagination';

export const FilteringActions = {
    SET_PAGING_PARAMS: 'SET_PAGING_PARAMS',
    SET_PREDICATES: 'SET_PREDICATES',
    RESET_PREDICATES: 'RESET_PREDICATES',
    RESET_SORTING_PREDICATES: 'RESET_SORTING_PREDICATES',
} as const;

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
