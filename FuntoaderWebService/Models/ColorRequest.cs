using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuntoaderWebService.Models
{
    public class ColorRequest
    {
        public int[] rgba { get; set; }
        public int displaySetting { get; set; }
    }
}