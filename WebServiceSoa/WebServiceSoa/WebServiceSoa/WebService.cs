using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebServiceSoa
{
    class WebService
    {
        public string name;
        public Dictionary<string, Method> methodList;
        public string url;
        public string actionPrefix;
        XmlDocument wsdlFile;
        XmlNamespaceManager manager;

        public WebService(string newName, string newUrl, XmlDocument configFile)
        {
            name = newName;
            methodList = new Dictionary<string, Method>();
            url = newUrl;
            wsdlFile = new XmlDocument();

            manager = new XmlNamespaceManager(wsdlFile.NameTable);
            manager.AddNamespace("s", "http://www.w3.org/2001/XMLSchema");
            manager.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/");

            wsdlFile.Load(url + "?WSDL");
            XmlNode def = wsdlFile.SelectSingleNode("/wsdl:definitions", manager);
            actionPrefix = def.Attributes["targetNamespace"].InnerText;

            XmlNode webServiceNode = configFile.SelectSingleNode("/Configuration/WebService[@name='" + name + "']");
            XmlNodeList methodListFromConfig = webServiceNode.SelectNodes("./Method");
            foreach (XmlNode methodNode in methodListFromConfig)
            {
                // method
                string methodName = methodNode.Attributes["name"].InnerText;
                Method newMethod = new Method(methodNode, wsdlFile, manager);
                methodList.Add(methodName, newMethod);
            }

        }

    }
}
