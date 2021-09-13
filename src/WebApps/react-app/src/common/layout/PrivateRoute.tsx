import { Redirect, Route, RouteComponentProps, RouteProps } from 'react-router-dom';
import { getAccessToken } from '../auth/authHeaders';
import { CommonRoutes } from '../constants/routeConstants';
import { COMPONENT_ANY, COMPONENT_PROPS_ANY } from '../models/anyAliases';

interface Props extends RouteProps {
    component: React.ComponentType<RouteComponentProps<COMPONENT_PROPS_ANY>> | React.ComponentType<COMPONENT_ANY>;
}

export default function PrivateRoute({ component: Component, ...rest }: Props) {
    const token = getAccessToken();

    return (
        <Route
            {...rest}
            render={(props) => {
                if (!token) {
                    return <Redirect to={CommonRoutes.defaultRoute} />;
                }

                return <Component {...props} />;
            }}
        />
    );
}
