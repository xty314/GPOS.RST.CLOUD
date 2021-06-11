using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NPOIhelper
/// </summary>
public class NPOIhelper
{

    /// <summary>
    ///     从内存获取文件，无需将上传文件保存在服务器  
    /// </summary>
    /// <param name="stream">Request.Files["template"].InputStream</param>
    /// <param name="fileName">Request.Files["template"].FileName</param>
    /// <returns></returns>
    public static DataTable ExcelStreamToDataTable(Stream stream,string fileName)
    {
        var dt = new DataTable();
   
        string fileType = Path.GetExtension(fileName).ToLower();
        //string fileType = ".xls";
        if (fileType != ".xls" && fileType != ".xlsx")
        {
            return dt;
        }
        using (stream)
        {
            IWorkbook workbook;
            if (fileType == ".xls")
            {
                workbook = new HSSFWorkbook(stream);
            }
            else
            {
                workbook = new XSSFWorkbook(stream);
            }
            var sheet = workbook.GetSheetAt(0);

            IRow firstRow = sheet.GetRow(0);// 标题 第一行为key
            foreach (ICell cell in firstRow.Cells)
            {
                if(!IsNullOrWhiteSpace(cell.ToString()))
                    dt.Columns.Add(cell.ToString());
            }


            int ExcelRowsCount = sheet.LastRowNum;
            var rows = sheet.GetRowEnumerator();
            //while (rows.MoveNext())
            for (int j = 1; j <= ExcelRowsCount; j++)
            {
                IRow row = sheet.GetRow(j);
                var dr = dt.NewRow();
                for (var i = 0; i < row.LastCellNum; i++)
                {
                    var cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                dr[i] = null;
                                break;
                            case CellType.Boolean:
                                dr[i] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                dr[i] = cell.ToString();
                                break;
                            case CellType.String:
                                dr[i] = cell.StringCellValue;
                                break;
                            case CellType.Error:
                                dr[i] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                                try
                                {
                                    dr[i] = cell.NumericCellValue;
                                }
                                catch
                                {
                                    dr[i] = cell.StringCellValue;
                                }
                                break;

                            default:
                                dr[i] = "=" + cell.CellFormula;
                                break;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    /// <summary>
    /// Excel转换成DataTable（.xls）
    /// </summary>
    /// <param name="filePath">Excel文件路径</param>
    /// <returns></returns>

    public static DataTable ExcelToDataTable(string filePath)
        {



            var dt = new DataTable();
            string fileType = Path.GetExtension(filePath).ToLower();
            if (fileType != ".xls" && fileType != ".xlsx")
            {
                return dt;
            }
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook;
                if (fileType == ".xls")
                {
                    workbook = new HSSFWorkbook(file);
                }
                else 
                {
                    workbook = new XSSFWorkbook(file);
                }
                    var sheet = workbook.GetSheetAt(0);
          
                IRow firstRow = sheet.GetRow(0);// 标题 第一行为key
                foreach (ICell cell in firstRow.Cells)
                {
                    dt.Columns.Add(cell.ToString());
                }


                int ExcelRowsCount = sheet.LastRowNum;
                var rows = sheet.GetRowEnumerator();
                //while (rows.MoveNext())
                for (int j = 1; j <= ExcelRowsCount; j++)
                {
                    IRow row = sheet.GetRow(j);
                    var dr = dt.NewRow();
                    for (var i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dr[i] = null;
                                    break;
                                case CellType.Boolean:
                                    dr[i] = cell.BooleanCellValue;
                                    break;
                                case CellType.Numeric:
                                    dr[i] = cell.ToString();
                                    break;
                                case CellType.String:
                                    dr[i] = cell.StringCellValue;
                                    break;
                                case CellType.Error:
                                    dr[i] = cell.ErrorCellValue;
                                    break;
                                case CellType.Formula:
                                    try
                                    {
                                        dr[i] = cell.NumericCellValue;
                                    }
                                    catch
                                    {
                                        dr[i] = cell.StringCellValue;
                                    }
                                    break;
                                
                                default:
                                    dr[i] = "=" + cell.CellFormula;
                                    break;
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// Excel转换成DataSet（.xlsx/.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath, out string strMsg)
        {
            strMsg = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string fileType = Path.GetExtension(filePath).ToLower();
            string fileName = Path.GetFileName(filePath).ToLower();
            try
            {
                ISheet sheet = null;
                int sheetNumber = 0;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (fileType == ".xlsx")
                {
                    // 2007版本
                    XSSFWorkbook workbook = new XSSFWorkbook(fs);
                    sheetNumber = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetNumber; i++)
                    {
                        string sheetName = workbook.GetSheetName(i);
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet != null)
                        {
                            dt = GetSheetDataTable(sheet, out strMsg);
                            if (dt != null)
                            {
                                dt.TableName = sheetName.Trim();
                                ds.Tables.Add(dt);
                            }
                            else
                            {
                                //MessageBox.Show("Sheet数据获取失败，原因：" + strMsg);
                            }
                        }
                    }
                }
                else if (fileType == ".xls")
                {
                    // 2003版本
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    sheetNumber = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetNumber; i++)
                    {
                        string sheetName = workbook.GetSheetName(i);
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet != null)
                        {
                            dt = GetSheetDataTable(sheet, out strMsg);
                            if (dt != null)
                            {
                                dt.TableName = sheetName.Trim();
                                ds.Tables.Add(dt);
                            }
                            else
                            {
                                //MessageBox.Show("Sheet数据获取失败，原因：" + strMsg);
                            }
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取sheet表对应的DataTable
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static DataTable GetSheetDataTable(ISheet sheet, out string strMsg)
        {
            strMsg = "";
            DataTable dt = new DataTable();
            string sheetName = sheet.SheetName;
            int startIndex = 0;// sheet.FirstRowNum;
            int lastIndex = sheet.LastRowNum;
            //最大列数
            int cellCount = 0;
            IRow maxRow = sheet.GetRow(0);
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null && cellCount < row.LastCellNum)
                {
                    cellCount = row.LastCellNum;
                    maxRow = row;
                }
            }
            //列名设置
            try
            {
                for (int i = 0; i < maxRow.LastCellNum; i++)//maxRow.FirstCellNum
                {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + i).ToString());
                    //DataColumn column = new DataColumn("Column" + (i + 1).ToString());
                    //dt.Columns.Add(column);
                }
            }
            catch
            {
                strMsg = "工作表" + sheetName + "中无数据";
                return null;
            }
            //数据填充
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow drNew = dt.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                    {
                        if (row.GetCell(j) != null)
                        {
                            ICell cell = row.GetCell(j);
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    drNew[j] = "";
                                    break;
                                case CellType.Numeric:
                                    short format = cell.CellStyle.DataFormat;
                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理 
                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                        drNew[j] = cell.DateCellValue;
                                    else
                                        drNew[j] = cell.NumericCellValue;
                                    if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                        drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                    break;
                                case CellType.String:
                                    drNew[j] = cell.StringCellValue;
                                    break;
                                case CellType.Formula:
                                    try
                                    {
                                        drNew[j] = cell.NumericCellValue;
                                        if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                            drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            drNew[j] = cell.StringCellValue;
                                        }
                                        catch { }
                                    }
                                    break;
                                case CellType.Error:
                                    break;
                                default:
                                    drNew[j] = cell.StringCellValue;
                                    break;
                            }
                        }
                    }
                }
                dt.Rows.Add(drNew);
            }
            return dt;
        }

