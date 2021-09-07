import { connectRouter, routerMiddleware } from 'connected-react-router';
import { createBrowserHistory, createMemoryHistory, MemoryHistory, History } from 'history';
import { applyMiddleware, combineReducers, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import logger from 'redux-logger';
import createSagaMiddleware from 'redux-saga';
import { productsReducer } from '../../../components/catalog/state/reducers/productsReducer';
import { errorReducer } from '../reducers/errorReducer';
import { loaderReducer } from '../reducers/loaderReducer';
import rootSaga from '../sagas/rootSaga';

export let history: MemoryHistory<unknown> | History<unknown>;

if (process.env.NODE_ENV === 'development' || process.env.NODE_ENV === 'production') {
    history = createBrowserHistory();
} else {
    history = createMemoryHistory();
}

const reducers = {
    loader: loaderReducer,
    errors: errorReducer,
    products: productsReducer,
    router: connectRouter(history),
};

const rootReducer = combineReducers({
    ...reducers,
});

export type RootState = ReturnType<typeof rootReducer>;

let store = null;

export const getStore = () => {
    const sagaMiddleware = createSagaMiddleware();

    const middlewares = [sagaMiddleware, routerMiddleware(history), logger];
    const enhancers = [applyMiddleware(...middlewares)];

    store = createStore(rootReducer, composeWithDevTools(...enhancers));

    sagaMiddleware.run(rootSaga);

    return store;
};
