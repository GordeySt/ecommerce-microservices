import { Container, createStyles, makeStyles } from '@material-ui/core';
import SignInForm from '../../components/auth/SignInForm';

const useStyles = makeStyles(() =>
    createStyles({
        homeContainer: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100vh',
        },
    })
);

const SignInPage = () => {
    const classes = useStyles();
    return (
        <Container className={classes.homeContainer}>
            <SignInForm />
        </Container>
    );
};

export default SignInPage;
