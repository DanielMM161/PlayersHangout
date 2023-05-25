import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../../../shared/hooks/redux"
import ButtonsStepper from "../ButtonsStepper"
import { getAllInstruments } from "../../../../redux/Slice/instrumentSlice";
import { Instrument } from "../../../../shared/model/Instrument";
import SelectChip from "../../../../shared/components/SelectChip";
import './style.scss';

interface Props {
    backStep: () => void;
    nextStep: () => void;
    setSelectedInstrument: React.Dispatch<React.SetStateAction<Instrument[]>>
    selectedInstruments: Instrument[]
}

function SelectInstrument({
    backStep,
    nextStep,    
    setSelectedInstrument,
    selectedInstruments
}: Props) {
    const [disableButton, setDisableButton] = useState(true)
    const dispatch = useAppDispatch()
    const instrumentState = useAppSelector((state) => state.instruments);
    const {data: instruments} = instrumentState
    

    useEffect(() => {        
        if (!instruments.length) dispatch(getAllInstruments({}))
    }, [instruments])

    function handleOnAction(items: Instrument[]) {
        setSelectedInstrument(items)
        items.length > 0 ? setDisableButton(false) : setDisableButton(true)
    }

    return (
       <>
            <SelectChip<Instrument>
                key={instruments.length}
                title="Select Maxium 5 instruments, if you don't see what you're looking for try searching"
                data={instruments}
                selected={selectedInstruments}
                maxElements={5}
                onAction={(value) => handleOnAction(value)}
            >
                <ButtonsStepper
                    disabledContinue={disableButton}
                    onBackClick={backStep}
                    onContinueClick={nextStep}                
                />                  
            </SelectChip>
       </>
    )
}

export default SelectInstrument