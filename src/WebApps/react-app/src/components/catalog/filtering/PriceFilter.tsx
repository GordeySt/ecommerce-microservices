import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { sortingTypes } from './utils/sortingValues';

interface IProps {
    classes: ClassNameMap<'form'>;
    priceOrderType: string;
    // eslint-disable-next-line no-unused-vars
    handlePriceOrderTypeChange: (event: React.ChangeEvent<{ value: unknown }>) => void;
}

const PriceFilter = ({ classes, priceOrderType, handlePriceOrderTypeChange }: IProps) => {
    return (
        <FormControl className={classes.form}>
            <InputLabel id="demo-simple-select-label">Price</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={priceOrderType}
                onChange={handlePriceOrderTypeChange}
            >
                {sortingTypes.map((sortingType) => (
                    <MenuItem key={sortingType.toUpperCase()} value={sortingType}>
                        {sortingType}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

export default PriceFilter;
