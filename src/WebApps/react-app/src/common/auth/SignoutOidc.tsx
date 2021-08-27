import { FC, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { CommonRoutes } from '../constants/routeConstants';
import Loader from '../layout/Loader';
import { signoutRedirectCallback } from './userService';

const SignoutOidc: FC<{}> = () => {
    const history = useHistory();
    useEffect(() => {
        const signoutAsync = async () => {
            await signoutRedirectCallback();
            history.push(CommonRoutes.defaultRoute);
        };
        signoutAsync();
    }, [history]);
    return <Loader />;
};

export default SignoutOidc;
