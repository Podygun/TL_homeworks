import { AUTH_KEY } from '../utils/configuration.js';

const isAuthenticated = () => Boolean(localStorage.getItem(AUTH_KEY));

const clearUser = () => localStorage.removeItem(AUTH_KEY);

const getUsername = () => isAuthenticated() ? localStorage.getItem(AUTH_KEY) : undefined;

const setUsername = (username) => localStorage.setItem(AUTH_KEY, username);

export const AuthHandler = { getUsername, setUsername, clearUser, isAuthenticated };