const isBrowser = typeof window !== 'undefined';

const clientLocal = {
    set: <T = string>(key: string, value: T) => {
        if (!isBrowser) {
            return null;
        }

        const stringified = typeof value === 'string'
            ? value
            : JSON.stringify(value);
        localStorage.setItem(key, stringified);
    },

    get: <T = string>(key: string): T | null => {
        if (!isBrowser) {
            return null;
        }

        const value = localStorage.getItem(key);
        if (value === null) return null;

        try {
            return JSON.parse(value) as T;
        } catch {
            return value as T;
        }
    },

    remove: (key: string) => {
        if (!isBrowser) {
            return null;
        }

        localStorage.removeItem(key);
    },
};

export default clientLocal;
