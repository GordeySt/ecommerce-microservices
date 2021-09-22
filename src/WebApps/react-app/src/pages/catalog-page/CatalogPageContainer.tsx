import React from 'react';
import { useDispatch } from 'react-redux';
import { PagingParams } from '../../common/models/pagination';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { getProductsRequest, loadMoreProductsRequest } from '../../components/catalog/state/actions/actions';
import {
    getLoadMoreLoadingStatus,
    getPagination,
    getProducts,
} from '../../components/catalog/state/selectors/productsSelectors';
import CatalogPage, { ICatalogPageProps } from './CatalogPage';

export const CatalogPageContainer = () => {
    const dispatch = useDispatch();
    const loading = useTypedSelector(getLoadingStatus);
    const isLoadingMore = useTypedSelector(getLoadMoreLoadingStatus);
    const products = useTypedSelector(getProducts);
    const pagination = useTypedSelector(getPagination);

    const handleGetNext = () => {
        pagination && dispatch(loadMoreProductsRequest(new PagingParams(pagination.currentPage + 1)));
    };

    const onComponentLoad = () => {
        if (products.length <= 1) {
            dispatch(getProductsRequest(new PagingParams(1)));
        }
    };

    const catalogPageProps = {
        loading,
        isLoadingMore,
        products,
        pagination,
        handleGetNext,
        onComponentLoad,
    } as ICatalogPageProps;

    return <CatalogPage {...catalogPageProps} />;
};
