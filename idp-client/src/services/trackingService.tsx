import useTokenTracking from "../hooks/useTokenTracking";
import usePageTracking from "../hooks/usePageTracking";
import React, {useEffect} from "react";
import {useDispatch, useSelector} from "react-redux";
import {useLocation, useNavigate, useParams } from "react-router";
import {login} from "../slices/authSlice";
import {RootState} from "../IdpClient";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    const dispatch = useDispatch();
    const location = useLocation();

    useEffect(() => {
        const parts = location.pathname.split('/');
        const token = parts.length === 4 ? parts[3] : null;
        if(token) {
            dispatch(login({token: token}))
        }
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