import { Button } from '@material-ui/core';

interface IProps {
    handleOnClickButton: () => void;
}

const AllProductsButton = ({ handleOnClickButton }: IProps) => {
    return (
        <Button onClick={() => handleOnClickButton()} variant="outlined" size="small" color="primary">
            Show All
        </Button>
    );
};

export default AllProductsButton;
