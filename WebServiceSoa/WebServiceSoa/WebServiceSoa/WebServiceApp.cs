/*
* FILE : SOA_Assignment_02.cs
* PROJECT : PROG3080 - Service Oriented Architecture - Assignment 2
* PROGRAMMERS : Bobby Vu and Lev Cocarell
* FIRST VERSION : 2018-24-12
* DESCRIPTION :
* 
* 
*/
// TODO:
// - delete outputDictionary, inputDictionary
// - take care of the special character parsing
// - get dropdown from a list
// - max/min occur
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

            RenderUIFromParameterList(groupBox_request, currentMethod.requestParams);

            // Response UI
            RenderUIFromParameterList(groupBox_response, currentMethod.responseParams);
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
                richTextBox2.Text = soapResult;
                result.LoadXml(soapResult);
                foreach (Parameter param in currentMethod.responseParams)
                {
                    XmlNamespaceManager manager = new XmlNamespaceManager(result.NameTable);
                    manager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    manager.AddNamespace("ns", currentWebService.actionPrefix);
                    XmlNode temp = result.SelectSingleNode("/soap:Envelope/soap:Body/ns:" + currentMethod.name + "Response/ns:" + param.name, manager);
                    XmlNode temp1 = temp.SelectSingleNode(".//ns:Name", manager);
                    // what if this is list box
                    if (param.control.GetType() == typeof(TextBox))
                    {
                        param.control.Text = temp.InnerText;
                    }
                    else if (param.control.GetType() == typeof(RichTextBox))
                    {
                        if (param.type == TypeCode.String)
                        {
                            //XmlNodeList tempChildren = temp.SelectNodes("./ns:string", manager);
                            //XmlNodeList tempChildren = temp.SelectNodes("./ns:Definitions/ns:Definition", manager);
                            XmlNodeList tempChildren = temp.SelectNodes("." + param.link, manager);
                            foreach (XmlNode tempChild in tempChildren)
                            {
                                if (param.isComplex)
                                {
                                    foreach (XmlNode element in param.elements)
                                    {
                                        ((RichTextBox)param.control).Text += tempChild.SelectSingleNode("." + element.Attributes["link"].InnerText, manager).InnerText + "\n";
                                    }

                                }
                                else
                                {
                                    ((RichTextBox)param.control).Text += tempChild.InnerXml + "\n";
                                }

                            }
                        }
                        else
                        {
                            // if it's an array but not string (array of int, date)
                        }

                    }

                }
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            //var url = "http://www.dneonline.com/calculator.asmx";
            //var action = "http://tempuri.org/Add";
            // Send xml to webservice
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
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
                xmlBody += "<" + param.name + ">" + param.Value + "</" + param.name + ">";
            }

            xmlBody += "</" + currentMethod.name + ">";
            soapEnvelopeDocument.LoadXml("<soap:Envelope " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<soap:Body>" +
                xmlBody +
                "</soap:Body>" +
                "</soap:Envelope>");
            richTextBox1.Text = "<soap:Envelope " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<soap:Body>" +
                xmlBody +
                "</soap:Body>" +
                "</soap:Envelope>";
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        private void RenderUIFromParameterList(GroupBox targetGroupBox, List<Parameter> parameterList)
        {
            int i = 55;
            foreach (Parameter param in parameterList)
            {
                Label lbl = new Label();
                lbl.Text = param.name;
                Control ctrl = param.control;

                targetGroupBox.Controls.Add(ctrl);
                targetGroupBox.Controls.Add(lbl);
                ctrl.Location = new Point(10, i + 40);
                lbl.Location = new Point(7, i + 25);

                i += 55;
            }

        }
    }
}
