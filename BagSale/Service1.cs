using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BagSale
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanan = new List<Pemesanan>(); // proses utk mendeclare nama list yang sudah dibuat
            try
            {
                string sql = " select ID_transaksi, Nama_customer, No_telpon," + "Jumlah_transaksi, Nama_produk from dbo.Pemesanan p join dbo.Produk l on p.ID_produk = l.ID_produk";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    /*nama class*/
                    Pemesanan data = new Pemesanan(); // deklarasi data, mengambil 1persatu dari database
                    //bentuk array
                    data.IDTransaksi = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahTransaksi = reader.GetInt32(3);
                    data.Produk = reader.GetString(4);
                    pemesanan.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanan;
        }
        string constring = "Data Source=DESKTOP-2J1AS4M;Initial Catalog=WCFBag;Persist Security Info=True;User ID=sa;Password=indah18"; // Koneksi server
        SqlConnection connection;
        SqlCommand com; //untuk mengkoneksikan database ke visual studio

        public string pemesanan(string IDTransaksi, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDProduk)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDTransaksi + "', '" + NamaCustomer + "', '" + NoTelpon + "', "
                    + "" + JumlahPemesanan + ", '" + IDProduk + "')";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Produk set Kuota = Kuota - " + JumlahPemesanan + " where ID_produk = '" + IDProduk + "' ";
                connection = new SqlConnection(constring); //fungsi konek database
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<Produk> Produk()
        {
            List<Produk> LokasiFull = new List<Produk>(); //proses utk mendeclare nama list yg telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_produk, Nama_produk, Dekskripsi, Kuota from dbo.Produk";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection); //proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query
                while (reader.Read())
                {
                    /*nama class*/
                    Produk data = new Produk(); //deklarasi data, mengambil 1persatu dari database
                    //bentuk array
                    data.IDProduk = reader.GetString(0); //0 itu index, ada kolom keberapa di string sql diatas
                    data.NamaProduk = reader.GetString(1);
                    data.Deksripsi = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }



        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string editPemesanan(string IDTransaksi, string Namacustomer, string No_telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_customer = '" + Namacustomer + "', No_telpon = '" + No_telpon + "'" + " where ID_transaksi = '" + IDTransaksi + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string deletePemesanan(string IDTransaksi)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_transaksi= '" + IDTransaksi + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from Login where Username='"+username+"' and Password='"+password+"'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                kategori = sdr.GetString(0);
            }

            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into Login values('"+username+"','"+password+"','"+kategori+"')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            } 
            catch (Exception es)
            {
                return es.ToString();
            }
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql = "update Login set Username='"+username+"',Password='"+password+"',Kategori='"+kategori+"' where ID_login="+id+"";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch(Exception es)
            {
                return es.ToString();
            }
        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "Select ID_login from Login where Username='"+username+"'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete from Login where ID_login="+id+"";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception es)
            {
                return es.ToString();
            }
        }

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_login,Username,Password,Kategori from Login";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                connection.Close();
            }
            catch(Exception es)
            {
                es.ToString();
            }
            return list;
        }
    }
}
