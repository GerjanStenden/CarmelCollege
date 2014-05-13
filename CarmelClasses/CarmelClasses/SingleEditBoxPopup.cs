using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarmelClasses
{
    public partial class SingleEditBoxPopup : Form
    {
        public string EditedValue { get { return txtBox_Edit.Text; } }

        public SingleEditBoxPopup()
        {
            InitializeComponent();
        }

        public void Setup(string titleText, string labelText, string currentValue)
        {
            Text = titleText;
            lbl_Edit.Text = labelText;

            txtBox_Edit.Text = currentValue;
        }
    }
}
