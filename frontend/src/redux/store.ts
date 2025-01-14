import { Action, ThunkAction, configureStore } from "@reduxjs/toolkit";
import { genreSlice } from "./Slice/genreSlice";
import { instrumentSlice } from "./Slice/instrumentSlice";
import { profileSlice } from "./Slice/profileSlice";
import { citySlice } from "./Slice/citySlice";

export const store = configureStore({
    reducer: {       
        genres: genreSlice.slice.reducer,
        instruments: instrumentSlice.slice.reducer,
        cities: citySlice.slice.reducer
    }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
