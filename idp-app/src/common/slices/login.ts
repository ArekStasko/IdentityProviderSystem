import emptyApi from "../../app/api/emptyApi";

export type LoginRequest = {
    Username: string,
    Password: string
}

export const loginApi = emptyApi.injectEndpoints({
    endpoints: (build) => ({
        Login: build.mutation<string, LoginRequest>({
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