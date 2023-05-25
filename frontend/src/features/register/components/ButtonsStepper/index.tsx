
interface Props {    
    onBackClick: () => void;
    onContinueClick: () => void;
    disabledContinue: boolean;  
    lastStep?: boolean  
}

function ButtonsStepper({    
    onBackClick,
    onContinueClick,
    disabledContinue,
    lastStep = false
}: Props) {
    
    return (
        <div className="buttons_container">
            <button 
                onClick={() => onBackClick()}
            >
                Go Back
            </button>

            {lastStep ? (
                <button
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