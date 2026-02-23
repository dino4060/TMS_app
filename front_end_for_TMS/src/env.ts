function getEnvSafely(value: string | undefined, name: string): string {
  if (!value) throw new Error(`>>> Environment variable ${name} is missing <<<`);
  return value
}

export const env = {
  BACKEND_URL: getEnvSafely(process.env.NEXT_PUBLIC_BACKEND_URL, "NEXT_PUBLIC_BACKEND_URL"),
  BACKEND_API_URL: getEnvSafely(process.env.NEXT_PUBLIC_BACKEND_API_URL, "NEXT_PUBLIC_BACKEND_API_URL"),
}
