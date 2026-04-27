import { useState } from 'react';

const LoginPage = ({ onLoginSuccess }) => {
    const [credentials, setCredentials] = useState({ username: '', password: '' });

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await fetch('http://localhost:5191/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(credentials)
            });

            if (res.ok) {
                const data = await res.json();
                localStorage.setItem('token', data.token);
                localStorage.setItem('role', data.role);
                onLoginSuccess(data);
            } else {
                alert('Giriş başarısız! Bilgileri kontrol edin.');
            }
        } catch (err) {
            alert('Sunucuya bağlanılamadı.');
        }
    };

    return (
        <div style={{ height: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center', background: '#f0f2f5', fontFamily: 'sans-serif' }}>
            <form onSubmit={handleSubmit} style={{ background: 'white', padding: '40px', borderRadius: '12px', boxShadow: '0 8px 24px rgba(0,0,0,0.1)', width: '350px' }}>
                <h2 style={{ textAlign: 'center', color: '#1a202c', marginBottom: '30px' }}>Bookstore Giriş</h2>
                <input
                    type="text"
                    placeholder="Kullanıcı Adı"
                    onChange={e => setCredentials({ ...credentials, username: e.target.value })}
                    style={{ width: '100%', padding: '12px', marginBottom: '15px', border: '1px solid #ddd', borderRadius: '6px', boxSizing: 'border-box' }}
                    required
                />
                <input
                    type="password"
                    placeholder="Şifre"
                    onChange={e => setCredentials({ ...credentials, password: e.target.value })}
                    style={{ width: '100%', padding: '12px', marginBottom: '25px', border: '1px solid #ddd', borderRadius: '6px', boxSizing: 'border-box' }}
                    required
                />
                <button type="submit" style={{ width: '100%', padding: '12px', background: '#3182ce', color: 'white', border: 'none', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
                    Giriş Yap
                </button>
            </form>
        </div>
    );
};

export default LoginPage;