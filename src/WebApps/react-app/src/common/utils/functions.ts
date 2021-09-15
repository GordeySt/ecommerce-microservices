import { IPredicate } from '../../components/catalog/state/reducers/productsReducer';
import { PagingParams } from '../models/pagination';

export const formUrlSearchParams = (pagingParams: PagingParams, predicates: IPredicate[]) => {
    const params = new URLSearchParams();

    params.append('pageSize', pagingParams.pageSize.toString());
    params.append('pageNumber', pagingParams.pageNumber.toString());
    predicates.map((predicate) => {
        return params.append(predicate.key, predicate.value);
    });

    return params;
};
