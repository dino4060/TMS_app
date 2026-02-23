import { TApiResult } from "@/types/base.types";

export const initialApiResult: TApiResult<any> = {
  success: true,
  status: 1,
  code: 1,
  error: '',
  data: {} as any,
}

export const createAppError = <T,>(error: string): TApiResult<T> => {
  return {
    success: false,
    status: 500,
    code: 0,
    error,
    data: {} as T,
  }
}