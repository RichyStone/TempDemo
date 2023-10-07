using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTools.McsFile.Word
{
    /// <summary>
    /// 段落设定
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

        /// <summary>
        /// 是否倾斜，默认不倾斜
        /// </summary>
        public bool HasItalic { get; set; } = false;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public ParagraphAlignment ParagraphAlignment { get; set; }

        /// <summary>
        /// 下划线
        /// </summary>
        public UnderlinePatterns UnderlinePatterns { get; set; }
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