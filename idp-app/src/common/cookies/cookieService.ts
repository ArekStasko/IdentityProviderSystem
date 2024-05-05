import Cookies from 'js-cookie'

export type UserData = {
    id: number,
    value: string
}

export const SaveUserData = (userData: UserData) => {
    console.log(userData)
    Cookies.set('id', userData.id.toString());
    Cookies.set('token', userData.value);
}