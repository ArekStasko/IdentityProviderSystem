import { useNavigate } from 'react-router';
import {useEffect, useState} from "react";
import {useCheckTokenQuery} from "../RTK/checkTokenExpiration/checkTokenExpiration";
import {useDispatch, useSelector} from "react-redux";
import {GetToken} from "../services/cookieService";
import {RootState} from "../IdpClient";
import {logout} from "../slices/authSlice";

const useTokenTracking = () => {
    const navigate = useNavigate();
    const [token, setToken] = useState<string | undefined>(GetToken());
    const [interval, setInterval] = useState<number | undefined>(undefined);
    const { data: isTokenValid, isFetching, refetch } = useCheckTokenQuery(token!, { skip: !token });
    const dispatch = useDispatch();
    const isAuth = useSelector((state: RootState) => state.auth.isAuthenticated);
    const authBaseRoute = useSelector((state: RootState) => state.auth.authBaseRoute);
    const dasboardRoute = useSelector((state: RootState) => state.auth.dasboardRoute);

    useEffect(() => {
        const token = GetToken();
        setToken(token);
        if(isAuth){
            const intervalToSet = window.setInterval(() => refetch(), 30000);
            setInterval(intervalToSet);
            navigate(dasboardRoute);
        }
        if(!isAuth){
            clearInterval(interval);
            navigate(authBaseRoute);
        }
    }, [isAuth]);

    useEffect(() => {
        if (!isTokenValid && isTokenValid !== undefined) {
            clearInterval(interval);
            dispatch(logout())
        }
    }, [isFetching]);
}

export default useTokenTracking;