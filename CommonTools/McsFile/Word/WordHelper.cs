using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CommonTools.McsFile.Word
{
    public static class WordHelper
    {

        public static void CreateNewDoc(string path)
        {
            XWPFDocument doc = new XWPFDocument();

            XWPFParagraph p1 = doc.CreateParagraph();   //向新文档中添加段落
            p1.Alignment = ParagraphAlignment.CENTER; //段落对其方式为居中

            XWPFRun r1 = p1.CreateRun();                //向该段落中添加文字
            r1.SetText("测试段落一");

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.LEFT;

            XWPFRun r2 = p2.CreateRun();
            r2.SetText("测试段落二");
            r2.FontSize = 16;//设置字体大小
            r2.IsBold = true;//设置粗体

            using (FileStream sw = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                doc.Write(sw);
                sw.Close();
            }
        }

        public static void MainMethod(string directory, string fileName)
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                string checkTime = DateTime.Now.ToString("yymmddss");//检查时间

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (var stream = new FileStream(Path.Combine(directory, $"{fileName}.docx"), FileMode.Create, FileAccess.Write))
                {

                    //创建document文档对象对象实例
                    XWPFDocument document = new XWPFDocument();

                    #region 页脚
                    document.Document.body.sectPr = new CT_SectPr();

                    var with = document.Document.body.sectPr.pgSz.w;
                    //设置为纵向
                    document.Document.body.sectPr.pgSz.w = document.Document.body.sectPr.pgSz.h;
                    document.Document.body.sectPr.pgSz.h = with;

                    //页面边距
                    document.Document.body.sectPr.pgMar.left = 800;//左边距
                    document.Document.body.sectPr.pgMar.right = 800;//右边距
                    document.Document.body.sectPr.pgMar.top = 850;//上边距
                    document.Document.body.sectPr.pgMar.bottom = 850;//下边距

                    CT_SectPr m_SectPr = document.Document.body.sectPr;
                    //创建页脚
                    CT_Ftr m_ftr = new CT_Ftr();

                    m_ftr.AddNewP().AddNewR().AddNewT().Value = "Wuxi XDC ADC Contract Proposal";
                    //创建页脚关系（footern.xml）
                    XWPFRelation Frelation = XWPFRelation.FOOTER;
                    XWPFFooter m_f = (XWPFFooter)document.CreateRelationship(Frelation, XWPFFactory.GetInstance(), document.FooterList.Count + 1);

                    //设置页脚
                    m_f.SetHeaderFooter(m_ftr);
                    m_f.SetXWPFDocument(document);
                    CT_HdrFtrRef m_HdrFtr1 = m_SectPr.AddNewFooterReference();
                    m_HdrFtr1.type = ST_HdrFtr.@default;
                    m_HdrFtr1.id = m_f.GetPackageRelationship().Id;
                    #endregion

                    #region 头部
                    int pos = 0;
                    //图片标题
                    //document.SetParagraph(ParagraphInsertImg(document, ""), 0);

                    //文本标题
                    document.SetParagraph(ParagraphInstanceSetting(document, "", true, 18, "宋体", ParagraphAlignment.CENTER), pos++);
                    //文本标题
                    document.SetParagraph(ParagraphInstanceSetting(document, "", true, 18, "宋体", ParagraphAlignment.CENTER), pos++);
                    //文本标题《项目编号》
                    document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 18, "宋体", ParagraphAlignment.CENTER), pos++);

                    //创建文档中的表格对象实例
                    XWPFTable firstXwpfTable = document.CreateTable(8, 3);//显示的行列数rows:8行,cols:3列
                    firstXwpfTable.Width = 6000;//总宽度
                    firstXwpfTable.SetColumnWidth(0, 2000); /* 设置列宽 */
                    firstXwpfTable.SetColumnWidth(1, 2000); /* 设置列宽 */
                    firstXwpfTable.SetColumnWidth(2, 2000); /* 设置列宽 */
                    firstXwpfTable.SetTopBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetBottomBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetLeftBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetRightBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetInsideHBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetInsideVBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "");
                    firstXwpfTable.SetCellMargins(0, 100, 0, 0);

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(0).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(0).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "委托方:", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(0).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"{"苏州光明生物制药有限公司"}", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(1).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(1).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(1).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(2).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(2).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(2).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(3).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(3).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(3).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"{""}", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(4).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(4).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(4).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(5).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(5).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(5).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(6).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(6).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(6).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable.GetRow(7).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "            ", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(7).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, ":", ParagraphAlignment.RIGHT, 24, true, 10));
                    //firstXwpfTable.GetRow(7).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, $"", ParagraphAlignment.LEFT, 24, false));


                    #endregion

                    #region 内容
                    //文本
                    document.SetParagraph(ParagraphInstanceSetting(document, "xxxx.", false, 10, "宋体", ParagraphAlignment.LEFT), pos++);

                    //下划线标题
                    document.SetParagraph(ParagraphInstanceSetting(document, $"xxx", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);

                    //文本
                    document.SetParagraph(ParagraphInstanceSetting(document, "xxxx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    //文本
                    document.SetParagraph(ParagraphInstanceSetting(document, @"xx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    //下划线标题
                    document.SetParagraph(ParagraphInstanceSetting(document, $"xx", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);

                    //文本
                    document.SetParagraph(ParagraphInstanceSetting(document, @"xxx。", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    //下划线标题
                    document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, "", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    （包括液体和冻干制剂）", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);


                    //下划线标题
                    document.SetParagraph(ParagraphInstanceSetting(document, $"，", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, @"", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    document.SetParagraph(ParagraphInstanceSetting(document, $"表格1", true, 12, "宋体", ParagraphAlignment.LEFT, UnderlinePatterns.None, "000000", false), pos++);
                    #endregion

                    #region 文档第一个表格对象实例
                    //创建文档中的表格对象实例
                    XWPFTable firstXwpfTable1 = document.CreateTable(2, 3);//显示的行列数rows:3行,cols:4列
                    firstXwpfTable1.Width = 3000;//总宽度
                    firstXwpfTable1.SetColumnWidth(0, 1000); /* 设置列宽 */
                    firstXwpfTable1.SetColumnWidth(1, 1000); /* 设置列宽 */

                    //Table 表格第一行展示...后面的都是一样，只改变GetRow中的行数
                    //firstXwpfTable1.GetRow(0).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, " ", ParagraphAlignment.CENTER, 24, true));
                    //firstXwpfTable1.GetRow(0).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, "xxxx", ParagraphAlignment.CENTER, 24, false));
                    //firstXwpfTable1.GetRow(0).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, "xx", ParagraphAlignment.CENTER, 24, true));

                    ////Table 表格第二行
                    //firstXwpfTable1.GetRow(1).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, "xx", ParagraphAlignment.CENTER, 24, true));
                    //firstXwpfTable1.GetRow(1).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, "xx", ParagraphAlignment.CENTER, 24, false));
                    //firstXwpfTable1.GetRow(1).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable1, "xx", ParagraphAlignment.CENTER, 24, true));

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"xx", false, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    document.SetParagraph(ParagraphInstanceSetting(document, " •    xxx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    x", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    xx ", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    xxx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    xxx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    x", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    xx", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    x", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"x", false, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"x", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, true), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $" x", false, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    document.SetParagraph(ParagraphInstanceSetting(document, " •    x", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " •    x", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);
                    document.SetParagraph(ParagraphInstanceSetting(document, " ••••••", false, 12, "宋体", ParagraphAlignment.LEFT), pos++);

                    //document.SetParagraph(ParagraphInstanceSetting(document, $"x", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);
                    //document.SetParagraph(ParagraphInstanceSetting(document, $"", true, 12, "宋体", ParagraphAlignment.LEFT, false, "", "000000", false, false), pos++);

                    //创建文档中的表格对象实例
                    XWPFTable firstXwpfTable2 = document.CreateTable(4, 3);//显示的行列数rows:3行,cols:4列
                    firstXwpfTable2.Width = 3000;//总宽度
                    firstXwpfTable2.SetColumnWidth(0, 1000); /* 设置列宽 */
                    firstXwpfTable2.SetColumnWidth(1, 1000); /* 设置列宽 */
                    firstXwpfTable2.SetColumnWidth(2, 1000); /* 设置列宽 */
                    #endregion

                    var checkPeopleNum = 0;//检查人数
                    var totalScore = 0;//总得分

                    #region 文档第二个表格对象实例（遍历表格项）
                    //创建文档中的表格对象实例
                    XWPFTable secoedXwpfTable = document.CreateTable(5, 4);//显示的行列数rows:8行,cols:4列
                    secoedXwpfTable.Width = 5200;//总宽度
                    secoedXwpfTable.SetColumnWidth(0, 1300); /* 设置列宽 */
                    secoedXwpfTable.SetColumnWidth(1, 1100); /* 设置列宽 */
                    secoedXwpfTable.SetColumnWidth(2, 1400); /* 设置列宽 */
                    secoedXwpfTable.SetColumnWidth(3, 1400); /* 设置列宽 */

                    //遍历表格标题
                    //secoedXwpfTable.GetRow(0).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "员工姓名", ParagraphAlignment.CENTER, 24, true));
                    //secoedXwpfTable.GetRow(0).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "性别", ParagraphAlignment.CENTER, 24, true));
                    //secoedXwpfTable.GetRow(0).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "年龄", ParagraphAlignment.CENTER, 24, true));
                    //secoedXwpfTable.GetRow(0).GetCell(3).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "综合评分", ParagraphAlignment.CENTER, 24, true));

                    //遍历四条数据
                    for (var i = 1; i < 5; i++)
                    {
                        //secoedXwpfTable.GetRow(i).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "小明" + i + "号", ParagraphAlignment.CENTER, 24, false));
                        //secoedXwpfTable.GetRow(i).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, "男", ParagraphAlignment.CENTER, 24, false));
                        //secoedXwpfTable.GetRow(i).GetCell(2).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, 20 + i + "岁", ParagraphAlignment.CENTER, 24, false));
                        //secoedXwpfTable.GetRow(i).GetCell(3).SetParagraph(SetTableParagraphInstanceSetting(document, firstXwpfTable, 90 + i + "分", ParagraphAlignment.CENTER, 24, false));

                        checkPeopleNum++;
                        totalScore += 90 + i;
                    }

                    #endregion

                    #region 文档第三个表格对象实例
                    //创建文档中的表格对象实例
                    XWPFTable thirdXwpfTable = document.CreateTable(5, 4);//显示的行列数rows:5行,cols:4列
                    thirdXwpfTable.Width = 5200;//总宽度
                    thirdXwpfTable.SetColumnWidth(0, 1300); /* 设置列宽 */
                    thirdXwpfTable.SetColumnWidth(1, 1100); /* 设置列宽 */
                    thirdXwpfTable.SetColumnWidth(2, 1400); /* 设置列宽 */
                    thirdXwpfTable.SetColumnWidth(3, 1400); /* 设置列宽 */
                    //Table 表格第一行，后面的合并3列(注意关于表格中行合并问题，先合并，后填充内容)
                    thirdXwpfTable.GetRow(0).MergeCells(0, 3);//从第一列起,合并3列

                    //thirdXwpfTable.GetRow(0).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "                                                                                         " + "检查内容: " +
                    //    $"于{checkTime}下午检查了企业员工培训考核并对员工的相关信息进行了相关统计，统计结果如下：                                                                                                                                                                                                                " +
                    //    "-------------------------------------------------------------------------------------" +
                    //    $"共对该企业（{checkPeopleNum}）人进行了培训考核，培训考核总得分为（{totalScore}）分。 " + "", ParagraphAlignment.LEFT, 30, false));
                    ////Table 表格第二行
                    //thirdXwpfTable.GetRow(1).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "检查结果: ", ParagraphAlignment.CENTER, 24, true));
                    //thirdXwpfTable.GetRow(1).MergeCells(1, 3);//从第二列起，合并三列
                    //thirdXwpfTable.GetRow(1).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "该企业非常优秀，坚持每天学习打卡，具有蓬勃向上的活力。", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第三行
                    //thirdXwpfTable.GetRow(2).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "处理结果: ", ParagraphAlignment.CENTER, 24, true));
                    //thirdXwpfTable.GetRow(2).MergeCells(1, 3);
                    //thirdXwpfTable.GetRow(2).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "通过检查，评分为优秀！", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第四行，后面的合并3列(注意关于表格中行合并问题，先合并，后填充内容),额外说明
                    //thirdXwpfTable.GetRow(3).MergeCells(0, 3);//合并3列
                    //thirdXwpfTable.GetRow(3).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "备注说明: 记住，坚持就是胜利，永远保持一种求知，好问的心理！", ParagraphAlignment.LEFT, 24, false));

                    ////Table 表格第五行
                    //thirdXwpfTable.GetRow(4).MergeCells(0, 1);
                    //thirdXwpfTable.GetRow(4).GetCell(0).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "                                                                                                                                                                                                 检查人员签名：              年 月 日", ParagraphAlignment.LEFT, 30, false));
                    //thirdXwpfTable.GetRow(4).MergeCells(1, 2);

                    //thirdXwpfTable.GetRow(4).GetCell(1).SetParagraph(SetTableParagraphInstanceSetting(document, thirdXwpfTable, "                                                                                                                                                                                                 企业法人签名：              年 月 日", ParagraphAlignment.LEFT, 30, false));

                    //创建表格 
                    var col = 5;
                    XWPFTable table = document.CreateTable(1, 5);//思路，数据一行一行画
                    table.RemoveRow(0);//去掉第一行空白的
                    table.Width = 1000 * 5;
                    table.SetColumnWidth(0, 1000);/* 设置列宽 */
                    table.SetColumnWidth(1, 1000);

                    for (int i = 0; i < col - 3; i++)
                    {
                        table.SetColumnWidth(2 + i, 1000);/* 设置列宽 */
                    }

                    CT_Row nr = new CT_Row();
                    XWPFTableRow mr = new XWPFTableRow(nr, table);//创建行 
                    table.AddRow(mr);//将行添加到table中 

                    XWPFTableCell c1 = mr.CreateCell();//创建单元格
                    CT_Tc ct = c1.GetCTTc();
                    CT_TcPr cp = ct.AddNewTcPr();

                    //第1行
                    cp.AddNewVMerge().val = ST_Merge.restart;//合并行
                    cp.AddNewVAlign().val = ST_VerticalJc.center;//垂直
                    ct.GetPList()[0].AddNewPPr().AddNewJc().val = ST_Jc.center;
                    ct.GetPList()[0].AddNewR().AddNewT().Value = "序号";

                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();

                    cp.AddNewVMerge().val = ST_Merge.restart;//合并行
                    cp.AddNewVAlign().val = ST_VerticalJc.center;//垂直
                    ct.GetPList()[0].AddNewPPr().AddNewJc().val = ST_Jc.center;
                    ct.GetPList()[0].AddNewR().AddNewT().Value = "指标名称";

                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();
                    cp.gridSpan = new CT_DecimalNumber();
                    cp.gridSpan.val = Convert.ToString(col - 3); //合并列  
                    cp.AddNewVAlign().val = ST_VerticalJc.center;
                    ct.GetPList()[0].AddNewPPr().AddNewJc().val = ST_Jc.center;//单元格内容居中显示
                    ct.GetPList()[0].AddNewR().AddNewT().Value = "年龄段";


                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();

                    cp.AddNewVMerge().val = ST_Merge.restart;//合并行
                    cp.AddNewVAlign().val = ST_VerticalJc.center;//垂直
                    ct.GetPList()[0].AddNewPPr().AddNewJc().val = ST_Jc.center;
                    ct.GetPList()[0].AddNewR().AddNewT().Value = "合计";
                    //=====第一行表头结束=========


                    //2行，多行合并类似
                    nr = new CT_Row();
                    mr = new XWPFTableRow(nr, table);
                    table.AddRow(mr);

                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();
                    cp.AddNewVMerge().val = ST_Merge.@continue;//合并行 序号

                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();
                    cp.AddNewVMerge().val = ST_Merge.@continue;//合并行 指标名称
                                                               //年龄段分组
                                                               //["20岁以下","21-30","31-40","41-50","51-60","70岁以上"]
                    var alAge = new List<string>() { "20岁以下", "21-30", "31-40", "41-50", "51-60", "70岁以上" };//年龄段数组
                    for (int i = 0; i < alAge.Count; i++)
                    {
                        mr.CreateCell().SetText(Convert.ToString(alAge[i]));//年龄段单元格
                    }
                    c1 = mr.CreateCell();//创建单元格
                    ct = c1.GetCTTc();
                    cp = ct.AddNewTcPr();
                    cp.AddNewVMerge().val = ST_Merge.@continue;//合并行 合计
                    #endregion
                    //向文档流中写入内容，生成word
                    document.Write(stream);

                }
            }
            catch (Exception ex)
            {
            }

        }

        /// <summary>
        /// 读取word文档，并保存文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static bool ReadWordFileAndSave<T>(out string savePath, T models)
        {
            savePath = "";

            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string checkTime = DateTime.Now.ToString("yymmddss");//检查时间
            var uploadPath = @"D:\MyCode\Aspose.Words\Files\";
            var readPath = @"D:\MyCode\Aspose.Words\Files\Test.docx";
            string workFileName = checkTime + "Test";
            string fileName = string.Format("{0}.docx", workFileName, System.Text.Encoding.UTF8);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            using (var stream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create, FileAccess.Write))
            {
                FileStream fileStream = new FileStream(readPath, FileMode.Open, FileAccess.Read);
                //读取document文档对象对象实例
                XWPFDocument document = new XWPFDocument(fileStream);
                //写入document文档对象对象实例
                XWPFDocument newDocument = new XWPFDocument();

                newDocument = document;
                var counts = new ArrayList();
                foreach (var para in document.Paragraphs)
                {
                    counts.Add(newDocument.GetPosOfParagraph(para));
                }
                foreach (var para in document.Tables)
                {
                    counts.Add(newDocument.GetPosOfTable(para));
                }
                foreach (var c in counts)
                {
                    newDocument.RemoveBodyElement((int)c);
                }
                var i = 0;
                var ii = 0;
                //遍历段落，替换内容
                foreach (var para in document.Paragraphs)
                {
                    Type entityType = typeof(T);
                    PropertyInfo[] properties = entityType.GetProperties();
                    string paratext = para.ParagraphText;
                    string text = paratext;

                    if (!string.IsNullOrWhiteSpace(paratext))
                    {
                        foreach (var p in properties)
                        {
                            string propteryName = p.Name;//Word模板中设定的需要替换的标签
                            object value = p.GetValue(models);
                            if (value == null)
                            {
                                value = "";
                            }
                            if (text.Contains(propteryName))
                            {
                                text = text.Replace(propteryName, value.ToString());
                            }
                        }
                    }
                    if (paratext != text)
                    {
                        XWPFRun xwpfRunCopy = para.Runs[0];
                        for (var i1 = para.Runs.Count - 1; i1 >= 0; i1--)
                        {
                            para.RemoveRun(i1);
                        }
                        XWPFRun xwpfRun = para.CreateRun();//创建段落文本对象
                        xwpfRun = xwpfRunCopy;
                        xwpfRun.SetText(text);//填充内容
                    }
                    var newpara = para;
                    newDocument.SetParagraph(newpara, i);
                    i += 1;
                }
                //遍历table，替换单元格内容
                foreach (var table in document.Tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                Type entityType = typeof(T);
                                PropertyInfo[] properties = entityType.GetProperties();
                                string paratext = para.ParagraphText;
                                string text = paratext;

                                if (!string.IsNullOrWhiteSpace(paratext))
                                {
                                    foreach (var p in properties)
                                    {
                                        string propteryName = p.Name;//Word模板中设定的需要替换的标签
                                        object value = p.GetValue(models);
                                        if (value == null)
                                        {
                                            value = "";
                                        }
                                        if (text.Contains(propteryName))
                                        {
                                            text = text.Replace(propteryName, value.ToString());
                                        }
                                    }
                                }
                                if (paratext != text)
                                {
                                    XWPFRun xwpfRunCopy = para.Runs[0];
                                    for (var i1 = para.Runs.Count - 1; i1 >= 0; i1--)
                                    {
                                        para.RemoveRun(i1);
                                    }
                                    XWPFRun xwpfRun = para.CreateRun();//创建段落文本对象
                                    xwpfRun = xwpfRunCopy;
                                    xwpfRun.SetText(text);//填充内容
                                }
                            }
                        }
                    }
                    newDocument.SetTable(ii, table);
                    ii += 1;
                }

                //向文档流中写入内容，生成word
                newDocument.Write(stream);

                savePath = "/SaveWordFile/" + currentDate + "/" + fileName;
            }
            return true;
        }

        public static XWPFParagraph ParagraphInsertImg(XWPFDocument document, string imgPath, PictureType pictureType = PictureType.JPEG, ParagraphAlignment alignment = ParagraphAlignment.CENTER)
        {
            XWPFParagraph paragraph = document.CreateParagraph();//创建段落对象
            paragraph.Alignment = alignment;

            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
                                                    //标题图片
            FileStream gfs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            xwpfRun.AddPicture(gfs, (int)pictureType, Path.GetFileName(imgPath), 330 * 9525, 100 * 9525);
            gfs.Close();
            return paragraph;
        }

        /// <summary>
        /// 创建word文档中的段落对象和设置段落文本的基本样式（字体大小，字体，字体颜色，字体对齐位置）
        /// </summary>
        /// <param name="document">document文档对象</param>
        /// <param name="fillContent">段落第一个文本对象填充的内容</param>
        /// <param name="isBold">是否加粗</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="paragraphAlign">段落排列（左对齐，居中，右对齐）</param>
        /// <param name="isStatement">是否在同一段落创建第二个文本对象（解决同一段落里面需要填充两个或者多个文本值的情况，多个文本需要自己拓展，现在最多支持两个）</param>
        /// <param name="secondFillContent">第二次声明的文本对象填充的内容，样式与第一次的一致</param>
        /// <param name="fontColor">字体颜色--十六进制</param>
        /// <param name="isItalic">是否设置斜体（字体倾斜）</param>
        /// <returns></returns>
        public static XWPFParagraph ParagraphInstanceSetting(XWPFDocument document, string fillContent, bool isBold, double fontSize, string fontFamily, ParagraphAlignment paragraphAlign, UnderlinePatterns underlinePatterns = UnderlinePatterns.None, string fontColor = "000000", bool isItalic = false)
        {
            XWPFParagraph paragraph = document.CreateParagraph();//创建段落对象
            paragraph.Alignment = paragraphAlign;//文字显示位置,段落排列（左对齐，居中，右对齐）

            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
            xwpfRun.IsBold = isBold;//文字加粗
            xwpfRun.SetText(fillContent);//填充内容
            xwpfRun.FontSize = fontSize;//设置文字大小
            xwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
            xwpfRun.SetColor(fontColor);//设置字体颜色--十六进制
            xwpfRun.SetFontFamily(fontFamily, FontCharRange.None); //设置标题样式如：（微软雅黑，隶书，楷体）根据自己的需求而定
            xwpfRun.Underline = underlinePatterns;

            return paragraph;
        }

        /// <summary> 
        /// 创建Word文档中表格段落实例和设置表格段落文本的基本样式（字体大小，字体，字体颜色，字体对齐位置）
        /// </summary> 
        /// <param name="document">document文档对象</param> 
        /// <param name="table">表格对象</param> 
        /// <param name="fillContent">要填充的文字</param> 
        /// <param name="paragraphAlign">段落排列（左对齐，居中，右对齐）</param>
        /// <param name="textPosition">设置文本位置（设置两行之间的行间,从而实现表格文字垂直居中的效果），从而实现table的高度设置效果 </param>
        /// <param name="isBold">是否加粗（true加粗，false不加粗）</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色--十六进制</param>
        /// <param name="isItalic">是否设置斜体（字体倾斜）</param>
        /// <returns></returns> 
        public static XWPFParagraph SetTableParagraphInstanceSetting(XWPFTable table, string fillContent, ParagraphAlignment paragraphAlign, bool isBold = false, double fontSize = 10, string fontColor = "000000", bool isItalic = false)
        {
            var para = new CT_P();
            //设置单元格文本对齐
            para.AddNewPPr().AddNewTextAlignment();

            XWPFParagraph paragraph = new XWPFParagraph(para, table.Body);//创建表格中的段落对象
            paragraph.Alignment = paragraphAlign;//文字显示位置,段落排列（左对齐，居中，右对齐）

            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
            xwpfRun.SetText(fillContent);
            xwpfRun.FontSize = fontSize;//字体大小
            xwpfRun.SetColor(fontColor);//设置字体颜色--十六进制
            xwpfRun.IsItalic = isItalic;//是否设置斜体（字体倾斜）
            xwpfRun.IsBold = isBold;//是否加粗
            xwpfRun.SetFontFamily("宋体", FontCharRange.None);//设置字体（如：微软雅黑,华文楷体,宋体）
            return paragraph;
        }
    }
}
