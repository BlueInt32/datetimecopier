using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DateCopier
{
    internal class CustomApplicationContext : ApplicationContext
    {
        NotifyIcon _notifyIcon;
        const string _baseDateFormat = "yyyy-dd-MM";
        const string _baseTimeFormat = "yyyy-MM-dd hh-mm-ss";
        string _defaultDateFormat = _baseDateFormat;
        Form1 control;

        public CustomApplicationContext()
        {
            InitializeContext();
        }

        private void InitializeContext()
        {
            var components = new System.ComponentModel.Container();
            _notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = IconResources.Time,
                Text = "Set the current time to clipboard !",
                Visible = true
            };
            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            //            _notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            _notifyIcon.Click += notifyIcon_Click;

            Hotkey hk = new Hotkey();
            hk.KeyCode = Keys.Multiply;
            hk.Control = true;
            hk.Pressed += delegate
            {
                SetClipboard(DateTime.Now.ToString(_defaultDateFormat));
            };

            control = new Form1();
            control.Hide();
            hk.Register(control);
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            HandleContextMenu();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            HandleContextMenu();
        }

        private void HandleContextMenu()
        {
            _notifyIcon.ContextMenuStrip.Items.Clear();

            var item = _notifyIcon.ContextMenuStrip.Items.Add(_baseDateFormat);
            if (_defaultDateFormat == _baseDateFormat)
                ((ToolStripMenuItem)item).Checked = true;
            item.Click += DateFormat1_Click;

            var item2 = _notifyIcon.ContextMenuStrip.Items.Add(_baseTimeFormat);
            if (_defaultDateFormat == _baseTimeFormat)
                ((ToolStripMenuItem)item2).Checked = true;
            item2.Click += DateFormat2_Click;


            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            var itemExit = _notifyIcon.ContextMenuStrip.Items.Add("Exit");
            itemExit.Click += ItemExit_Click;

        }

        private void ItemExit_Click(object sender, EventArgs e)
        {
            control.Dispose();
            _notifyIcon.Dispose();
            Application.Exit();
        }

        private void DateFormat1_Click(object sender, EventArgs e)
        {
            _defaultDateFormat = _baseDateFormat;
            SetClipboard(DateTime.Now.ToString(_defaultDateFormat));
        }
        private void DateFormat2_Click(object sender, EventArgs e)
        {
            _defaultDateFormat = _baseTimeFormat;
            SetClipboard(DateTime.Now.ToString(_defaultDateFormat));
        }

        private void SetClipboard(string time)
        {
            Clipboard.SetText(time);
            _notifyIcon.ShowBalloonTip(1, time + " set to clipboard", "you can alos use ctrl + * to set clipboard value", ToolTipIcon.Info);
        }
    }
}