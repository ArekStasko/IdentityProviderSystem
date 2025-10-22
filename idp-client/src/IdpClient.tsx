/* eslint-disable @typescript-eslint/no-explicit-any */
import React from "react";
import {Provider} from "react-redux";
import {configureStore, Middleware} from "@reduxjs/toolkit";
import idpApi from "./api/idpApi";
import authReducer, {AuthSliceState, setAuthBaseRoute, setDashboardRoute} from "./slices/authSlice";
import TrackingService from "./services/trackingService";
import useSessionControll from "./RTK/sessionControl/sessionControll";
import ExpirationBanner from "./services/expirationBanner";

export interface RootState {
    auth: AuthSliceState
}

interface IdpClientProps {
    children: React.ReactNode;
    clientApi: any;
    authBaseRoute: string;
    dashboardRoute: string;
    renderExpirationBanner: () => React.ReactNode;
}

const IdpClient = ({children, clientApi, authBaseRoute, dashboardRoute, renderExpirationBanner}: IdpClientProps) => {

    const store = configureStore({
        reducer: {
            [clientApi.reducerPath]: clientApi.reducer,
            [idpApi.reducerPath]: idpApi.reducer,
            auth: authReducer
        },
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(idpApi.middleware, clientApi.middleware),
    });
    store.dispatch(setAuthBaseRoute(authBaseRoute));
    store.dispatch(setDashboardRoute(dashboardRoute));

    useSessionControll();
    return(
        <Provider store={store}>
            <TrackingService>
                <ExpirationBanner onExpiration={renderExpirationBanner} />
                {children}
            </TrackingService>
        </Provider>
    )
}

export default IdpClient