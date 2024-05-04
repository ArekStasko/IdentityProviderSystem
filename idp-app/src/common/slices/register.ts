import emptyApi from "../../app/api/emptyApi";

export type RegisterRequest = {
    Username: string,
    Password: string,
    RepeatPassword: string
}

export const registerApi = emptyApi.injectEndpoints({
    endpoints: (build) => ({
        Register: build.mutation<string, RegisterRequest>({
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