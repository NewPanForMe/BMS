import loginApi from './login/index'
import userApi from './user/index'

const api = {
    ...loginApi,
    ...userApi
}
export default api