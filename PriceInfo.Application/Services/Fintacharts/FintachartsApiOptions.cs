namespace PriceInfo.Application.Services.Fintacharts
{
    public class FintachartsApiOptions
    {
        public const string Section = "Fintacharts";

        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UriWss { get; set; }
    }
}
