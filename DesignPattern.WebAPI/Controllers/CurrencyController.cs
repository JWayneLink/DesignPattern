using DesignPattern.BLL;
using DesignPattern.BLL.Base;
using DesignPattern.Model.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.WebAPI.Controllers
{
    [ApiController, Route("api/currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly IBusinessLogicFactory _factory;
        public CurrencyController(IBusinessLogicFactory factory) : base()
        {
            this._factory = factory;
        }


        [HttpGet, Produces("application/json"), Route("GetRegion")]
        public ActionResult<IEnumerable<Currency>> GetCurrency()
        {
            try
            {
                return Ok(_factory.GetLogic<ICurrencyLogic>().ReadCurrency());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost, Produces("application/json"), Route("CreateCurrency")]
        public ActionResult CreateCurrency(string currencyCode, string name, DateTime createDate)
        {
            try
            {
                _factory.GetLogic<ICurrencyLogic>().CreateCurrency(currencyCode, name, createDate);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPatch, Produces("application/json"), Route("UpdateCurrency")]
        public ActionResult<IEnumerable<Region>> UpdateCurrency(string currencyCode, string name, DateTime modifiedDate)
        {
            try
            {
                _factory.GetLogic<ICurrencyLogic>().UpdateCurrency(currencyCode, name, modifiedDate);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete, Produces("application/json"), Route("DeleteCurrency")]
        public ActionResult<IEnumerable<Region>> DeleteCurrency(string currencyCode)
        {
            try
            {
                _factory.GetLogic<ICurrencyLogic>().DeleteCurrency(currencyCode);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
