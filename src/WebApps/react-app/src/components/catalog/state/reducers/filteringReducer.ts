import { UrlSearchParamsConstants } from '../../../../common/constants/urlSearchParamsConstants';
import { PagingParams } from '../../../../common/models/pagination';
import { FilteringActions, FilteringActionType, IPredicate, SetPredicatesActionType } from '../types/filteringTypes';

export interface IFilteringState {
    pagingParams: PagingParams;
    predicates: IPredicate[];
}

const initialState: IFilteringState = {
    pagingParams: {} as PagingParams,
    predicates: [] as IPredicate[],
};

export const filteringReducer = (state = initialState, action: FilteringActionType) => {
    switch (action.type) {
        case FilteringActions.SET_PAGING_PARAMS:
            return { ...state, pagingParams: action.payload };
        case FilteringActions.SET_PREDICATES:
            return handleSetPredicates(state, action);
        case FilteringActions.RESET_PREDICATES:
            return { ...state, predicates: [] };
        case FilteringActions.RESET_SORTING_PREDICATES:
            return handleResetSortingPredicates(state);
        default:
            return state;
    }
};

const handleSetPredicates = (state: IFilteringState, action: SetPredicatesActionType): IFilteringState => {
    let { predicates } = state;
    predicates = predicates.filter((predicate) => predicate.key !== action.payload.key);
    return { ...state, predicates: [...predicates, action.payload] };
};

const handleResetSortingPredicates = (state: IFilteringState): IFilteringState => {
    const newPredicates = state.predicates.filter(
        (predicate) =>
            predicate.key !== UrlSearchParamsConstants.priceOrderType &&
            predicate.key !== UrlSearchParamsConstants.ratingOrderType
    );

    return { ...state, predicates: newPredicates };
};
