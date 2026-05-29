using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Services;
using POSApp.Utilities;
using POSApp.Models;

namespace POSApp.Forms
{
    public partial class UserManagementForm : Form
    {
        private readonly UserService _userService;
        private int _selectedUserId = 0;

        public UserManagementForm()
        {
            InitializeComponent();
            _userService = new UserService();
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            ThemeHelper.ApplyCardStyle(pnlForm);
            ThemeHelper.ApplyModernButton(btnSave, ThemeHelper.AccentBlue);
            ThemeHelper.ApplyModernButton(btnPromote, ThemeHelper.AccentGreen);
            ThemeHelper.ApplyModernButton(btnDemote, Color.Orange);
            ThemeHelper.ApplyModernButton(btnToggleStatus, ThemeHelper.AccentRed);
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadRoles();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = _userService.GetAllUsers();
        }

        private void LoadRoles()
        {
            var dt = _userService.GetRoles();
            cmbRole.SelectedIndex = -1;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";
            cmbRole.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and Password are required.");
                return;
            }

            try
            {
                bool success = _userService.AddUser(
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    Convert.ToInt32(cmbRole.SelectedValue),
                    txtFullName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    1 // Default branch for now
                );

                if (success)
                {
                    MessageBox.Show("User added successfully.");
                    ClearForm();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsers.Rows.Count)
            {
                var userIdVal = dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value;
                if (userIdVal != null && userIdVal != DBNull.Value)
                {
                    _selectedUserId = Convert.ToInt32(userIdVal);
                    lblSelectedUser.Text = "Selected: " + (dgvUsers.Rows[e.RowIndex].Cells["Username"].Value?.ToString() ?? "N/A");
                }
            }
        }

        private void btnPromote_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0) return;

            // Simple promotion to Admin (RoleID 1)
            if (_userService.UpdateUserRole(_selectedUserId, 1))
            {
                MessageBox.Show("User promoted to Admin.");
                LoadUsers();
            }
        }

        private void btnDemote_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0) return;

            // Simple demotion to Cashier (RoleID 3)
            if (_userService.UpdateUserRole(_selectedUserId, 3))
            {
                MessageBox.Show("User demoted to Cashier.");
                LoadUsers();
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0 || dgvUsers.CurrentRow == null) return;

            var activeVal = dgvUsers.CurrentRow.Cells["IsActive"].Value;
            bool currentStatus = activeVal != null && activeVal != DBNull.Value && Convert.ToBoolean(activeVal);

            if (_userService.ToggleUserStatus(_selectedUserId, !currentStatus))
            {
                MessageBox.Show("User status updated.");
                LoadUsers();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            _selectedUserId = 0;
            lblSelectedUser.Text = "Selected: None";
        }
    }
}
