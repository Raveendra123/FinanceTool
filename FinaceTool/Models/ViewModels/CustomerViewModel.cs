using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models.ViewModels
{
    public class CustomerViewModel:customer
    {
        public IEnumerable<customer> GetcustomerDetails { get; set; }
    }
}