import { useCallback, useState } from 'react';
import { useDispatch } from 'react-redux';
import { IProduct } from '../../common/models/product';
import { IProductRating } from '../../common/models/rating';
import { IRatingUser } from '../../common/models/user';
import ProductCard, { IProductCardProps } from './ProductCard';
import { addRatingRequest, changeRatingRequest } from './state/actions/actions';

interface IProps {
    product: IProduct;
    user: IRatingUser;
}

export const ProductCardContainer = ({ product, user }: IProps) => {
    const dispatch = useDispatch();
    const [userRating, setUserRating] = useState<IProductRating | null>(null);

    const findUserRating = useCallback(() => {
        user.ratings?.map((rating) => {
            if (rating.product?.id === product.id) {
                setUserRating(rating);
            }
        });
    }, [product.id, user.ratings]);

    const onRatingChange = (newValue: number | null) => {
        userRating
            ? dispatch(changeRatingRequest(product.id, newValue))
            : dispatch(addRatingRequest(product.id, newValue));

        const newUserRating: IProductRating = {
            ...userRating,
            rating: newValue,
        };

        setUserRating(newUserRating);
    };

    const productCardProps: IProductCardProps = {
        user,
        product,
        userRating,
        findUserRating,
        onRatingChange,
    };

    return <ProductCard {...productCardProps} />;
};
