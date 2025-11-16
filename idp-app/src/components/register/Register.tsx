import {Alert, Box, Button, InputAdornment, LinearProgress, Link, TextField, Typography} from "@mui/material";
import styles from "../../common/styles/styles";
import AccountCircle from "@mui/icons-material/AccountCircle";
import {useNavigate} from "react-router";
import {useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import Validators from "../../common/validators/Validators";
import routingConstants from "../../app/routing/routingConstants";
import {RegisterRequest, useRegisterMutation} from "../../common/slices/register";
import {returnToBaseSite} from "../../common/routingService/routingService";

export const Register = () => {
    const navigate = useNavigate();
    const [register, {isLoading, isError}] = useRegisterMutation();
    const formMethods = useForm({
        mode: 'onChange',
        resolver: yupResolver(Validators.register)
    })

    const registerUser = async () => {
        const registerRequest = {
            Username: formMethods.getValues("username"),
            Password: formMethods.getValues("password"),
            RepeatPassword: formMethods.getValues("repeatPassword")
        } as RegisterRequest
        const result = await register(registerRequest)
        if("data" in result){
            returnToBaseSite(result.data.refreshToken, result.data.accessToken)
        }
    }

    return (
        <Box sx={styles.Container}>
            <Box sx={styles.TitleWrapper}>
                <Typography sx={styles.Title}>
                    Register
                </Typography>
            </Box>
            <Box sx={styles.AlertWrapper}>
                {
                    isError && (
                        <Alert severity="error">Sorry, something went wrong.</Alert>
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
                <TextField
                    {...formMethods.register('repeatPassword')}
                    variant="filled"
                    label="Repeat Password"
                    type="password"
                    error={!!formMethods.formState.errors.repeatPassword}
                    helperText={formMethods.formState.errors?.repeatPassword?.message?.toString()}
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
                        <Link onClick={() => navigate(routingConstants.root)}>Already have account ?</Link>
                        <Button disabled={!formMethods.formState.isValid} onClick={() => registerUser()} variant="contained">Register</Button>
                    </Box>
                )
            }
        </Box>
    )
}

export default Register;