//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const idpApi = createApi({
    reducerPath: 'idpApi',
    baseQuery: fetchBaseQuery({
        baseUrl: 'http://192.168.1.40:8081/api/idp-v1'
    }),
    endpoints: (build) => ({})
});

export default idpApi;
