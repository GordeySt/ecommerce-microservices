import { Container } from '@material-ui/core';
import React, { useEffect } from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import SignUpSuccess from './components/auth/SignUpSuccess';
import VerifyEmail from './components/auth/VerifyEmail';
import { AuthRoutes, CatalogRoutes, CommonRoutes } from './common/constants/routeConstants';
import NotFound from './common/layout/NotFound';
import StartPage from './pages/start-page/StartPage';
import userManager, { loadUser } from './common/auth/userService';
import SignInOidc from './common/auth/SignInOidc';
import AuthProvider from './common/auth/authProvider';
import SignoutOidc from './common/auth/SignoutOidc';
import PrivateRoute from './common/layout/PrivateRoute';
import { WelcomePage } from './pages/welcome-page/WelcomePage';
import NavBar from './common/layout/NavBar';
import { useDispatch } from 'react-redux';
import { getUserByIdRequest } from './common/state/actions/userActions';
import { getUserId } from './common/auth/authHeaders';
import CatalogPageContainer from './pages/catalog-page/CatalogPageContainer';
import ProductDetailsPage from './pages/catalog-page/ProductDetailsPage';

const App = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        loadUser();
        if (getUserId()) {
            dispatch(getUserByIdRequest(getUserId()));
        }
    }, [dispatch]);

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
                                        <PrivateRoute
                                            path={`${CatalogRoutes.catalogPageRoute}/:id`}
                                            component={ProductDetailsPage}
                                        />
                                        <PrivateRoute path={CommonRoutes.welcomePageRoute} component={WelcomePage} />
                                        <PrivateRoute
                                            path={CatalogRoutes.catalogPageRoute}
                                            component={CatalogPageContainer}
                                        />
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
