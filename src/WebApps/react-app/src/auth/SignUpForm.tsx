import { Button } from '@material-ui/core'
import { Field, Form } from 'react-final-form'
import { useDispatch, useSelector } from 'react-redux'
import { IUserFormValues } from '../common/models/user'
import { signUpUserRequest } from './state/actions/actions'
import { createStyles, makeStyles } from '@material-ui/core/styles'
import { Loader } from '../common/layout/Loader'

const useStyles = makeStyles(() =>
    createStyles({
        inputs: {
            marginBottom: '10px',
        },
    })
)

export const SignUpForm = () => {
    const dispatch = useDispatch()
    const classes = useStyles()
    const loading = useSelector((state: any) => state.loader.loading)

    const onSubmit = (values: IUserFormValues) => {
        dispatch(signUpUserRequest(values))
    }

    console.log(loading)

    if (loading) return <Loader />

    return (
        <div>
            <div style={{ fontWeight: 600, fontSize: 22, marginBottom: 10 }}>Sign Up</div>
            <Form
                onSubmit={onSubmit}
                render={({ handleSubmit, submitting, pristine }) => (
                    <form onSubmit={handleSubmit}>
                        <div className={classes.inputs}>
                            <Field name="email" component="input" type="text" placeholder="Email" />
                        </div>
                        <div className={classes.inputs}>
                            <Field name="password" component="input" type="password" placeholder="Password" />
                        </div>

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
