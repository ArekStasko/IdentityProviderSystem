import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const emptyApi = createApi({
    reducerPath: 'emptyApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'http://192.168.1.40:8081/api/idp'
    }),
    endpoints: (build) => ({})
});

export default emptyApi;