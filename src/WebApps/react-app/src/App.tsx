import { Container } from '@material-ui/core'
import React from 'react'
import { Route, Switch, withRouter } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import { SignUpSuccess } from './auth/SignUpSuccess'
import { VerifyEmail } from './auth/VerifyEmail'
import { AuthRoutes, CommonRoutes } from './common/constants/routeConstants'
import { HomePage } from './home-page/HomePage'

const App = () => {
    return (
        <React.Fragment>
            <ToastContainer position="bottom-right" />
            <Route exact path={CommonRoutes.defaultRoute} component={HomePage} />
            <Route
                path={'/(.+)'}
                render={() => (
                    <React.Fragment>
                        <Container maxWidth="lg">
                            <Switch>
                                <Route path={AuthRoutes.signUpSuccessRoute} component={SignUpSuccess} />
                                <Route path={AuthRoutes.verifyEmailRoute} component={VerifyEmail} />
                            </Switch>
                        </Container>
                    </React.Fragment>
                )}
            />
        </React.Fragment>
    )
}

export default withRouter(App)
