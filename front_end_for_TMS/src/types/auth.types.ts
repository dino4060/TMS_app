export type TAuthResult = {
  success: boolean;
  token?: string;
  refreshToken?: string;
  errors?: string[];
};

export type TUserProfile = {
  email: string;
  userName: string;
  roles: string[];
};

export type TTokenDto = {
  token: string;
  refreshToken: string;
};

export type TRegisterDto = {
  email: string;
  password: string;
};

export type TLoginDto = {
  email: string;
  password: string;
};