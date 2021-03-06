﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPA.Control;
using SPA.Entity;
using SPA.Enums;
using SPA.Cache;

namespace SPA
{
    public partial class Login : SPAForm 
    {
        private System.Drawing.Color m_tbcolorenter = System.Drawing.Color.LightSteelBlue;
        private System.Drawing.Color m_tbcolorleave = System.Drawing.Color.White;
        private System.Drawing.Color m_tbcolorerror = System.Drawing.Color.Pink;
        private Boolean m_tbcolorerrorflag = false;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox tb = null;
            if (sender is TextBox)
            {
                tb = (TextBox)sender;
                if (m_tbcolorerrorflag)
                    tb.BackColor = m_tbcolorerror;
                else
                    tb.BackColor = m_tbcolorenter;
            }

            return;

        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox tb = null;
            if (sender is TextBox)
            {
                tb = (TextBox)sender;
                tb.BackColor = m_tbcolorleave;
            }

            return;
        }

        private void TextBox_Error(object sender, EventArgs e)
        {
            TextBox tb = null;
            if (sender is TextBox)
            {
                tb = (TextBox)sender;
                tb.BackColor = m_tbcolorerror;
                tb.Focus();
                m_tbcolorerrorflag = false;
            }
            return;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserReader user = new UserReader();
            List<FilterElement> filterCollection = new List<FilterElement>();
            FilterElement filterUsername = new FilterElement();
            filterUsername.Key = Enums.Data.KeyElements.UseColumnName;
            filterUsername.ColumnName = "Username";
            filterUsername.Value = txtUsername.Text.Trim();

            FilterElement filterPassword = new FilterElement();
            filterPassword.Key = Enums.Data.KeyElements.UseColumnName;
            filterPassword.ColumnName = "Password";
            filterPassword.Value = txtPassword.Text.Trim();
            filterCollection.Add(filterUsername);
            filterCollection.Add(filterPassword);

            if (user.ReadSingle(filterCollection))
            {
                UserCache.CurrentUserID = user.SingleRecord.ID;
                UserCache.CurrentName = user.SingleRecord.Name;
                UserCache.CurrentUserName = user.SingleRecord.Username;
                UserCache.CurrentUserPassword = user.SingleRecord.Password;
                if (user.SingleRecord.UserTypeID == 1)
                {
                    UserCache.CurrentUserType = UserEnum.UserType.Administrator;                    
                }
                else
                {
                    UserCache.CurrentUserType = UserEnum.UserType.RegularUser;
                }
            }
            else if(EnvironmentCache.IsDebug())
            {
                UserCache.CurrentUserID = 1;
                UserCache.CurrentName = "DEBUGGER";
                UserCache.CurrentUserType = UserEnum.UserType.Administrator;   
            }
            else
            {
                ShowMessage("Nama pengguna atau kata laluan ini tidak ditemui. Sila masukkan sekali lagi.", MessageFor.Restriction);
                return;
            }

            if (UserCache.CurrentUserID > 0)
            {
                Cache.SettingsCache.LoadSettings();
                Cache.AccountCache.LoadAccounts();
                Cache.AccountCache.LoadAccountSettings();
                Cache.MemberCache.LoadMembers();
                Close();                
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnLogin_Click(sender, e);
            }
        }

    }
}
