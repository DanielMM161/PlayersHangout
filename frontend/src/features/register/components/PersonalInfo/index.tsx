import ButtonsStepper from '../ButtonsStepper';
import UsePersonalInfo from '../../hooks/usePersonalInfo';
import { UserInfo } from '../../containers/RegisterPage';
import './style.scss';
import Input from '../../../../shared/components/Input';

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
        handleInputChange,
    } = UsePersonalInfo({actualUserInfo})

    return (
        <div className='card'>
            <div>
                <Input 
                    name='name'
                    label='Name'
                    placeHolder='Name' 
                    className='input_personal_info'
                    value={userForm.name} 
                    onChange={(e) => handleInputChange(e)} 
                />

                <Input 
                    name='lastName'
                    label='Last Name'
                    placeHolder='Last Name' 
                    className='input_personal_info'
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
                    value={userForm.email} 
                    onChange={(e) => handleInputChange(e)} 
                />

                <Input 
                    name='password'
                    label='Password'
                    placeHolder='Password' 
                    className='input_personal_info'
                    type='password'
                    value={userForm.password} 
                    onChange={(e) => handleInputChange(e)} 
                />
            </div>


            <Input 
                name='city'
                label='City'
                placeHolder='City' 
                className='input_personal_info'
                value={userForm.city} 
                onChange={(e) => handleInputChange(e)} 
            />

            <ButtonsStepper
                disabledContinue={disableButton}
                onBackClick={() => {}}
                onContinueClick={() => nextStep(userForm)}                
            />
        </div>
    )
}

export default PersonalInfo