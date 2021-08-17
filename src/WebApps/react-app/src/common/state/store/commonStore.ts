import { connectRouter, routerMiddleware } from 'connected-react-router'
import { createBrowserHistory } from 'history'
import { applyMiddleware, combineReducers, createStore } from 'redux'
import { composeWithDevTools } from 'redux-devtools-extension'
import logger from 'redux-logger'
import createSagaMiddleware from 'redux-saga'
import { loaderReducer } from '../reducers/loaderReducer'
import rootSaga from '../sagas/rootSaga'

export const history = createBrowserHistory()

const reducers = {
    loader: loaderReducer,
    router: connectRouter(history),
}

const rootReducer = combineReducers({
    ...reducers,
})

let store = null

export const getStore = () => {
    const sagaMiddleware = createSagaMiddleware()

    const middlewares = [sagaMiddleware, routerMiddleware(history), logger]
    const enhancers = [applyMiddleware(...middlewares)]

    store = createStore(rootReducer, composeWithDevTools(...enhancers))

    sagaMiddleware.run(rootSaga)

    return store
}
