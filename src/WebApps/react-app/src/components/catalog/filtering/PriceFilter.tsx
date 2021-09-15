import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { useState } from 'react';

interface IProps {
    classes: ClassNameMap<'form'>;
}

const PriceFilter = ({ classes }: IProps) => {
    const [priceOrderType, setPriceOrderType] = useState('');

    const handlePriceOrderTypeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setPriceOrderType(event.target.value as string);
    };

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
