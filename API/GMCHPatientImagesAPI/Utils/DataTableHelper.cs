using GMCHPatientImagesDtos.Attributes;
using GMCHPatientImagesDtos.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GMCHPatientImages.Utils
{
    public static class DataTableHelper
    {

        
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable IsDataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.IsDefined(typeof(IsDataTable), false)).ToArray(); ;
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                IsDataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                IsDataTable.Rows.Add(values);
            }
            
            //put a breakpoint here and check datatable
            return IsDataTable;
        }

    }
}
