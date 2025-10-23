import React, {useEffect, useMemo} from "react";
import {useDispatch, useSelector} from "react-redux";
import { useLocation, useNavigate } from "react-router";
import {onSuccessfullLogin} from "../slices/authSlice";
import {RootState} from "../IdpClient";
import {ExpirationBanner} from "../models/ExpirationBannerInterface";

interface TrackingServiceProps {
    children: React.ReactNode;
    ExpirationBanner: React.ComponentType<ExpirationBanner>
}


const TrackingService = ({children, ExpirationBanner}: TrackingServiceProps) => {
    // true for test purposes
    const [closeExpirationBanner, setCloseExpirationBanner] = React.useState(true);
    const location = useLocation();
    const navigate = useNavigate();
    const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
    const dashboardRoute = useSelector((state: RootState) => state.auth.dasboardRoute);
    const authBaseRoute = useSelector((state: RootState) => state.auth.authBaseRoute);
    const dispatch = useDispatch();

    useEffect(() => {
        if(isAuthenticated) navigate(dashboardRoute);
        navigate(authBaseRoute);
    }, [isAuthenticated]);

    useMemo(() => {
        const parts = location.pathname.split('/');
        const refreshToken = parts.length === 4 ? parts[2] : null;
        const accessToken = parts.length === 4 ? parts[3] : null;
        if(refreshToken && accessToken) dispatch(onSuccessfullLogin({accessToken, refreshToken}));
    }, [location.pathname]);

    return (
        <>
            <ExpirationBanner
                close={closeExpirationBanner}
                onClose={() => setCloseExpirationBanner(false)}
                onRefresh={() => console.log("refresh")}
                onLogout={() => console.log("refresh")}
                />
            {children}
        </>
    )
}

export default TrackingService;