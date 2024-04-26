import * as yup from 'yup';

const login = yup.object().shape({
    username: yup.string().required(),
    password: yup.string().required(),
});

const register = yup.object().shape({
    username: yup.string().required(),
    password: yup.string().required(),
    repeatPassword: yup.string().required()
});