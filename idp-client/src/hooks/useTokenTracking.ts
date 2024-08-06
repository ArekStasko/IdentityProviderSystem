import { useNavigate } from 'react-router';
import {useEffect, useState} from "react";
import {useCheckTokenQuery} from "../RTK/checkTokenExpiration/checkTokenExpiration.ts";
import {useDispatch, useSelector} from "react-redux";
import {GetToken} from "../services/cookieService.ts";
import {RootState} from "../IdpClient.tsx";
import {logout} from "../slices/authSlice.ts";

const useTokenTracking = () => {
    const navigate = useNavigate();
    const [token, setToken] = useState<string | undefined>(undefined);
    const { data: isTokenValid, isFetching, refetch } = useCheckTokenQuery(token!, { skip: !token });
    const dispatch = useDispatch();
    const isAuth = useSelector((state: RootState) => state.auth.isAuthenticated);
    const authBaseRoute = useSelector((state: RootState) => state.auth.authBaseRoute);

    useEffect(() => {
        let interval: number | undefined;

        const token = GetToken();
        if (!token) navigate(authBaseRoute);
        setToken(token);
        if(isAuth){
            interval = setInterval(() => {
                refetch();
            }, 30000);
        }
        if(!isAuth){
            clearInterval(interval);
        }
    }, [isAuth]);

    useEffect(() => {
        if (!isTokenValid && isTokenValid !== undefined) dispatch(logout())
    }, [isFetching]);
}

export default useTokenTracking;