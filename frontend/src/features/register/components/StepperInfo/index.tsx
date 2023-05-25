import Divider from '../../../../shared/components/Divider'
import './style.scss'

interface Props {
    steps: string[]
    actualStep: number

}

function StepperInfo({
    steps,
    actualStep
}: Props) {
    return (
        <div className="steps_container">                    
            {steps.map((item, index) => {
                return (
                    <div className='step_item'>
                        <div className={'step_element ' + (actualStep == index ? 'active' : '')}>
                            {index + 1}
                        </div>
                        {item}
                    </div>                    
                )
            })}            
        </div>
    )
}

export default StepperInfo