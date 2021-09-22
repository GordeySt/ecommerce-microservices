import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import Loader from '../../common/layout/Loader';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { getProductByIdRequest } from '../../components/catalog/state/actions/productActions';
import { getProduct } from '../../components/catalog/state/selectors/productsSelectors';

const ProductDetailsPage = () => {
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
        <>
            <div>{product.id}</div>
        </>
    );
};

export default ProductDetailsPage;
