const isBrowser = typeof window !== 'undefined';

const clientCookies = {
    set: (key: string, value: string, days: number = 1, path: string = '/') => {
        if (!isBrowser) {
            return null;
        }

        const expires = new Date();
        expires.setDate(expires.getDate() + days);
        document.cookie = `${key}=${encodeURIComponent(value)}; expires=${expires.toUTCString()}; path=${path}`;
    },

    get: (key: string): string | null => {
        if (!isBrowser) {
            return null;
        }

        const nameEQ = `${key}=`;
        const ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length));
        }
        return null;
    },

    remove: (key: string, path: string = '/') => {
        if (!isBrowser) {
            return null;
        }

        const expires = new Date(0).toUTCString(); // epoch time: Thu, 01 Jan 1970
        document.cookie = `${key}=; expires=${expires}; path=${path}`;
    },

    // remove2: (key: string, path: string = '/') => {
    //     if (!isBrowser) {
    //         return null;
    //     }

    //     document.cookie = `${key}=; Max-Age=0; path=${path}`;
    // },
};

export default clientCookies;