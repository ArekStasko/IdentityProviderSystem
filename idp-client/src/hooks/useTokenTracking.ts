import { useNavigate } from 'react-router';
import {useEffect, useState} from "react";
import {useCheckTokenQuery} from "../RTK/checkTokenExpiration/checkTokenExpiration";
import {useDispatch, useSelector} from "react-redux";
import {GetToken} from "../services/cookieService";
import {RootState} from "../IdpClient";
import {logout} from "../slices/authSlice";

const useTokenTracking = () => {
    const navigate = useNavigate();
    const [token, setToken] = useState<string | undefined>(undefined);
    const { data: isTokenValid, isFetching, refetch } = useCheckTokenQuery(token!, { skip: !token });
    const dispatch = useDispatch();
    const isAuth = useSelector((state: RootState) => state.auth.isAuthenticated);
    const authBaseRoute = useSelector((state: RootState) => state.auth.authBaseRoute);
    const dasboardRoute = useSelector((state: RootState) => state.auth.dasboardRoute);

    useEffect(() => {
        let interval: number | undefined;

        const token = GetToken();
        //if (!token) navigate(authBaseRoute);
        setToken(token);
        if(isAuth){
            console.log("WORK")
            interval = window.setInterval(() => {
                refetch();
            }, 30000);
            navigate(dasboardRoute);
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