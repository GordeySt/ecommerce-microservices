import { Button } from '@material-ui/core'
import { Field, Form } from 'react-final-form'
import { IUserFormValues } from '../common/models/user'

export const SignUpForm = () => {
    const onSubmit = (values: IUserFormValues) => {
        console.log(values)
    }

    return (
        <Form
            onSubmit={onSubmit}
            validate={(values: IUserFormValues) => {
                const errors = {
                    email: '',
                    password: '',
                }

                if (!values.email) {
                    errors.email = 'Email Required'
                }

                if (!values.password) {
                    errors.password = 'Password Required'
                }

                return errors
            }}
            render={({ handleSubmit, submitting, pristine }) => (
                <form onSubmit={handleSubmit}>
                    <Field name="email">
                        {({ input, meta }) => (
                            <div style={{ marginBottom: '10px' }}>
                                <input {...input} type="text" placeholder="Email" />
                                {(meta.error || meta.submitError) && meta.touched && (
                                    <div style={{ color: 'red', marginTop: '5px', fontSize: '100%' }}>
                                        {meta.error || meta.submitError}
                                    </div>
                                )}
                            </div>
                        )}
                    </Field>
                    <Field name="password">
                        {({ input, meta }) => (
                            <div style={{ marginBottom: '10px' }}>
                                <input {...input} type="password" placeholder="Password" />
                                {(meta.error || meta.submitError) && meta.touched && (
                                    <div style={{ color: 'red', marginTop: '5px', fontSize: '100%' }}>
                                        {meta.error || meta.submitError}
                                    </div>
                                )}
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
                            SignUp
                        </Button>
                    </div>
                </form>
            )}
        />
    )
}
