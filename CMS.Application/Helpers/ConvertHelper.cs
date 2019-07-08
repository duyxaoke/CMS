using AutoMapper;
using Core.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CMS.Application.Helpers
{
    public static class ConvertHelper
    {
        public static string ConnectionStringEINVOICENET { get; set; }
        public static int SQLInsertMaxRow = 1000;
        public static int ExcelRecordMaxValue = 50000;
        public static string ConnectionString_PCM { get; set; }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            DataTable table = new DataTable();
            if (typeof(T) == typeof(int))
            {
                table.Columns.Add("IntValue", typeof(int));
                if (data == null)
                    return table;
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    row["IntValue"] = item;
                    table.Rows.Add(row);
                }
                return table;
            }
            else if (typeof(T) == typeof(string))
            {
                table.Columns.Add("StringValue", typeof(string));
                if (data == null)
                    return table;
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    row["StringValue"] = item;
                    table.Rows.Add(row);
                }
                return table;
            }
            else if (typeof(T) == typeof(decimal))
            {
                table.Columns.Add("DecimalValue", typeof(decimal));
                if (data == null)
                    return table;
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    row["DecimalValue"] = item;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                if (data != null)
                    foreach (T item in data)
                    {
                        var values = new object[Props.Length];
                        for (int i = 0; i < Props.Length; i++)
                        {
                            //inserting property values to datatable rows
                            values[i] = Props[i].GetValue(item, null);
                        }
                        dataTable.Rows.Add(values);
                    }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
        #region Convert ToSQLSelectStatement
        private static string ToSQLSelectStatement_ListInt<T>(this IEnumerable<T> data)
        {
            var lsValues = new List<string>();
            foreach (T item in data)
            {
                lsValues.Add(string.Concat("(", item.ToString(), ")"));
            }

            return string.Concat("SELECT * FROM(VALUES ", string.Join(",", lsValues), ") AS T(intValue)");
        }
        private static string ToSQLSelectStatement_ListString<T>(this IEnumerable<T> data)
        {
            var lsValues = new List<string>();
            foreach (T item in data)
            {
                if (string.IsNullOrEmpty(item.ToString()))
                {
                    lsValues.Add("('')");
                }
                else
                    lsValues.Add(string.Concat("(N'", item.ToString().Replace("'", "''"), "')"));
            }

            return string.Concat("SELECT * FROM(VALUES ", string.Join(",", lsValues), ") AS T(stringValue)");
        }
        private static string ToSQLSelectStatement_ListObject<T>(this IEnumerable<T> data, List<PropertyInfo> correctProps, List<string> tCol)
        {
            var lsValues = new List<string>();
            foreach (T item in data)
            {
                var lsPropValue = new List<string>();
                for (int i = 0; i < correctProps.Count; i++)
                {
                    PropertyInfo prop = correctProps[i];
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    if (prop.GetValue(item, null) == null)
                    {
                        lsPropValue.Add("NULL");
                    }
                    else
                    {
                        if (type.Name == "Boolean")
                        {
                            lsPropValue.Add((bool)prop.GetValue(item, null) ? "1" : "0");
                        }
                        else
                        if (type.Name == "String" || type.Name == "Date" || type.Name == "DateTime")
                        {
                            if (string.IsNullOrEmpty(prop.GetValue(item, null).ToString()))
                            {
                                lsPropValue.Add("''");
                            }
                            else
                                lsPropValue.Add(string.Concat("N'", prop.GetValue(item, null).ToString().Replace("'", "''"), "'"));
                        }
                        else if (type.Name == "Decimal" || type.Name == "Float" || type.Name == "Double")

                            lsPropValue.Add(prop.GetValue(item, null).ToString().Replace(",", "."));
                        else
                            lsPropValue.Add(prop.GetValue(item, null).ToString());
                    }
                }
                lsValues.Add(string.Concat("(", string.Join(",", lsPropValue), ")"));
            }

            return string.Concat(
                "SELECT * FROM(VALUES "
                , string.Join(",", lsValues)
                , ") AS T("
                , string.Join(",", tCol)
                , ")"
                );
        }

        public static string ToSQLSelectStatement<T>(this IEnumerable<T> data)
        {
            string result = string.Empty;
            if (data == null || !data.Any())
                return result;
            int totalData = data.Count();
            int iCount = totalData % SQLInsertMaxRow > 0 ? totalData / SQLInsertMaxRow + 1 : totalData / SQLInsertMaxRow;
            if (iCount > 1)
            {
                if (typeof(T) == typeof(int))
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        var dataSkip = data.Skip(i * SQLInsertMaxRow).Take(SQLInsertMaxRow);
                        result += dataSkip.ToSQLSelectStatement_ListInt<T>();
                    }
                    return result;
                }
                else if (typeof(T) == typeof(string))
                {

                    for (int i = 0; i < iCount; i++)
                    {
                        var dataSkip = data.Skip(i * SQLInsertMaxRow).Take(SQLInsertMaxRow);
                        result += dataSkip.ToSQLSelectStatement_ListString<T>();
                    }
                    return result;
                }
                else
                {
                    //Get all the properties
                    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var correctProps = new List<PropertyInfo>();
                    foreach (PropertyInfo prop in Props)
                    {
                        //Setting column names as Property names
                        object[] attrs = prop.GetCustomAttributes(true);
                        bool ignore = false;
                        foreach (var attr in attrs)
                        {
                            TableConvertAtribute tableAtribute = attr as TableConvertAtribute;
                            if (tableAtribute != null)
                                ignore = tableAtribute.Ignore;
                        }
                        if (ignore == false)
                        {
                            correctProps.Add(prop);
                        }
                    }

                    var tCol = new List<string>();
                    for (int i = 0; i < correctProps.Count; i++)
                    {
                        tCol.Add("col_" + i.ToString());
                    }
                    for (int i = 0; i < iCount; i++)
                    {
                        var dataSkip = data.Skip(i * SQLInsertMaxRow).Take(SQLInsertMaxRow);
                        result += dataSkip.ToSQLSelectStatement_ListObject<T>(correctProps, tCol);
                    }

                    #region bỏ dùng Threat
                    //Thread[] array = new Thread[iCount];
                    //for (int i = 0; i < iCount; i++)
                    //{
                    //    var dataSkip = data.Skip(i * SQLInsertMaxRow).Take(SQLInsertMaxRow);
                    //    // Start the thread with a ThreadStart.
                    //    ThreadStart start = new ThreadStart(
                    //        delegate ()
                    //        {
                    //            result += dataSkip.ToSQLSelectStatement_ListObject<T>(correctProps, tCol);
                    //        }
                    //      );
                    //    array[i] = new Thread(start);
                    //    array[i].Start();
                    //}
                    //// Join all the threads.
                    //for (int i = 0; i < iCount; i++)
                    //{
                    //    array[i].Join();
                    //}
                    #endregion
                    return result;
                }
            }
            else
            {
                if (typeof(T) == typeof(int))
                {
                    result = data.ToSQLSelectStatement_ListInt<T>();
                }
                else if (typeof(T) == typeof(string))
                {
                    result = data.ToSQLSelectStatement_ListString<T>();
                }
                else
                {
                    //Get all the properties
                    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var correctProps = new List<PropertyInfo>();
                    foreach (PropertyInfo prop in Props)
                    {
                        //Setting column names as Property names
                        object[] attrs = prop.GetCustomAttributes(true);
                        bool ignore = false;
                        foreach (var attr in attrs)
                        {
                            TableConvertAtribute tableAtribute = attr as TableConvertAtribute;
                            if (tableAtribute != null)
                                ignore = tableAtribute.Ignore;
                        }
                        if (ignore == false)
                        {
                            correctProps.Add(prop);
                        }
                    }

                    var tCol = new List<string>();
                    for (int i = 0; i < correctProps.Count; i++)
                    {
                        tCol.Add("col_" + i.ToString());
                    }

                    result = data.ToSQLSelectStatement_ListObject<T>(correctProps, tCol);
                }
            }
            return result;
        }
        #endregion
        public static Int32 ConvertINT32(object obj, int valuedefault)
        {
            try
            {
                int value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    value = valuedefault;
                else
                    value = Convert.ToInt32(obj);
                return value;
            }
            catch
            {
                return valuedefault;
            }
        }
        public static long ConvertINT64(object obj, long valuedefault)
        {
            try
            {
                long value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    value = valuedefault;
                else
                    value = Convert.ToInt64(obj);
                return value;
            }
            catch
            {
                return valuedefault;
            }
        }
        public static decimal ConvertDecimal(object obj, decimal valueDefault)
        {
            try
            {
                decimal value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    value = valueDefault;
                else
                {
                    obj = String.Format(new CultureInfo("en-US"), "{0:C}", obj.ToString());
                    value = Convert.ToDecimal(obj);
                }
                return value;
            }
            catch
            {
                return valueDefault;
            }
        }
        public static string ConvertString(object obj, string valueDefault)
        {
            try
            {
                string value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    value = valueDefault;
                else
                    value = obj.ToString();
                return value;
            }
            catch
            {
                return valueDefault;
            }
        }
        public static bool ConvertBool(object obj, bool valueDefault)
        {
            try
            {
                bool value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    value = valueDefault;
                else
                    value = bool.Parse(obj.ToString());
                return value;
            }
            catch
            {
                return valueDefault;
            }
        }

        /// <summary>
        /// Xuất dữ liệu kiểu DataReader
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="sum"></param>
        /// <param name="columnName"></param>
        /// <param name="nameSheet"></param>
        public static void ExportExcel(this IDataReader dataReader, string path, string fileName, bool sum, List<string> columnName, string nameSheet, out int totalRow)
        {
            //if (objTable.Count != 0)
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(nameSheet);
                DataTable schemaTable = dataReader.GetSchemaTable();
                ws.Cells["A1"].LoadFromDataReader(dataReader, true, "", TableStyles.Dark10);
                //ws.Cells["A2"].LoadFromDataReader(dataReader, false, "", TableStyles.Dark10);
                //for (int i = 0; i < schemaTable.Rows.Count; i++)
                //{
                //    ws.Cells[1, i + 1, 1, i + 1].Value = schemaTable.Rows[i]["ColumnName"];
                //}

                ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
                ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Font.Bold = true;
                int lastRow = ws.Dimension.End.Row;
                totalRow = lastRow;
                if (lastRow > 1)
                {
                    foreach (DataRow schemarow in schemaTable.Rows)
                    {
                        int colindex = Convert.ToInt32(schemarow["ColumnOrdinal"].ToString()) + 1;
                        ws.Column(colindex).AutoFit();
                        switch (System.Type.GetType(schemarow["DataType"].ToString()).FullName)
                        {
                            case "System.Int32":
                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                break;
                            case "System.Int64":
                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                break;
                            case "System.Double":
                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                break;
                            case "System.Decimal":
                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                break;
                            case "System.DateTime":
                                ws.Column(colindex).Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                                break;
                        }
                    }

                    //Sum column
                    if (sum)
                    {
                        ws.Cells[lastRow + 1, 1, lastRow + 1, dataReader.FieldCount].Style.Font.Bold = true;
                        columnName.ForEach(x =>
                        {
                            ws.Cells[lastRow + 1, Convert.ToInt32(x)].Formula =
                                "SUM(" + ws.Cells[2, Convert.ToInt32(x)] + ":" + ws.Cells[lastRow, Convert.ToInt32(x)] +
                                ")";
                            ws.Cells[lastRow + 1, Convert.ToInt32(x)].Style.Numberformat.Format = "#,###,###,##0";
                        });
                    }

                    if (File.Exists(path + fileName))
                        File.Delete(path + fileName);
                    CreateFolder(path);
                    FileStream objStrem = File.Create(path + fileName);
                    objStrem.Close();
                    //FileInfo newFile = new FileInfo(path + fileName);
                    //pack.SaveAs(newFile);
                    File.WriteAllBytes(path + fileName, pack.GetAsByteArray());
                }
                else
                {
                    if (File.Exists(path + fileName))
                        File.Delete(path + fileName);
                }
            }
        }

        public static void ExportExcel(this DataTable objTable, string path, string fileName, string sheetName)
        {
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(sheetName);
                ws.Cells["A1"].LoadFromDataTable(objTable, true);
                int colNumber = 1;

                foreach (DataColumn col in objTable.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy";
                    }
                    colNumber++;
                }

                ws.Cells.AutoFitColumns();
                ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
                ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Font.Bold = true;
                if (File.Exists(path + fileName))
                    File.Delete(path + fileName);
                CreateFolder(path);
                FileStream objStrem = File.Create(path + fileName);
                objStrem.Close();
                File.WriteAllBytes(path + fileName, pack.GetAsByteArray());
            }
        }

        public static void ExportExcel(this List<DataTable> objTable, string path, string fileName, bool sum, List<string> columnName)
        {
            if (objTable.Count > 0)
            {
                using (ExcelPackage pack = new ExcelPackage())
                {
                    foreach (DataTable dataTable in objTable)
                    {
                        ExcelWorksheet ws = pack.Workbook.Worksheets.Add(dataTable.TableName);
                        ws.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.Dark10);

                        foreach (DataColumn objTableColumn in dataTable.Columns)
                        {
                            int colindex = Convert.ToInt32(dataTable.Columns.IndexOf(objTableColumn)) + 1;
                            ws.Column(colindex).AutoFit();
                            switch (System.Type.GetType(objTableColumn.DataType.ToString()).FullName)
                            {
                                case "System.Int32":
                                    ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                    break;
                                case "System.Int64":
                                    ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                    break;
                                case "System.Double":
                                    ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                    break;
                                case "System.Decimal":
                                    ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
                                    break;
                                case "System.DateTime":
                                    ws.Column(colindex).Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                                    break;
                            }
                        }

                        ws.Cells.AutoFitColumns();
                        ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Fill.BackgroundColor
                            .SetColor(Color.FromArgb(155, 194, 230));
                        ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;

                        int lastRow = ws.Dimension.End.Row;
                        if (sum)
                        {
                            ws.Cells[lastRow + 1, 1, lastRow + 1, dataTable.Columns.Count].Style.Font.Bold = true;
                            columnName.ForEach(x =>
                            {
                                ws.Cells[lastRow + 1, Convert.ToInt32(x)].Formula = "SUM(" + ws.Cells[2, Convert.ToInt32(x)] + ":" + ws.Cells[lastRow, Convert.ToInt32(x)] + ")";
                                ws.Cells[lastRow + 1, Convert.ToInt32(x)].Style.Numberformat.Format = "#,###,###,##0";
                            });
                        }
                    }

                    if (File.Exists(path + fileName))
                        File.Delete(path + fileName);
                    CreateFolder(path);
                    FileStream objStrem = File.Create(path + fileName);
                    objStrem.Close();
                    File.WriteAllBytes(path + fileName, pack.GetAsByteArray());
                }
            }
        }
        private static bool CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return true;
        }

        public class DatatableConvertAttribute : Attribute
        {
            public bool Ignore { get; set; }
            public DatatableConvertAttribute(bool ignore)
            {
                Ignore = ignore;
            }

        }
    }
}
