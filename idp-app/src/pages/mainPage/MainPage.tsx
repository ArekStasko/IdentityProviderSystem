import {Box} from "@mui/material";
import styles from "./MainPage.styles"
import {FunctionComponent, ReactElement} from "react";

type MainPageProps = {
    children: ReactElement;
};

export const MainPage: FunctionComponent<MainPageProps> = ({ children }) => {

    return(
        <Box sx={styles.mainPageContainer}>
           <Box sx={styles.contentWrapper}>
               {children}
           </Box>
        </Box>
    )
}

export default MainPage;