/* eslint-disable @typescript-eslint/no-explicit-any */
import React from "react";
import {Provider} from "react-redux";
import {configureStore, Middleware} from "@reduxjs/toolkit";
import idpApi from "./api/idpApi";
import authReducer, {AuthSliceState, setAuthBaseRoute} from "./slices/authSlice";
import TrackingService from "./services/trackingService";

export interface RootState {
    auth: AuthSliceState
}

interface IdpClientProps {
    children: React.ReactNode;
    clientApi: any;
    authBaseRoute: string;
}

const IdpClient = ({children, clientApi, authBaseRoute}: IdpClientProps) => {

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

    return(
        <Provider store={store}>
            <TrackingService>
                {children}
            </TrackingService>
        </Provider>
    )
}

export default IdpClient