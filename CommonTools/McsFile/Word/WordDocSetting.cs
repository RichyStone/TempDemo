using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTools.McsFile.Word
{
    /// <summary>
    /// 设置文档
    /// </summary>
    public class WordDocSetting
    {
        /// <summary>
        /// 文档类型，默认为A4纵向
        /// </summary>
        public PaperType PaperType { get; set; } = PaperType.A4_V;

        /// <summary>
        /// 保存地址，包含文件名
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 文档标题相关
        /// </summary>
        public ContentItemSetting TitleSetting { get; set; }

        /// <summary>
        /// 文档主要内容
        /// </summary>
        public ContentItemSetting MainContentSetting { get; set; }
    }

    /// <summary>
    /// 文档内容相关
    /// </summary>
    public class ContentItemSetting
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 主要内容
        /// </summary>
        public string MainContent { get; set; }

        /// <summary>
        /// 使用字体
        /// </summary>
        public string FontName { get; set; } = "宋体";

        /// <summary>
        /// 字体大小，默认2号字体
        /// </summary>
        public int FontSize { get; set; } = 44;

        /// <summary>
        /// 是否加粗，默认不加粗
        /// </summary>
        public bool HasBold { get; set; } = false;
    }

    /// <summary>
    /// 纸张类型
    /// </summary>
    public enum PaperType
    {
        /// <summary>
        /// A4纵向
        /// </summary>
        A4_V,

        /// <summary>
        /// A4横向
        /// </summary>
        A4_H,

        /// <summary>
        /// A5纵向
        /// </summary>
        A5_V,

        /// <summary>
        /// A5横向
        /// </summary>
        A5_H,

        /// <summary>
        /// A6纵向
        /// </summary>
        A6_V,

        /// <summary>
        /// A6横向
        /// </summary>
        A6_H
    }
}