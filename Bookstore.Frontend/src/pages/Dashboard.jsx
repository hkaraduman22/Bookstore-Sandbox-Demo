import { useEffect, useState } from "react";
import { useDemoReset } from "../hooks/useDemoReset";

const Dashboard = ({ user, onLogout }) => {
    // 1. State Tanımlamaları (Kategori ID varsayılan olarak eklendi)
    const [books, setBooks] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [newBook, setNewBook] = useState({ title: '', author: '', price: 0, categoryId: 1 });

    // 2. Veri Çekme Fonksiyonu
    const fetchBooks = async () => {
        try {
            const res = await fetch('http://localhost:5191/api/books', {
                headers: { 'Authorization': `Bearer ${user.token}` }
            });
            const data = await res.json();

            if (res.ok && Array.isArray(data)) {
                setBooks(data);
            } else {
                setBooks([]);
            }
        } catch (err) {
            console.error("Bağlantı hatası:", err);
            setBooks([]);
        }
    };

    // 3. Golden State Reset Hook'u
    const { restore, isRestoring } = useDemoReset(user.token, fetchBooks);

    // 4. İlk Yükleme
    useEffect(() => {
        fetchBooks();
    }, []);

    // 5. Veri Ekleme (POST) Fonksiyonu
    const handleSave = async (e) => {
        e.preventDefault();
        try {
            const res = await fetch('http://localhost:5191/api/books', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${user.token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newBook)
            });

            if (res.ok) {
                setIsModalOpen(false);
                // İşlem başarılıysa formu sıfırla (Kategori ID'yi koruyarak)
                setNewBook({ title: '', author: '', price: 0, categoryId: 1 });
                fetchBooks();
            } else {
                const errorData = await res.text();
                console.error("Backend Hatası:", errorData);
                alert("Kitap eklenemedi! Backend'den dönen hata mesajı için konsolu (F12) kontrol edin.");
            }
        } catch (err) {
            alert("İstek gönderilirken hata oluştu.");
        }
    };

    return (
        <div style={{ display: 'flex', minHeight: '100vh', background: '#f7fafc', fontFamily: 'sans-serif' }}>

            {/* SOL MENÜ (SIDEBAR) */}
            <div style={{ width: '260px', background: '#2d3748', color: 'white', padding: '30px', display: 'flex', flexDirection: 'column' }}>
                <h2 style={{ fontSize: '20px', marginBottom: '10px' }}>📚 Bookstore</h2>
                <div style={{ background: '#4a5568', padding: '10px', borderRadius: '5px', fontSize: '13px' }}>
                    <strong>Rol:</strong> {user.role}
                </div>
                <div style={{ flexGrow: 1 }}></div>
                <button onClick={onLogout} style={{ padding: '12px', background: '#e53e3e', border: 'none', color: 'white', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
                    Çıkış Yap
                </button>
            </div>

            {/* ANA İÇERİK ALANI */}
            <div style={{ flex: 1, padding: '40px' }}>

                {/* KONTROL BUTONLARI (ROLE GÖRE) */}
                <div style={{ display: 'flex', gap: '15px', marginBottom: '30px' }}>
                    {(user.role === 'SELLER' || user.role === 'ADMIN') && (
                        <button
                            onClick={() => setIsModalOpen(true)}
                            style={{ background: '#38a169', color: 'white', padding: '12px 25px', borderRadius: '8px', border: 'none', cursor: 'pointer', fontWeight: 'bold' }}
                        >
                            ➕ Yeni Kitap Ekle
                        </button>
                    )}

                    {user.role === 'ADMIN' && (
                        <button
                            onClick={restore}
                            disabled={isRestoring}
                            style={{ background: '#3182ce', color: 'white', padding: '12px 25px', borderRadius: '8px', border: 'none', cursor: 'pointer', fontWeight: 'bold' }}
                        >
                            {isRestoring ? "Sıfırlanıyor..." : "🔄 Sistemi Sıfırla (Golden State)"}
                        </button>
                    )}
                </div>

                <h1 style={{ color: '#1a202c', marginBottom: '20px' }}>Kitap Envanteri</h1>

                {/* ENVANTER TABLOSU */}
                <div style={{ background: 'white', borderRadius: '12px', boxShadow: '0 4px 6px rgba(0,0,0,0.05)', overflow: 'hidden' }}>
                    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                        <thead style={{ background: '#edf2f7' }}>
                            <tr>
                                <th style={{ padding: '15px', textAlign: 'left', color: '#4a5568' }}>Başlık</th>
                                <th style={{ padding: '15px', textAlign: 'left', color: '#4a5568' }}>Yazar</th>
                                <th style={{ padding: '15px', textAlign: 'left', color: '#4a5568' }}>Fiyat</th>
                            </tr>
                        </thead>
                        <tbody>
                            {Array.isArray(books) && books.length > 0 ? (
                                books.map(b => (
                                    <tr key={b.id} style={{ borderBottom: '1px solid #edf2f7' }}>
                                        <td style={{ padding: '15px', fontWeight: '500' }}>{b.title}</td>
                                        <td style={{ padding: '15px', color: '#718096' }}>{b.author}</td>
                                        <td style={{ padding: '15px', fontWeight: 'bold' }}>{b.price.toFixed(2)} TL</td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="3" style={{ padding: '40px', textAlign: 'center', color: '#a0aec0' }}>
                                        Görüntülenecek kitap bulunamadı.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>

            {/* KİTAP EKLEME MODALI */}
            {isModalOpen && (
                <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.6)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000 }}>
                    <div style={{ background: 'white', padding: '35px', borderRadius: '15px', width: '400px', boxShadow: '0 20px 25px rgba(0,0,0,0.2)' }}>
                        <h2 style={{ marginTop: 0, marginBottom: '20px' }}>Yeni Kitap Kaydı</h2>
                        <form onSubmit={handleSave}>
                            <div style={{ marginBottom: '15px' }}>
                                <label style={{ display: 'block', marginBottom: '5px', fontSize: '14px', fontWeight: '600' }}>Kitap Adı</label>
                                <input
                                    type="text"
                                    required
                                    style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e0' }}
                                    onChange={e => setNewBook({ ...newBook, title: e.target.value })}
                                />
                            </div>
                            <div style={{ marginBottom: '15px' }}>
                                <label style={{ display: 'block', marginBottom: '5px', fontSize: '14px', fontWeight: '600' }}>Yazar</label>
                                <input
                                    type="text"
                                    required
                                    style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e0' }}
                                    onChange={e => setNewBook({ ...newBook, author: e.target.value })}
                                />
                            </div>
                            <div style={{ marginBottom: '15px' }}>
                                <label style={{ display: 'block', marginBottom: '5px', fontSize: '14px', fontWeight: '600' }}>Fiyat (TL)</label>
                                <input
                                    type="number"
                                    step="0.01"
                                    required
                                    style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e0' }}
                                    onChange={e => setNewBook({ ...newBook, price: parseFloat(e.target.value) })}
                                />
                            </div>
                            <div style={{ marginBottom: '25px' }}>
                                <label style={{ display: 'block', marginBottom: '5px', fontSize: '14px', fontWeight: '600' }}>Kategori ID</label>
                                <input
                                    type="number"
                                    required
                                    defaultValue={1}
                                    style={{ width: '100%', padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e0' }}
                                    onChange={e => setNewBook({ ...newBook, categoryId: parseInt(e.target.value) })}
                                />
                                <small style={{ color: '#718096' }}>Geçerli bir kategori ID girin (Örn: 1 veya 2).</small>
                            </div>

                            <div style={{ display: 'flex', gap: '10px' }}>
                                <button
                                    type="button"
                                    onClick={() => setIsModalOpen(false)}
                                    style={{ flex: 1, padding: '12px', background: '#edf2f7', border: 'none', borderRadius: '6px', cursor: 'pointer' }}
                                >
                                    İptal
                                </button>
                                <button
                                    type="submit"
                                    style={{ flex: 2, padding: '12px', background: '#38a169', color: 'white', border: 'none', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}
                                >
                                    Sisteme Ekle
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Dashboard;