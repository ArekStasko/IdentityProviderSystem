/* eslint-disable @typescript-eslint/no-explicit-any */
import React from "react";
import {Provider} from "react-redux";
import {configureStore} from "@reduxjs/toolkit";
import idpApi from "./api/idpApi.ts";
import authReducer, {AuthSliceState, setAuthBaseRoute} from "./slices/authSlice.ts";

export interface RootState {
    auth: AuthSliceState
}

interface IdpClientProps {
    children: React.ReactNode;
    clientStore: any;
    authBaseRoute: string;
}

const IdpClient = ({children, clientStore, authBaseRoute}: IdpClientProps) => {

    const store = configureStore({
        reducer: {
            ...clientStore.reducer,
            [idpApi.reducerPath]: idpApi.reducer,
            auth: authReducer
        },
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(idpApi.middleware, clientStore.middleware),
    });
    store.dispatch(setAuthBaseRoute(authBaseRoute));

    return(
        <Provider store={store}>
            {children}
        </Provider>
    )
}

export default IdpClient