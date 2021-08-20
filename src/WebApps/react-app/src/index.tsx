import ReactDOM from 'react-dom';
import { ConnectedRouter } from 'connected-react-router';
import App from './App';
import { Provider } from 'react-redux';
import { getStore, history } from './common/state/store/commonStore';
import 'react-toastify/dist/ReactToastify.min.css';

ReactDOM.render(
    <Provider store={getStore()}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root')
);
