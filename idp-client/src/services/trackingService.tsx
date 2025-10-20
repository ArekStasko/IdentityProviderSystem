import React, {useMemo} from "react";
import {useDispatch} from "react-redux";
import { useLocation } from "react-router";
import {onSuccessfullLogin} from "../slices/authSlice";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const location = useLocation();
    const dispatch = useDispatch();

    useMemo(() => {
        const parts = location.pathname.split('/');
        const refreshToken = parts.length === 4 ? parts[2] : null;
        const accessToken = parts.length === 4 ? parts[3] : null;
        if(refreshToken && accessToken) dispatch(onSuccessfullLogin({accessToken, refreshToken}));
    }, [location.pathname]);

    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;