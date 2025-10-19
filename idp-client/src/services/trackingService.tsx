import React, {useEffect, useMemo} from "react";
import {useSelector} from "react-redux";
import { useLocation } from "react-router";
import {onSuccessfullLogin, refreshAccessToken} from "../slices/authSlice";
import { GetRefreshToken} from "./localStorageService";
import {useRefreshSessionMutation} from "../RTK/refreshSession/refreshSession";
import {RootState} from "../IdpClient";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const [refreshSession, { data }] = useRefreshSessionMutation();
    const accessToken = useSelector((state: RootState) => state.auth.accessToken);
    const location = useLocation();

    useEffect(() => {
        const refreshToken = GetRefreshToken();
        console.log("REFRESH TOKEN")
        console.log(refreshToken);
        if(refreshToken && !accessToken) {
            const runRefreshSession = async () => await refreshSession(refreshToken).unwrap();
            runRefreshSession().catch(console.error);
            return;
        }
    }, []);

    useEffect(() => {
        if(data && data.accessToken && data.refreshToken){
            refreshAccessToken(data.accessToken);
        }
    }, [data]);

    useMemo(() => {
        const parts = location.pathname.split('/');
        const refreshToken = parts.length === 4 ? parts[2] : null;
        const accessToken = parts.length === 4 ? parts[3] : null;
        console.log("---")
        console.log(parts)
        console.log(accessToken)
        console.log(refreshToken)
        console.log("---")
        if(refreshToken && accessToken) onSuccessfullLogin({accessToken, refreshToken});
    }, [location.pathname]);

    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;