import { UrlSearchParamsConstants } from '../../common/constants/urlSearchParamsConstants';
import { PagingParams } from '../../common/models/pagination';
import {
    resetPredicates,
    resetSortingPredicates,
    setPredicates,
} from '../../components/catalog/state/actions/filteringActions';
import { filteringReducer, IFilteringState } from '../../components/catalog/state/reducers/filteringReducer';
import { FilteringActions, IPredicate } from '../../components/catalog/state/types/filteringTypes';

const initialState: IFilteringState = {
    pagingParams: {} as PagingParams,
    predicates: [] as IPredicate[],
};

describe('FilteringReducer action type responses for', () => {
    describe(`${FilteringActions.RESET_PREDICATES}`, () => {
        // Arrange
        const action = resetPredicates();
        const newState = filteringReducer(initialState, action);

        // Act and Assert
        it('Predicates is reset', () => {
            expect(newState.predicates).toEqual([]);
        });
    });

    describe(`${FilteringActions.RESET_SORTING_PREDICATES}`, () => {
        // Arrange
        const resetAction = resetSortingPredicates();
        const setAgeRatingAction = setPredicates(UrlSearchParamsConstants.minimumAge, '12');
        const setRatingOrderTypeAction = setPredicates(UrlSearchParamsConstants.ratingOrderType, 'ASC');

        // Act
        const ageRatingState = filteringReducer(initialState, setAgeRatingAction);
        const ratingOrderTypeState = filteringReducer(ageRatingState, setRatingOrderTypeAction);
        const newState = filteringReducer(ratingOrderTypeState, resetAction);

        // Act and Assert
        it('Predicates is reset', () => {
            expect(newState.predicates).toEqual([{ key: UrlSearchParamsConstants.minimumAge, value: '12' }]);
        });
    });
});
