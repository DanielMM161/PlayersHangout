import { useState } from 'react';
import PersonalInfo from '../components/PersonalInfo';
import SelectInstrument from '../components/SelectInstrument';
import SelectGenre from '../components/SelectGenre';
import { Instrument } from '../../../shared/model/Instrument';
import { Genre } from '../../../shared/model/Genre';
import './styles.scss';
import { RegisterRequest } from '../../../redux/Slice/profileSlice';
import StepperInfo from '../components/StepperInfo';
import Divider from '../../../shared/components/Divider';

export interface UserInfo {
    name: '',
    lastName: '',
    email: '',
    password: '',
    repeatPassword: '',
    city: ''
}

const steps = ['Personal Info', 'Select Instrument', 'Select Genre']

function RegisterPage() {
    
    const [actualStep, setActualStep] = useState(0)
    const [userInfo, setUserInfo] = useState<UserInfo>({
        name: '',
        lastName: '',
        email: '',
        password: '',
        repeatPassword: '',
        city: ''
    })
    const [selectedInstruments, setSelectedInstruments] = useState<Instrument[]>([])
    const [selectedGenre, setSelectedGenre] = useState<Genre[]>([])

    function handleNexStepPersonalInfo(item: UserInfo) {
        setUserInfo(item)
        setActualStep(actualStep + 1)
    }

    function handleSubmitForm() {
        const register: RegisterRequest = {
            name: userInfo.name,
            lastName: userInfo.lastName,
            email: userInfo.email,
            password: userInfo.password,
            city: userInfo.city,
            latitude: 0,
            longitude: 0,
            genres: selectedGenre,
            instruments: selectedInstruments
        }
    }

    return (
        <div className="page">
            <div className='register_container center'>
                
                <StepperInfo steps={steps} actualStep={actualStep} />

                <Divider />

                <form className='form_container'>                    
                        <h2>{steps[actualStep]}</h2>
                        {actualStep == 0 ? (
                            <PersonalInfo 
                                actualUserInfo={userInfo}
                                nextStep={(value) => handleNexStepPersonalInfo(value)} 
                            />
                        ) : null}

                        {actualStep == 1 ? (                        
                            <SelectInstrument 
                                backStep={ () => setActualStep(actualStep - 1) } 
                                nextStep={() => setActualStep(actualStep + 1)}
                                selectedInstruments={selectedInstruments}
                                setSelectedInstrument={setSelectedInstruments}
                            />
                        ) : null}

                        {actualStep == 2 ? (
                            <SelectGenre 
                                backStep={ () => setActualStep(actualStep - 1) } 
                                lastStep={() => handleSubmitForm()} 
                                selectedGenre={selectedGenre}
                                setSelectedGenre={setSelectedGenre}
                            />
                        ) : null}

                    </form>
            </div>            
        </div>
    )
}

export default RegisterPage;