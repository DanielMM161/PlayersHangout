import './style.scss';

interface Props {
    title: string;
    selected?: boolean;
    closable?: boolean;
    onClickChip?: () => void; 
    onClosableClick?: () => void;
}

function Chip({
    title,
    selected = false,
    closable = false,
    onClickChip,
    onClosableClick
}: Props) {

    return (
        <div className={'chip '+ (selected ? 'selected' : '')} onClick={onClickChip}>
            {title}
            {closable ? (
                <span className="closebtn" onClick={onClosableClick}>&times;</span>
            ) : null}            
        </div>
    )
}

export default Chip