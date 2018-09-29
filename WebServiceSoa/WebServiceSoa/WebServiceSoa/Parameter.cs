using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WebServiceSoa
{
    class Parameter
    {
        public string name;
        private string val;
        public TypeCode type;
        public Control control;
        public bool isComplex;
        public bool isArray;
        public XmlNodeList elements;
        public string link;

        public string Value
        {
            get
            {
                string ret = val;
                if (control.GetType() == typeof(TextBox))
                {
                    ret = control.Text;
                } else if (control.GetType() == typeof(DateTimePicker))
                {
                    ret = ((DateTimePicker)control).Text + "T00:00:00";
                } else
                {
                    // if not textbox or datetimepicker
                }
                return ret;
            }
            set
            {
                val = value;
            }
        }
        public Parameter(XmlNode paramNodeFromConfig, string random)
        {
            isComplex = paramNodeFromConfig.Attributes["isComplex"] != null
                ? (paramNodeFromConfig.Attributes["isComplex"].InnerText == "true" ? true : false)
                : false;
            name = paramNodeFromConfig.Attributes["name"].InnerText;
            string typeStr = paramNodeFromConfig.Attributes["type"].InnerText;
            string controlStr = paramNodeFromConfig.Attributes["control"].InnerText;
            isArray = paramNodeFromConfig.Attributes["isArray"] != null
                ? (paramNodeFromConfig.Attributes["isArray"].InnerText == "true" ? true : false)
                : false;

            switch (typeStr)
            {
                case "s:int":
                    type = TypeCode.Int32;
                    break;

                case "s:string":
                    type = TypeCode.String;
                    break;

                case "s:decimal":
                    type = TypeCode.Double;
                    break;

                case "s:dateTime":
                    type = TypeCode.DateTime;
                    control = new DateTimePicker();
                    ((DateTimePicker)control).Format = DateTimePickerFormat.Custom;
                    ((DateTimePicker)control).CustomFormat = "yyyy-MM-dd";
                    break;

                case "tns:ArrayOfString":
                    type = TypeCode.String;
                    isArray = true;
                    control = new RichTextBox();
                    break;

                default:
                    // Should probably throw an exception in here
                    type = TypeCode.Empty;
                    break;
            }

            switch (controlStr)
            {
                case "RichTextBox":
                    control = new RichTextBox();
                    control.Width = 300;
                    control.Height = 400;
                    break;

                default:
                    break;
            }
            
            elements = paramNodeFromConfig.SelectNodes("./Element");
            link = paramNodeFromConfig.Attributes["link"].InnerText;
        }

        public Parameter(XmlNode paramNode)
        {
            isComplex = false;
            name = paramNode.Attributes["name"].InnerText;
            string typeStr = paramNode.Attributes["type"].InnerText;
            isArray = false;
            control = new TextBox();
            switch (typeStr)
            {
                case "s:int":
                    type = TypeCode.Int32;
                    break;

                case "s:string":
                    type = TypeCode.String;
                    break;

                case "s:decimal":
                    type = TypeCode.Double;
                    break;

                case "s:dateTime":
                    type = TypeCode.DateTime;
                    control = new DateTimePicker();
                    ((DateTimePicker)control).Format = DateTimePickerFormat.Custom;
                    ((DateTimePicker)control).CustomFormat = "yyyy-MM-dd";
                    break;

                case "tns:ArrayOfString":
                    type = TypeCode.String;
                    isArray = true;
                    control = new RichTextBox();
                    break;
                
                default:
                    // Should probably throw an exception in here
                    type = TypeCode.Empty;
                    break;
            }
        }
    }
}
