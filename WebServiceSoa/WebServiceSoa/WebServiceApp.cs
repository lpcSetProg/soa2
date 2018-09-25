/*
* FILE : SOA_Assignment_02.cs
* PROJECT : PROG3080 - Service Oriented Architecture - Assignment 2
* PROGRAMMERS : Lev Cocarell and Bobby Vu 
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServiceSoa
{
    public partial class WebServiceApp : Form
    {
        public WebServiceApp()
        {
            InitializeComponent();

            string v1 = System.Configuration.ConfigurationManager.AppSettings["k1"];
           
            comboBox_webServiceSelector.Items.Insert(0, v1);
           
        }


        /*
        * Method     : comboBox_webServiceSelector_SelectedIndexChanged()
        * Description: 
        * Parameters:
        * Returns:
        */
        private void comboBox_webServiceSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
    }
}
