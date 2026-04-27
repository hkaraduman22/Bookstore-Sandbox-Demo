import { useState } from 'react';

export const useDemoReset = (token, onSuccess) => {
    const [isRestoring, setIsRestoring] = useState(false);

    const restore = async () => {
        if (!window.confirm("Sistemi ilk Altın Durumuna (Golden State) döndürmek istediğinize emin misiniz?")) return;

        setIsRestoring(true);
        try {
            const res = await fetch('http://localhost:5191/api/system/restore', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (res.ok) {
                alert("Sistem başarıyla sıfırlandı!");
                if (onSuccess) onSuccess();
            } else {
                alert("Sıfırlama yetkiniz yok veya bir hata oluştu.");
            }
        } catch (error) {
            alert("Bağlantı hatası: Sunucu çalışıyor mu?");
        } finally {
            setIsRestoring(false);
        }
    };

    return { restore, isRestoring };
};