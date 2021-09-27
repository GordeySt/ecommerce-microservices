import { InferValueTypes } from '../../../../common/state/types/commonTypes';
import * as actions from '../actions/filteringActions';

export const FilteringActions = {
    SET_PAGING_PARAMS: 'SET_PAGING_PARAMS',
    SET_PREDICATES: 'SET_PREDICATES',
    RESET_PREDICATES: 'RESET_PREDICATES',
    RESET_SORTING_PREDICATES: 'RESET_SORTING_PREDICATES',
} as const;

export type FilteringActionType = ReturnType<InferValueTypes<typeof actions>>;

export type SetPredicatesActionType = ReturnType<typeof actions.setPredicates>;

export interface IPredicate {
    key: string;
    value: string;
}
