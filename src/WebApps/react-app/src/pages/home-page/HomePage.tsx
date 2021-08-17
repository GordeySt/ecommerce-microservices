import { Container, createStyles, makeStyles } from '@material-ui/core'
import { SignUpForm } from '../../auth/SignUpForm'

const useStyles = makeStyles(() =>
    createStyles({
        homeContainer: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100vh',
        },
    })
)

export const HomePage = () => {
    const classes = useStyles()
    return (
        <Container className={classes.homeContainer}>
            <SignUpForm />
        </Container>
    )
}
