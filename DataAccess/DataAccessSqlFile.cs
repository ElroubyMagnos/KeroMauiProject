//using AndroidX.Emoji2.Text.FlatBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using KeroFruits.Model.Usine.Calendar;
using KeroFruits.Model.Usine.Payment;
using KeroFruits.Utilities.DataAccess.Files;
using KeroFruits.Utilities.Interfaces;
using System.Data;
using KeroFruits.Model.Usine.People;

namespace KeroFruits.Utilities.DataAccess
{
    public class DataAccessSqlFile : DataAccess
    {

        public DataAccessSqlFile()
        {
        }
        /// <summary>
        /// Constructor associated with a datafileManager, it contains path to a config text file
        /// wich contains path to csv files
        /// </summary>
        /// <param name="dfm"></param>
        public DataAccessSqlFile(DataFilesManager dfm, IAlertService alertService) : base(dfm) 
        {
            this.alertService = alertService;
        }

        private Reservation GetReservation(SqlDataReader Line, ClientsCollection Clients)
        {
            Client c = null;
            foreach (var oneclient in Clients)
            {
                if (oneclient.Id == Line.GetInt32(6))
                {
                    c = oneclient;
                    break;
                }
            }
            if (c == null) { return null; }//عندما نتعامل مع الفرونت سيظهر لنا صفحة نضيف فيها العميل

            return new Reservation(c, Line.GetInt32(0), Line.GetDateTime(1), Line.GetDateTime(2), Line.GetInt32(4), null, Line.GetInt32(6));
        }

        public ReservationsCollection GetAllReservations(ClientsCollection Clients)
        {
            try// تجربة الكود
            {
                ReservationsCollection Reservations = new ReservationsCollection();//محتوي الكل
                string sql = "SELECT * FROM Reservations;";//الامر علي شكل نص

                if (MauiProgram.con.State != ConnectionState.Open)
                    MauiProgram.con.Open();//فتح الاتصال من المتصل الرئيسي (MauiProgram.con)

                SqlCommand cmd = new SqlCommand(sql, MauiProgram.con);//الامر في شكل Class

                SqlDataReader dataReader = cmd.ExecuteReader();//تنفيذ الامر علي هيئة داتا ريدر او قارئ بيانات

                while (dataReader.Read())//قراءة البيانات سطر بعد سطر والانتهاء عند False
                {
                    Reservation sm = GetReservation(dataReader, Clients);//احضار كلاس واحد للتخزين
                    if (sm != null)//التاكد من عدم تلفه او فراغه
                    {
                        Reservations.Add(sm);//اضافة علي المجموع الخاص بالكلاس
                    }
                }
                dataReader.Close();//اغلاق قارئ البيانات المستدعي

                MauiProgram.con.Close();// الغاء الاتصال بقاعدة البيانات
                return Reservations;// تمرير المجموعة
            }
            catch (Exception ex)//امساك الاخطاء
            {
                alertService.ShowAlert("Database Request Error", ex.Message);//عرض الاخطاء
                MauiProgram.con.Close();// اغلاق الاتصال بقاعدة البيانات
                return null;// تمرير لا شئ
            }
        }//end GetAllReservations()


        private Client GetClient(SqlDataReader dataReader)
        {
            if (dataReader.GetInt32(0) != 0)
            {
                Client c = new Client(id: dataReader.GetInt32(0), lastName: dataReader.GetString(1), firstName: dataReader.GetString(2), gender: dataReader.GetInt16(3) == 1, email: dataReader.GetString(4), mobilePhoneNumber: dataReader.GetString(5));
                
                return c;
            }
            else
            {
                return null;
            }

        }

        public ClientsCollection GetAllClients()
        {
            try// تجربة الكود
            {
                ClientsCollection Clients = new ClientsCollection();//محتوي الكل
                string sql = "SELECT * FROM Clients;";//الامر علي شكل نص

                if (MauiProgram.con.State != ConnectionState.Open)
                    MauiProgram.con.Open();//فتح الاتصال من المتصل الرئيسي (MauiProgram.con)

                SqlCommand cmd = new SqlCommand(sql, MauiProgram.con);//الامر في شكل Class

                SqlDataReader dataReader = cmd.ExecuteReader();//تنفيذ الامر علي هيئة داتا ريدر او قارئ بيانات

                while (dataReader.Read())//قراءة البيانات سطر بعد سطر والانتهاء عند False
                {
                    Client cl = GetClient(dataReader);//احضار كلاس واحد للتخزين
                    if (cl != null)//التاكد من عدم تلفه او فراغه
                    {
                        Clients.Add(cl);//اضافة علي المجموع الخاص بالكلاس
                    }
                }
                dataReader.Close();//اغلاق قارئ البيانات المستدعي

                MauiProgram.con.Close();// الغاء الاتصال بقاعدة البيانات
                return Clients;// تمرير المجموعة
            }
            catch (Exception ex)//امساك الاخطاء
            {
                alertService.ShowAlert("Database Request Error", ex.Message);//عرض الاخطاء
                MauiProgram.con.Close();// اغلاق الاتصال بقاعدة البيانات
                return null;// تمرير لا شئ
            }
        }//end GetAllReservations()


