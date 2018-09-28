using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebServiceSoa
{
    class Method
    {
        public string name;
        public List<Parameter> requestParams;
        public List<Parameter> responseParams;

        public Method(XmlNode methodNode, XmlDocument wsdlFile, XmlNamespaceManager manager)
        {
            name = methodNode.Attributes["name"].InnerText;
            requestParams = new List<Parameter>();
            responseParams = new List<Parameter>();

            
            // TODO: Handle exception here: what if the wsdl file doesn't follow this structure?
            string wsdlPrefix = "/wsdl:definitions/wsdl:types/s:schema/s:element[@name='";
            string wsdlPostfix = "']/s:complexType/s:sequence/s:element";
            XmlNodeList requestParamsFromWsdl = wsdlFile.SelectNodes(wsdlPrefix + name + wsdlPostfix, manager);
            XmlNodeList responseParamsFromWsdl = wsdlFile.SelectNodes(wsdlPrefix + name + "Response" + wsdlPostfix, manager);
            foreach (XmlNode paramNode in requestParamsFromWsdl)
            {
                Parameter newParameter = new Parameter(paramNode);
                requestParams.Add(newParameter);
            }

            foreach (XmlNode paramNode in responseParamsFromWsdl)
            {
                Parameter newParameter = new Parameter(paramNode);
                responseParams.Add(newParameter);
            }
        }
    }
}
