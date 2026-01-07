using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Utils
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]

    public class IgnoreParamAttribute : Attribute
    {
    }
}
