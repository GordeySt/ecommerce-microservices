import { Container } from '@material-ui/core';
import React, { useEffect } from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import { AuthRoutes, CatalogRoutes, CommonRoutes } from './common/constants/routeConstants';
import userManager, { loadUser } from './common/auth/userService';
import AuthProvider from './common/auth/authProvider';
import PrivateRoute from './common/layout/PrivateRoute';
import NavBar from './common/layout/NavBar';
import { useDispatch } from 'react-redux';
import { getUserByIdRequest } from './common/state/actions/userActions';
import { getUserId } from './common/auth/authHeaders';
import Loader from './common/layout/Loader';

const SignInOidc = React.lazy(() => import('./common/auth/SignInOidc'));
const SignoutOidc = React.lazy(() => import('./common/auth/SignoutOidc'));
const SignUpSuccess = React.lazy(() => import('./components/auth/SignUpSuccess'));
const VerifyEmail = React.lazy(() => import('./components/auth/VerifyEmail'));
const NotFound = React.lazy(() => import('./common/layout/NotFound'));
const StartPage = React.lazy(() => import('./pages/start-page/StartPage'));
const WelcomePage = React.lazy(() => import('./pages/welcome-page/WelcomePage'));
const CatalogPageContainer = React.lazy(() => import('./pages/catalog-page/CatalogPageContainer'));
const ProductDetailsPage = React.lazy(() => import('./pages/catalog-page/ProductDetailsPage'));

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
                    <React.Suspense fallback={<Loader />}>
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
                                            <PrivateRoute
                                                path={CommonRoutes.welcomePageRoute}
                                                component={WelcomePage}
                                            />
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
                    </React.Suspense>
                </>
            </AuthProvider>
        </React.Fragment>
    );
};

export default withRouter(App);
