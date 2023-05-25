import { useEffect, useState } from "react";
import Input from "../Input"

interface Props {
    onUpdate: (value: string) => void;    
}

function DelayInput({
    onUpdate,    
}: Props) {
    const [value, setValue] = useState('');

    useEffect(() => {
        const debounceOnUpdate = setTimeout(() => {
          onUpdate(value);
        }, 300);
    
        return () => clearTimeout(debounceOnUpdate);
    }, [value, onUpdate]);

    return (
        <Input 
            value={value}
            onChange={(text) => setValue(text)}
        />
    )
}

export default DelayInput