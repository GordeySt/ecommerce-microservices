import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { useState } from 'react';

interface IProps {
    classes: ClassNameMap<'form'>;
}

const AgeRatingFilter = ({ classes }: IProps) => {
    const [ageRating, setAgeRating] = useState('');

    const handleAgeRatingChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setAgeRating(event.target.value as string);
    };

    return (
        <FormControl className={classes.form}>
            <InputLabel id="demo-simple-select-label">Age Rating</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={ageRating}
                onChange={handleAgeRatingChange}
            >
                <MenuItem value={3}>3+</MenuItem>
                <MenuItem value={7}>7+</MenuItem>
                <MenuItem value={12}>12+</MenuItem>
                <MenuItem value={16}>16+</MenuItem>
                <MenuItem value={18}>18+</MenuItem>
            </Select>
        </FormControl>
    );
};

export default AgeRatingFilter;
