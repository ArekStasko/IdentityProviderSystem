import useTokenTracking from "../hooks/useTokenTracking";
import usePageTracking from "../hooks/usePageTracking";
import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { useLocation } from "react-router";
import { login } from "../slices/authSlice";
import {GetToken} from "./cookieService";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const dispatch = useDispatch();
    const location = useLocation();

    useEffect(() => {
        const parts = location.pathname.split('/');
        const token = parts.length === 4 ? parts[3] : null;
        const cachedToken = GetToken();

        if(cachedToken) {
            dispatch(login({token: cachedToken}))
            return;
        }

        if(token) dispatch(login({token: token}))
    }, [location]);

    useTokenTracking()
    usePageTracking()
    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;