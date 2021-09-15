import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';

interface IProps {
    classes: ClassNameMap<'form'>;
    priceOrderType: string;
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
                <MenuItem value="ASC">Asc</MenuItem>
                <MenuItem value="DESC">Desc</MenuItem>
            </Select>
        </FormControl>
    );
};

export default PriceFilter;
