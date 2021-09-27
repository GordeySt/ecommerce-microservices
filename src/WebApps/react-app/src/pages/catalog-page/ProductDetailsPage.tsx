import { makeStyles, Paper, Typography } from '@material-ui/core';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import Loader from '../../common/layout/Loader';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { getProductByIdRequest } from '../../components/catalog/state/actions/productActions';
import { getProduct } from '../../components/catalog/state/selectors/productsSelectors';

const useStyles = makeStyles({
    wrapper: {
        padding: '10px',
    },
    typography: {
        marginBottom: '5px',
    },
});

const ProductDetailsPage = () => {
    const classes = useStyles();
    const { id } = useParams<{ id: string }>();
    const dispatch = useDispatch();
    const loading = useTypedSelector(getLoadingStatus);
    const product = useTypedSelector(getProduct);

    useEffect(() => {
        if (product.id !== id) {
            dispatch(getProductByIdRequest(id));
        }
    }, [dispatch, id, product.id]);

    if (loading) {
        return <Loader />;
    }

    return (
        <Paper className={classes.wrapper} elevation={1}>
            <Typography className={classes.typography}>Product name: {product.name}</Typography>
            <Typography className={classes.typography}>Product ageRating: {product.ageRating}+</Typography>
            <Typography className={classes.typography}>Product category: {product.category}</Typography>
            <Typography className={classes.typography}>Product summary: {product.summary}</Typography>
            <Typography className={classes.typography}>Product description: {product.description}</Typography>
            <Typography className={classes.typography}>Product price: {product.price}</Typography>
            <Typography className={classes.typography}>Product rating: {product.averageRating.toFixed(1)}</Typography>
        </Paper>
    );
};

export default ProductDetailsPage;
