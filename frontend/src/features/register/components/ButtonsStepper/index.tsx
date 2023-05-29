import './style.scss'

interface Props {    
    onBackClick: () => void;
    onContinueClick: () => void;
    showGoBack?: boolean; 
    disabledContinue: boolean;  
    lastStep?: boolean  
}

function ButtonsStepper({    
    onBackClick,
    onContinueClick,
    showGoBack = true,
    disabledContinue,
    lastStep = false
}: Props) {
    
    return (
        <div className="buttons_container">
           {showGoBack ? (
                <button onClick={() => onBackClick()} >Go Back</button>
            ) : null}

            {lastStep ? (
                <button
                    className='continue_button'
                    disabled={disabledContinue}
                    onClick={(e) => {
                        e.preventDefault() 
                        onContinueClick()
                    }}
                >
                    Finish
                </button>
            ) : (
                <button
                    className='continue_button'
                    disabled={disabledContinue}
                    onClick={(e) => {
                        e.preventDefault() 
                        onContinueClick()
                    }}
                >
                    Continue
                </button>
            )}           
        </div>  
    )
}

export default ButtonsStepper