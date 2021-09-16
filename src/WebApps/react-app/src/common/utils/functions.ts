import { IPredicate } from '../../components/catalog/state/reducers/productsReducer';
import { UrlSearchParamsConstants } from '../constants/urlSearchParamsConstants';
import { PagingParams } from '../models/pagination';

export const formUrlSearchParams = (pagingParams: PagingParams, predicates: IPredicate[]) => {
    const params = new URLSearchParams();

    params.append(UrlSearchParamsConstants.pageSize, pagingParams.pageSize.toString());
    params.append(UrlSearchParamsConstants.pageNumber, pagingParams.pageNumber.toString());
    predicates.map((predicate) => {
        return params.append(predicate.key, predicate.value);
    });

    return params;
};
