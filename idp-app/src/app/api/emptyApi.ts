import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const emptyApi = createApi({
    reducerPath: 'emptyApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'http://localhost:8081/api/idp-v1'
    }),
    endpoints: (build) => ({})
});

export default emptyApi;