using DesignPattern.BLL.Base;
using DesignPattern.DAL;
using DesignPattern.DAL.Base;
using DesignPattern.Model;
using DesignPattern.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BLL
{
    public interface ICurrencyLogic : IDataDrivenLogic
    {
        IEnumerable<Currency> ReadCurrency();
        void CreateCurrency(string currencyCode, string name, DateTime createDate);
        void UpdateCurrency(string currencyCode, string name, DateTime modifiedDate);
        void DeleteCurrency(string currencyCode);
    }
    internal class CurrencyLogic: DataDrivenLogic, ICurrencyLogic
    {
        public CurrencyLogic(IBusinessLogicFactory BusinessLogicFactory, IRepositoryFactory RepositoryFactory = null) : base(BusinessLogicFactory, RepositoryFactory)
        {
        }

        public void CreateCurrency(string currencyCode, string name, DateTime createDate)
        {
            var dbContext = CreateSqlRepository<ICurrencyRepository>(Database.AdventureWorks2012);
            
            dbContext.Create(currencyCode, name, createDate);
        }

        public void DeleteCurrency(string currencyCode)
        {
            var dbContext = CreateSqlRepository<ICurrencyRepository>(Database.AdventureWorks2012);
            dbContext.Delete(currencyCode);
        }

        public IEnumerable<Currency> ReadCurrency()
        {
            var dbContext = CreateSqlRepository<ICurrencyRepository>(Database.AdventureWorks2012);
            return dbContext.Read();
        }

        public void UpdateCurrency(string currencyCode, string name, DateTime modifiedDate)
        {
            var dbContext = CreateSqlRepository<ICurrencyRepository>(Database.AdventureWorks2012);
            dbContext.Update(currencyCode, name, modifiedDate);
        }
    }
}
