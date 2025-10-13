import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {DeleteToken, SaveToken} from "../services/cookieService";

export type AuthSliceState = {
    isAuthenticated: boolean;
    authBaseRoute: string;
    dasboardRoute: string;
    accessToken: string;
};
const initialState = {isAuthenticated: false, authBaseRoute: ""} as AuthSliceState;

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        login(state, action) {
            SaveToken(action.payload.RefreshToken);
            state.isAuthenticated = true;
        },
        logout(state) {
            DeleteToken();
            state.isAuthenticated = false;
        },
        setAccessToken(state, action: PayloadAction<string>){
          state.accessToken = action.payload
        },
        setAuthBaseRoute(state, action: PayloadAction<string>) {
            state.authBaseRoute = action.payload;
        },
        setDashboardRoute(state, action: PayloadAction<string>) {
            state.dasboardRoute = action.payload;
        }
    }
})

export const {login, logout, setAccessToken, setAuthBaseRoute, setDashboardRoute} = authSlice.actions;
export default authSlice.reducer