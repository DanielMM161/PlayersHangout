import { Slice, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { Profile, ProfileState } from '../../shared/model/Profile';
import { Instrument } from '../../shared/model/Instrument';

export interface RegisterRequest {
    name: string;
    lastName: string;
    email: string;
    password: string;
    cityId?: string;
    city?: string,
    latitude: 0,
    longitude: 0,
    genres?: string[] | null,
    instruments?: string[] | null,
}

export interface LoginRequest {
    email: string;
    password: string
}

class ProfileSlice {
    constructor() {
        this.baseUrl = import.meta.env.VITE_BASE_URL ?? "";
        this.slice = createSlice({
            name: 'profile',
            initialState: this.initialState,
            reducers: {
            },
            extraReducers: (build) => {
                build.addCase(this.register.fulfilled, (_, action) => {  
                    const { payload } = action;
                    return { data: payload, fetching: false }          
                })
                build.addCase(this.login.fulfilled, (_, action) => {  
                    const { payload } = action;
                    return { data: payload, fetching: false }          
                })
                build.addCase(this.logout.fulfilled, (_, action) => {  
                    const { payload } = action;
                    if (payload) {
                    return { data: undefined, fetching: false }          
                    }
                })
            },
        });

        this.register = createAsyncThunk<Profile | undefined, RegisterRequest, { rejectValue: AxiosError }>(
            'Register',
            async (item, thunkAPI) => {
                try {
                    const response = await axios.post<any, AxiosResponse<Profile>, RegisterRequest>(`${this.baseUrl}/Auth/signup`, item);
                    localStorage.setItem('token', JSON.stringify(response.data.token));
                    console.log("response ---> ", response.data)
                    return thunkAPI.fulfillWithValue(response.data);
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im update... sorry!");   
                }
            }
        );

        this.login = createAsyncThunk<Profile | undefined, LoginRequest, { rejectValue: AxiosError }>(
            'Register',
            async (item, thunkAPI) => {
                try {
                    const response = await axios.post<any, AxiosResponse<Profile>, LoginRequest>('register', item);
                    localStorage.setItem('token', JSON.stringify(response.data.token));
                    return thunkAPI.fulfillWithValue(response.data);
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im update... sorry!");   
                }
            }
        );

        this.logout = createAsyncThunk<boolean | undefined, void, { rejectValue: AxiosError }>(
            'Register',
            async (_, thunkAPI) => {
                try {
                    const response = await axios.post<any, AxiosResponse<boolean>, void>('register');
                    localStorage.removeItem('token');
                    return thunkAPI.fulfillWithValue(response.data);
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im update... sorry!");   
                }
            }
        );
    }

    slice: Slice<ProfileState>;
    private initialState: ProfileState = { data: undefined, fetching: false }  ;
    baseUrl: string;
    register: ReturnType<typeof createAsyncThunk<Profile | undefined, RegisterRequest, { rejectValue: AxiosError }>>;
    login: ReturnType<typeof createAsyncThunk<Profile | undefined, LoginRequest, { rejectValue: AxiosError }>>;
    logout: ReturnType<typeof createAsyncThunk<boolean | undefined, void, { rejectValue: AxiosError }>>; 
}

export const profileSlice = new ProfileSlice();
export const { login, logout, register } = profileSlice


