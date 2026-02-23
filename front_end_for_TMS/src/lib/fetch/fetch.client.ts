// src/lib/fetch/fetch.client.ts
import clientCookies from "../storage/cookie.client";
import { fetchSafely } from "./config";
import { TTECH_TOKEN } from "../constants/string";
import clientLocal from "../storage/local.client";
import { TApi } from "@/types/base.types";

const buildHeader = async (
  options: RequestInit = {},
  withAuth: boolean = true,
): Promise<HeadersInit> => {
  // init header
  const headers: Record<string, string> = {
    "Content-Type": "application/json",
    ...(options.headers as Record<string, string>),
  };

  // include ACCESS_TOKEN
  const accessToken = clientLocal.get(TTECH_TOKEN);

  if (withAuth && accessToken) {
    headers["Authorization"] = `Bearer ${accessToken}`;
  }

  return headers;
};

const fetchCore = async (
  endpoint: RequestInfo,
  options: RequestInit = {},
  withAuth: boolean = true,
) => {
  // config header (include ACCESS_TOKEN)
  const headers = await buildHeader(options, withAuth);

  // fetch (include REFRESH_TOKEN)
  return await fetch(endpoint, {
    ...options,
    credentials: "include",
    headers,
  });
};

export const clientFetch = async <T = any>(api: TApi<T>) => {
  return fetchSafely<T>(api, fetchCore);
};
