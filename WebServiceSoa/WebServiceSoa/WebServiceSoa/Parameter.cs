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
        public TypeCode type;
        public Boolean isArray;
        public string name;
        public Control control;
        private string val;
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
        public Parameter(XmlNode paramNode)
        {
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
                    control = new ListBox();
                    break;
                
                default:
                    // Should probably throw an exception in here
                    type = TypeCode.Empty;
                    break;
            }
        }
    }
}
