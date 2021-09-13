import { Button, Container, createStyles, makeStyles } from '@material-ui/core';
import { signinRedirect } from '../../common/auth/userService';
import SignUpForm from '../../components/auth/SignUpForm';

const useStyles = makeStyles(() =>
    createStyles({
        homeContainer: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100vh',
        },
        loginLink: {
            marginTop: 10,
        },
    })
);

const StartPage = () => {
    const classes = useStyles();
    return (
        <Container className={classes.homeContainer}>
            <div>
                <SignUpForm />
                <div className={classes.loginLink}>
                    <Button onClick={() => signinRedirect()}>Have an account?</Button>
                </div>
            </div>
        </Container>
    );
};

export default StartPage;
