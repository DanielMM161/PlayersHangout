import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../../../shared/hooks/redux";
import { Genre } from '../../../../shared/model/Genre';
import { getAllGenre } from "../../../../redux/Slice/genreSlice";
import SelectChip from "../../../../shared/components/SelectChip";
import ButtonsStepper from "../ButtonsStepper";


interface Props {
    backStep: () => void
    lastStep: () => void
    setSelectedGenre: React.Dispatch<React.SetStateAction<Genre[]>>
    selectedGenre: Genre[]
}

function SelectGenre({
    backStep,
    lastStep,
    setSelectedGenre,
    selectedGenre
}: Props) {
    const [disableButton, setDisableButton] = useState(true)
    const dispatch = useAppDispatch()
    const genresState = useAppSelector((state) => state.genres);
    const {data: genres} = genresState

    useEffect(() => {        
        if (!genres.length) dispatch(getAllGenre({}))
    }, [genres])

    function handleOnAction(items: Genre[]) {
        setSelectedGenre(items)
        items.length > 0 ? setDisableButton(false) : setDisableButton(true)
    }

    return (
       <>
            <SelectChip<Genre>
                key={genres.length}
                title="Select Maxium 3 Genres, if you don't see what you're looking for try searching"
                data={genres}
                selected={selectedGenre}
                messageMainItem="The first element would be the genre that most like you"
                maxElements={3}
                onAction={(value) => handleOnAction(value)}
            >
                  <ButtonsStepper
                    disabledContinue={disableButton}
                    lastStep={true}
                    onBackClick={backStep}
                    onContinueClick={lastStep}                
                    />                 
            </SelectChip>
       </>
    )
}

export default SelectGenre