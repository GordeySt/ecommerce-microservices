import { IPredicate } from '../../components/catalog/state/types/filteringTypes';
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

export const deepEqual = (obj1: any, obj2: any) => {
    const keys1 = Object.keys(obj1);
    const keys2 = Object.keys(obj2);

    if (keys1.length !== keys2.length) {
        return false;
    }

    for (const key of keys1) {
        const val1 = obj1[key];
        const val2 = obj2[key];
        const areObjects = isObject(val1) && isObject(val2);

        if ((areObjects && !deepEqual(val1, val2)) || (!areObjects && val1 !== val2)) {
            return false;
        }
    }

    return true;
};

const isObject = (obj: any) => {
    return obj != null && typeof obj === 'object';
};
