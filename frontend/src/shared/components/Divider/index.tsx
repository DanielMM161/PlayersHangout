import './style.scss';

interface Props {
    className?: string
}

function Divider({className = 'solid'}: Props) {
    return (
        <hr className={'divider ' + className} />
    )
}

export default Divider