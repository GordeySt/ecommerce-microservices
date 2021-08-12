import { Container } from '@material-ui/core'
import { SignUpForm } from '../auth/SignUpForm'

export const HomePage = () => {
    return (
        <Container style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', height: '100vh' }}>
            <SignUpForm />
        </Container>
    )
}
