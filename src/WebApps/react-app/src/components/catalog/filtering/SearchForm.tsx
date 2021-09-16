import { Button, createStyles, makeStyles } from '@material-ui/core';
import { Field, Form } from 'react-final-form';
import { useDispatch } from 'react-redux';
import { UrlSearchParamsConstants } from '../../../common/constants/urlSearchParamsConstants';
import { setPredicates } from '../state/actions/filteringActions';
import { getProductsRequest, resetProducts } from '../state/actions/productActions';

interface IFormValue {
    productName: string;
}

const useStyles = makeStyles(() =>
    createStyles({
        searchDiv: {
            display: 'flex',
            justifyContent: 'center',
        },
        inputs: {
            marginBottom: 10,
        },
        errorText: {
            color: 'red',
        },
    })
);

export const SearchForm = () => {
    const classes = useStyles();
    const dispatch = useDispatch();

    const handleSubmit = (value: IFormValue) => {
        dispatch(setPredicates(UrlSearchParamsConstants.productName, value.productName));
        dispatch(resetProducts());
        dispatch(getProductsRequest());
    };

    return (
        <div className={classes.searchDiv}>
            <Form
                onSubmit={(value: IFormValue) => {
                    handleSubmit(value);
                }}
                render={({ handleSubmit, submitting, pristine }) => (
                    <form onSubmit={handleSubmit}>
                        <div>
                            <Field name="productName">
                                {({ input }) => (
                                    <div className={classes.inputs}>
                                        <input {...input} type="text" placeholder="Search by product name" />
                                    </div>
                                )}
                            </Field>
                            <div>
                                <Button
                                    size="small"
                                    type="submit"
                                    disabled={submitting || pristine}
                                    variant="contained"
                                    color="primary"
                                >
                                    Search
                                </Button>
                            </div>
                        </div>
                    </form>
                )}
            />
        </div>
    );
};
