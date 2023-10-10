using System.Collections.Generic;

namespace WpfNet6.CommonUi.MessagerTools
{
    public class MessagerTransData
    {
        public string CommondMes { get; set; }

        public int Index { get; set; }

        public object Data { get; set; }
    }

    public class MessagerTransData<T>
    {
        public string CommondMes { get; set; }

        public int Index { get; set; }

        public object Data { get; set; }

        public List<T> List { get; set; }
    }
}