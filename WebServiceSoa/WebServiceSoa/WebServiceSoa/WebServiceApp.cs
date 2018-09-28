/*
* FILE : SOA_Assignment_02.cs
* PROJECT : PROG3080 - Service Oriented Architecture - Assignment 2
* PROGRAMMERS : Bobby Vu and Lev Cocarell
* FIRST VERSION : 2018-24-12
* DESCRIPTION :
* 
* 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WebServiceSoa
{
    public partial class WebServiceApp : Form
    {
        XmlDocument configFile;

        Dictionary<string, WebService> webServiceList;

        WebService currentWebService;
        Method currentMethod;

        Dictionary<string, Control> inputDictionary;
        Dictionary<string, Control> outputDictionary;

        public WebServiceApp()
        {
            InitializeComponent();
            ReadAndParseXML();
        }

        private void ReadAndParseXML()
        {
            configFile = new XmlDocument();
            configFile.Load("C:\\config.xml");
            webServiceList = new Dictionary<string, WebService>();
            XmlNodeList webServiceListFromConfig = configFile.SelectNodes("/Configuration/WebService");
            comboBox_webServiceSelector.Items.Clear();
            foreach (XmlNode webService in webServiceListFromConfig)
            {
                string webServiceName = webService.Attributes["name"].InnerText;
                string webServiceUrl = webService.Attributes["url"].InnerText;
                comboBox_webServiceSelector.Items.Add(webServiceName);

                // web service
                WebService newWebService = new WebService(webServiceName, webServiceUrl, configFile);
                
                webServiceList.Add(webServiceName, newWebService);

            }
        }


        /*
        * Method     : comboBox_webServiceSelector_SelectedIndexChanged()
        * Description: 
        * Parameters:
        * Returns:
        */
        private void comboBox_webServiceSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentWebServiceKey = comboBox_webServiceSelector.Items[comboBox_webServiceSelector.SelectedIndex].ToString();
            currentWebService = webServiceList[currentWebServiceKey];

            comboBox_methodSelector.Items.Clear();
            foreach (KeyValuePair<string, Method> method in currentWebService.methodList)
            {
                comboBox_methodSelector.Items.Add(method.Key);
            }
            comboBox_methodSelector.SelectedIndex = 0;
        }

        private void comboBox_methodSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentMethodKey = comboBox_methodSelector.Items[comboBox_methodSelector.SelectedIndex].ToString();
            currentMethod = currentWebService.methodList[currentMethodKey];

            UpdateUI();
        }

        private void UpdateUI()
        {
            groupBox_request.Controls.Clear();
            groupBox_response.Controls.Clear();
            // Request UI
            Button submitButton = new Button();
            groupBox_request.Controls.Add(submitButton);
            submitButton.Location = new Point(76, 25);
            submitButton.Text = "Send";
            submitButton.Click += new System.EventHandler(submitButton_Clicked);

            inputDictionary = new Dictionary<string, Control>();
            RenderUIFromParameterList(groupBox_request, currentMethod.requestParams, inputDictionary);

            // Response UI
            outputDictionary = new Dictionary<string, Control>();
            RenderUIFromParameterList(groupBox_response, currentMethod.responseParams, outputDictionary);
        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            CallWebService();
        }

        public void CallWebService()
        {

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(currentWebService.url, currentWebService.actionPrefix + currentMethod.name);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                

                XmlDocument result = new XmlDocument();
                result.LoadXml(soapResult);
                foreach(Parameter param in currentMethod.responseParams)
                {
                    XmlNamespaceManager manager = new XmlNamespaceManager(result.NameTable);
                    manager.AddNamespace("soap12", "http://schemas.xmlsoap.org/wsdl/soap12/");
                    richTextBox1.Text = soapResult;
                    richTextBox2.Text = "/soap12:Envelope/soap12:Body/" + currentMethod.name + "Response/" + param.name;
                    XmlNode temp = result.SelectSingleNode("/soap12:Envelope/soap12:Body/" + currentMethod.name + "Response/" + param.name, manager);
                    //outputDictionary[param.name].Text = temp.Value;
                }
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            //var url = "http://www.dneonline.com/calculator.asmx";
            //var action = "http://tempuri.org/Add";
            // Send xml to webservice
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "application/soap+xml; charset=utf-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();

            string xmlBody = "<" + currentMethod.name + " xmlns=\"" + currentWebService.actionPrefix + "\">";
            foreach (Parameter param in currentMethod.requestParams)
            {
                xmlBody += "<" + param.name + ">" + inputDictionary[param.name].Text + "</" + param.name + ">";
            }
            xmlBody += "</" + currentMethod.name + ">";
            soapEnvelopeDocument.LoadXml("<soap12:Envelope " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                "xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">" +
                "<soap12:Body>" + 
                xmlBody +
                "</soap12:Body>" +
                "</soap12:Envelope>");

            richTextBox3.Text = "<soap12:Envelope " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                "xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">" +
                "<soap12:Body>" +
                xmlBody +
                "</soap12:Body>" +
                "</soap12:Envelope>";
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        private void RenderUIFromParameterList(GroupBox targetGroupBox, List<Parameter> parameterList, Dictionary<string, Control> parameterDict)
        {
            int i = 55;
            foreach (Parameter param in parameterList)
            {
                Label lbl = new Label();
                lbl.Text = param.name;
                Control ctrl = new Control();

                switch(param.type)
                {
                    case TypeCode.Int32:
                        ctrl = new TextBox();
                        break;
                    default:
                        break;
                }

                targetGroupBox.Controls.Add(ctrl);
                targetGroupBox.Controls.Add(lbl);
                ctrl.Location = new Point(10, i + 40);
                lbl.Location = new Point(7, i + 25);

                i += 55;
                parameterDict.Add(param.name, ctrl);
            }

        }
    }
}
