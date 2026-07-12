using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImages.Framework.Utils
{
    public static class Helper
    {
        public static decimal GetOTP()
        {
            decimal abc;
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            abc = System.Convert.ToDecimal(r);
            return abc;

        }
        public static string GetOrder()
        {
            decimal abc;
            Random generator = new Random();
            String rendom = generator.Next(0, 1000000).ToString("D10");

            return rendom;
        }
        //public static DataTable ToDataTable<T>(List<T> items, string tableName)
        //{
        //    DataTable dataTable = new DataTable(typeof(T).Name);
        //    //Get all the properties
        //    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (PropertyInfo prop in Props)
        //    {
        //        dataTable.Columns.Add(prop.Name);
        //    }
        //    foreach (T item in items)
        //    {
        //        var values = new object[Props.Length];
        //        for (int i = 0; i < Props.Length; i++)
        //        {
        //            values[i] = Props[i].GetValue(item, null);
        //        }
        //        dataTable.Rows.Add(values);
        //    }
        //    dataTable.TableName = tableName;

        //    return dataTable;
        //}

        public static DataTable ToDataTable<T>(List<T> items, string tableName)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            // Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType == typeof(DateTime) ? typeof(string) : prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    object value = Props[i].GetValue(item, null);

                    // Convert DateTime to "dd-MMM-yyyy" format
                    if (value is DateTime dateTimeValue)
                    {
                        values[i] = dateTimeValue.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        values[i] = value;
                    }
                }
                dataTable.Rows.Add(values);
            }

            dataTable.TableName = tableName;
            return dataTable;
        }
        public static void WriteMsg(Exception ex)
        {
            string appath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = appath + "Error.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
        public static void WriteMsg(string msg)
        {
            string appath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = appath + "Error.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + msg + "<br/>" + Environment.NewLine +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
        public static void WriteLog(string msg)
        {
            string appath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = appath + "tLog.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + msg + "<br/>" + Environment.NewLine +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
        public static double GetDistanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in kilometers

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));
            double distance = R * c;

            return distance;
        }
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public static async Task<double> GetDrivingDistanceInKmAsync(double lat1, double lon1, double lat2, double lon2, string apiKey)
        {
            string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={lat1},{lon1}&destination={lat2},{lon2}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

                if (result.routes.Count > 0)
                {
                    var distanceMeters = (double)result.routes[0].legs[0].distance.value;
                    return distanceMeters / 1000.0; // Convert to kilometers
                }

                return -1; // or throw an exception
            }
        }
    }
}
