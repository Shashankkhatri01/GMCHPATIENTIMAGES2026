using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public class ExcelReport
    {
        IWebHostEnvironment _environment;




        public short BorderStyle { get; private set; }

        public ExcelReport(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;

        }


        public async Task<MemoryStream> GetExcelDetails(DataTable dt, string ReportName = "")
        {
            string webRootPath = System.IO.Path.Combine(_environment.ContentRootPath, "excel.xlsx");
            
            try
            {
                FileInfo file = new FileInfo(Path.Combine(webRootPath));
                var memoryStream = new MemoryStream();
                using (var fs = new FileStream(webRootPath, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet("Sheet1");
                    excelSheet.DisplayGridlines = true;



                    IFont headerfont = workbook.CreateFont();
                    headerfont.FontHeightInPoints = (short)9;
                    headerfont.FontName = "Times New Roman";
                    headerfont.IsBold = false; 

                    //Header cell style
                    ICellStyle headerCellStyle = workbook.CreateCellStyle();
                    headerCellStyle.VerticalAlignment = VerticalAlignment.Center;

                    headerCellStyle.SetFont(headerfont);


                    ICell cell;

                    IRow row = excelSheet.CreateRow(0);
                    row.Height = 800;
                    headerfont.IsBold = false;
                    //  row.IsBold = true;


                    //dt.DefaultView.Sort = "Invoice No.";
                    dt = dt.DefaultView.ToTable();

                    var columns = new string[dt.Columns.Count];


                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        columns[j] = (dt.Columns[j].ColumnName);
                    }

                    for (int j = 0; j < columns.Length; j++)
                    {
                        string cellValue = columns[j].ToString();

                        ExcelUtilities.CreateCell(row, j, cellValue, headerCellStyle);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //var rowIndex = i + 1;
                        var rows = excelSheet.CreateRow(i + 1);

                        for (int j = 0; j < columns.Length; j++)
                        {
                            string cellValue = dt.Rows[i][j].ToString();

                            ExcelUtilities.CreateCell(rows, j, cellValue, headerCellStyle);
                        }

                    }
                    for (int i = 0; i < columns.Length; i++)
                    {
                        excelSheet.SetColumnWidth(i, 3700);


                    }
                   // excelSheet.SetColumnWidth(1, 15000);



                    workbook.Write(fs);
                }
                using (var fileStream = new FileStream(webRootPath, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                return memoryStream;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
     
}
