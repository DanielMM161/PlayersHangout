import './index.scss'

interface Props {
    value: string;
    label: string;
    placeHolder: string;
    required?: boolean;
    name: string;
    type?: string;
    className?: string
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

function Input({
    value,
    label,
    placeHolder,
    required = false,
    name,
    type = 'text',
    className = '',
    onChange
}: Props) {

    return (
        <div className='input_container'>
            <label>{label}</label>
            <input
                name={name}                
                placeholder={placeHolder}
                className={className}
                value={value}      
                type={type}
                required={required}
                
                onChange={(e) => onChange(e)}
                autoFocus
            />
        </div>
    )
}

export default Input;