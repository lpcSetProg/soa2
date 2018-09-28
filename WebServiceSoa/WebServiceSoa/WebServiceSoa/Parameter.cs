using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebServiceSoa
{
    class Parameter
    {
        public TypeCode type;
        public string name;

        public Parameter(XmlNode paramNode)
        {
            name = paramNode.Attributes["name"].InnerText;
            string typeStr = paramNode.Attributes["type"].InnerText;
            switch (typeStr)
            {
                case "s:int":
                    type = TypeCode.Int32;
                    break;

                default:
                    // Should probably throw an exception in here
                    type = TypeCode.Empty;
                    break;
            }
        }
    }
}
