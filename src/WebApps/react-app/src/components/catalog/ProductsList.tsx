import { Fragment } from 'react';
import { IProduct } from '../../common/models/product';
import { getCurrentUser } from '../../common/state/selectors/userSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { ProductCardContainer } from './ProductCardContainer';

interface IProps {
    products: IProduct[];
}

export const ProductsList = ({ products }: IProps) => {
    const user = useTypedSelector(getCurrentUser);

    return (
        <div>
            {products && products.length > 0 ? (
                products.map((product) => (
                    <Fragment key={product.id}>
                        <ProductCardContainer product={product} user={user} />
                    </Fragment>
                ))
            ) : (
                <div>No products</div>
            )}
        </div>
    );
};
