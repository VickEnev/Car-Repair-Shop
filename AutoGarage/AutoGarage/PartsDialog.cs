﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoGarage.Controller;
using AutoGarage.ViewModels;

namespace AutoGarage
{
    public partial class PartsDialog : Form
    {
        private MiscController MiscController { get; set; }
        private bool IsFromMaintenanceCard { get; set; }
        public List<PartsViewModel> SelectedParts { get; private set; }

        public PartsDialog()
        {
            InitializeComponent();
        }

        public PartsDialog(MiscController miscController, bool isFromMaintenanceCard)
        {
            InitializeComponent();
            this.MiscController = miscController;
            this.IsFromMaintenanceCard = isFromMaintenanceCard;
            SelectedParts = new List<PartsViewModel>();
        }

        private void PartsDialog_Load(object sender, EventArgs e)
        {
            if (!IsFromMaintenanceCard)
            {
                this.Width = 551;
                lb_SelectedParts.Dispose();
                btn_addSelected.Dispose();
            }

            LoadParts();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pEditor = new PartsEditor(MiscController);
            if (pEditor.ShowDialog() == DialogResult.OK)
                LoadParts();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = (PartsViewModel)lb_AllParts.SelectedItem;
            var pEditor = new PartsEditor(MiscController, MiscController.GetPartsById(selected.Id));
            if (pEditor.ShowDialog() == DialogResult.OK)
                LoadParts();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = (PartsViewModel)lb_AllParts.SelectedItem;
            MiscController.DeletePartById(selected.Id);
            LoadParts();
        }

        private void LoadParts()
        {
            if (lb_AllParts.Items.Count > 0)
                lb_AllParts.Items.Clear();

            lb_AllParts.Items.AddRange(MiscController.GetSparePartsViewModels().ToArray());
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (IsFromMaintenanceCard)
            {
                CheckIfHasDeleted();
                SelectedParts.AddRange(lb_SelectedParts.Items.Cast<PartsViewModel>());
            }
           
        }

        private void CheckIfHasDeleted()
        {
            try
            {
                for (int i = 0; i < SelectedParts.Count; i++)
                {
                    if (MiscController.IsPartDeleted(SelectedParts[i].Id))
                    {
                        SelectedParts.RemoveAt(i);
                    }
                }
            }
            catch (NullReferenceException) { }
          
            
        }

        private void btn_addSelected_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var i in lb_AllParts.SelectedItems.Cast<PartsViewModel>())
                {
                    lb_SelectedParts.Items.Add(i);
                }
            }
            catch (NullReferenceException) { }
            
        }
    }
}
