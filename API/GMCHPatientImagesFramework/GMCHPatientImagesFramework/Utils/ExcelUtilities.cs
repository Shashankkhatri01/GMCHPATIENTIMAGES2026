using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public class ExcelUtilities
    {
        public static async Task<DataTable> GetExcelDataTableAsync(IFormFile formFile, bool hasHeader = true)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString());
                            }
                        }
                    }
                    if(rowList.Count>0)
                    dtTable.Rows.Add(rowList.ToArray());
                    rowList.Clear(); 
                }
                return dtTable;
            }
        }


        public static void setBorder(ISheet sheet, ICellStyle borderStyle, CellRangeAddress region)
        {
            for (int j = region.FirstRow; j <= region.LastRow; j++)
            {
                IRow row = sheet.GetRow(j);
                for (int i = region.FirstColumn; i <= region.LastColumn; i++)
                {
                    ICell cell = row.GetCell(i);
                    cell.CellStyle = borderStyle;

                }
            }
            //setBorderTop(sheet, borderStyle, region);
            //setBorderBottom(sheet, borderStyle, region);
            //setBorderLeft(sheet, borderStyle, region);
            //setBorderRight(sheet, borderStyle, region);
        }

        public static void setRegionBorder(int border, CellRangeAddress cellRange, ISheet sheet, IWorkbook workbook)
        {
           // RegionUtil.SetBorderBottom(border, cellRange, sheet, workbook);
           // RegionUtil.SetBorderLeft(border, cellRange, sheet, workbook);
           // RegionUtil.SetBorderTop(border, cellRange, sheet, workbook);
//RegionUtil.SetBorderRight(border, cellRange, sheet, workbook);
        }

        public static void CreateCell(IRow CurrentRow, int CellIndex, string Value, ICellStyle CellStyle, VerticalAlignment VAlign = VerticalAlignment.Center, HorizontalAlignment HAlign = HorizontalAlignment.Left, int width=0)
        {
            ICellStyle style = CellStyle;
            style.VerticalAlignment = VAlign;
            style.Alignment = HAlign;
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            Cell.CellStyle = style;
        }

        public static async Task<DataTable> GetDataTableFromTextFileAsync(IFormFile formFile)
        {
            DataTable dt = null;
            List<string> lineList = new List<string>();
            using (var stream = formFile.OpenReadStream())
            {
                using(var reader = new StreamReader(stream))
                {
                    string line;
                    while( (line = await reader.ReadLineAsync()) != null)
                    {
                        
                        lineList.Add(line);
                    }
                } 
            }
            if(lineList.Count > 0)
            {
                dt = new DataTable();
                // Create columns
                var columns = lineList[0].Split(",");
                foreach (var col in columns)
                {
                    dt.Columns.Add(col.ToString());
                }
                foreach(var item in lineList)
                {
                    var currentLine = item.Split(",");
                    dt.Rows.Add(currentLine);
                }
                dt.Rows.RemoveAt(0);
            }
            
            return dt;
        }


    }
}
