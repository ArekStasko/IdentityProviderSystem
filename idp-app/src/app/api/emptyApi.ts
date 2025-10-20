import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const emptyApi = createApi({
    reducerPath: 'emptyApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'http://localhost:5274/api/idp'
    }),
    endpoints: (build) => ({})
});

export default emptyApi;