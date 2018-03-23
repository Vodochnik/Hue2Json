﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rca.Hue2Xml
{
    public partial class MainView : Form
    {
        Controller m_Controller;

        public MainView()
        {
            #region Form initialisieren
            InitializeComponent();
            handleBridgeIp();
            setupParameterSelection();
            setAllParameters(true);
            #endregion

            m_Controller = new Controller();
        }

        /// <summary>
        /// Bridge-IP aus Einstellungen in Textbox übernehmen
        /// </summary>
        void handleBridgeIp()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.lastBridgeIp))
            {
                txt_BridgeIp.Text = Properties.Settings.Default.lastBridgeIp;
                btn_ConnectBridge.Enabled = true;
            }
        }

        void setupParameterSelection()
        {
            foreach (HueParameterEnum param in Enum.GetValues(typeof(HueParameterEnum)))
                clb_Parameter.Items.Add(param.DisplayName());
        }

        /// <summary>
        /// Checkboxen der Parameterauswahl setzen
        /// </summary>
        /// <param name="state">Checkbox-Status</param>
        void setAllParameters(bool state)
        {
            for (int i = 0; i < clb_Parameter.Items.Count; i++)
            {
                clb_Parameter.SetItemChecked(i, state);
            }
        }

        /// <summary>
        /// Abfrage der ausgewählten Parameter
        /// </summary>
        /// <returns></returns>
        HueParameterEnum getSelectedParams()
        {
            var res = HueParameterEnum.Configuration;

            foreach (var item in clb_Parameter.SelectedItems)
            {
                //TODO: !!!!!!!!!!!!!!!
            }


            foreach (HueParameterEnum param in Enum.GetValues(typeof(HueParameterEnum)))
            {
                clb_Parameter.Items.Add(param.DisplayName());
            }

            return HueParameterEnum.Configuration;
        }

        private void btn_SearchBridge_Click(object sender, EventArgs e)
        {
            var ips = m_Controller.SearchBridges();

            if (ips.Length == 1)
            {
                switch (MessageBox.Show("Es wurde im Netzwerk eine Bridge mit der IP " + ips[0] + " gefunden.\nSoll diese verbunden werden?",
                    "Bridge gefunden", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        if (m_Controller.ConnectBridge(ips[0]))
                        {
                            txt_BridgeIp.Text = ips[0];
                            Properties.Settings.Default.lastBridgeIp = ips[0];
                            Properties.Settings.Default.Save();
                            btn_ConnectBridge.Enabled = false;
                            btn_ReadParameters.Enabled = true;
                        }
                        break;
                    case DialogResult.No:
                        txt_BridgeIp.Text = ips[0];
                        btn_ConnectBridge.Enabled = true;
                        break;
                    default: //Cancel
                        return;
                }
            }
            else
            {
                //Meherere Bridges gefunden
                throw new NotImplementedException();
            }
        }

        private void btn_ConnectBridge_Click(object sender, EventArgs e)
        {
            if (m_Controller.ConnectBridge(txt_BridgeIp.Text))
            {
                Properties.Settings.Default.lastBridgeIp = txt_BridgeIp.Text;
                Properties.Settings.Default.Save();
                btn_ConnectBridge.Enabled = false;
                btn_ReadParameters.Enabled = true;
            }
        }

        private void btn_ReadParameters_Click(object sender, EventArgs e)
        {
            if (m_Controller.ReadParameters(getSelectedParams()))
            {
                if (MessageBox.Show("Parameter wurden erfolgreich ausgelesen.\nSollen diese in eine Datei gespeichert werden?",
                    "Bridge gefunden", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveFileDialog.Title = "Parameter-Datei speichern";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        m_Controller.SaveParameterFile(saveFileDialog.FileName); //TODO: Ergebnis anzeigen
                }
                else
                {
                    m_Controller.Parameters = null;
                }
            }
            
        }
    }
}
