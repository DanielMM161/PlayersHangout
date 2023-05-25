import { Instrument } from "../../shared/model/Instrument";
import { BaseCrudSlice, SliceState } from "./BaseCrudSlice";


export const instrumentSlice = new BaseCrudSlice<Instrument, Instrument, Instrument>('InstrumentSlice', 'Instruments');
export const { getAllAsync: getAllInstruments } = instrumentSlice

