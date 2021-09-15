import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { useState } from 'react';

interface IProps {
    classes: ClassNameMap<'form'>;
}

const RatingFilter = ({ classes }: IProps) => {
    const [ratingOrderType, setRatingOrderType] = useState('');

    const handleRatingOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setRatingOrderType(event.target.value as string);
    };

    return (
        <FormControl className={classes.form}>
            <InputLabel id="demo-simple-select-label">Rating</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={ratingOrderType}
                onChange={handleRatingOrderTypeChange}
            >
                <MenuItem value="ASC">Asc</MenuItem>
                <MenuItem value="DESC">Desc</MenuItem>
            </Select>
        </FormControl>
    );
};

export default RatingFilter;
