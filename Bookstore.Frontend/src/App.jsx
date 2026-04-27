import { useState, useEffect } from 'react';
import LoginPage from './pages/LoginPage.jsx';
import Dashboard from './pages/Dashboard.jsx';

function App() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem('token');
        const role = localStorage.getItem('role');
        if (token && role) setUser({ token, role });
    }, []);

    const logout = () => {
        localStorage.clear();
        setUser(null);
    };

    return (
        <div>
            {!user ? (
                <LoginPage onLoginSuccess={(data) => setUser(data)} />
            ) : (
                <Dashboard user={user} onLogout={logout} />
            )}
        </div>
    );
}

export default App;