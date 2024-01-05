using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGR.Functions.Common
{
    public class FunctionHelper
    {

        public T GetEnvironmentVariable<T>(string name)
        {
            string sValue = System.Environment.GetEnvironmentVariable(name);
            T result = (T)Convert.ChangeType(sValue, typeof(T));

            return result;
        }

        
        public string ToJosn(object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(obj, options);

            return jsonString;
        }

        
    }
}
