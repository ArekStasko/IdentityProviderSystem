import Cookies from 'js-cookie';

export const SaveToken = (token: string) => {
    Cookies.set('refreshToken', token);
};

export const DeleteToken = () => {
    Cookies.remove('refreshToken');
};

export const GetRefreshToken = (): string | undefined => {
    const token = Cookies.get('refreshToken');
    if (!token) return undefined;
    return token;
};
