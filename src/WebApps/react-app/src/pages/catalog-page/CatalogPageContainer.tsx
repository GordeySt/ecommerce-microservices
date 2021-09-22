import { useDispatch } from 'react-redux';
import { PagingParams } from '../../common/models/pagination';
import { useTypedSelector } from '../../common/utils/hooks';
import { loadMoreProductsRequest } from '../../components/catalog/state/actions/productActions';
import { setPagingParams } from '../../components/catalog/state/actions/filteringActions';
import {
    getLoadingProductsStatus,
    getLoadMoreLoadingStatus,
    getPagination,
    getProducts,
} from '../../components/catalog/state/selectors/productsSelectors';
import CatalogPage, { ICatalogPageProps } from './CatalogPage';

export const CatalogPageContainer = () => {
    const dispatch = useDispatch();
    const isLoadingMore = useTypedSelector(getLoadMoreLoadingStatus);
    const isLoadingProducts = useTypedSelector(getLoadingProductsStatus);
    const products = useTypedSelector(getProducts);
    const pagination = useTypedSelector(getPagination);

    const handleGetNext = () => {
        pagination && dispatch(setPagingParams(new PagingParams(pagination.currentPage + 1)));
        dispatch(loadMoreProductsRequest());
    };

    const catalogPageProps = {
        dispatch,
        isLoadingMore,
        products,
        pagination,
        handleGetNext,
        isLoadingProducts,
    } as ICatalogPageProps;

    return <CatalogPage {...catalogPageProps} />;
};
