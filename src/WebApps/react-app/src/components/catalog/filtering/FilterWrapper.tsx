import { FormControl, InputLabel, MenuItem, Select } from '@material-ui/core';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';

interface IProps {
    classes: ClassNameMap<'form'>;
    orderType: string;
    sortingTypes: number[] | string[];
    inputLabel: string;
    // eslint-disable-next-line no-unused-vars
    handleOrderTypeChange: (event: React.ChangeEvent<{ value: unknown }>) => void;
}

export const FilterWrapper = ({ classes, orderType, handleOrderTypeChange, sortingTypes, inputLabel }: IProps) => {
    return (
        <FormControl className={classes.form}>
            <InputLabel id="demo-simple-select-label">{inputLabel}</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={orderType}
                onChange={handleOrderTypeChange}
            >
                {sortingTypes.map((sortingType) => (
                    <MenuItem key={sortingType} value={sortingType}>
                        {sortingType}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};
