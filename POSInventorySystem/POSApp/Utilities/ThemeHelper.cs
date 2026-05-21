using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace POSApp.Utilities
{
    public static class ThemeHelper
    {
        // Modern Professional Color Palette
        public static readonly Color PrimaryDark = Color.FromArgb(45, 45, 48);
        public static readonly Color SecondaryDark = Color.FromArgb(28, 28, 28);
        public static readonly Color AccentBlue = Color.FromArgb(0, 122, 204);
        public static readonly Color AccentGreen = Color.FromArgb(34, 139, 34);
        public static readonly Color AccentRed = Color.FromArgb(204, 50, 50);
        public static readonly Color TextWhite = Color.White;
        public static readonly Color TextLightGray = Color.FromArgb(200, 200, 200);

        // Level-based Colors
        public static readonly Color LevelCritical = Color.FromArgb(120, 0, 0); // Deep Red
        public static readonly Color LevelWarning = Color.FromArgb(120, 80, 0); // Dark Orange
        public static readonly Color LevelSafe = Color.FromArgb(0, 80, 0);     // Dark Green

        public static void ApplyTheme(Form form)
        {
            form.BackColor = SecondaryDark;
            form.ForeColor = TextWhite;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            form.StartPosition = FormStartPosition.CenterScreen;

            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent || btn.BackColor == SecondaryDark)
                    {
                        btn.BackColor = PrimaryDark;
                    }
                    btn.ForeColor = TextWhite;
                    btn.Cursor = Cursors.Hand;
                }
                else if (control is Label lbl)
                {
                    if (lbl.ForeColor == SystemColors.ControlText)
                        lbl.ForeColor = TextWhite;
                }
                else if (control is TextBox txt)
                {
                    control.BackColor = PrimaryDark;
                    control.ForeColor = TextWhite;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox cmb)
                {
                    control.BackColor = PrimaryDark;
                    control.ForeColor = TextWhite;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (control is NumericUpDown num)
                {
                    control.BackColor = PrimaryDark;
                    control.ForeColor = TextWhite;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = PrimaryDark;
                    dgv.ForeColor = Color.Black;
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = SecondaryDark;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextWhite;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    dgv.ColumnHeadersHeight = 35;
                    dgv.RowTemplate.Height = 30;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                    dgv.DefaultCellStyle.ForeColor = TextWhite;
                    dgv.DefaultCellStyle.SelectionBackColor = AccentBlue;
                    dgv.DefaultCellStyle.SelectionForeColor = TextWhite;
                    dgv.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                }
                else if (control is Panel pnl)
                {
                    if (pnl.BackColor == SystemColors.Control)
                        pnl.BackColor = SecondaryDark;
                    ApplyToControls(pnl.Controls);
                }

                if (control.HasChildren)
                {
                    ApplyToControls(control.Controls);
                }
            }
        }

        public static void DrawStatusBadge(Graphics g, Rectangle rect, string text, Color backColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = GetRoundedRect(rect, 10))
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    g.FillPath(brush, path);
                }
            }
            TextRenderer.DrawText(g, text, new Font("Segoe UI", 8F, FontStyle.Bold), rect, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private static GraphicsPath GetRoundedRect(Rectangle baseRect, int radius)
        {
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(baseRect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arc, 180, 90);
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
