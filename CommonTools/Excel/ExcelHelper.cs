using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace CommonTools.Excel
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Excel行高，Height的单位是1/20个点。例：设置高度为25个点
        /// </summary>
        private static short rowHeight = 20 * 20;

        #region 导出Excel

        #region 导出

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="strHeader">列名</param>
        /// <param name="fileName">绝对路径</param>
        /// <param name="sheetName">sheet页名</param>
        public static void Export(DataTable dt, string strHeader, string fileName, string sheetName = "Sheet1")
        {
            try
            {
                // 使用 NPOI 组件导出 Excel 文件

                //XSSFWorkbook:是操作Excel2007的版本，扩展名是.xlsx
                XSSFWorkbook workbook = new XSSFWorkbook();

                //HSSFWorkbook:是操作Excel2003以前（包括2003）的版本，扩展名是.xls
                //HSSFWorkbook workbook = new HSSFWorkbook();

                //创建Sheet
                ISheet sheet = workbook.CreateSheet(sheetName);

                //对列名拆分
                string[] strArry = strHeader.Split(',');

                //设置单元格样式
                ICellStyle style = SetCellStyle(workbook);

                //创建表格
                NPOICreateTable(dt, strArry, sheet, style, 0);

                // 将 Excel 文件保存到磁盘
                NPOISaveFile(workbook, fileName);

                // 释放资源
                //workbook.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 自定义框架，导出

        /// <summary>
        /// 自定义顶部，导出
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="strArry">拆分后列名</param>
        /// <param name="fileName">绝对路径，路径+文件名+后缀</param>
        /// <param name="num">新一行索引，开始</param>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        public static void Export(DataTable dt, string[] strArry, string fileName, int num, XSSFWorkbook workbook, ISheet sheet)
        {
            //设置单元格样式
            ICellStyle style = SetCellStyle(workbook);

            //创建表格
            NPOICreateTable(dt, strArry, sheet, style, num);

            // 将 Excel 文件保存到磁盘
            NPOISaveFile(workbook, fileName);

            // 释放资源
            //workbook.Dispose();
        }

        #endregion

        #endregion

        #region 读取Excel
        /// <summary>
        /// 读取Excel文件到DataTable中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();

            XSSFWorkbook xssfworkbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                xssfworkbook = new XSSFWorkbook(file);
            }
            ISheet sheet = xssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        #endregion

        #region 获取用户选择保存路径

        /// <summary>
        /// 获取让用户选择保存文件的绝对路径
        /// </summary>
        /// <returns></returns>
        //public static string GetSaveFileRoute(string fileName, string filter = "Excel(*.xlsx)|*.xlsx", string initialDir = "")
        //{
        //    SaveFileDialog dialog = new SaveFileDialog();
        //    try
        //    {
        //        dialog.Filter = filter;
        //        dialog.InitialDirectory = string.IsNullOrWhiteSpace(initialDir) ? $"{Environment.CurrentDirectory}" : initialDir;
        //        dialog.FileName = fileName;
        //        var res = dialog.ShowDialog();
        //        if (res == DialogResult.Yes || res == DialogResult.OK)
        //        {
        //            if (dialog.OverwritePrompt)
        //                File.Delete(dialog.FileName);

        //            return dialog.FileName;
        //        }

        //        return string.Empty;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        dialog.Dispose();
        //    }
        //}

        #endregion

        #region 保存文件

        private static string NPOISaveFile(XSSFWorkbook workbook, string fileName)
        {
            try
            {
                // 将 Excel 文件保存到磁盘
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }

                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 创建表格

        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="strArry">拆分后的列名</param>
        /// <param name="sheet">Sheet页</param>
        /// <param name="style">样式</param>
        /// <param name="num">行索引</param>
        private static void NPOICreateTable(DataTable dt, string[] strArry, ISheet sheet, ICellStyle style, int num = 0)
        {
            #region 创建列名

            //在索引 num 的位置 创建一行
            IRow row = sheet.CreateRow(num);
            row.Height = rowHeight;//设置行高

            //循环列名数组，创建单元格并赋值、样式
            for (int i = 0; i < strArry.Length; i++)
            {
                row.CreateCell(i).SetCellValue(strArry[i]);
                row.GetCell(i).CellStyle = style;
            }

            #endregion

            #region 创建行

            //循环数据源 创建行
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建行
                row = sheet.CreateRow(num + 1);
                row.Height = rowHeight;//设置行高
                num++;//行索引自增

                //循环数据源列集合，创建单元格
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string ValueType = "";//值类型
                    string Value = "";//值
                                      //类型 和 值，赋值
                    if (dt.Rows[i][j].ToString() != null)
                    {
                        ValueType = dt.Rows[i][j].GetType().ToString();
                        Value = dt.Rows[i][j].ToString();
                    }

                    //根据不同数据类型，对数据处理。处理后创建单元格并赋值 和 样式
                    switch (ValueType)
                    {
                        case "System.String"://字符串类型
                            row.CreateCell(j).SetCellValue(Value);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(Value, out dateV);
                            row.CreateCell(j).SetCellValue(dateV.ToString("yyyy-MM-dd"));
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(Value, out boolV);
                            row.CreateCell(j).SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(Value, out intV);
                            row.CreateCell(j).SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            if (double.TryParse(Value, out double doubV))
                                row.CreateCell(j).SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            row.CreateCell(j).SetCellValue("");
                            break;
                        default:
                            row.CreateCell(j).SetCellValue("");
                            break;
                    }
                    row.GetCell(j).CellStyle = style;
                    //设置宽度
                    //sheet.SetColumnWidth(j, (Value.Length + 10) * 256);
                }
            }

            #endregion

            //循环列名数组，多所有列 设置 自动列宽
            for (int i = 0; i < strArry.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        #endregion

        #region 设置单元格样式

        public static ICellStyle SetCellStyle(XSSFWorkbook workbook)
        {
            #region 单元格样式

            //创建一个样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//水平对齐
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直对齐

            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//下边框为细线边框
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;//左边框
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;//上边框
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;//右边框

            #endregion

            return style;
        }

        #endregion
    }
}
