import React, {useEffect, useMemo} from "react";
import {useDispatch, useSelector} from "react-redux";
import { useLocation } from "react-router";
import {refreshAccessToken} from "../slices/authSlice";
import { GetRefreshToken} from "./cookieService";
import {useRefreshSessionMutation} from "../RTK/refreshSession/refreshSession";
import {RootState} from "../IdpClient";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const [refreshSession, { data }] = useRefreshSessionMutation();
    const dispatch = useDispatch();
    const accessToken = useSelector((state: RootState) => state.auth.accessToken);
    const location = useLocation();

    useEffect(() => {
        const refreshToken = GetRefreshToken();

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
        //const parts = location.pathname.split('/');
        //const token = parts.length === 4 ? parts[3] : null;
    }, [location.pathname]);

    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;