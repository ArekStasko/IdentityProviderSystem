import idpApi from "../../api/idpApi";

export const validateTokenApi = idpApi.injectEndpoints({
    endpoints: (build) => ({
        validateToken: build.query<boolean, string>({
            query: (accessToken: string) => `/access-token/validate?token=${accessToken}`
        })
    }),
    overrideExisting: false
});

export const { useValidateTokenQuery } = validateTokenApi;
