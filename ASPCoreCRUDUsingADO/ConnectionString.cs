namespace ASPCoreCRUDUsingADO
{
    public static class ConnectionString
    {
        private static string cs = "Server=DREAM\\SQLEXPRESS;Database=CrudADOdb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        public static string dbcs { get => cs; }
    }
}
