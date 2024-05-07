import emptyApi from "../../app/api/emptyApi";
import {UserData} from "../cookies/cookieService";

export type LoginRequest = {
    Username: string,
    Password: string
}

export const loginApi = emptyApi.injectEndpoints({
    endpoints: (build) => ({
        Login: build.mutation<UserData, LoginRequest>({
            query: ({ ...data }) => ({
                url: '/user/login',
                method: 'POST',
                body: data
            })
        })
    }),
    overrideExisting: false
});

export const { useLoginMutation } = loginApi;