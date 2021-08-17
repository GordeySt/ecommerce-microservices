import { Button } from '@material-ui/core'
import { Field, Form } from 'react-final-form'
import { useDispatch, useSelector } from 'react-redux'
import { IUserFormValues } from '../../common/models/user'
import { signUpUserRequest } from './state/actions/actions'
import { createStyles, makeStyles } from '@material-ui/core/styles'
import { Loader } from '../../common/layout/Loader'
import { isRequired } from 'revalidate'
import { RootState } from '../../common/state/store/commonStore'
import { SubmitErrorMessage } from '../../common/form/SubmitErrorMessage'

const useStyles = makeStyles(() =>
    createStyles({
        inputs: {
            marginBottom: 10,
        },
        signUpText: {
            fontWeight: 600,
            fontSize: 22,
            marginBottom: 10,
        },
        errorText: {
            color: 'red',
        },
    })
)

export const SignUpForm = () => {
    const dispatch = useDispatch()
    const classes = useStyles()
    const loading = useSelector((state: RootState) => state.loader.loading)
    const errors = useSelector((state: RootState) => state.errors.error)

    if (loading) {
        return <Loader />
    }

    return (
        <div>
            <div className={classes.signUpText}>Sign Up</div>
            <Form
                onSubmit={(values: IUserFormValues) => {
                    dispatch(signUpUserRequest(values))
                }}
                render={({ handleSubmit, submitting, pristine }) => (
                    <form onSubmit={handleSubmit}>
                        <Field name="email" validate={isRequired('email')}>
                            {({ input, meta }) => (
                                <div className={classes.inputs}>
                                    <input {...input} type="text" placeholder="Email" />
                                    {meta.error && meta.touched && (
                                        <div className={classes.errorText}>{meta.error}</div>
                                    )}
                                </div>
                            )}
                        </Field>
                        <Field name="password" validate={isRequired('password')}>
                            {({ input, meta }) => (
                                <div className={classes.inputs}>
                                    <input {...input} type="password" placeholder="Password" />
                                    {meta.error && meta.touched && (
                                        <div className={classes.errorText}>{meta.error}</div>
                                    )}
                                </div>
                            )}
                        </Field>
                        {errors && <SubmitErrorMessage errors={errors} />}
                        <div>
                            <Button
                                size="small"
                                type="submit"
                                disabled={submitting || pristine}
                                variant="contained"
                                color="primary"
                            >
                                SignUp
                            </Button>
                        </div>
                    </form>
                )}
            />
        </div>
    )
}
