import { createStyles, makeStyles } from '@material-ui/core';
import AgeRatingFilter from './AgeRatingFilter';
import AllProductsButton from './AllProductsButton';
import PriceFilter from './PriceFilter';
import RatingFilter from './RatingFilter';

const useStyles = makeStyles(() =>
    createStyles({
        filteringSection: {
            display: 'flex',
            flexDirection: 'column',
        },
        form: {
            width: '120px',
            marginBottom: '10px',
        },
    })
);

export const FilteringSection = () => {
    const classes = useStyles();

    return (
        <div className={classes.filteringSection}>
            <AgeRatingFilter classes={classes} />
            <PriceFilter classes={classes} />
            <RatingFilter classes={classes} />
            <AllProductsButton />
        </div>
    );
};
