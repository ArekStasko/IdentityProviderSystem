import React, {useEffect, useMemo} from "react";
import { useDispatch } from "react-redux";
import { useLocation } from "react-router";
import { login } from "../slices/authSlice";
import { GetRefreshToken } from "./cookieService";
import {useRefreshSessionMutation} from "../RTK/refreshSession/refreshSession";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const [refreshSession, { data }] = useRefreshSessionMutation();
    const dispatch = useDispatch();
    const location = useLocation();

    useEffect(() => {
        const refreshToken = GetRefreshToken();

        if(refreshToken) {
            const runRefreshSession = async () => await refreshSession(refreshToken).unwrap();
            runRefreshSession().catch(console.error);
            return;
        }
    }, []);

    useMemo(() => {
        //const parts = location.pathname.split('/');
        //const token = parts.length === 4 ? parts[3] : null;
    }, [location.pathname]);

    useMemo(() => {
        if(data) dispatch(login(data))
    }, [data])

    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;