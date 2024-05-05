import emptyApi from "../../app/api/emptyApi";
import {UserData} from "../cookies/cookieService";

export type RegisterRequest = {
    Username: string,
    Password: string,
    RepeatPassword: string
}

export const registerApi = emptyApi.injectEndpoints({
    endpoints: (build) => ({
        Register: build.mutation<UserData, RegisterRequest>({
            query: ({ ...data }) => ({
                url: '/user/register',
                method: 'POST',
                body: data
            })
        })
    }),
    overrideExisting: false
});

export const { useRegisterMutation } = registerApi;