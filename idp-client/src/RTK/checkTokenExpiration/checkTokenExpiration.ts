import idpApi from "../../api/idpApi.ts";

export const checkTokenExpirationApi = idpApi.injectEndpoints({
    endpoints: (build) => ({
        checkToken: build.query<boolean, string>({
            query: (token: string) => `/token/checkTokenExp?token=${token}`
        })
    }),
    overrideExisting: false
});

export const { useCheckTokenQuery } = checkTokenExpirationApi;
