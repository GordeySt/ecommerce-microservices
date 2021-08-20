import ReactDOM from 'react-dom'
import { Router } from 'react-router-dom'
import App from './App'
import { Provider } from 'react-redux'
import { getStore, history } from './common/state/store/commonStore'
import 'react-toastify/dist/ReactToastify.min.css'

ReactDOM.render(
    <Provider store={getStore()}>
        <Router history={history}>
            <App />
        </Router>
    </Provider>,
    document.getElementById('root')
)
