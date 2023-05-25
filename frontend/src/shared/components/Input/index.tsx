import './index.scss'

interface Props {
    value: string;
    label: string;
    placeHolder: string;
    name: string;
    type?: string;
    className?: string
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

function Input({
    value,
    label,
    placeHolder,
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
                onChange={(e) => onChange(e)}
                autoFocus
            />
        </div>
    )
}

export default Input;