import { BaseCrudSlice } from "./BaseCrudSlice";
import { City } from '../../shared/model/City';


export const citySlice = new BaseCrudSlice<City, City, City>('CitySlice', 'cities');
export const { getAllAsync: getAllCities } = citySlice