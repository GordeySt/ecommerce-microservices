import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import Loader from '../../common/layout/Loader';
import { PagingParams } from '../../common/models/pagination';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { ProductsList } from '../../components/catalog/ProductsList';
import { getProductsRequest, loadMoreProductsRequest } from '../../components/catalog/state/actions/actions';
import InfiniteScroll from 'react-infinite-scroller';
import {
    getLoadMoreLoadingStatus,
    getPagination,
    getProducts,
} from '../../components/catalog/state/selectors/productsSelectors';

const useStyles = makeStyles(() =>
    createStyles({
        catalogPageContainer: {
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            flexDirection: 'column',
        },
        loader: {
            marginTop: '10px',
        },
    })
);

const CatalogPage = () => {
    const classes = useStyles();
    const dispatch = useDispatch();
    const loading = useTypedSelector(getLoadingStatus);
    const isLoadingMore = useTypedSelector(getLoadMoreLoadingStatus);
    const products = useTypedSelector(getProducts);
    const pagination = useTypedSelector(getPagination);
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        if (products.length <= 1) dispatch(getProductsRequest(new PagingParams(currentPage)));
    }, [dispatch, products.length, currentPage]);

    const handleGetNext = () => {
        setCurrentPage((prev) => prev + 1);
        dispatch(loadMoreProductsRequest(new PagingParams(currentPage + 1)));
    };

    if (loading) {
        return <Loader />;
    }

    return (
        <div className={classes.catalogPageContainer}>
            <InfiniteScroll
                pageStart={0}
                loadMore={handleGetNext}
                hasMore={!isLoadingMore && !!pagination && currentPage < pagination.totalPages}
                initialLoad={false}
            >
                <ProductsList products={products} />
            </InfiniteScroll>
            {isLoadingMore && <CircularProgress color="secondary" className={classes.loader} />}
        </div>
    );
};

export default CatalogPage;
