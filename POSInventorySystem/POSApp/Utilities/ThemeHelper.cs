using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace POSApp.Utilities
{
    public static class ThemeHelper
    {
        // Modern Professional Color Palette
        public static readonly Color PrimaryDark = Color.FromArgb(30, 30, 35);
        public static readonly Color SecondaryDark = Color.FromArgb(22, 22, 26);
        public static readonly Color AccentBlue = Color.FromArgb(0, 150, 255);
        public static readonly Color AccentGreen = Color.FromArgb(0, 200, 100);
        public static readonly Color AccentRed = Color.FromArgb(255, 70, 70);
        public static readonly Color TextWhite = Color.FromArgb(240, 240, 245);
        public static readonly Color TextLightGray = Color.FromArgb(160, 160, 175);

        // Level-based Colors
        public static readonly Color LevelCritical = Color.FromArgb(180, 30, 30);
        public static readonly Color LevelWarning = Color.FromArgb(200, 150, 0);
        public static readonly Color LevelSafe = Color.FromArgb(30, 150, 30);

        public static void ApplyTheme(Form form)
        {
            form.BackColor = SecondaryDark;
            form.ForeColor = TextWhite;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            form.StartPosition = FormStartPosition.CenterScreen;

            ApplyToControls(form.Controls);
        }

        public static void ApplyCardStyle(Panel pnl)
        {
            pnl.BackColor = Color.FromArgb(38, 38, 42);
            pnl.BorderStyle = BorderStyle.None;
            pnl.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(60, 60, 65), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
                }
            };
        }

        public static void ApplyModernButton(Button btn, Color backColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
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
                    btn.FlatAppearance.BorderSize = 0;
                    if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent || btn.BackColor == SecondaryDark || btn.BackColor == Color.White)
                    {
                        btn.BackColor = PrimaryDark;
                    }
                    btn.ForeColor = TextWhite;
                    btn.Cursor = Cursors.Hand;
                }
                else if (control is Label lbl)
                {
                    if (lbl.ForeColor == SystemColors.ControlText || lbl.ForeColor == Color.Black)
                        lbl.ForeColor = TextWhite;

                    if (lbl.BackColor == SystemColors.Control || lbl.BackColor == Color.White)
                        lbl.BackColor = Color.Transparent;
                }
                else if (control is TextBox txt)
                {
                    control.BackColor = PrimaryDark;
                    control.ForeColor = TextWhite;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox cmb)
                {
                    try
                    {
                        cmb.BackColor = PrimaryDark;
                        cmb.ForeColor = TextWhite;
                    }
                    catch { }
                }
                else if (control is NumericUpDown num)
                {
                    control.BackColor = PrimaryDark;
                    control.ForeColor = TextWhite;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = PrimaryDark;
                    dgv.ForeColor = TextWhite;
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgv.GridColor = Color.FromArgb(50, 50, 60);

                    dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 50);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextWhite;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
                    dgv.ColumnHeadersHeight = 40;
                    dgv.RowTemplate.Height = 35;
                    dgv.EnableHeadersVisualStyles = false;

                    dgv.DefaultCellStyle.BackColor = PrimaryDark;
                    dgv.DefaultCellStyle.ForeColor = TextWhite;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 80);
                    dgv.DefaultCellStyle.SelectionForeColor = AccentBlue;
                    dgv.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 42);
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else if (control is Panel pnl)
                {
                    if (pnl.BackColor == SystemColors.Control || pnl.BackColor == Color.White)
                        pnl.BackColor = SecondaryDark;
                }
                else if (control is ListBox lst)
                {
                    lst.BackColor = PrimaryDark;
                    lst.ForeColor = TextWhite;
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
