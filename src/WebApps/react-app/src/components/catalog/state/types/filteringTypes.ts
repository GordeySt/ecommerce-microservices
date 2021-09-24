import * as actions from '../actions/filteringActions';

export type FilteringActionType =
    | ReturnType<typeof actions.setPagingParams>
    | ReturnType<typeof actions.setPredicates>
    | ReturnType<typeof actions.resetPredicates>
    | ReturnType<typeof actions.resetSortingPredicates>;

export type SetPredicatesActionType = ReturnType<typeof actions.setPredicates>;

export interface IPredicate {
    key: string;
    value: string;
}
