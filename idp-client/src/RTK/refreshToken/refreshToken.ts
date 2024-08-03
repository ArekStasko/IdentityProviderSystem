import {GetToken} from "../../services/cookieService.ts";
import idpApi from "../../api/idpApi.ts";

export const refreshTokenApi = idpApi.injectEndpoints({
    endpoints: (build) => ({
        RefreshToken: build.mutation<boolean, void>({
            query: () => ({
                url: `/token/refreshToken?token=${GetToken()}`,
                method: 'POST',
                body: {}
            })
        })
    }),
    overrideExisting: false
});

export const { useRefreshTokenMutation } = refreshTokenApi;
