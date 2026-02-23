import { HttpMethod, TApi } from "@/types/base.types"
import {
  TLoginDto,
  TRegisterDto,
  TTokenDto,
  TAuthResult,
  TUserProfile
} from "@/types/auth.types"

const BACKEND_URL = "http://localhost:8080";
const BACKEND_API_URL = `${BACKEND_URL}/api`;
const buildBackendApiRoute = (resource: string, endpoint: string) => `${BACKEND_API_URL}/${resource}/${endpoint}`;

const AccountRoute = (endpoint: string) => buildBackendApiRoute('Account', endpoint);

export const accountApi = {
  getInfo: (): TApi<{}> => ({
    route: AccountRoute('GetInfo'),
    method: HttpMethod.GET,
  }),

  login: (body: TLoginDto): TApi<TAuthResult> => ({
    route: AccountRoute('Login'),
    method: HttpMethod.POST,
    body,
  }),

  register: (body: TRegisterDto): TApi<TAuthResult> => ({
    route: AccountRoute('Register'),
    method: HttpMethod.POST,
    body,
  }),

  refreshToken: (body: TTokenDto): TApi<TAuthResult> => ({
    route: AccountRoute('RefreshToken'),
    method: HttpMethod.POST,
    body,
  }),

  logout: (): TApi<{ message: string }> => ({
    route: AccountRoute('Logout'),
    method: HttpMethod.POST,
  }),

  getMe: (): TApi<TUserProfile> => ({
    route: AccountRoute('GetMe'),
    method: HttpMethod.GET,
  }),
}