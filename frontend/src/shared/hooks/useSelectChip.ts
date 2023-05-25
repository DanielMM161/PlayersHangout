import { useState } from "react";
import { BaseModel } from "../model/BaseModel";

interface Props<T> {
    data: T[],
    maxElements: number,
    selected: T[]
}

function UseSelectChip<T extends BaseModel>({
    data,
    maxElements = 3,
    selected
}: Props<T>) {
    const [elementsFound, setEelementsFound] = useState<T[]>([])
    const [selectedItems, setSelectedItems] = useState<T[]>(selected)    

    function searchChip(seachName: string) {
        const itemsFound = data.filter(item => {
            return item.name
                .toLocaleLowerCase()
                .includes(seachName.toLocaleLowerCase().trim())
        })
        setEelementsFound(itemsFound)
    }

    function handleSelectedChip(item: T) {        
        const alreadySelected = selectedItems.includes(item)
        if (alreadySelected) {
            const newState = selectedItems.filter(instrument => instrument.id !== item.id);
            setSelectedItems(newState);          
        } else {
            selectedItems.length < maxElements ? setSelectedItems([...selectedItems, item]) : null            
        }
    }

    function handleOnClosableClick(item: T) {
        const elements = selectedItems.filter(inst => inst.id !== item.id);
        setSelectedItems(elements)
    }

    return {
        selectedItems,
        elementsFound,        
        searchChip,
        handleSelectedChip,
        handleOnClosableClick
    }
}

export default UseSelectChip