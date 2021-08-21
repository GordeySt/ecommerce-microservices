import { Container, createStyles, makeStyles } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { AuthRoutes } from '../../common/constants/routeConstants';
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
                    <Link to={AuthRoutes.signInRoute}>Have an account?</Link>
                </div>
            </div>
        </Container>
    );
};

export default StartPage;
