import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router';
import { useRefreshTokenMutation } from '../RTK/refreshToken/refreshToken';
import { useSelector } from "react-redux";
import {RootState} from "../IdpClient";
import {GetToken} from "../services/cookieService";

const usePageTracking = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [refreshToken, { isLoading }] = useRefreshTokenMutation();
    const isAuth = useSelector((state: RootState) => state.auth.isAuthenticated);
    const authBaseRoute = useSelector((state: RootState) => state.auth.authBaseRoute);

    useEffect(() => {
        if (location.pathname === authBaseRoute || isLoading) return;
        if(!isAuth) {
            navigate(authBaseRoute);
            return;
        }
        const token = GetToken();
        if (!token) {
            navigate(authBaseRoute);
            return;
        }
        const runRefreshToken = async () => await refreshToken().unwrap();
        runRefreshToken().catch(console.error);
    }, [location]);
};

export default usePageTracking;