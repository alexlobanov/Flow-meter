using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlowMeterLibr;
using log4net;

namespace FlowMeterConnect
{
    public partial class MainForm : Form
    {
        private static readonly ILog Log = Program.log;
        static FlowMeterManager _flowMeterManager = new FlowMeterManager();
        private bool _connect = false;

        public MainForm()
        {
            InitializeComponent();

            timerConnect.Interval = 1000;
            timerConnect.Tick += TimerConnectOnTick;
            timerConnect.Start();

            timerUpdate.Interval = 1000;
            timerUpdate.Tick += TimerUpdateOnTick;
        }

        private void TimerUpdateOnTick(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void TimerConnectOnTick(object sender, EventArgs eventArgs)
        {
            _connect = ConnectToDevice();
            if (!_connect) return;
            Log.Debug("Device conneted from timer");
            timerConnect.Start();
            timerConnect.Stop();
        }


        private bool ConnectToDevice()
        {
            if (_flowMeterManager.OpenDevice())
            {
                _flowMeterManager.DeviceAttached += FlowMeterManagerOnDeviceAttached;
                _flowMeterManager.DeviceRemoved += FlowMeterManagerOnDeviceRemoved;
                _flowMeterManager.TimeChange += FlowMeterManagerOnTimeChange;
                _flowMeterManager.ConfigGet += FlowMeterManagerOnConfigGet;
                Debug.WriteLine("FlowMate found, press any key to exit.");
                return true;
            }
            else
            {
                Debug.WriteLine("Could not find a FlowMate.");
                return false;
            }
        }

        private void FlowMeterManagerOnConfigGet(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            throw new NotImplementedException();
        }

        private void FlowMeterManagerOnTimeChange(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            throw new NotImplementedException();
        }

        private void FlowMeterManagerOnDeviceRemoved(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void FlowMeterManagerOnDeviceAttached(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Установить время расходомера\nпо локальным часам?";
            string caption = "Установка времени";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Set device time.
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            comboBox6.Items.Add("Включен");
            comboBox6.Items.Add("Отключен");
            comboBox6.SelectedIndex = comboBox6.Items.IndexOf("Включен");

            comboBox7.Items.Clear();
            comboBox7.Items.Add("Включен");
            comboBox7.Items.Add("Отключен");
            comboBox7.SelectedIndex = comboBox6.Items.IndexOf("Включен");

            comboBox3.Items.Clear();
            comboBox3.Items.Add("Импульсный");
            comboBox3.Items.Add("Частотный");
            comboBox3.Items.Add("Логический");
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf("Импульсный");

            comboBox2.Items.Clear();
            comboBox2.Items.Add("Импульсный");
            comboBox2.Items.Add("Частотный");
            comboBox2.Items.Add("Логический");
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf("Импульсный");
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem.ToString() == "Отключен")
            {
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox8.Enabled = false;
                label32.Enabled = false;
                label31.Enabled = false;
                label37.Enabled = false;
                label48.Enabled = false;
                label47.Enabled = false;
                textBox26.Enabled = false;
                textBox25.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox8.Enabled = true;
                label32.Enabled = true;
                label31.Enabled = true;
                label37.Enabled = true;
                label48.Enabled = true;
                label47.Enabled = true;
                textBox26.Enabled = true;
                textBox25.Enabled = true;
            }
            adjustComboBox3();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem.ToString() == "Отключен")
            {
                comboBox2.Enabled = false;
                comboBox5.Enabled = false;
                comboBox10.Enabled = false;
                label30.Enabled = false;
                label29.Enabled = false;
                label38.Enabled = false;
                label46.Enabled = false;
                label45.Enabled = false;
                textBox24.Enabled = false;
                textBox23.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
                comboBox5.Enabled = true;
                comboBox10.Enabled = true;
                label30.Enabled = true;
                label29.Enabled = true;
                label38.Enabled = true;
                label46.Enabled = true;
                label45.Enabled = true;
                textBox24.Enabled = true;
                textBox23.Enabled = true;
            }
            adjustComboBox2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            string message = "Калибровку необходимо запускать ТОЛЬКО при условии отсутствия течения жидкости через УПР.\n\nЗапустить сейчас?";
            string caption = "Калибровка измерительной части";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Calibration start.
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            string message = "Будет выполнен возврат к\nзаводским настройкам расходомера.\n\nПродолжить?";
            string caption = "Заводские настройки";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Do factory reset.
            }
        }

        private void adjustComboBox3()
        {
            label48.Enabled = false;
            label47.Enabled = false;
            textBox26.Enabled = false;
            textBox25.Enabled = false;

            if (comboBox6.SelectedItem.ToString() == "Включен")
            {
                switch (comboBox3.SelectedItem.ToString())
                {
                    case "Импульсный":
                        label48.Enabled = true;
                        textBox26.Enabled = true;
                        label47.Enabled = true;
                        textBox25.Enabled = true;
                        break;

                    case "Частотный":
                        label48.Enabled = false;
                        textBox26.Enabled = false;
                        label47.Enabled = true;
                        textBox25.Enabled = true;
                        break;

                    case "Логический":
                        label48.Enabled = false;
                        textBox26.Enabled = false;
                        label47.Enabled = true;
                        textBox25.Enabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustComboBox3();
        }

        private void adjustComboBox2()
        {
            label46.Enabled = false;
            label45.Enabled = false;
            textBox24.Enabled = false;
            textBox23.Enabled = false;

            if (comboBox7.SelectedItem.ToString() == "Включен")
            {
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "Импульсный":
                        label46.Enabled = true;
                        textBox24.Enabled = true;
                        label45.Enabled = true;
                        textBox23.Enabled = true;
                        break;

                    case "Частотный":
                        label46.Enabled = false;
                        textBox24.Enabled = false;
                        label45.Enabled = true;
                        textBox23.Enabled = true;
                        break;

                    case "Логический":
                        label46.Enabled = false;
                        textBox24.Enabled = false;
                        label45.Enabled = true;
                        textBox23.Enabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjustComboBox2();
        }
    }
}
