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
        XmlDocument doc;

        XmlNodeList webServiceList;
        XmlNode currentWebService;
        string currentWebServiceString;

        XmlNodeList methodList;
        XmlNode currentMethod;
        string currentMethodString;

        Dictionary<string, Control> inputDictionary;
        Dictionary<string, Control> outputDictionary;

        public WebServiceApp()
        {
            InitializeComponent();
            ReadAndParseXML();
        }

        private void ReadAndParseXML()
        {
            doc = new XmlDocument();
            doc.Load("C:\\config.xml");

            webServiceList = doc.SelectNodes("/Configuration/WebService");
            comboBox_webServiceSelector.Items.Clear();
            foreach (XmlNode webService in webServiceList)
            {
                comboBox_webServiceSelector.Items.Add(webService.Attributes["name"].InnerText.ToString());
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
            currentWebServiceString = comboBox_webServiceSelector.Items[comboBox_webServiceSelector.SelectedIndex].ToString();
            currentWebService = doc.SelectSingleNode("/Configuration/WebService[@name='" + currentWebServiceString + "']");
            methodList = currentWebService.SelectNodes("./Method");
            comboBox_methodSelector.Items.Clear();
            foreach (XmlNode method in methodList)
            {
                comboBox_methodSelector.Items.Add(method.Attributes["name"].InnerText.ToString());
            }
            comboBox_methodSelector.SelectedIndex = 0;
        }

        private void comboBox_methodSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentMethodString = comboBox_methodSelector.Items[comboBox_methodSelector.SelectedIndex].ToString();
            currentMethod = currentWebService.SelectSingleNode("./Method[@name='" + currentMethodString + "']");
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Request UI
            Button submitButton = new Button();
            groupBox_request.Controls.Add(submitButton);
            submitButton.Location = new Point(76, 25);
            submitButton.Text = "Send";
            submitButton.Click += new System.EventHandler(submitButton_Clicked);

            inputDictionary = new Dictionary<string, Control>();
            XmlNodeList inputList = currentMethod.SelectNodes("./Request/Parameter");
            RenderUIFromParameterList(groupBox_request, inputList, inputDictionary);

            // Response UI
            outputDictionary = new Dictionary<string, Control>();
            XmlNodeList outputList = currentMethod.SelectNodes("./Response/Parameter");
            RenderUIFromParameterList(groupBox_response, outputList, outputDictionary);
        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            // Send xml to webservice
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
        }

        private void RenderUIFromParameterList(GroupBox targetGroupBox, XmlNodeList parameterList, Dictionary<string, Control> parameterDict)
        {
            int i = 55;
            foreach (XmlNode parameter in parameterList)
            {
                Label lbl = new Label();
                lbl.Text = parameter.Attributes["name"].InnerText;
                Control ctrl = new Control();

                switch(parameter.Attributes["inputType"].InnerText)
                {
                    case "textBox":
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
                parameterDict.Add(parameter.Attributes["name"].InnerText, ctrl);
            }

        }
    }
}
