import { cookies } from "next/headers";

export const serverCookies = {
    get: async (KEY: string) => {
        const cookieStore = await cookies();
        const value = cookieStore.get(KEY)?.value;
        return value || null;
    }
}