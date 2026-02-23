import { TApi } from "@/types/base.types";
import { cookies } from "next/headers";
import { REFRESH_REFRESH, TTECH_TOKEN } from "../constants/string";
import { fetchSafely } from "./config";

export const buildHeader = async (
  options: RequestInit = {},
  withAuth: boolean = true
): Promise<HeadersInit> => {
  // init header
  const headers: Record<string, string> = {
    'Content-Type': 'application/json',
    ...(options.headers as Record<string, string>),
  };


  // get server cookies
  const cookieStore = await cookies();
  const refreshToken = cookieStore.get(REFRESH_REFRESH)?.value;
  const accessToken = cookieStore.get(TTECH_TOKEN)?.value; // Error: (TODO) Should get TTECH_TOKEN from local

  // include ACCESS_TOKEN
  if (withAuth && accessToken) {
    headers['Authorization'] = `Bearer ${accessToken}`;
  }

  // include REFRESH_TOKEN
  // NOTE: 'Cookie'
  // Next tự inject cookie từ next/headers() khi bạn fetch từ RRC
  // Đính cookie thủ công nếu dùng Api route h hay Server action
  if (refreshToken) {
    headers['Cookie'] = `REFRESH_TOKEN=${refreshToken}`;

    // TEST
    // console.log('>>> serverFetch: cookies toString: ', cookieStore.toString());
    // console.log('>>> serverFetch: cookies getValue: ', REFRESH_TOKEN);
  }

  return headers;
}

const fetchCore = async (
  endpoint: RequestInfo,
  options: RequestInit = {},
  withAuth: boolean = false
) => {
  // config header: include a pair of token
  const headers = await buildHeader(options, withAuth);

  // fetch
  return await fetch(endpoint, {
    ...options,
    headers,
  });
}

export const serverFetch = async <T = any>(api: TApi<T>) => {
  return fetchSafely<T>(api, fetchCore);
}