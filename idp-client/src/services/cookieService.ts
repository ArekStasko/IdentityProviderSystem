import Cookies from 'js-cookie';

export const SaveRefreshToken = (token: string) => {
    Cookies.set('refreshToken', token);
};

export const DeleteRefreshToken = () => {
    Cookies.remove('refreshToken');
};

export const GetRefreshToken = (): string | undefined => {
    const token = Cookies.get('refreshToken');
    if (!token) return undefined;
    return token;
};
