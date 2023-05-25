import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Login from './pages/Login'
import RegisterPage from './features/register/containers/RegisterPage'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
