namespace Developer_Hub_For_UWP.Presentation
{

    class insideten
    {
        public static string add_format(string address)
        {
            if (address == "") address = "https://blogs.windows.com/blog/tag/windows-insider-program/";
            else if (address.StartsWith("mspoweruser")) address = "http://" + address;
            else if (address.StartsWith("blogs")) address = "https://" + address;
            return address;
        }
        public class Pcwrp
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Pcwif
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Pcwis
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Mowrp
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Mowif
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Mowis
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Internal
        {
            public string build { get; set; }
            public string source { get; set; }
        }

        public class RootObject
        {
            public Pcwrp pcwrp { get; set; }
            public Pcwif pcwif { get; set; }
            public Pcwis pcwis { get; set; }
            public Mowrp mowrp { get; set; }
            public Mowif mowif { get; set; }
            public Mowis mowis { get; set; }
            public Internal @internal { get; set; }
        }
    }
}
