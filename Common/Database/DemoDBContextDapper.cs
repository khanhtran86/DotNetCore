namespace DotnetCoreVCB.Common.Database
{
    public class DemoDBContextDapper : DapperDatabaseContext
    {
        public DemoDBContextDapper(string connectionString) : base(connectionString)
        {
        }
    }
}
