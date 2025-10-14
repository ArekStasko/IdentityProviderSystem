const refreshTokenKey = "refreshToken";

export const SaveRefreshToken = (token: string) => {
    localStorage.setItem(refreshTokenKey, token);
};

export const DeleteRefreshToken = () => {
    localStorage.removeItem(refreshTokenKey);
};

export const GetRefreshToken = (): string | undefined => {
    const token = localStorage.getItem(refreshTokenKey);
    if (!token) return undefined;
    return token;
};
