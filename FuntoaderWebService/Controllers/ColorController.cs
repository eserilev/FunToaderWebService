using FuntoaderWebService.Models;
using FuntoaderWebService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FuntoaderWebService.Controllers
{
    public class ColorController : ApiController
    {
        private readonly ColorService colorService;

        public ColorController(ColorService colorService)
        {
            this.colorService = colorService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendColorMessage([FromBody]ColorRequest colorRequest)
        {
            try
            {
                var m = colorService.SendArgbColorCommand(colorRequest);
                return Ok(m);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
