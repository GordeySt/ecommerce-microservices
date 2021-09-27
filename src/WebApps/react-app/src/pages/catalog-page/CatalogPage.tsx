import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import { useEffect } from 'react';
import { IPagination, PagingParams } from '../../common/models/pagination';
import ProductsList from '../../components/catalog/ProductsList';
import InfiniteScroll from 'react-infinite-scroller';
import { IProduct } from '../../common/models/product';
import { SearchForm } from '../../components/catalog/filtering/SearchForm';
import { FilteringSection } from '../../components/catalog/filtering/FilteringSection';
import { Dispatch } from 'redux';
import { setPagingParams } from '../../components/catalog/state/actions/filteringActions';
import { getProductsRequest } from '../../components/catalog/state/actions/productActions';

const useStyles = makeStyles(() =>
    createStyles({
        catalogPageContainer: {
            display: 'flex',
            justifyContent: 'center',
        },
        filters: {
            marginLeft: '30px',
        },
        loaderWrapper: {
            display: 'flex',
            justifyContent: 'center',
        },
        loader: {
            marginTop: '10px',
        },
    })
);

export interface ICatalogPageProps {
    loading: boolean;
    isLoadingMore: boolean;
    products: IProduct[];
    pagination: IPagination;
    handleGetNext: () => void;
    onComponentLoad: () => void;
    isLoadingProducts: boolean;
    dispatch: Dispatch<any>;
}

const CatalogPage = (props: ICatalogPageProps) => {
    const classes = useStyles();
    const { isLoadingProducts, handleGetNext, isLoadingMore, pagination, products, dispatch } = props;

    useEffect(() => {
        dispatch(setPagingParams(new PagingParams(1)));

        if (!products.length) {
            dispatch(getProductsRequest());
        }
    }, [dispatch, products.length]);

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
            <div className={classes.loaderWrapper}>
                {isLoadingMore && <CircularProgress color="secondary" className={classes.loader} />}
            </div>
        </>
    );
};

export default CatalogPage;
