import { Route, Switch } from 'react-router-dom'
import { Hello } from './Hello'

export const App = () => {
    return (
        <>
            <Switch>
                <Route exact path="/hello" component={Hello} />
            </Switch>
        </>
    )
}
