import { Fragment } from 'react';
import { IProduct } from '../../common/models/product';
import { getCurrentUser } from '../../common/state/selectors/userSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import ProductCard from './ProductCard';

interface IProps {
    products: IProduct[];
}

export const ProductsList = ({ products }: IProps) => {
    const user = useTypedSelector(getCurrentUser);

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
