using DesignPattern.Model;
using DesignPattern.BLL.Base;
using DesignPattern.DAL;
using DesignPattern.DAL.Base;
using DesignPattern.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BLL
{
    public interface IRegionLogic : IDataDrivenLogic
    {
        IEnumerable<Region> ReadRegion();
        void CreateRegion(int regionId, string regionDescription);
        void UpdateRegion(int regionId, string regionDescription);
        void DeleteRegion(int regionId);
    }
    internal class RegionLogic : DataDrivenLogic, IRegionLogic
    {
        public RegionLogic(IRepositoryFactory RepositoryFactory = null) : base(RepositoryFactory)
        {
        }

        public IEnumerable<Region> ReadRegion()
        {
            var dbContext = CreateSqlRepository<IRegionRepository>(Database.Northwind);
            return dbContext.Read();
        }

        public void CreateRegion(int regionId, string regionDescription)
        {
            var dbContext = CreateSqlRepository<IRegionRepository>(Database.Northwind);
            dbContext.Create(regionId, regionDescription);
        }

        public void UpdateRegion(int regionId, string regionDescription)
        {
            var dbContext = CreateSqlRepository<IRegionRepository>(Database.Northwind);
            dbContext.Update(regionId, regionDescription);
        }

        public void DeleteRegion(int regionId)
        {
            var dbContext = CreateSqlRepository<IRegionRepository>(Database.Northwind);
            dbContext.Delete(regionId);
        }
    }
}
