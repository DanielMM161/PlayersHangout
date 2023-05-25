import { useEffect, useState } from 'react'
import PersonalInfo from './components/PersonalInfo'
import SelectInstrument from './components/SelectInstrument'
import SelectGenre from './components/SelectGenre'
import './styles.scss'

export interface Form {
    name: '',
    lastName: '',
    email: '',
    password: '',
    repeatPassword: '',
    city: ''
}

const steps = ['Personal Info', 'Select Instrument', 'Select Genre']

function Register() {
    const [disableButton, setDisableButton] = useState(true)
    const [actualStep, setActualStep] = useState(0)
    const [form, setForm] = useState<Form>({
        name: '',
        lastName: '',
        email: '',
        password: '',
        repeatPassword: '',
        city: ''
    })

    useEffect(() => {
        checkFields()
    }, [form])
    

    function checkFields() {
        setDisableButton(true)
        const noEmptyFields = Object.values(form).every((item) => item.trim() !== "")
        if (noEmptyFields) setDisableButton(false)
    }


    return (
        <div className="page">
            <div className='register_container center'>
                <div className="steps_container">                    
                    {steps.map((item, index) => {
                        return (
                            <div className={'step_element ' + (actualStep == index ? 'active' : '')}>
                                {index + 1}
                            </div>
                        )
                    })}
                </div>
                <div className='form_container'>
                    <form>                    
                        <h2>{steps[actualStep]}</h2>
                        {actualStep == 0 ? (
                            <PersonalInfo nextStep={() => setActualStep(actualStep + 1)} />
                        ) : null}

                        {actualStep == 1 ? (                        
                            <SelectInstrument 
                                backStep={ () => setActualStep(actualStep - 1) } 
                                nextStep={() => setActualStep(actualStep + 1)} 
                            />
                        ) : null}

                        {actualStep == 2 ? (
                            <SelectGenre 
                                backStep={ () => setActualStep(actualStep - 1) } 
                                nextStep={() => setActualStep(actualStep + 1)} 
                            />
                        ) : null}

                    </form>
                </div>
            </div>
            
        </div>
    )
}

export default Register;