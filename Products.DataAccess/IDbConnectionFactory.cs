using System.Data;

namespace Products.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}