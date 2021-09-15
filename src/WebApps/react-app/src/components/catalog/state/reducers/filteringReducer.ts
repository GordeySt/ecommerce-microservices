import { PagingParams } from '../../../../common/models/pagination';
import { FilteringActions } from '../actions/filteringActions';
import { FilteringActionType } from '../types/filteringTypes';

export interface IPredicate {
    key: string;
    value: string;
}

const initialState = {
    pagingParams: {} as PagingParams,
    predicates: [] as IPredicate[],
};

export const filteringReducer = (state = initialState, action: FilteringActionType) => {
    switch (action.type) {
        case FilteringActions.SET_PAGING_PARAMS:
            return { ...state, pagingParams: action.payload };
        case FilteringActions.SET_PREDICATES:
            const { predicates } = state;
            return { ...state, predicates: [...predicates, action.payload] };
        case FilteringActions.RESET_PREDICATES:
            return { ...state, predicates: [] };
        case FilteringActions.RESET_SORTING_PREDICATES:
            const newPredicates = state.predicates.filter((predicate) => predicate.key === 'minimumAge');

            return { ...state, predicates: newPredicates };
        default:
            return state;
    }
};
