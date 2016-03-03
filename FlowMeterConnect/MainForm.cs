using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FlowMeterLibr;
using FlowMeterLibr.Enums;
using FlowMeterLibr.Events;
using FlowMeterLibr.Structs;
using FlowMeterLibr.Сommunication;
using FlowMeterLibr.TO;
using HidLibrary;
using log4net;
using Tennis_Betfair;

namespace FlowMeterConnect
{
    public partial class MainForm : Form
    {
        private bool _connect;
        private bool _isHaveChanges;
        private int pulseIndex;
        private bool _isFirst;
        private static readonly ILog Log = Program.log;
        private static HidDevice _device;
        private static readonly FlowMeterManager _flowMeterManager = new FlowMeterManager();
        private FlowUITabs _currentTab;
        private FlowUITabs _prevTab;
        private FlowTypeWork _stateFlowWork;

        public MainForm()
        {
            InitializeComponent();

            timerConnect.Interval = 1000;
            timerConnect.Tick += TimerConnectOnTick;
            timerConnect.Start();

            timerUpdate.Interval = 1000;
            timerUpdate.Tick += TimerUpdateOnTick;
            _prevTab = FlowUITabs.None;

            buttonSaveChanges.Enabled = false;
            _isFirst = true;
        }

        private void setChangeEvents()
        {
            foreach (Control c in tabPage2.Controls)
            {
                c.TextChanged += COnTextChanged;
            }
            foreach (Control c in tabPage3.Controls)
            {
                c.TextChanged += COnTextChanged;
            }
        }

        private void unSetChangeEvents()
        {
            buttonSaveChanges.Enabled = false;
            foreach (Control c in tabPage2.Controls)
            {
                c.TextChanged -= COnTextChanged;
            }
            foreach (Control c in tabPage3.Controls)
            {
                c.TextChanged -= COnTextChanged;
            }
        }

        private void COnTextChanged(object sender, EventArgs eventArgs)
        {
            buttonSaveChanges.Enabled = true;
        }


