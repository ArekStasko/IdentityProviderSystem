import {Box, Button, InputAdornment, Link, TextField, Typography} from "@mui/material";
import {useForm} from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import AccountCircle from '@mui/icons-material/AccountCircle';
import Validators from "../../common/validators/Validators";
import styles from './Login.styles'


export const Login = () => {

    const formMethods = useForm({
        mode: 'onChange',
        resolver: yupResolver(Validators.login)
    })

    return (
        <Box sx={styles.Container}>
                <Typography>
                    Login
                </Typography>
                <Box sx={styles.FieldWrapper}>
                    <TextField
                        {...formMethods.register('username')}
                        InputProps={{
                            startAdornment: (
                                <InputAdornment position="start">
                                    <AccountCircle />
                                </InputAdornment>
                            ),
                        }}
                        variant="filled"
                        label="Username"
                        error={!!formMethods.formState.errors.username}
                        helperText={formMethods.formState.errors?.username?.message?.toString()}
                        sx={styles.Field}
                    />
                    <TextField
                        {...formMethods.register('password')}
                        variant="filled"
                        label="Password"
                        type="password"
                        error={!!formMethods.formState.errors.password}
                        helperText={formMethods.formState.errors?.password?.message?.toString()}
                        sx={styles.Field}
                    />
                </Box>
            <Box sx={styles.ButtonWrapper}>
                <Link href="#">I don`t have an acount</Link>
                <Button variant="contained">Login</Button>
            </Box>
        </Box>
    )
}

export default Login;