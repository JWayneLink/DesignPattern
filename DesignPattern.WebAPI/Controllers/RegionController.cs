using DesignPattern.BLL;
using DesignPattern.BLL.Base;
using DesignPattern.Model.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.WebAPI.Controllers
{
    [ApiController, Route("api/region")]
    public class RegionController : ControllerBase
    {
        private readonly IBusinessLogicFactory _factory;
        public RegionController(IBusinessLogicFactory factory):base()
        {
            this._factory = factory;
        }

        [HttpGet, Produces("application/json"), Route("GetRegion")]
        public ActionResult<IEnumerable<Region>> GetRegion()
        {
            try
            {
                return Ok(_factory.GetLogic<IRegionLogic>().ReadRegion());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost, Produces("application/json"), Route("CreateRegion")]
        public ActionResult CreateRegion(int regionId, string regionDescription)
        {
            try
            {
                _factory.GetLogic<IRegionLogic>().CreateRegion(regionId, regionDescription);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPatch, Produces("application/json"), Route("UpdateRegion")]
        public ActionResult<IEnumerable<Region>> UpdateRegion(int regionId, string regionDescription)
        {
            try
            {
                _factory.GetLogic<IRegionLogic>().UpdateRegion(regionId, regionDescription);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete, Produces("application/json"), Route("DeleteRegion")]
        public ActionResult<IEnumerable<Region>> DeleteRegion(int regionId)
        {
            try
            {
                _factory.GetLogic<IRegionLogic>().DeleteRegion(regionId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
