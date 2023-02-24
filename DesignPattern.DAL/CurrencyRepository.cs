using Dapper;
using DesignPattern.DAL.Base;
using DesignPattern.Model.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.DAL
{
    public interface ICurrencyRepository : ISQLRepository
    {
        void Create(string currencyCode, string name, DateTime createDate);
        IEnumerable<Currency> Read();
        void Update(string currencyCode, string name, DateTime modifiedDate);
        void Delete(string currencyCode);
    }
    internal class CurrencyRepository : SQLRepository, ICurrencyRepository
    {
        public CurrencyRepository(IDbConnection connection) : base(connection)
        {
        }
        public void Create(string currencyCode, string name, DateTime createDate)
        {
            Connection.Execute($"INSERT INTO [Sales].[Currency]([CurrencyCode],[Name],[ModifiedDate]) VALUES('{currencyCode}', '{name}', '{createDate}')");
        }

        public void Delete(string currencyCode)
        {
            Connection.Execute($"DELETE [Sales].[Currency] WHERE CurrencyCode = '{currencyCode}'");
        }

        public IEnumerable<Currency> Read()
        {
            return Connection.Query<Currency>("SELECT * FROM [Sales].[Currency]");
        }

        public void Update(string currencyCode, string name, DateTime modifiedDate)
        {
            Connection.Execute($"UPDATE [Sales].[Currency] SET [Name] = '{name}', [ModifiedDate] = {modifiedDate} WHERE CurrencyCode = '{currencyCode}'");
        }
    }
}
