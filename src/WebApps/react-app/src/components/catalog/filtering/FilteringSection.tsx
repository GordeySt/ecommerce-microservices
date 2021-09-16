import { createStyles, makeStyles } from '@material-ui/core';
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { resetProducts } from '../state/actions/productActions';
import { resetPredicates, resetSortingPredicates, setPredicates } from '../state/actions/filteringActions';
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
    const dispatch = useDispatch();
    const [ageRating, setAgeRating] = useState('');
    const [ratingOrderType, setRatingOrderType] = useState('');
    const [priceOrderType, setPriceOrderType] = useState('');

    const handleAgeRatingChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const ageRatingValue = event.target.value as string;
        dispatch(setPredicates('minimumAge', ageRatingValue));
        setAgeRating(ageRatingValue);
        dispatch(resetProducts());
    };

    const handlePriceOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const priceOrderTypeValue = event.target.value as string;
        dispatch(resetSortingPredicates());
        setRatingOrderType('');
        dispatch(setPredicates('PriceOrderType', priceOrderTypeValue));
        setPriceOrderType(priceOrderTypeValue);
        dispatch(resetProducts());
    };

    const handleRatingOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const ratingOrderType = event.target.value as string;
        dispatch(resetSortingPredicates());
        setPriceOrderType('');
        dispatch(setPredicates('RatingOrderType', ratingOrderType));
        setRatingOrderType(ratingOrderType);
        dispatch(resetProducts());
    };

    const resetFiltersValue = () => {
        setAgeRating('');
        setPriceOrderType('');
        setRatingOrderType('');
    };

    const handleOnClickButton = () => {
        resetFiltersValue();
        dispatch(resetPredicates());
        dispatch(resetProducts());
    };

    return (
        <div className={classes.filteringSection}>
            <AgeRatingFilter classes={classes} ageRating={ageRating} handleAgeRatingChange={handleAgeRatingChange} />
            <PriceFilter
                classes={classes}
                priceOrderType={priceOrderType}
                handlePriceOrderTypeChange={handlePriceOrderTypeChange}
            />
            <RatingFilter
                classes={classes}
                ratingOrderType={ratingOrderType}
                handleRatingOrderTypeChange={handleRatingOrderTypeChange}
            />
            <AllProductsButton handleOnClickButton={handleOnClickButton} />
        </div>
    );
};
