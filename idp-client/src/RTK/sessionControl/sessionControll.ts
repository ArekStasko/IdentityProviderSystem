import {useRefreshSessionMutation} from "../refreshSession/refreshSession";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../../IdpClient";
import {useEffect} from "react";
import {GetRefreshToken} from "../../services/localStorageService";
import {refreshAccessToken} from "../../slices/authSlice";


const useSessionControll = () => {
    const [refreshSession, { data }] = useRefreshSessionMutation();
    const accessToken = useSelector((state: RootState) => state.auth.accessToken);
    const dispatch = useDispatch();

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
            dispatch(refreshAccessToken(data.accessToken));
        }
    }, [data]);
}

export default useSessionControll;