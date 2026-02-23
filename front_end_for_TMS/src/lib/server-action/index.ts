// lib/server-action.ts
'use server';
import { serverFetch } from "@/lib/fetch/fetch.server";
import { TApi } from "@/types/base.types";

export async function serverAction<T>(api: TApi<T>) {
  return await serverFetch(api);
}