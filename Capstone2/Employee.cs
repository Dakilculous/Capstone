using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone2
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        void AddForm(Form frm, Button btn)
        {
            btn.BackColor = Color.White;
            switch (btn.Name)
            {
                case "BtnHome": btn.ImageIndex = 3; break;
                case "PermaRecord": btn.ImageIndex = 2; break;
                case "BtnrequestList": btn.ImageIndex = 1; break;
                default: break;
            }
            AddOwnedForm(frm);
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            PnlPage.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        void SelectBtn(Button btn)
        {
            switch(btn.Name)
            {
                case "BtnHome": btn.ImageIndex = 3; break;
                case "PermaRecord": btn.ImageIndex = 2; break;
                case "BtnrequestList": btn.ImageIndex = 1; break;
                default: break;
            }
            btn.BackColor = Color.White;
            PnlPage.Controls.Clear();
        }

        void ResetBtn()
        {
            foreach (var btn in TLPnlMenu.Controls.OfType<Button>())
                btn.BackColor = Color.FromArgb(153, 180, 209);
            BtnHome.ImageIndex = 3;
            PermaRecord.ImageIndex = 2;
            BtnRequestList.ImageIndex = 1;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            LoginF login = new LoginF();
            login.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            ResetBtn();
            SelectBtn(BtnHome);
        }

        private void PermaRecord_Click(object sender, EventArgs e)
        {
            ResetBtn();
            SelectBtn(PermaRecord);
        }

        private void BtnRequestList_Click(object sender, EventArgs e)
        {
            ResetBtn();
            AddForm(new Frm_RequestList(), BtnRequestList);

        }

        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            LineSearch.Visible = true;
            txtSearch.BackColor = Color.FromArgb(139,199,255);
            PnlSearch.BackColor = Color.FromArgb(139,199,255);
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            LineSearch.Visible = false;
            txtSearch.BackColor = Color.FromArgb(99, 180, 255);
            PnlSearch.BackColor = Color.FromArgb(99, 180, 255);
        }

        private void PnlPage_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
