import Cookies from 'js-cookie'

export type UserData = {
    id: number,
    value: string
}

export const SaveReturnSite = (returnSite: string) => {
    Cookies.set('returnSite', returnSite);
}

export const GetReturnSite = () => {
    const site = Cookies.get('returnSite')
    if(!site) return undefined;
    return atob(site);
}