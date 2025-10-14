import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {DeleteRefreshToken, SaveRefreshToken} from "../services/localStorageService";

export type AuthSliceState = {
    isAuthenticated: boolean;
    authBaseRoute: string;
    dasboardRoute: string;
    accessToken: string | null;
};
const initialState = {isAuthenticated: false, authBaseRoute: ""} as AuthSliceState;

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        onSuccessfullLogin(state, action) {
            SaveRefreshToken(action.payload.RefreshToken);
            state.accessToken = action.payload.AccessToken;
            state.isAuthenticated = true;
        },
        onSuccessfullLogout(state) {
            DeleteRefreshToken();
            state.accessToken = null;
            state.isAuthenticated = false;
        },
        refreshAccessToken(state, action){
            state.accessToken = action.payload
            state.isAuthenticated = true;
        },
        setAuthBaseRoute(state, action: PayloadAction<string>) {
            state.authBaseRoute = action.payload;
        },
        setDashboardRoute(state, action: PayloadAction<string>) {
            state.dasboardRoute = action.payload;
        }
    }
})

export const {onSuccessfullLogin, onSuccessfullLogout, refreshAccessToken, setAuthBaseRoute, setDashboardRoute} = authSlice.actions;
export default authSlice.reducer