        private void TimerUpdateOnTick(object sender, EventArgs eventArgs)
        {
            switch (_currentTab)
            {
                case FlowUITabs.DefaultTab:           
                    _device.SendDataToDevice(FlowCommands.RtcTime);
                    if (_isFirst)
                    {
                        _device.SendDataToDevice(FlowCommands.DeviceInfo);
                        _isFirst = false;
                    }
                    break;
                case FlowUITabs.SettingTab:
                    if (_prevTab == FlowUITabs.DefaultTab)
                        _device.SendDataToDevice(FlowCommands.DeviceInfoStop);
                    break;
                case FlowUITabs.ServiceTab:
                    if (_prevTab == FlowUITabs.DefaultTab)
                        _device.SendDataToDevice(FlowCommands.DeviceInfoStop);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _prevTab = _currentTab;
        }

        private void TimerConnectOnTick(object sender, EventArgs eventArgs)
        {
            _connect = ConnectToDevice();
            if (!_connect) return;
            Log.Debug("Device conneted from timer");
            timerUpdate.Start();
            timerConnect.Stop();
        }


        private void InvokeCustomEvents()
        { 
            _flowMeterManager.TimeChange += FlowMeterManagerOnTimeChange;
            _flowMeterManager.ConfigGet += FlowMeterManagerOnConfigGet;
            _flowMeterManager.CommonInfoGet += FlowMeterManagerOnCommonInfoGet;
            _flowMeterManager.TypeWork += FlowMeterManagerOnTypeWork;
            _flowMeterManager.ModBus += FlowMeterManagerOnModBus;
            _flowMeterManager.PulseCfg += FlowMeterManagerOnPulseCfg;
        }

        private void FlowMeterManagerOnPulseCfg(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            Log.Debug("[GET] PulseCfg");
            var pulseCfg = flowMeterEventArgs.State.Pulse.FlowStruct;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdatePulseUI(pulseCfg);
                }));
            }
            else
                UpdatePulseUI(pulseCfg);
        }


        private void FlowMeterManagerOnModBus(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            Log.Debug("[GET] ModBus");
            var modBusCfg = flowMeterEventArgs.State.ModBus.FlowStruct;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateModBusUI(modBusCfg);
                }));
            }
            else
            {
                UpdateModBusUI(modBusCfg);
            }


        }

        private void FlowMeterManagerOnTypeWork(object sender, FlowMeterWorkStatusEventsArgs flowMeterWorkStatusEventsArgs)
        {
            Log.Debug("[GET] Type");
            _stateFlowWork = flowMeterWorkStatusEventsArgs.TypeWork;
            if (InvokeRequired)
                Invoke(new Action(() => { UpdateSecurityModules(_stateFlowWork); }));
            else
            {
                UpdateSecurityModules(_stateFlowWork);
            }
            var strToChangeType = flowMeterWorkStatusEventsArgs.TypeWork.GetDescription();
            if (InvokeRequired)
            {
                labelTypeOfWork.Invoke(new Action(() =>
                {
                    labelTypeOfWork.Text = strToChangeType;
                }));
            }
            else
            {
                labelTypeOfWork.Text = strToChangeType;
            }
        }

        private void FlowMeterManagerOnConfigGet(object sender, FlowMeterEventArgs flowMeterEventArgs)
        { 
            Log.Debug("[GET] Config");
            var config = flowMeterEventArgs.State.Config.GetConfigStruct;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateConfigUI(config); 
                    
                }));
            }
            else
                UpdateConfigUI(config);
        }

        private void FlowMeterManagerOnTimeChange(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            // Debug.WriteLine("[GET] Time");
            Log.Debug("[GET] Time");
            if (InvokeRequired)
            {
                Invoke(new Action(() => { textBox7.Text = flowMeterEventArgs.State.DateTime.ConvertedDateTime.ToString(); }));
            }
            else
            {
                textBox7.Text = flowMeterEventArgs.State.DateTime.ConvertedDateTime.ToString();
            }
        }

        private void FlowMeterManagerOnCommonInfoGet(object sender, FlowMeterEventArgs flowMeterEventArgs)
        {
            //Debug.WriteLine("[GET] Common");
            var flowStruct = flowMeterEventArgs.State.DevInfo.FlowStruct;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateCommonInfoUI(flowStruct);
                }));
            }
            else
            {
                UpdateCommonInfoUI(flowStruct);
            }
        }

        private void UpdateCommonInfoUI(DevInfoStruct flowStruct)
        {
            unSetChangeEvents();
            qCurrentTextBox.Text = flowStruct.QCurrent1.ToString();
            vModule1TextBox.Text = flowStruct.VModule1.ToString();
            vPlusTextBox.Text = flowStruct.VPlus1.ToString();
            vMinusTextBox.Text = flowStruct.VMinus1.ToString();
            teTimeTextBox.Text = flowStruct.TeTime1.ToString();
            tpTimeTextBox.Text = flowStruct.TpTime1.ToString();
            deviceCrcTextBox.Text = flowStruct.DeviceCrc1.IntTohHexString();
            deviceSerialTextBox.Text = flowStruct.DeviceSerial.ToString();
            firmwareTextBox.Text = flowStruct.FirmwareName;
            setChangeEvents();
        }

        private void UpdatePulseUI(PulseStruct pulseCfg)
        {
            unSetChangeEvents();
            switch (pulseCfg._pusleOutNbr)
            {
                case 1:
                    pulseOutModeComboBox.SelectedIndex = pulseCfg._pulseOutMode;
                    logicUnitComboBox.SelectedIndex = pulseCfg._logicUnit;
                    pulseDescrComboBox.SelectedIndex = 1;
                    weightPulseTextBox.Text = pulseCfg._weightPulse.ToString();
                    maxFrequencyTextBox.Text = pulseCfg._maxFrequency.ToString();
                    pulseOutEnableComboBox.SelectedIndex = pulseCfg._pulseOutEnable;
                    break;
                case 2:
                    pulseOutMode2ComboBox.SelectedIndex = pulseCfg._pulseOutMode;
                    logicUnit2ComboBox.SelectedIndex = pulseCfg._logicUnit;
                    pulseDescription2ComboBox.SelectedIndex = pulseCfg._pulseDescription;
                    weightPulse2.Text = pulseCfg._weightPulse.ToString();
                    maxFreq2TextBox.Text = pulseCfg._maxFrequency.ToString();
                    pulseOutEnable2ComboBox.SelectedIndex = pulseCfg._pulseOutEnable;
                    break;
            }
            setChangeEvents();
        }

        private void UpdateConfigUI(ConfigStruct config)
        {
            unSetChangeEvents();
            dVnutTextBox.Text = (config.pipeDiamer * 1000).ToString();
            sensorDistanseTextBox.Text = (config.sensorDistance * 1000).ToString();
            angleTextBox.Text = config.angle.ToString();
            coTextBox.Text = config.CO.ToString();
            nullThresoldTextBox.Text = config.nullThresold.ToString();
            nbrValuesTextBox.Text = config.nbrValuesForAvg.ToString();
            nbrValuesForCalTextBox.Text = config.nbrValuesForCalibrates.ToString();
            comboBox1.Text = config.schemeSelect.ToString();
            setChangeEvents();
        }

        private void UpdateModBusUI(BusStruct modBusCfg)
        {
            unSetChangeEvents();
            MbSlaveAdress.Text = modBusCfg.MbSlaveAdress.ToString();
            mBbaudRateComboBox.Text = modBusCfg.MbBaudRate.ToString(); //TODO: FIX; (Selected index)
            mbparityModeComboBox.Text = modBusCfg.MbParityMode.ToString();
            setChangeEvents();
        }


        private bool ConnectToDevice()
        {
            if (_flowMeterManager.OpenDevice())
            {
                _flowMeterManager.DeviceAttached += FlowMeterManagerOnDeviceAttached;
                _flowMeterManager.DeviceRemoved += FlowMeterManagerOnDeviceRemoved;

                InvokeCustomEvents();
                labelTypeOfWork.Text = "Устройство подключенно";
                Log.Debug("[FlowMate] found");
                Debug.WriteLine("FlowMate found");
                return true;
            }
            Debug.WriteLine("Could not find a FlowMate.");
            return false;
        }


        private void FlowMeterManagerOnDeviceRemoved(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, EventArgs>(FlowMeterManagerOnDeviceRemoved), sender, eventArgs);
                return;
            }
            _connect = false;
            labelTypeOfWork.Text = "Нет подключения";
            Log.Debug("Device disconect");
            Debug.WriteLine("FlowMeter removed.");
        }

        private void FlowMeterManagerOnDeviceAttached(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, EventArgs>(FlowMeterManagerOnDeviceAttached), sender, eventArgs);
                return;
            }
            _connect = true;
            _device = _flowMeterManager.device;
            Log.Debug("Device attached");
            Debug.WriteLine("FlowMeter attached.");
        }


        private void UpdateSecurityModules(FlowTypeWork workType)
        {
            switch (workType)
            {
                case FlowTypeWork.None:
                    InitializeComponent();
                    break;
                case FlowTypeWork.ServiceWork:
                    buttonCalibrate.Enabled = true;
                    buttonFactoryReset.Enabled = true;
                    //Todo Disable timeChange

                    dVnutTextBox.Enabled = true;
                    sensorDistanseTextBox.Enabled = true;
                    angleTextBox.Enabled = true;

                    coTextBox.Enabled = true;
                    textBox17.Enabled = true;
                    nullThresoldTextBox.Enabled = true;

                    nbrValuesTextBox.Enabled = true;
                    nbrValuesForCalTextBox.Enabled = true;

                    //сервис
                    MbSlaveAdress.Enabled = true;
                    mBbaudRateComboBox.Enabled = true;
                    mbparityModeComboBox.Enabled = true;
                    //выход n1
                    comboBox6_SelectedIndexChanged(new object(), null);
                    //выход n2
                    comboBox7_SelectedIndexChanged(new object(), null);
                    break;
                case FlowTypeWork.NormalWork:
                    buttonSaveChanges.Enabled = false;
                    buttonCalibrate.Enabled = false;
                    buttonFactoryReset.Enabled = false;
                    //Todo Disable timeChange

                    dVnutTextBox.Enabled = false;
                    sensorDistanseTextBox.Enabled = false;
                    angleTextBox.Enabled = false;

                    coTextBox.Enabled = false;
                    textBox17.Enabled = false;
                    nullThresoldTextBox.Enabled = false;

                    nbrValuesTextBox.Enabled = false;
                    nbrValuesForCalTextBox.Enabled = false;

                    //сервис
                    MbSlaveAdress.Enabled = false;
                    mBbaudRateComboBox.Enabled = false;
                    mbparityModeComboBox.Enabled = false;

                    pulseOutEnableComboBox.SelectedItem = "Отключен";
                    pulseOutEnable2ComboBox.SelectedItem = "Отключен";
                    pulseOutEnableComboBox.Enabled = false;
                    pulseOutEnable2ComboBox.Enabled = false;
                    //выход n1
                    comboBox6_SelectedIndexChanged(new object(), null);
                    //выход n2
                    comboBox7_SelectedIndexChanged(new object(), null);
                    break;
                case FlowTypeWork.ErrorWork:
                    MessageBox.Show(
                        "[ВНИМАНИЕ] Авария в приборе, проверьте работу прибора и продолжите работу с программой");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(workType), workType, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = "Установить время расходомера\nпо локальным часам?";
            var caption = "Установка времени";
            var buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _device.SendDataToDevice(FlowCommands.RtcTime, new FlowDateStruct(DateTime.Now));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pulseOutEnableComboBox.Items.Clear();
            pulseOutEnableComboBox.Items.Add("Включен");
            pulseOutEnableComboBox.Items.Add("Отключен");
            pulseOutEnableComboBox.SelectedIndex = pulseOutEnableComboBox.Items.IndexOf("Включен");

            pulseOutEnable2ComboBox.Items.Clear();
            pulseOutEnable2ComboBox.Items.Add("Включен");
            pulseOutEnable2ComboBox.Items.Add("Отключен");
            pulseOutEnable2ComboBox.SelectedIndex = pulseOutEnableComboBox.Items.IndexOf("Включен");

            pulseOutModeComboBox.Items.Clear();
            pulseOutModeComboBox.Items.Add("Импульсный");
            pulseOutModeComboBox.Items.Add("Частотный");
            pulseOutModeComboBox.Items.Add("Логический");
            pulseOutModeComboBox.SelectedIndex = pulseOutModeComboBox.Items.IndexOf("Импульсный");

            pulseOutMode2ComboBox.Items.Clear();
            pulseOutMode2ComboBox.Items.Add("Импульсный");
            pulseOutMode2ComboBox.Items.Add("Частотный");
            pulseOutMode2ComboBox.Items.Add("Логический");
            pulseOutMode2ComboBox.SelectedIndex = pulseOutMode2ComboBox.Items.IndexOf("Импульсный");

            mBbaudRateComboBox.Items.Clear();
            mBbaudRateComboBox.DataSource = Enum.GetValues(typeof(FlowMBSpeed)); ;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pulseOutEnableComboBox.SelectedItem.ToString() == "Отключен")
            {
                pulseOutModeComboBox.Enabled = false;
                logicUnitComboBox.Enabled = false;
                pulseDescrComboBox.Enabled = false;
                label32.Enabled = false;
                label31.Enabled = false;
                label37.Enabled = false;
                label48.Enabled = false;
                label47.Enabled = false;
                weightPulseTextBox.Enabled = false;
                maxFrequencyTextBox.Enabled = false;
            }
            else
            {
                pulseOutModeComboBox.Enabled = true;
                logicUnitComboBox.Enabled = true;
                pulseDescrComboBox.Enabled = true;
                label32.Enabled = true;
                label31.Enabled = true;
                label37.Enabled = true;
                label48.Enabled = true;
                label47.Enabled = true;
                weightPulseTextBox.Enabled = true;
                maxFrequencyTextBox.Enabled = true;
            }
            AdjustComboBox3();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pulseOutEnable2ComboBox.SelectedItem.ToString() == "Отключен")
            {
                pulseOutMode2ComboBox.Enabled = false;
                logicUnit2ComboBox.Enabled = false;
                pulseDescription2ComboBox.Enabled = false;
                label30.Enabled = false;
                label29.Enabled = false;
                label38.Enabled = false;
                label46.Enabled = false;
                label45.Enabled = false;
                weightPulse2.Enabled = false;
                maxFreq2TextBox.Enabled = false;
            }
            else
            {
                pulseOutMode2ComboBox.Enabled = true;
                logicUnit2ComboBox.Enabled = true;
                pulseDescription2ComboBox.Enabled = true;
                label30.Enabled = true;
                label29.Enabled = true;
                label38.Enabled = true;
                label46.Enabled = true;
                label45.Enabled = true;
                weightPulse2.Enabled = true;
                maxFreq2TextBox.Enabled = true;
            }
            AdjustComboBox2();
        }

        private void CalibrationClick(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            var message = "Калибровку необходимо запускать ТОЛЬКО при условии отсутствия течения жидкости через УПР.\n\nЗапустить сейчас?";
            var caption = "Калибровка измерительной части";
            var buttons = MessageBoxButtons.YesNo;

            // Displays the MessageBox.
            var result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                _device.SendDataToDevice(FlowCommands.RunCalibrate);
                //TODO wait calibration
                // Calibration start.
            }
        }

        private void FactoryResetClick(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            var message = "Будет выполнен возврат к\nзаводским настройкам расходомера.\n\nПродолжить?";
            var caption = "Заводские настройки";
            var buttons = MessageBoxButtons.YesNo;

            // Displays the MessageBox.
            var result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _device.SendDataToDevice(FlowCommands.FactoryReset);
                //TODO wait factory reset
                // Do factory reset.
            }
        }

        private void AdjustComboBox3()
        {
            label48.Enabled = false;
            label47.Enabled = false;
            weightPulseTextBox.Enabled = false;
            maxFrequencyTextBox.Enabled = false;

            if (pulseOutEnableComboBox.SelectedItem.ToString() == "Включен")
            {
                switch (pulseOutModeComboBox.SelectedItem.ToString())
                {
                    case "Импульсный":
                        label48.Enabled = true;
                        weightPulseTextBox.Enabled = true;
                        label47.Enabled = true;
                        maxFrequencyTextBox.Enabled = true;
                        break;

                    case "Частотный":
                        label48.Enabled = false;
                        weightPulseTextBox.Enabled = false;
                        label47.Enabled = true;
                        maxFrequencyTextBox.Enabled = true;
                        break;

                    case "Логический":
                        label48.Enabled = false;
                        weightPulseTextBox.Enabled = false;
                        label47.Enabled = true;
                        maxFrequencyTextBox.Enabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdjustComboBox3();
        }

        private void AdjustComboBox2()
        {
            label46.Enabled = false;
            label45.Enabled = false;
            weightPulse2.Enabled = false;
            maxFreq2TextBox.Enabled = false;

            if (pulseOutEnable2ComboBox.SelectedItem.ToString() == "Включен")
            {
                switch (pulseOutMode2ComboBox.SelectedItem.ToString())
                {
                    case "Импульсный":
                        label46.Enabled = true;
                        weightPulse2.Enabled = true;
                        label45.Enabled = true;
                        maxFreq2TextBox.Enabled = true;
                        break;

                    case "Частотный":
                        label46.Enabled = false;
                        weightPulse2.Enabled = false;
                        label45.Enabled = true;
                        maxFreq2TextBox.Enabled = true;
                        break;

                    case "Логический":
                        label46.Enabled = false;
                        weightPulse2.Enabled = false;
                        label45.Enabled = true;
                        maxFreq2TextBox.Enabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdjustComboBox2();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (_device == null)
                return;
            switch (e.TabPageIndex)
            {
                case 0:
                    _currentTab = FlowUITabs.DefaultTab;
                    if (_device.IsConnected)
                        _device.SendDataToDevice(FlowCommands.DeviceInfo);
                    break;
                case 1:
                    _currentTab = FlowUITabs.SettingTab;
                    if ((_prevTab == FlowUITabs.DefaultTab) && ((_device.IsConnected)))
                        _device.SendDataToDevice(FlowCommands.DeviceInfoStop);
                    if (_device.IsConnected)
                        _device.SendDataToDevice(FlowCommands.MainCfg);
                    break;
                case 2:
                    _currentTab = FlowUITabs.ServiceTab;
                    if ((_prevTab == FlowUITabs.DefaultTab) && ((_device.IsConnected)))
                        _device.SendDataToDevice(FlowCommands.DeviceInfoStop);
                    if (_device.IsConnected)
                    { 
                        _device.SendDataToDevice(FlowCommands.ModBusCfg);
                        _device.SendDataToDevice(FlowCommands.PulseCfg);
                    }
            break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((_currentTab == FlowUITabs.DefaultTab) && (_device != null) && (_device.IsConnected)) //отписываемся от потоковой рассылки

                _device.SendDataToDevice(FlowCommands.DeviceInfoStop);

            if ((_device != null) && (_device.IsConnected))
                _device.CloseDevice();
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
          /*  int value;
            if (int.TryParse(coTextBox.Text, out value))
            {
                if (value > 1543)
                    coTextBox.Text = "1543";
                else if (value < 1403)
                    coTextBox.Text = "1403";
            }*/
        }


        private void AnnimateScreen()
        {
            panel1.Visible = false;
            LoadingAnimator.Wire(panel3);
        }

        private void UnAnnimateScreen()
        {
            LoadingAnimator.UnWire(panel3);
            panel1.Visible = true;
        }

        private ConfigStruct getConfigStruct()
        {
            return new ConfigStruct
            {
                pipeDiamer = float.Parse(dVnutTextBox.Text) / 1000,
                sensorDistance = float.Parse(sensorDistanseTextBox.Text),
                angle = float.Parse(angleTextBox.Text),
                CO = float.Parse(coTextBox.Text),
                nullThresold = float.Parse(nullThresoldTextBox.Text),
                nbrValuesForAvg = byte.Parse(nbrValuesTextBox.Text),
                nbrValuesForCalibrates = byte.Parse(nbrValuesForCalTextBox.Text),
                schemeSelect = 2
            };
        }

        /* if (pulseCfg._pusleOutNbr == 1) //Первый дисктерный вывод.
            {
                pulseOutModeComboBox.SelectedIndex = pulseCfg._pulseOutMode;
                logicUnitComboBox.SelectedIndex = pulseCfg._logicUnit;
                pulseDescrComboBox.SelectedIndex = 1; //TODO; FIX THIS
                weightPulseTextBox.Text = pulseCfg._weightPulse.ToString();
                maxFrequencyTextBox.Text = pulseCfg._maxFrequency.ToString();
                pulseOutEnableComboBox.SelectedIndex = pulseCfg._pulseIsConfigured;
                return;
            }
            if (pulseCfg._pusleOutNbr == 2) //второй дисктерный вывод.
            {
                pulseOutMode2ComboBox.SelectedIndex = pulseCfg._pulseOutMode;
                logicUnit2ComboBox.SelectedIndex = pulseCfg._logicUnit;
                pulseDescription2ComboBox.SelectedIndex = 1; //TODO; FIX THIS
                weightPulse2.Text = pulseCfg._weightPulse.ToString();
                maxFreq2TextBox.Text = pulseCfg._maxFrequency.ToString();
                pulseOutEnable2ComboBox.SelectedIndex = pulseCfg._pulseIsConfigured;
            }*/

     /*   private BusStruct getBusStruct()
        {
            return  new BusStruct
            {
                   
            }
        }*/

        private BusStruct getBusStruct()
        {
            return new BusStruct()
            {
                MbBaudRate = byte.Parse(mBbaudRateComboBox.SelectedIndex.ToString()),
                MbParityMode = Byte.Parse(mbparityModeComboBox.SelectedIndex.ToString()),
                MbSlaveAdress = byte.Parse(MbSlaveAdress.Text),
                MbUcPort = byte.Parse(0.ToString()) //по умолчанию 0
            };
        }

        private PulseStruct getSecondPulseStruct()
        {
            return new PulseStruct
            {
                _pusleOutNbr = 2,
                _logicUnit = byte.Parse(logicUnit2ComboBox.SelectedIndex.ToString()),
                _maxFrequency = byte.Parse(maxFreq2TextBox.Text),
                _pulseDescription = ushort.Parse(pulseDescription2ComboBox.SelectedIndex.ToString()),
                _pulseOutEnable = byte.Parse(pulseOutEnable2ComboBox.SelectedIndex.ToString()),
                _pulseOutMode = byte.Parse(pulseOutMode2ComboBox.SelectedIndex.ToString()),
                _weightPulse = byte.Parse(weightPulse2.Text)
            };
        }
        private PulseStruct getFirstPulseStruct()
        {
            return new PulseStruct
            {
                _pusleOutNbr = 1,
                _logicUnit = byte.Parse(logicUnitComboBox.SelectedIndex.ToString()),
                _maxFrequency = ushort.Parse(maxFrequencyTextBox.Text),
                _pulseDescription = ushort.Parse(pulseDescrComboBox.SelectedIndex.ToString()),
                _pulseOutEnable = byte.Parse(pulseOutEnableComboBox.Text), 
                _weightPulse = float.Parse(weightPulseTextBox.Text),
                _pulseOutMode = byte.Parse(pulseOutModeComboBox.Text)
            };
        }

        private void buttonSave_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                buttonSaveChanges.Enabled = false;
                AnnimateScreen();
                switch (_currentTab)
                {
                    case FlowUITabs.None:
                        buttonSaveChanges.Enabled = false;
                        break;
                    case FlowUITabs.DefaultTab:
                        MessageBox.Show("Изменить измерительные значения нельзя","Предупреждение");
                        break;
                    case FlowUITabs.SettingTab:
                        _device.SendDataToDevice(FlowCommands.MainCfg, getConfigStruct());
                        _device.SendDataToDevice(FlowCommands.MainCfg);
                        break;
                    case FlowUITabs.ServiceTab:
                        _device.SendDataToDevice(FlowCommands.PulseCfg, getFirstPulseStruct());
                        _device.SendDataToDevice(FlowCommands.PulseCfg, getSecondPulseStruct());
                        _device.SendDataToDevice(FlowCommands.ModBusCfg, getBusStruct());
                        //device
                        _device.SendDataToDevice(FlowCommands.PulseCfg);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _device.SendDataToDevice(FlowCommands.SaveAllSettings2memory);
                UnAnnimateScreen();
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте введённые данные и попробуйте снова", "Ошибка ввода");
            }
        }
    }
}