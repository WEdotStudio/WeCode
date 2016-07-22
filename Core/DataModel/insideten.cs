namespace Core.DataModel
{

    public class insideten
    {
        public static string add_format(string address)
        {
            if (address == "") address = "https://blogs.windows.com/blog/tag/windows-insider-program/";
            else if (address.StartsWith("mspoweruser")) address = "http://" + address;
            else if (address.StartsWith("blogs")) address = "https://" + address;
            return address;
        }
        public class pcwrp
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class pcwif
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class pcwis
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class mowrp
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class mowif
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class mowis
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class Internal
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }
        public class internalservice
        {
            public string build { get; set; }
            public string version { get; set; }
            public string more { get; set; }
            public string release_date { get; set; }
        }

        public class RootObject
        {
            public pcwrp pcwrp { get; set; }
            public pcwif pcwif { get; set; }
            public pcwis pcwis { get; set; }
            public mowrp mowrp { get; set; }
            public mowif mowif { get; set; }
            public mowis mowis { get; set; }
            public Internal @internal { get; set; }
            public internalservice internalservice { get; set; }
        }
    }
}
