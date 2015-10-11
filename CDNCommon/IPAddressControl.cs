using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace CDN
{
    public partial class IPAddressControl : UserControl
    {
        public IPAddressControl()
        {
            InitializeComponent();
        }

        public bool ReadOnly
        {
            get
            {
                return firstTextBox.ReadOnly && 
                    secondTextBox.ReadOnly && 
                    thirdTextBox.ReadOnly && 
                    fourthTextBox.ReadOnly && 
                    portTextBox.ReadOnly;
            }

            set
            {
                firstTextBox.ReadOnly = value;
                secondTextBox.ReadOnly = value;
                thirdTextBox.ReadOnly = value;
                fourthTextBox.ReadOnly = value;
                portTextBox.ReadOnly = value;
            }
        }

        public IPEndPoint Value
        {
            get
            {
                IPAddress ip;
                String address = firstTextBox.Text + "." + 
                    secondTextBox.Text + "." +
                    thirdTextBox.Text + "." + 
                    fourthTextBox.Text;            
                if(IPAddress.TryParse(address, out ip))
                {
                    int port = int.Parse(portTextBox.Text);
                    return new IPEndPoint(ip, port);
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if(value != null)
                {
                    byte[] bytes = value.Address.GetAddressBytes();
                    firstTextBox.Text = bytes[0].ToString();
                    secondTextBox.Text = bytes[1].ToString();
                    thirdTextBox.Text = bytes[2].ToString();
                    fourthTextBox.Text = bytes[3].ToString();
                    portTextBox.Text = value.Port.ToString();
                }
            }
        }

        public override string Text
        {
            get
            {
                return this.Value.ToString();
            }

            set
            {
                if(value != null)
                {
                    IPAddress ip;
                    string[] context = value.Split(':');
                    if (IPAddress.TryParse(context[0], out ip))
                    {
                        int port = int.Parse(context[1]);
                        this.Value = new IPEndPoint(ip, port);
                    }
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char KeyChar = e.KeyChar;
            int TextLength = (sender as TextBox).TextLength;

            if (KeyChar == '.' || KeyChar == '。' || KeyChar == ' ' || KeyChar == ':')
            {
                if ((((sender as TextBox)).SelectedText.Length == 0) && (TextLength > 0) && (((sender as TextBox)) != portTextBox))
                {
                    SendKeys.Send("{Tab}");
                }

                e.Handled = true;
            }

            if (Regex.Match(KeyChar.ToString(), "[0-9]").Success)
            {
                if((sender as TextBox) != portTextBox)
                {
                    if (TextLength == 2)
                    {
                        if (int.Parse(((sender as TextBox)).Text + e.KeyChar.ToString()) > 255)
                        {
                            e.Handled = true;
                        }
                    }
                    else if (TextLength == 0)
                    {
                        if (KeyChar == '0')
                        {
                            e.Handled = true;
                        }
                    }
                }

            }
            else
            {
                if (KeyChar == '\b')
                {
                    if (TextLength == 0)
                    {
                        if ((sender as TextBox) != firstTextBox)
                        {
                            SendKeys.Send("+{TAB}{End}");
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}