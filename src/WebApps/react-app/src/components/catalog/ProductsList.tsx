import { createStyles, makeStyles } from '@material-ui/core';
import { Fragment } from 'react';
import { IProduct } from '../../common/models/product';
import { getCurrentUser } from '../../common/state/selectors/userSelectors';
import { useTypedSelector } from '../../common/utils/hooks';
import { FilteringSection } from './filtering/FilteringSection';
import ProductCard from './ProductCard';

const useStyles = makeStyles(() =>
    createStyles({
        container: {
            display: 'flex',
        },
        products: {
            marginRight: '20px',
        },
    })
);

interface IProps {
    products: IProduct[];
}

export const ProductsList = ({ products }: IProps) => {
    const classes = useStyles();
    const user = useTypedSelector(getCurrentUser);

    return (
        <div className={classes.container}>
            <div className={classes.products}>
                {products.map((product) => (
                    <Fragment key={product.id}>
                        <ProductCard product={product} user={user} />
                    </Fragment>
                ))}
            </div>
            <div>
                <FilteringSection />
            </div>
        </div>
    );
};
