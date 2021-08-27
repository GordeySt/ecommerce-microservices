﻿import { createStyles, makeStyles } from '@material-ui/core';
import { ERROR_ANY } from '../models/anyAliases';

interface IProps {
    errors: ERROR_ANY;
}

const useStyles = makeStyles(() =>
    createStyles({
        errorsList: {
            color: 'red',
        },
    })
);

export const SubmitErrorMessage = ({ errors }: IProps) => {
    const classes = useStyles();
    return (
        <ul className={classes.errorsList}>
            {errors.data.errors && Object.keys(errors.data.errors).length ? (
                <>
                    {Object.values(errors.data.errors)
                        .flat()
                        .map((err: ERROR_ANY, i) => (
                            <li key={i}>{err}</li>
                        ))}
                </>
            ) : (
                <div className={classes.errorsList}>{errors.data}</div>
            )}
        </ul>
    );
};
