import {useRefreshSessionMutation} from "../refreshSession/refreshSession";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../../IdpClient";
import React, {useEffect} from "react";
import {GetRefreshToken} from "../../services/localStorageService";
import {refreshAccessToken} from "../../slices/authSlice";

const useSessionControll = () => {
    const [sessionExpired, setSessionExpired] = React.useState(false);
    const [refreshSession, { data }] = useRefreshSessionMutation();
    const accessToken = useSelector((state: RootState) => state.auth.accessToken);
    const dispatch = useDispatch();

    useEffect(() => {
        const refreshToken = GetRefreshToken();
        if(refreshToken && !accessToken) {
            const runRefreshSession = async () => await refreshSession(refreshToken).unwrap();
            runRefreshSession().catch(console.error);
            return;
        }
    }, []);

    useEffect(() => {
        if(!data) return;
        if('error' in data) return;
        if(data.accessToken && data.refreshToken){
            dispatch(refreshAccessToken(data.accessToken));
            setSessionExpired(false)
        }
    }, [data]);

    const onRefreshSession = async () => {
        const refreshToken = GetRefreshToken();

        if(refreshToken){
            await refreshSession(refreshToken);
        }
    }

    const onLogout = async () => {
        console.log("Logout")
    }

    return {
        sessionExpired,
        setSessionExpired,
        onRefreshSession,
        onLogout
    }
}

export default useSessionControll;