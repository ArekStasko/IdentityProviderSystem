import * as yup from 'yup';

const login = yup.object().shape({
    username: yup.string().required("Username field is required"),
    password: yup.string().required("Password field is required"),
});

const register = yup.object().shape({
    username: yup.string().required("Username field is required"),
    password: yup.string().required("Password field is required"),
    repeatPassword: yup.string().oneOf([yup.ref('password'), undefined], 'Passwords must match')
});

export default {
    login,
    register
}