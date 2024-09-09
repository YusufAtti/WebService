Imports System.Web.Services
Imports System.Data.SqlClient

<System.Web.Services.WebService([Namespace]:="http://microsoft.com/webservices/")>
Public Class service
    Inherits System.Web.Services.WebService



    ' Personal Fonksiyonu - Veritabanından veri çeker
    <WebMethod>
    Public Function Personal(adi As String, soyadi As String) As DataSet
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' SQL komutunu doğru şekilde hazırlıyoruz
        Dim Komut As New SqlCommand("SELECT * FROM dbo.Table_1 WHERE Name LIKE @adi AND Surname LIKE @soyadi", baglanti)

        ' Parametre ekleme, SQL Injection'a karşı güvenlik
        Komut.Parameters.AddWithValue("@adi", "%" & adi & "%")
        Komut.Parameters.AddWithValue("@soyadi", "%" & soyadi & "%")

        ' Verileri almak için SqlDataAdapter kullanımı
        Dim adap As New SqlDataAdapter(Komut)
        Dim ds As New DataSet

        ' Bağlantıyı aç ve veriyi doldur
        baglanti.Open()
        adap.Fill(ds)
        baglanti.Close()

        Return ds
    End Function

    ' Veritabanına yeni bir kayıt eklemek için kullanılan fonksiyon
    <WebMethod>
    Public Function VeriEkle(ad As String, soyad As String) As String
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' SQL komutunu hazırlıyoruz (INSERT INTO)
        Dim Komut As New SqlCommand("INSERT INTO dbo.Table_1 (Name, Surname) VALUES (@ad, @soyad)", baglanti)

        ' Parametreleri SQL sorgusuna ekliyoruz
        Komut.Parameters.AddWithValue("@ad", ad)
        Komut.Parameters.AddWithValue("@soyad", soyad)

        ' Bağlantıyı aç, sorguyu çalıştır ve bağlantıyı kapat
        Try
            baglanti.Open()
            Komut.ExecuteNonQuery() ' Veri eklemek için ExecuteNonQuery kullanılır
            baglanti.Close()
            Return "Kayıt başarıyla eklendi!"
        Catch ex As Exception
            Return "Hata: " & ex.Message
        End Try
    End Function

    ' TumKayitlar Fonksiyonu - Veritabanındaki tüm verileri getirir
    <WebMethod>
    Public Function TumKayitlar() As DataSet
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' Tüm kayıtları seçmek için SQL komutunu oluşturuyoruz
        Dim Komut As New SqlCommand("SELECT * FROM dbo.Table_1", baglanti)

        ' Verileri almak için SqlDataAdapter kullanımı
        Dim adap As New SqlDataAdapter(Komut)
        Dim ds As New DataSet

        ' Bağlantıyı aç ve veriyi doldur
        baglanti.Open()
        adap.Fill(ds)
        baglanti.Close()

        Return ds
    End Function

    ' Veritabanından ID'ye göre kayıt silme fonksiyonu
    <WebMethod>
    Public Function KayitSilID(ID As Integer) As String
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' SQL DELETE komutunu oluşturuyoruz
        Dim Komut As New SqlCommand("DELETE FROM dbo.Table_1 WHERE ID = @ID", baglanti)

        ' Parametreleri SQL sorgusuna ekliyoruz
        Komut.Parameters.AddWithValue("@ID", ID)

        ' Bağlantıyı aç, sorguyu çalıştır ve bağlantıyı kapat
        Try
            baglanti.Open()
            Dim rowsAffected As Integer = Komut.ExecuteNonQuery() ' Silme işlemi için ExecuteNonQuery kullanılır
            baglanti.Close()

            If rowsAffected > 0 Then
                Return "Kayıt başarıyla silindi!"
            Else
                Return "Kayıt bulunamadı!"
            End If
        Catch ex As Exception
            Return "Hata: " & ex.Message
        End Try
    End Function

    ' Hem ID hem de Name ve Surname ile kayıt silme fonksiyonu
    <WebMethod>
    Public Function KayitSilIDAdSoyad(ID As Integer, ad As String, soyad As String) As String
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' SQL DELETE komutunu oluşturuyoruz
        Dim Komut As New SqlCommand("DELETE FROM dbo.Table_1 WHERE ID = @ID AND Name = @ad AND Surname = @soyad", baglanti)

        ' Parametreleri SQL sorgusuna ekliyoruz
        Komut.Parameters.AddWithValue("@ID", ID)
        Komut.Parameters.AddWithValue("@ad", ad)
        Komut.Parameters.AddWithValue("@soyad", soyad)

        ' Bağlantıyı aç, sorguyu çalıştır ve bağlantıyı kapat
        Try
            baglanti.Open()
            Dim rowsAffected As Integer = Komut.ExecuteNonQuery() ' Silme işlemi için ExecuteNonQuery kullanılır
            baglanti.Close()

            If rowsAffected > 0 Then
                Return "Kayıt başarıyla silindi!"
            Else
                Return "Kayıt bulunamadı!"
            End If
        Catch ex As Exception
            Return "Hata: " & ex.Message
        End Try
    End Function

    ' Kayıt güncelleme fonksiyonu
    <WebMethod>
    Public Function KayitGuncelle(ID As Integer, yeniAd As String, yeniSoyad As String) As String
        ' Veritabanı bağlantısı için gerekli bağlantı dizesi
        Dim baglanti As New SqlConnection("Data Source=10.6.40.162\ZAFER-MSI3,1433;Initial Catalog=YUSUF;User ID=sa;Password=D+123;Trusted_Connection=False")

        ' SQL UPDATE komutunu oluşturuyoruz
        Dim Komut As New SqlCommand("UPDATE dbo.Table_1 SET Name = @yeniAd, Surname = @yeniSoyad WHERE ID = @ID", baglanti)

        ' Parametreleri SQL sorgusuna ekliyoruz
        Komut.Parameters.AddWithValue("@ID", ID)
        Komut.Parameters.AddWithValue("@yeniAd", yeniAd)
        Komut.Parameters.AddWithValue("@yeniSoyad", yeniSoyad)

        ' Bağlantıyı aç, sorguyu çalıştır ve bağlantıyı kapat
        Try
            baglanti.Open()
            Dim rowsAffected As Integer = Komut.ExecuteNonQuery() ' Güncelleme işlemi için ExecuteNonQuery kullanılır
            baglanti.Close()

            If rowsAffected > 0 Then
                Return "Kayıt başarıyla güncellendi!"
            Else
                Return "Kayıt bulunamadı!"
            End If
        Catch ex As Exception
            Return "Hata: " & ex.Message
        End Try
    End Function

End Class
