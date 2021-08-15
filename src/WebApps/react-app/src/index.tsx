import ReactDOM from 'react-dom'
import { Router } from 'react-router-dom'
import { createBrowserHistory } from 'history'
import { App } from './App'
import { Provider } from 'react-redux'
import { getStore } from './common/state/store/commonStore'

export const history = createBrowserHistory()

ReactDOM.render(
    <Router history={history}>
        <Provider store={getStore()}>
            <App />
        </Provider>
    </Router>,
    document.getElementById('root')
)
