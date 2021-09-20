import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';

interface IProps {
    classes: ClassNameMap<'form'>;
    ratingOrderType: string;
    // eslint-disable-next-line no-unused-vars
    handleRatingOrderTypeChange: (event: React.ChangeEvent<{ value: unknown }>) => void;
}

const RatingFilter = ({ classes, ratingOrderType, handleRatingOrderTypeChange }: IProps) => {
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
