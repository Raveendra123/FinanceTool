using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class CompareDUHModel
    {
       
      public DUHModel DUHModel { get; set; }
        public List<DUHMainDetailsResult> DUHMainDetailsResult { get; set; }
        
        public List<DUHMainDetailsResult> OldDUHMainDetailsResult { get; set; }

    }
}