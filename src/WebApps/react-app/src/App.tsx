import { Container } from '@material-ui/core'
import React from 'react'
import { Route, Switch, withRouter } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import { SignUpSuccess } from './auth/SignUpSuccess'
import { Hello } from './Hello'
import { HomePage } from './home-page/HomePage'

const App = () => {
    return (
        <React.Fragment>
            <ToastContainer position="bottom-right" />
            <Route exact path="/" component={HomePage} />
            <Route
                path={'/(.+)'}
                render={() => (
                    <React.Fragment>
                        <Container maxWidth="lg">
                            <Switch>
                                <Route path="/hello" component={Hello}></Route>
                                <Route exact path="/signUpSuccess" component={SignUpSuccess} />
                            </Switch>
                        </Container>
                    </React.Fragment>
                )}
            />
        </React.Fragment>
    )
}

export default withRouter(App)
