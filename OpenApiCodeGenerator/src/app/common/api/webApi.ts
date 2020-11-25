import { environment } from "src/environments/environment";

const DEV_URL = environment.apiUrl;

export const WebAPI = {
    GET_API_PROPERTY: { api: DEV_URL + 'api/', mockApi: '/assets/mockdata/apiPropertyDetails.json' },
}