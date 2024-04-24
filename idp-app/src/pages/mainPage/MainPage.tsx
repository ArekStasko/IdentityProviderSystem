import {Box} from "@mui/material";
import Login from "../../components/login/Login";
import styles from "./MainPage.styles"

export const MainPage = () => {

    return(
        <Box sx={styles.mainPageContainer}>
           <Box sx={styles.contentWrapper}>
               <Login/>
           </Box>
        </Box>
    )
}

export default MainPage;