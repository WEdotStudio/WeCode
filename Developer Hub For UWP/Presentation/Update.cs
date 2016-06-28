namespace Developer_Hub_For_UWP.Presentation
{

    class Update
    {
        public class detail
        {
            public string en { get; set; }
            public string zh_hant { get; set; }
            public string zhhans { get; set; }
        }

        public class RootObject
        {
            public string version { get; set; }
            public detail detail { get; set; }
        }
    }
}