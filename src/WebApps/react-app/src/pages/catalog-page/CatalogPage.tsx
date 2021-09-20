import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import { useEffect } from 'react';
import Loader from '../../common/layout/Loader';
import { IPagination } from '../../common/models/pagination';
import { ProductsList } from '../../components/catalog/ProductsList';
import InfiniteScroll from 'react-infinite-scroller';
import { IProduct } from '../../common/models/product';

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

    if (loading) {
        return <Loader />;
    }

    return (
        <div className={classes.catalogPageContainer}>
            <InfiniteScroll
                pageStart={0}
                loadMore={handleGetNext}
                hasMore={!isLoadingMore && !!pagination && pagination.currentPage < pagination.totalPages}
                initialLoad={false}
            >
                <ProductsList products={products} />
            </InfiniteScroll>
            {isLoadingMore && <CircularProgress color="secondary" className={classes.loader} />}
        </div>
    );
};

export default CatalogPage;
