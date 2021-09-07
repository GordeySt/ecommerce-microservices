import { Container } from '@material-ui/core';
import React, { useEffect } from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import SignUpSuccess from './components/auth/SignUpSuccess';
import VerifyEmail from './components/auth/VerifyEmail';
import { AuthRoutes, CommonRoutes } from './common/constants/routeConstants';
import NotFound from './common/layout/NotFound';
import StartPage from './pages/start-page/StartPage';
import userManager, { loadUser } from './common/auth/userService';
import SignInOidc from './common/auth/SignInOidc';
import AuthProvider from './common/auth/authProvider';
import SignoutOidc from './common/auth/SignoutOidc';
import NavBar from './common/layout/NavBar';
import PrivateRoute from './common/layout/PrivateRoute';
import CatalogPage from './pages/catalog-page/CatalogPage';

const App = () => {
    useEffect(() => {
        loadUser();
    }, []);

    return (
        <React.Fragment>
            <ToastContainer position="bottom-right" />
            <AuthProvider userManager={userManager}>
                <>
                    <Route exact path={CommonRoutes.defaultRoute} component={StartPage} />
                    <Route
                        path={'/(.+)'}
                        render={() => (
                            <React.Fragment>
                                <NavBar />
                                <Container maxWidth="lg">
                                    <Switch>
                                        <PrivateRoute path={CommonRoutes.welcomePageRoute} component={CatalogPage} />
                                        <Route path={AuthRoutes.signInOidcRoute} component={SignInOidc} />
                                        <Route path={AuthRoutes.signOutOidcRoute} component={SignoutOidc} />
                                        <Route path={AuthRoutes.signUpSuccessRoute} component={SignUpSuccess} />
                                        <Route path={AuthRoutes.verifyEmailRoute} component={VerifyEmail} />
                                        <Route component={NotFound} />
                                    </Switch>
                                </Container>
                            </React.Fragment>
                        )}
                    />
                </>
            </AuthProvider>
        </React.Fragment>
    );
};

export default withRouter(App);
