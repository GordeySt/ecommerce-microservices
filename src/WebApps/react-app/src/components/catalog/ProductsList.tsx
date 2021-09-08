import { Fragment, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import Loader from '../../common/layout/Loader';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { getCurrentUser } from '../../common/state/selectors/userSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import ProductCard from './ProductCard';
import { getProductsRequest } from './state/actions/actions';
import { getProducts } from './state/selectors/productsSelectors';

export const ProductsList = () => {
    const dispatch = useDispatch();
    const loading = useTypedSelector(getLoadingStatus);
    const products = useTypedSelector(getProducts);
    const user = useTypedSelector(getCurrentUser);

    useEffect(() => {
        dispatch(getProductsRequest());
    }, [dispatch]);

    if (loading) {
        return <Loader />;
    }

    return (
        <div>
            {products.map((product) => (
                <Fragment key={product.id}>
                    <ProductCard product={product} user={user} />
                </Fragment>
            ))}
        </div>
    );
};
