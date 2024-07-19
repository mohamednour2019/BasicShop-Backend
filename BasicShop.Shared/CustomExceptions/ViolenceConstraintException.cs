using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Shared.CustomExceptions
{
    public class ViolenceConstraintException:Exception
    {
        public ViolenceConstraintException(string message):base(message) { }
    }
}
