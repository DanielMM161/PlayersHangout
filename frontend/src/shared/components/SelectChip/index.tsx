import Chip from "../Chip";
import DelayInput from "../DelayInput";
import { BaseModel } from "../../model/BaseModel";
import UseSelectChip from "../../hooks/useSelectChip";
import './styles.scss';
import { useEffect } from "react";
import Loader from "../Loader";

interface Props<T> {
    data: T[];
    selected: T[];
    messageMainItem?: string;
    title: string;
    maxElements: number;
    fetching: boolean;
    onAction: (items: T[]) => void;
    children?: React.ReactNode;
}

function SelectChip<T extends BaseModel>({
    data,
    selected,
    messageMainItem = '',
    title,
    maxElements,
    fetching,
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
                <>
                    <div className="selected_items_container">
                        <ul>
                            {selectedItems.map((item, idx) => 
                                <Chip
                                    key={item.id+item.name}
                                    className={idx === 0 ? 'main' : ''}
                                    title={item.name}
                                    selected={true}
                                    closable={true}   
                                    onClosableClick={() => handleOnClosableClick(item)}                                         
                                />    
                            )}
                        </ul>
                    </div>
                    <div className="message_main_item">
                        {/* {INSERT IMAGE} */}
                        <span>{messageMainItem}</span>
                    </div>
                </>
            ) : null}

            {fetching ? (
                 <div className="chips_container" style={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
                    <Loader />
                </div>                
            ) : (
                <div className="chips_container">
                    <ul>
                        {showItems()}
                    </ul>
                </div>
            )}            

            {children}                                  
        </div>
    )
}

export default SelectChip