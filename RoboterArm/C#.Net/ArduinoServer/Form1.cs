using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;


namespace ArduinoServer
{
    public partial class Form : System.Windows.Forms.Form
    {
        SerialPort COMPort;
        bool connected = false;

        public Form()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                COMPort = new SerialPort(cmbPort.Text.Trim(), 9600);
                COMPort.ErrorReceived += new SerialErrorReceivedEventHandler(COMPort_ErrorReceived);
                COMPort.Open();
                connected = true;
                MessageBox.Show(COMPort.PortName.ToString() + " üzerinden Baglanti kuruldu");
                btnConnect.BackColor = Color.Green;
                btnConnect.Enabled = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Baglanti kurulamadi...");
                return;
            }

        }

        void COMPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        //private void btnSend_Click(object sender, EventArgs e)
        //{
        //    char[] buffer = new char[3];
        //    buffer[0] = '#';
        //    buffer[1] = Convert.ToChar(Convert.ToInt16(txtServo.Text));
        //    buffer[2] = Convert.ToChar(Convert.ToInt16(txtValue.Text));

        //    COMPort.Write(buffer, 0, 3);
        //}

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (connected)
            {

            }
        }




        private void SendData( int servo, int value)
        {
            try
            {
                byte[] buffer = new byte[3];
                buffer[0] = 35;
                buffer[1] = Convert.ToByte(servo);
                buffer[2] = Convert.ToByte(value);
                COMPort.Write(buffer, 0, 3);
                this.Text = "Giden Veri: "+buffer[0]+ '-' + buffer[1] + '-' + buffer[2].ToString();

                //Bu alt kisim Program üzerindeki Labellerde giden Verileri göstermek icin´baska önemi yok
                switch (servo)
                {
                    case 1:
                        label3.Text = "Servo: " + servo + "; value: " + value;
                        hScrollBar1.Value = value;
                        break;
                    case 2:
                        label4.Text = "Servo: " + servo + "; value: " + value;
                        vScrollBar1.Value = value;
                        break;
                    case 3:
                        label5.Text = "Servo: " + servo + "; value: " + value;
                        vScrollBar2.Value = value;
                        break;
                    case 4:
                        label6.Text = "Servo: " + servo + "; value: " + value;
                        vScrollBar3.Value = value;
                        break;
                    case 5:
                        label7.Text = "Servo: " + servo + "; value: " + value;
                        vScrollBar4.Value = value;
                        break;
                    default:

                        break;

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Önce baglanti kurunuz");
            }
            
        }


        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(1, hScrollBar1.Value);

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        //private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        //{

        //    if (connected == true)
        //    {
        //        SendData(2, vScrollBar1.Value);

        //    }
        //    else
        //    {
        //        MessageBox.Show("Önce baglanti kurunuz");
        //    }
        //}

        private void vScrollBar3_ValueChanged(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(4, vScrollBar4.Value);
            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                SendData(7, 95); // burda 95 önemsiz

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                SendData(8, 95); // burda 95 önemsiz
            
            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                SendData(9, 95); // burda 95 önemsiz
            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }


        private void vScrollBar2_ValueChanged_1(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(3, vScrollBar2.Value);
                //label5.Text = "Servo: " + 3 + "; value: " + vScrollBar2.Value.ToString();

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void vScrollBar4_ValueChanged(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(5, vScrollBar4.Value);
                //label7.Text = "Servo: " + 5 + "; value: " + vScrollBar4.Value.ToString();
            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void vScrollBar3_ValueChanged_1(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(4, vScrollBar3.Value);
                //label6.Text = "Servo: " + 4 + "; value: " + vScrollBar3.Value.ToString();

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }



 

        private void vScrollBar1_ValueChanged_1(object sender, EventArgs e)
        {

            if (connected == true)
            {
                SendData(2, vScrollBar1.Value);

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connected == true)
            {
                SendData(6, 95);

            }
            else
            {
                MessageBox.Show("Önce baglanti kurunuz");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cmbPort.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())

                cmbPort.Items.Add(s);
            cmbPort.SelectedIndex = 0;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {


                if(COMPort.IsOpen)
                {
                    
                    COMPort.Close();
                    COMPort.Dispose();
                   
                    MessageBox.Show(COMPort.PortName.ToString() + " ile baglanti kesildi");
                    btnConnect.BackColor = Color.Red;
                    btnConnect.Enabled = true;
                }
            }
            catch (Exception)
            {
                return;
                
            } 
        }
    }
}
