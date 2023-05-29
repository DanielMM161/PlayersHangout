import { useEffect, useState } from "react"
import { UserInfo } from "../containers/RegisterPage"
import { useAppDispatch, useAppSelector } from "../../../shared/hooks/redux";
import { getAllCities } from "../../../redux/Slice/citySlice";

interface Props {
    actualUserInfo: UserInfo
}

function UsePersonalInfo({ actualUserInfo }: Props) {
    const dispatch = useAppDispatch();
    const cityState = useAppSelector((state) => state.cities);
    const {data: cities} = cityState;
    const [disableButton, setDisableButton] = useState(true)
    const [userForm, setUserForm] = useState<UserInfo>(actualUserInfo)

    useEffect(() => {
        checkFields()
    }, [userForm])

    useEffect(() => {
        if(!cities.length) dispatch(getAllCities({}))
    }, [cities])

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value} = event.target
        setUserForm(() => ({
            ...userForm,
            [name]: value
        }))
    }

    function checkFields() {        
        setDisableButton(true)
        const noEmptyFields = Object.values(userForm).every((item) => item.trim() !== "")
        if (noEmptyFields) setDisableButton(false)        
    }

    return {
        disableButton,
        userForm,
        cities,
        setUserForm,
        handleInputChange
    }
}

export default UsePersonalInfo