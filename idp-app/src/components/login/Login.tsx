import {Box, Button, TextField, Typography} from "@mui/material";


export const Login = () => {
    return (
        <Box>
            <Typography>
                Login
            </Typography>
            <Box>
                <TextField/>
                <TextField/>
            </Box>
            <Button>Submit</Button>
        </Box>
    )
}

export default Login;