        public bool UpdateAllReservations(ReservationsCollection Reservations)
        {
            try
            {
                if (MauiProgram.con.State != ConnectionState.Open)
                    MauiProgram.con.Open();

                SqlDataReader Reader = new SqlCommand("Select ID From Reservations", MauiProgram.con).ExecuteReader();

                List<int> ExistInts = new List<int>();

                while (Reader.Read())
                {
                    ExistInts.Add(Reader.GetInt32(0));
                }

                Reader.Close();

                foreach (Reservation reservation in Reservations)
                {
                    if (ExistInts.Contains(reservation.ID))
                    {
                        new SqlCommand(GetSqlUpdateReservationMember(reservation), MauiProgram.con).ExecuteNonQuery();
                    }
                    else new SqlCommand(GetSqlInsertReservationMember(reservation), MauiProgram.con).ExecuteNonQuery();
                }

                MauiProgram.con.Close();
            }
            catch (Exception ex)
            {
                alertService.ShowAlert("Database Request Error", ex.Message);
                MauiProgram.con.Close();
            }

            return true;
        }

        private string GetSqlInsertReservationMember(Reservation sm)
        {
            return $"INSERT INTO Reservations (Date, DeliveryEffectifDate, EstimatedDate, Quantity, EffectifLitresReceived, ClientID) " +
                $"VALUES ('{sm.Date}','{sm.DeliveryEffectifDate}','{sm.EstimatedDate}','{sm.Quantity}','{sm.EffectifLitresReceived}','{sm.ClientId}');SELECT SCOPE_IDENTITY();";

        }

        private string GetSqlUpdateReservationMember(Reservation sm)
        {
            return $"UPDATE Reservations SET Date= '{sm.Date}',DeliveryEffectifDate = '{sm.DeliveryEffectifDate}',EstimatedDate = '{sm.EstimatedDate}',Quantity = '{sm.Quantity}', EffectifLitresReceived = '{sm.EffectifLitresReceived}', ClientId = '{sm.ClientId}' WHERE ID = {sm.ID};";
        }

        public bool UpdateAllClients(ClientsCollection clients)
        {
            try
            {
                if (MauiProgram.con.State != ConnectionState.Open)
                    MauiProgram.con.Open();

                SqlDataReader Reader = new SqlCommand("Select ID From Clients", MauiProgram.con).ExecuteReader();
                
                List<int> ExistInts = new List<int>();  
                
                while (Reader.Read())
                {
                    ExistInts.Add(Reader.GetInt32(0));
                }

                Reader.Close();

                foreach (Client client in clients)
                {
                    if (ExistInts.Contains(client.Id))
                    {
                        new SqlCommand(GetSqlUpdateClientMember(client), MauiProgram.con).ExecuteNonQuery();
                    }
                    else new SqlCommand(GetSqlInsertClientMember(client), MauiProgram.con).ExecuteNonQuery();
                }

                MauiProgram.con.Close();
            }
            catch (Exception ex) 
            {
                alertService.ShowAlert("Database Request Error", ex.Message);
                MauiProgram.con.Close();
            }

            return true;
        }

        private string GetSqlInsertClientMember(Client sm)
        {
            return $"INSERT INTO Clients (LastName, FirstName, Gender, Email, mobilePhoneNumber) " +
                $"VALUES ('{sm.LastName}','{sm.FirstName}','{(sm.Gender ? "1" : "0")}','{sm.Email}','{sm.MobilePhoneNumber}');SELECT SCOPE_IDENTITY();";

        }

        private string GetSqlUpdateClientMember(Client sm)
        {
            return $"UPDATE Clients SET FirstName= '{sm.FirstName}',LastName = '{sm.LastName}',Gender = '{(sm.Gender ? "1" : "0")}',mobilePhoneNumber = '{sm.MobilePhoneNumber}', Email = '{sm.Email}' WHERE ID = {sm.Id}; ; ";
        }

        private bool IsInDb(int idValue, string idColumnName, string tableName)
        {
            string sql = $"SELECT * FROM {tableName} WHERE {idColumnName} = {idValue}";
            SqlCommand cmd = new SqlCommand(sql, MauiProgram.con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            bool inDb = dataReader.HasRows;
            dataReader.Close();
            return inDb;
        }

        public ReservationsCollection EnterClientGetReservations(Client client, ClientsCollection Clients)
        {
            var AllReserv = GetAllReservations(Clients);

            var List = new ReservationsCollection();

            var New = AllReserv.Where(x => x.ClientId == client.Id).ToList();

            foreach (var res in New)
            {
                List.Add(res);
            }

            return List;
        }
    }       
}
