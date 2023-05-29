import React, { useState } from 'react';
import useOutsideClick from '../../hooks/useOutsideClick';
import './style.scss';

interface AutocompleteInputProps {
  options: string[];
  label: string;
  onValueChange: (value: string) => void;
}

function AutoCompleteInput({ 
  options,
  label,
  onValueChange
}: AutocompleteInputProps) {
  const [inputValue, setInputValue] = useState('');
  const [matchedOptions, setMatchedOptions] = useState<string[]>([]);
  const ref = useOutsideClick<HTMLUListElement>(() => setMatchedOptions([]));

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    setInputValue(value);
    
    const filteredOptions = options.filter((option) =>
      option.toLowerCase().includes(value.toLowerCase().trim())
    );

    setMatchedOptions(filteredOptions);
    onValueChange(value);
  };

  const handleOptionClick = (option: string) => {
    setInputValue(option);
    setMatchedOptions([]);
    onValueChange(option);
  };

  return (
    <div className="autocomplete">
      <label>{label}</label>
      <input
        type="text"        
        value={inputValue}
        onChange={handleInputChange}
        placeholder="Write Here..."
        className=""
      />
      {matchedOptions.length > 0 && (
        <ul className="autocomplete__options" ref={ref}>
          {matchedOptions.map((option) => (
            <li
              key={option}
              onClick={() => handleOptionClick(option)}
              className="autocomplete__option"
            >
              {option}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default AutoCompleteInput;
