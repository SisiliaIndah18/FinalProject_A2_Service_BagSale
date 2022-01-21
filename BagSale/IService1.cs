using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BagSale
{
    [DataContract]
    public class Pemesanan //create
    {
        [DataMember]
        public string IDTransaksi { get; set; }

        [DataMember]
        public string NamaCustomer { get; set; } //method

        [DataMember]
        public string NoTelpon { get; set; }

        [DataMember]
        public int JumlahTransaksi { get; set; }

        [DataMember]
        public string Produk { get; set; }

    }
    [DataContract]
    public class Produk //menampilkan detail lokasi
    {
        [DataMember]
        public string IDProduk { get; set; } //variabel dari public class

        [DataMember]
        public string NamaProduk { get; set; }

        [DataMember]
        public string Deksripsi { get; set; }

        [DataMember]
        public int Kuota { get; set; }

    }

    [DataContract]
    public class DataRegister // Login dan Register
    {
        [DataMember(Order =1)]
        public int id { get; set; }
        [DataMember(Order =2)]
        public string username { get; set; }
        [DataMember(Order =3)]
        public string password { get; set; }
        [DataMember(Order =4)]
        public string kategori { get; set; }
    }
   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string pemesanan(string IDTransaksi, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDProduk); //method //proses input data

        [OperationContract]
        string editPemesanan(string IDTransaksi, string Namacustomer, string No_telpon);

        [OperationContract]
        string deletePemesanan(string IDTransaksi);

       

        [OperationContract]
        List<Produk> Produk();

        [OperationContract]
        List<Pemesanan> Pemesanan();

        // Login

        [OperationContract]
        string Login(string username, string password);
        [OperationContract]
        string Register(string username, string password, string kategori);
        [OperationContract]
        string UpdateRegister(string username, string password, string kategori, int id);
        [OperationContract]
        string DeleteRegister(string username);
        [OperationContract]
        List<DataRegister> DataRegist();

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ServiceReservasi.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
