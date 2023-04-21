using Microsoft.Data.SqlClient;
using TableDependency.SqlClient;

namespace AppServer.Subscription
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription
        where T : class,new()
    {
        readonly IConfiguration _configuration;

        public DatabaseSubscription(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        SqlTableDependency<T> _tableDependency;
        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"), tableName);
            _tableDependency.OnChanged += _tableDependency_OnChanged;
        }

        private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<T> e)
        {
            //client ile iletişime geçme senaryo örneği
            //signalr
            //rabbitmq entegrasyonu 
        }
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }

    }
}
