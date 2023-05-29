import ButtonsStepper from '../ButtonsStepper';
import UsePersonalInfo from '../../hooks/usePersonalInfo';
import { UserInfo } from '../../containers/RegisterPage';
import Input from '../../../../shared/components/Input';
import AutoCompleteInput from '../../../../shared/components/AutoCompleteInput';
import './style.scss';

interface Props {
    actualUserInfo: UserInfo;
    nextStep: (userInfo: UserInfo) => void;
}

function PersonalInfo({
    actualUserInfo,
    nextStep         
}: Props) {
    const {
        userForm,
        disableButton,
        cities,
        handleInputChange,
        setUserForm
    } = UsePersonalInfo({actualUserInfo})

    return (
        <div className='card'>
            <div>
                <Input 
                    name='name'
                    label='Name'
                    placeHolder='Name'
                    className='input_personal_info'
                    required={true}
                    value={userForm.name} 
                    onChange={(e) => handleInputChange(e)} 
                />

                <Input 
                    name='lastName'
                    label='Last Name'
                    placeHolder='Last Name' 
                    className='input_personal_info'
                    required={true}
                    value={userForm.lastName} 
                    onChange={(e) => handleInputChange(e)} 
                />
            </div>

            <div>
                <Input 
                    name='email'
                    label='Email'
                    placeHolder='Last Name' 
                    className='input_personal_info'
                    required={true}
                    value={userForm.email} 
                    onChange={(e) => handleInputChange(e)} 
                />

                <Input 
                    name='password'
                    label='Password'
                    placeHolder='Password' 
                    className='input_personal_info'
                    type='password'
                    required={true}
                    value={userForm.password} 
                    onChange={(e) => handleInputChange(e)} 
                />
            </div>

            <AutoCompleteInput                
                options={cities.map(item => item.name)}
                label='City'
                onValueChange={(value) => setUserForm(prevState => ({...prevState, city: value}))}
            />

            <ButtonsStepper
                showGoBack={false}
                disabledContinue={disableButton}
                onBackClick={() => {}}
                onContinueClick={() => nextStep(userForm)}                
            />
        </div>
    )
}

export default PersonalInfo