import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import { useEffect } from 'react';
import { IPagination } from '../../common/models/pagination';
import { ProductsList } from '../../components/catalog/ProductsList';
import InfiniteScroll from 'react-infinite-scroller';
import { IProduct } from '../../common/models/product';
import { SearchForm } from '../../components/catalog/filtering/SearchForm';
import { FilteringSection } from '../../components/catalog/filtering/FilteringSection';

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

export interface ICatalogPageProps {
    loading: boolean;
    isLoadingMore: boolean;
    products: IProduct[];
    pagination: IPagination;
    handleGetNext: () => void;
    onComponentLoad: () => void;
}

const CatalogPage = (props: ICatalogPageProps) => {
    const classes = useStyles();
    const { onComponentLoad, loading, handleGetNext, isLoadingMore, pagination, products } = props;

    useEffect(() => {
        onComponentLoad();
    }, [onComponentLoad]);

    return (
        <>
            <SearchForm />
            <div className={classes.catalogPageContainer}>
                <div>
                    {loading ? (
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
