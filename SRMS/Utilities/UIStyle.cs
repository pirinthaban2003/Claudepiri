using System.Drawing;
using System.Windows.Forms;

namespace SRMS.Utilities
{
    public static class UIStyle
    {
        public static Color PrimaryColor = Color.FromArgb(44, 62, 80);
        public static Color SecondaryColor = Color.FromArgb(52, 73, 94);
        public static Color AccentColor = Color.FromArgb(52, 152, 219);
        public static Color TextColor = Color.White;
        public static Color BackgroundColor = Color.FromArgb(236, 240, 241);

        public static void ApplyStyle(Form form)
        {
            form.BackColor = BackgroundColor;
            form.Font = new Font("Segoe UI", 10);

            foreach (Control control in form.Controls)
            {
                ApplyControlStyle(control);
            }
        }

        private static void ApplyControlStyle(Control control)
        {
            if (control is Button btn)
            {
                btn.BackColor = PrimaryColor;
                btn.ForeColor = TextColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
            }
            else if (control is Label lbl && lbl.Name != "lblTitle" && lbl.Parent?.Name != "sidebar")
            {
                lbl.ForeColor = Color.Black;
            }
            else if (control is Panel panel)
            {
                foreach (Control subControl in panel.Controls)
                {
                    ApplyControlStyle(subControl);
                }
            }
        }
    }
}
