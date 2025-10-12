import {GetReturnSite} from "../cookies/cookieService";

export const returnToBaseSite = (refreshToken: string, accessToken: string) => {
    const returnSite = GetReturnSite();
    if(!returnSite) return;
    const url = `${returnSite}/${refreshToken}/${accessToken}`
    window.location.href = url;
}