import { createStyles, makeStyles } from '@material-ui/core';
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { resetProducts } from '../state/actions/productActions';
import { resetPredicates, resetSortingPredicates, setPredicates } from '../state/actions/filteringActions';
import AllProductsButton from './AllProductsButton';
import { UrlSearchParamsConstants } from '../../../common/constants/urlSearchParamsConstants';
import FilterWrapper from './FilterWrapper';
import { ageRatings, sortingTypes } from './utils/sortingValues';

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

const FilteringSection = () => {
    const classes = useStyles();
    const dispatch = useDispatch();
    const [ageRating, setAgeRating] = useState('');
    const [ratingOrderType, setRatingOrderType] = useState('');
    const [priceOrderType, setPriceOrderType] = useState('');

    const handleAgeRatingChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const ageRatingValue = event.target.value as string;
        dispatch(setPredicates(UrlSearchParamsConstants.minimumAge, ageRatingValue));
        setAgeRating(ageRatingValue);
        dispatch(resetProducts());
    };

    const handlePriceOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const priceOrderTypeValue = event.target.value as string;
        dispatch(resetSortingPredicates());
        setRatingOrderType('');
        dispatch(setPredicates(UrlSearchParamsConstants.priceOrderType, priceOrderTypeValue));
        setPriceOrderType(priceOrderTypeValue);
        dispatch(resetProducts());
    };

    const handleRatingOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const ratingOrderType = event.target.value as string;
        dispatch(resetSortingPredicates());
        setPriceOrderType('');
        dispatch(setPredicates(UrlSearchParamsConstants.ratingOrderType, ratingOrderType));
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
            <FilterWrapper
                classes={classes}
                orderType={ageRating}
                handleOrderTypeChange={handleAgeRatingChange}
                sortingTypes={ageRatings}
                inputLabel="AgeRating"
            />
            <FilterWrapper
                classes={classes}
                orderType={priceOrderType}
                handleOrderTypeChange={handlePriceOrderTypeChange}
                sortingTypes={sortingTypes}
                inputLabel="Price"
            />
            <FilterWrapper
                classes={classes}
                orderType={ratingOrderType}
                handleOrderTypeChange={handleRatingOrderTypeChange}
                sortingTypes={sortingTypes}
                inputLabel="Rating"
            />
            <AllProductsButton handleOnClickButton={handleOnClickButton} />
        </div>
    );
};

export default FilteringSection;
