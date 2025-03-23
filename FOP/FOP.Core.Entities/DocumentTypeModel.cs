using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities
{
    public class DocumentTypeModel
    {
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentTypeCode { get; set; }
        public bool? IsActive { get; set; }
    }
}
