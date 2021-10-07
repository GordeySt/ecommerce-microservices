import { UrlSearchParamsConstants } from '../../common/constants/urlSearchParamsConstants';
import { PagingParams } from '../../common/models/pagination';
import { formUrlSearchParams } from '../../common/utils/functions';
import { IPredicate } from '../../components/catalog/state/types/filteringTypes';

describe('Utils Functions Test', () => {
    it('Should return URL query string on formUrlSearchParams function call', () => {
        const pageNumber = 1;
        const pageSize = 2;
        const pagingParams = new PagingParams(pageNumber, pageSize);
        const predicates: IPredicate[] = [
            {
                key: UrlSearchParamsConstants.minimumAge,
                value: '12',
            },
            {
                key: UrlSearchParamsConstants.priceOrderType,
                value: 'ASC',
            },
        ];

        const params = formUrlSearchParams(pagingParams, predicates);

        expect(params.toString()).toBe('pageSize=2&pageNumber=1&minimumAge=12&priceOrderType=ASC');
    });
});
