import Chip from "../Chip";
import DelayInput from "../DelayInput";
import { BaseModel } from "../../model/BaseModel";
import UseSelectChip from "../../hooks/useSelectChip";
import './styles.scss';
import { useEffect } from "react";

interface Props<T> {
    data: T[];
    selected: T[];
    title: string;
    maxElements: number;
    onAction: (items: T[]) => void;
    children?: React.ReactNode;
}

function SelectChip<T extends BaseModel>({
    data,
    selected,
    title,
    maxElements,
    onAction,
    children
}: Props<T>) {    
    const {
        selectedItems,
        elementsFound,        
        handleSelectedChip,
        handleOnClosableClick,
        searchChip
    } = UseSelectChip<T>({data, maxElements, selected})

    useEffect(() => {
        onAction(selectedItems)
    }, [selectedItems])

    function showItems() {
        if(elementsFound.length) {
            return elementsFound.map(item => 
                <Chip
                    key={item.id+item.name}
                    title={item.name}
                    selected={selectedItems.includes(item)}
                    onClickChip={() => handleSelectedChip(item)}                                
                />    
            )
        }        

        return data.map(item => 
            <Chip
                key={item.id+item.name}
                title={item.name}
                selected={selectedItems.includes(item)}
                onClickChip={() => handleSelectedChip(item)}                   
            />    
        )
    }

    return (
        <div className='card'>
            <h4>{title}</h4>
            <DelayInput onUpdate={(text) => searchChip(text)}/>
            {selectedItems.length ? (
                <div>
                    <ul>
                        {selectedItems.map(item => 
                            <Chip
                                key={item.id+item.name}
                                title={item.name}
                                selected={true}
                                closable={true}   
                                onClosableClick={() => handleOnClosableClick(item)}                                         
                            />    
                        )}
                    </ul>
                </div>
            ) : null}

            <div className="chips_container">
                <ul>
                    {showItems()}
                </ul>
            </div>

            {children}                                  
        </div>
    )
}

export default SelectChip