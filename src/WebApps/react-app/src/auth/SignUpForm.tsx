import { Button } from '@material-ui/core'
import { Field, Form } from 'react-final-form'
import { useDispatch } from 'react-redux'
import { IUserFormValues } from '../common/models/user'
import { signUpUserRequest } from './state/actions/actions'

export const SignUpForm = () => {
    const dispatch = useDispatch()

    const onSubmit = (values: IUserFormValues) => {
        console.log(values)
        dispatch(signUpUserRequest(values))
    }

    return (
        <Form
            onSubmit={onSubmit}
            render={({ handleSubmit, submitting, pristine }) => (
                <form onSubmit={handleSubmit}>
                    <div>
                        <Field name="email" component="input" type="text" placeholder="Email" />
                    </div>
                    <div>
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
    )
}
