import {GetToken} from "../../services/cookieService";
import idpApi from "../../api/idpApi";

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
