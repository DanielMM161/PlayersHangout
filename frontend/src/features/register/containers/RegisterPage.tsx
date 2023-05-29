import { useEffect, useState } from 'react';
import PersonalInfo from '../components/PersonalInfo';
import SelectInstrument from '../components/SelectInstrument';
import SelectGenre from '../components/SelectGenre';
import { Instrument } from '../../../shared/model/Instrument';
import { Genre } from '../../../shared/model/Genre';
import { RegisterRequest, register } from '../../../redux/Slice/profileSlice';
import StepperInfo from '../components/StepperInfo';
import Divider from '../../../shared/components/Divider';
import './styles.scss';
import { useAppDispatch } from '../../../shared/hooks/redux';

export interface UserInfo {
    name: string,
    lastName: string,
    email: string,
    password: string,    
    city: string
}

const steps = ['Personal Info', 'Select Instrument', 'Select Genre']

function RegisterPage() {
    const dispatch = useAppDispatch();
    const [actualStep, setActualStep] = useState(0)
    const [userInfo, setUserInfo] = useState<UserInfo>({
        name: '',
        lastName: '',
        email: '',
        password: '',        
        city: ''
    })
    const [selectedInstruments, setSelectedInstruments] = useState<Instrument[]>([])
    const [selectedGenre, setSelectedGenre] = useState<Genre[]>([])
    
    function handleNexStepPersonalInfo(item: UserInfo) {
        setUserInfo(item)
        setActualStep(actualStep + 1)
    }

    function handleSubmitForm() {
        const registerRequest: RegisterRequest = {
            name: userInfo.name,
            lastName: userInfo.lastName,
            email: userInfo.email,
            password: userInfo.password,
            city: userInfo.city,
            latitude: 0,
            longitude: 0,
            genres: null,
            instruments: selectedInstruments.map(item => '8d5e9897-b5c9-43a0-9c23-820c73370dfc')
        }
        dispatch(register(registerRequest))
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
                                setSelectedInstrument={(item) => {
                                    console.log("en set selected instrument ---> ", item)
                                    setSelectedInstruments(item)
                                }}
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