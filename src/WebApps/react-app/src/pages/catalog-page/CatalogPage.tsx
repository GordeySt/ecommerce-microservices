import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { PagingParams } from '../../common/models/pagination';
import { useTypedSelector } from '../../common/utils/hooks';
import { ProductsList } from '../../components/catalog/ProductsList';
import { getProductsRequest, loadMoreProductsRequest } from '../../components/catalog/state/actions/productActions';
import InfiniteScroll from 'react-infinite-scroller';
import {
    getLoadingProductsStatus,
    getLoadMoreLoadingStatus,
    getPagination,
    getProducts,
} from '../../components/catalog/state/selectors/productsSelectors';
import { FilteringSection } from '../../components/catalog/filtering/FilteringSection';
import { setPagingParams } from '../../components/catalog/state/actions/filteringActions';
import { SearchForm } from '../../components/catalog/filtering/SearchForm';

const useStyles = makeStyles(() =>
    createStyles({
        catalogPageContainer: {
            display: 'flex',
            justifyContent: 'center',
        },
        filters: {
            marginLeft: '30px',
        },
        loaderDiv: {
            display: 'flex',
            justifyContent: 'center',
        },
        loader: {
            marginTop: '10px',
        },
    })
);

const CatalogPage = () => {
    const constants = {
        initialPageNumber: 1,
    };
    const classes = useStyles();
    const dispatch = useDispatch();
    const isLoadingProducts = useTypedSelector(getLoadingProductsStatus);
    const isLoadingMore = useTypedSelector(getLoadMoreLoadingStatus);
    const products = useTypedSelector(getProducts);
    const pagination = useTypedSelector(getPagination);

    useEffect(() => {
        dispatch(setPagingParams(new PagingParams(constants.initialPageNumber)));
        if (products.length === 0) dispatch(getProductsRequest());
    }, [dispatch, products.length, constants.initialPageNumber]);

    const handleGetNext = () => {
        dispatch(setPagingParams(new PagingParams(pagination.currentPage + 1)));
        dispatch(loadMoreProductsRequest());
    };

    return (
        <>
            <SearchForm />
            <div className={classes.catalogPageContainer}>
                <div>
                    {isLoadingProducts ? (
                        <CircularProgress color="secondary" className={classes.loader} />
                    ) : (
                        <InfiniteScroll
                            pageStart={0}
                            loadMore={handleGetNext}
                            hasMore={!isLoadingMore && !!pagination && pagination.currentPage < pagination.totalPages}
                            initialLoad={false}
                        >
                            <div>
                                <ProductsList products={products} />
                            </div>
                        </InfiniteScroll>
                    )}
                </div>
                <div className={classes.filters}>
                    <FilteringSection />
                </div>
            </div>
            <div className={classes.loaderDiv}>
                {isLoadingMore && <CircularProgress color="secondary" className={classes.loader} />}
            </div>
        </>
    );
};

export default CatalogPage;
