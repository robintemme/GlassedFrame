using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassTest
{
    public partial class GlassFrame : Form
    {
        public GlassFrame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The API used to extend the Glass margins into the client area
        /// </summary>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        /// <summary>
        /// Determines whether the Desktop Windows Manager is enabled
        /// and can therefore display Aero 
        /// </summary>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// The struct used to pass the Glass margins to the Win32 API
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        /// <summary>
        /// Use the form padding values to define a Glass margin
        /// </summary>
        private void SetGlass()
        {
            if (DwmIsCompositionEnabled())
            {
                MARGINS margins = new MARGINS();
                margins.Top = -1; margins.Left = -1; margins.Bottom = -1; margins.Right = -1;
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }

        // When Form is painted, SetGlass()
        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            SetGlass();
        }
    }
}