    public static byte[] DatatableToExcel(DataTable dt, string file)
    {
        IWorkbook workbook;
        byte[] buf = null;
        string fileExt = Path.GetExtension(file).ToLower();
        if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } 
        else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); }
        else { workbook = null; }
        if (workbook == null) { return buf; }
        ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

        //表头  
        IRow row = sheet.CreateRow(0);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ICell cell = row.CreateCell(i);
            cell.SetCellValue(dt.Columns[i].ColumnName);
        }

        //数据  
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row1 = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);
                cell.SetCellValue(dt.Rows[i][j].ToString());
            }
        }

        //转为字节数组  
        MemoryStream stream = new MemoryStream();
        workbook.Write(stream);
         buf = stream.ToArray();

        /*用以下代买可以直接从内存生成文件下载，无需生成临时文件
         *Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
         *Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
        */
        return buf;
      
    }
    public static void DatatableToExcelFile(DataTable dt, string file)
    {
        byte[] buf = DatatableToExcel(dt, file);
        //保存为Excel文件  
        using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
        {
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
        }
    }
    public static bool IsNullOrWhiteSpace(string value)
    {
        if (value == null)
            return true;
        for (int index = 0; index < value.Length; ++index)
        {
            if (!char.IsWhiteSpace(value[index]))
                return false;
        }
        return true;
    }


}