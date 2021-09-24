﻿import { UrlSearchParamsConstants } from '../../../../common/constants/urlSearchParamsConstants';
import { PagingParams } from '../../../../common/models/pagination';
import { FilteringActions } from '../actions/filteringActions';
import { FilteringActionType, IPredicate } from '../types/filteringTypes';

const initialState = {
    pagingParams: {} as PagingParams,
    predicates: [] as IPredicate[],
};

export const filteringReducer = (state = initialState, action: FilteringActionType) => {
    switch (action.type) {
        case FilteringActions.SET_PAGING_PARAMS:
            return { ...state, pagingParams: action.payload };
        case FilteringActions.SET_PREDICATES:
            let { predicates } = state;
            predicates = predicates.filter((predicate) => predicate.key !== action.payload.key);
            return { ...state, predicates: [...predicates, action.payload] };
        case FilteringActions.RESET_PREDICATES:
            return { ...state, predicates: [] };
        case FilteringActions.RESET_SORTING_PREDICATES:
            const newPredicates = state.predicates.filter(
                (predicate) =>
                    predicate.key !== UrlSearchParamsConstants.priceOrderType &&
                    predicate.key !== UrlSearchParamsConstants.ratingOrderType
            );

            return { ...state, predicates: newPredicates };
        default:
            return state;
    }
};
