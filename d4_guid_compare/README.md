# Day 4 - GUID dosyasi karsilastirma

## Mentorun istedigi

1. Konsoldan kac GUID uretilecegini sor.
2. `Guid.NewGuid()` ile GUID'leri uret.
3. Her GUID'i `tasks.txt` dosyasinda ayri bir satira yaz.
4. Dosyayi baska bir dizine kopyala.
5. Kopyalanan dosyaya kullanicinin istedigi kadar yeni GUID ekle.
6. Ilk dosya ile kopyalanan dosyayi karsilastir.
7. Sonradan eklenen GUID'leri bul ve islem suresini `Stopwatch` ile goster.

## Neden HashSet?

Ilk dosyadaki GUID'ler `HashSet<string>` icine alinir. Kopyadaki her GUID'in
ilk dosyada bulunup bulunmadigi `Contains` ile kontrol edilir. `HashSet`
aramasi ortalama O(1), tum karsilastirma ise yaklasik O(n + m) karmasikligindadir.

Iki listeyi ic ice dongulerle karsilastirmak O(n * m) olur ve 100.000 gibi
buyuk sayilarda cok daha yavas calisir.

## Calistirma

```powershell
cd kocsistem\d4_guid_compare
dotnet run
```

Dizin sorularinda Enter'a basarsan proje calisma dizininde otomatik olarak
`source` ve `destination` klasorleri olusturulur.
