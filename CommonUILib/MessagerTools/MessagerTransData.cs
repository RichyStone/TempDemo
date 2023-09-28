using System.Collections.Generic;

namespace CommonUILib.MessagerTools
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