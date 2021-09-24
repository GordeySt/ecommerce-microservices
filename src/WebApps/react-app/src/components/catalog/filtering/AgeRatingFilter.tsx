import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { ageRatings } from './utils/sortingValues';

interface IProps {
    classes: ClassNameMap<'form'>;
    ageRating: string;
    // eslint-disable-next-line no-unused-vars
    handleAgeRatingChange: (event: React.ChangeEvent<{ value: unknown }>) => void;
}

const AgeRatingFilter = ({ classes, ageRating, handleAgeRatingChange }: IProps) => {
    return (
        <FormControl className={classes.form}>
            <InputLabel id="demo-simple-select-label">Age Rating</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={ageRating}
                onChange={handleAgeRatingChange}
            >
                {ageRatings.map((ageRating) => (
                    <MenuItem key={ageRating} value={ageRating}>
                        {ageRating}+
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

export default AgeRatingFilter;
