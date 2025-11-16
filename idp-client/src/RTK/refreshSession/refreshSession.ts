import idpApi from "../../api/idpApi";
import {UserData} from "../../models/UserData";

export const refreshSessionApi = idpApi.injectEndpoints({
    endpoints: (build) => ({
        refreshSession: build.mutation<UserData, string>({
            query: (refreshToken: string) => ({
                url: `/user/RefreshSession?refreshToken=${refreshToken}`,
                method: 'POST',
                body: {}
            })
        })
    }),
    overrideExisting: false
});

export const { useRefreshSessionMutation } = refreshSessionApi;
