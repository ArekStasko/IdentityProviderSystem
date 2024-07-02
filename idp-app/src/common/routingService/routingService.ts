import {GetReturnSite} from "../cookies/cookieService";

export const returnToBaseSite = (userId: string, token: string) => {
    const returnSite = GetReturnSite();
    if(!returnSite) return;
    const url = `${returnSite}/${userId}/${token}`
    console.log(url)
    window.location.href = url;
}