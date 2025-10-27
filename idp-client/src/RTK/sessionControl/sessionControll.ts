import {useRefreshSessionMutation} from "../refreshSession/refreshSession";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../../IdpClient";
import React, {useEffect} from "react";
import {GetRefreshToken} from "../../services/localStorageService";
import {refreshAccessToken} from "../../slices/authSlice";
import { useLazyValidateTokenQuery } from "../validateTokenApi/validateTokenApi";

const useSessionControll = () => {
    const [sessionExpired, setSessionExpired] = React.useState(false);
    const [intervalId, setIntervalId] = React.useState<number | null>(null);
    const [validateToken, { data: tokenValidationTime }] = useLazyValidateTokenQuery();
    const [refreshSession, { data: refreshSessionData }] = useRefreshSessionMutation();
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
        if(!tokenValidationTime) return;
        if(tokenValidationTime <= 0) {
            setSessionExpired(true);
        }
    }, [tokenValidationTime]);

    useEffect(() => {
        if(accessToken){
            const interval = window.setInterval(() => {
                validateToken(accessToken);
            });
            setIntervalId(interval)
        }

        if(intervalId && !accessToken){
            window.clearInterval(intervalId);
        }
    }, [accessToken])

    useEffect(() => {
        if(!refreshSessionData) return;
        if('error' in data) return;
        if(refreshSessionData.accessToken && data.refreshToken){
            dispatch(refreshAccessToken(data.accessToken));
            setSessionExpired(false)
        }
    }, [refreshSessionData]);

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