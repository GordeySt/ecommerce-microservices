import { useEffect, FC } from 'react';
import { useHistory } from 'react-router-dom';
import { CommonRoutes } from '../constants/routeConstants';
import Loader from '../layout/Loader';
import { signinRedirectCallback } from './userService';

const SigninOidc: FC<unknown> = () => {
    const history = useHistory();
    useEffect(() => {
        async function signinAsync() {
            await signinRedirectCallback();
            history.push(CommonRoutes.welcomePageRoute);
        }
        signinAsync();
    }, [history]);

    return <Loader />;
};

export default SigninOidc;
