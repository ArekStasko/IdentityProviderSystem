import {Alert, Box, CircularProgress} from "@mui/material";
import styles from './IdpManager.styles'
import {useParams} from "react-router";
import {useEffect} from "react";
import {useNavigate} from "react-router-dom";
import {SaveReturnSite} from "../../common/cookies/cookieService";

const IdpManager = () => {
    const { returnSite } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        if(!returnSite) return;
        SaveReturnSite(returnSite);
        navigate("/idp-login")
    }, [returnSite]);

    return(
        <Box sx={styles.IdpManagerContainer}>
            {
                !returnSite && (
                    <Alert sx={styles.IdpAlert} severity="error">Sorry, something went wrong.</Alert>
                )
            }
            <CircularProgress/>
        </Box>
    )
}

export default IdpManager;