import {Alert, Box, Button, InputAdornment, LinearProgress, Link, TextField, Typography} from "@mui/material";
import {useForm} from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import AccountCircle from '@mui/icons-material/AccountCircle';
import Validators from "../../common/validators/Validators";
import styles from "../../common/styles/styles";
import {LoginRequest, useLoginMutation} from "../../common/slices/login";
import {useNavigate} from "react-router";
import routingConstants from "../../app/routing/routingConstants";


export const Login = () => {
    const navigate = useNavigate();
    const [login, {isLoading, isError}] = useLoginMutation();
    const formMethods = useForm({
        mode: 'onChange',
        resolver: yupResolver(Validators.login)
    })

    const loginUser = async () => {
        const loginRequest = {
            Username: formMethods.getValues("username"),
            Password: formMethods.getValues("password")
        } as LoginRequest
        const result = await login(loginRequest)
        if("data" in result){
            console.log(result.data)
        }
    }

    return (
        <Box sx={styles.Container}>
            <Box sx={styles.TitleWrapper}>
                <Typography sx={styles.Title}>
                    Login
                </Typography>
            </Box>
            <Box sx={styles.AlertWrapper}>
                {
                    isError && (
                        <Alert severity="error">Wrong Username or Password.</Alert>
                    )
                }
            </Box>
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
            {
                isLoading ? (
                    <Box sx={styles.LoadingWrapper}>
                        <LinearProgress />
                    </Box>
                ) : (
                    <Box sx={styles.ButtonWrapper}>
                        <Link onClick={() => navigate(routingConstants.register)}>I don`t have an acount</Link>
                        <Button disabled={!formMethods.formState.isValid} onClick={() => loginUser()} variant="contained">Login</Button>
                    </Box>
                )
            }
        </Box>
    )
}

export default Login;