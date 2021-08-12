import { Container } from '@material-ui/core'
import React from 'react'
import { Route, Switch } from 'react-router-dom'
import { Hello } from './Hello'
import { HomePage } from './home-page/HomePage'

export const App = () => {
    return (
        <React.Fragment>
            <Route exact path="/" component={HomePage} />
            <Route
                path={'/(.+)'}
                render={() => (
                    <React.Fragment>
                        <Container maxWidth="lg">
                            <Switch>
                                <Route path="/hello" component={Hello}></Route>
                            </Switch>
                        </Container>
                    </React.Fragment>
                )}
            />
        </React.Fragment>
    )
}
