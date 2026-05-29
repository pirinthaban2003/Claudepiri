using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace POSApp.Utilities
{
    public static class ThemeHelper
    {
        // Modern Light Professional Color Palette (Mockup inspired)
        public static readonly Color PrimaryLight = Color.White;
        public static readonly Color SecondaryLight = Color.FromArgb(235, 242, 250);
        public static readonly Color AccentBlue = Color.FromArgb(0, 120, 215);
        public static readonly Color AccentGreen = Color.FromArgb(0, 180, 80);
        public static readonly Color AccentRed = Color.FromArgb(220, 50, 50);
        public static readonly Color TextDark = Color.FromArgb(30, 30, 30);
        public static readonly Color TextGray = Color.FromArgb(100, 100, 100);

        // Level-based Colors
        public static readonly Color LevelCritical = Color.FromArgb(255, 200, 200);
        public static readonly Color LevelWarning = Color.FromArgb(255, 240, 200);
        public static readonly Color LevelSafe = Color.FromArgb(200, 255, 200);

        public static void ApplyTheme(Form form)
        {
            form.BackColor = SecondaryLight;
            form.ForeColor = TextDark;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            form.StartPosition = FormStartPosition.CenterScreen;

            ApplyToControls(form.Controls);
        }

        public static void ApplyCardStyle(Panel pnl)
        {
            pnl.BackColor = PrimaryLight;
            pnl.BorderStyle = BorderStyle.None;
            pnl.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
                }
            };
        }

        public static void ApplyModernButton(Button btn, Color backColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = backColor;
            btn.BackColor = Color.White;
            btn.ForeColor = backColor;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;

                    if (btn.Text == "←")
                    {
                        btn.FlatAppearance.BorderSize = 0;
                        btn.BackColor = Color.Transparent;
                        btn.ForeColor = AccentBlue;
                        btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    }
                    else if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent || btn.BackColor == PrimaryLight || btn.BackColor == Color.FromArgb(22, 22, 26))
                    {
                        btn.BackColor = AccentBlue;
                        btn.ForeColor = Color.White;
                        btn.FlatAppearance.BorderSize = 0;
                    }
                    btn.Cursor = Cursors.Hand;
                }
                else if (control is Label lbl)
                {
                    if (lbl.ForeColor == Color.White || lbl.ForeColor == Color.FromArgb(240, 240, 245))
                        lbl.ForeColor = TextDark;

                    if (lbl.BackColor == Color.FromArgb(22, 22, 26))
                        lbl.BackColor = Color.Transparent;
                }
                else if (control is TextBox txt)
                {
                    control.BackColor = PrimaryLight;
                    control.ForeColor = TextDark;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox cmb)
                {
                    try
                    {
                        cmb.BackColor = PrimaryLight;
                        cmb.ForeColor = TextDark;
                    }
                    catch { }
                }
                else if (control is NumericUpDown num)
                {
                    control.BackColor = PrimaryLight;
                    control.ForeColor = TextDark;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.ForeColor = TextDark;
                    dgv.BorderStyle = BorderStyle.FixedSingle;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgv.GridColor = Color.FromArgb(230, 230, 230);

                    dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextDark;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
                    dgv.ColumnHeadersHeight = 40;
                    dgv.RowTemplate.Height = 35;
                    dgv.EnableHeadersVisualStyles = false;

                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = TextDark;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 245, 255);
                    dgv.DefaultCellStyle.SelectionForeColor = AccentBlue;
                    dgv.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else if (control is Panel pnl)
                {
                    if (pnl.BackColor == Color.FromArgb(22, 22, 26) || pnl.BackColor == Color.FromArgb(30, 30, 35))
                        pnl.BackColor = PrimaryLight;
                }
                else if (control is ListBox lst)
                {
                    lst.BackColor = PrimaryLight;
                    lst.ForeColor = TextDark;
                    lst.BorderStyle = BorderStyle.None;
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
