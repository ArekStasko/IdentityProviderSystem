/* eslint-disable @typescript-eslint/no-explicit-any */
import React from "react";
import {Provider} from "react-redux";
import {configureStore, Middleware} from "@reduxjs/toolkit";
import idpApi from "./api/idpApi";
import authReducer, {AuthSliceState, setAuthBaseRoute, setDashboardRoute} from "./slices/authSlice";
import TrackingService from "./services/trackingService";
import useSessionControll from "./RTK/sessionControl/sessionControll";
import {ExpirationBannerInterface} from "./models/ExpirationBannerInterface";

export interface RootState {
    auth: AuthSliceState
}

interface IdpClientProps {
    children: React.ReactNode;
    clientApi: any;
    authBaseRoute: string;
    dashboardRoute: string;
    expirationBanner: React.ComponentType<ExpirationBannerInterface>
}

const IdpClient = ({children, clientApi, authBaseRoute, dashboardRoute, expirationBanner}: IdpClientProps) => {

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
            <TrackingService ExpirationBanner={expirationBanner}>
                {children}
            </TrackingService>
        </Provider>
    )
}

export default IdpClient