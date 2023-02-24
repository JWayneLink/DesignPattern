using DesignPattern.DAL.Base;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BLL.Base
{
    public interface IBusinessLogicFactory : IDisposable
    {
        TLogic GetLogic<TLogic>();
    }
    public class BusinessLogicFactory : IBusinessLogicFactory
    {
        private IKernel _kernel;
        private readonly string RepositoryFactory = nameof(RepositoryFactory);

        public BusinessLogicFactory()
        {
            if (this._kernel == null)
            {
                this._kernel = new StandardKernel();
            }

            _kernel
                .Bind<IBusinessLogicFactory>()
                .ToConstant(this)
                .InSingletonScope();
            _kernel
                .Bind<IRegionLogic>()
                .To<RegionLogic>()
                .InSingletonScope()
                .WithConstructorArgument(RepositoryFactory, context => null);
            _kernel
                .Bind<ICurrencyLogic>()
                .To<CurrencyLogic>()
                .InSingletonScope()
                .WithConstructorArgument(RepositoryFactory, context => null);
        }

        public void Dispose()
        {
            if (this._kernel != null)
            {
                this._kernel.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public TLogic GetLogic<TLogic>()
        {
            return _kernel.Get<TLogic>();
        }

        public TLogic GetLogic<TLogic>(IRepositoryFactory RepositoryFactory = null)
        {
            var inject = new IParameter[]
            {
                new ConstructorArgument(nameof(RepositoryFactory), RepositoryFactory),
            };

            return _kernel.Get<TLogic>(inject);
        }
    }
}
