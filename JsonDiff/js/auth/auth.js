import { AUTH_KEY } from '../utils/configuration.js';

const isAuthenticated = () => {
    const user = localStorage.getItem(AUTH_KEY)

    if(Boolean(user)){
        return true
    }
    return false;
}

const clearUser = () => {
    localStorage.removeItem(AUTH_KEY);
}

const getUsername = () => {
    const username = localStorage.getItem(AUTH_KEY);

    if (isAuthenticated()){
        return username;
    }
    return undefined;
}

const setUsername = (username) => {
    localStorage.setItem(AUTH_KEY, username);
}

export const AuthHandler = { getUsername, setUsername, clearUser, isAuthenticated };
