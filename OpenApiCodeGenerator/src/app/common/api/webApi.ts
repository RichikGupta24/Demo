import { environment } from "src/environments/environment";

const HOST = environment.apiUrl;

export const WebAPI = {
    GET_API_PROPERTY: { api: HOST + '', mockApi: '/assets/mockdata/apiPropertyDetails.json' },
    GET_API_INFO: { api: HOST + 'reactCodeGen/api/retrieveAPIInfo', mockApi: '' },
}