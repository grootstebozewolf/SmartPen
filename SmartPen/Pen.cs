using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBSDKComWrapperLib;
using System.Runtime.InteropServices;
using System.Drawing;

namespace SmartPen
{
    public class Pen
    {
        #region Event handlers

        public class PenEventArgs : EventArgs
        {
            public Color Color;
            public PenEventArgs(Color color)
            {
                Color = color;
            }
        }
        public delegate void SelectedEventHandler(object sender, PenEventArgs args);


        public event SelectedEventHandler PenSelected;
        public event SelectedEventHandler EraserSelected;
        public event SelectedEventHandler FingerSelected;


        
        #endregion

        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessageA([MarshalAs(UnmanagedType.LPStr)] string lpString);
        private int SBSDKMessageID = RegisterWindowMessageA("SBSDK_NEW_MESSAGE");

        private static ISBSDKBaseClass2_4 Sbsdk;
        private static _ISBSDKBaseClass2_4Events_Event SbsdkEvents;
        private object Owner;
        public Pen()
        {
            Owner = this;
            Init();
        }

        private void Init()
        {
            try
            {
                Sbsdk = new SBSDKBaseClass2_4();
            }
            catch (Exception e)
            {
                // handle exception, we will do nothing
                Console.WriteLine("Exception in Main: " + e.Message);
            }
            if (Sbsdk != null)
            {
                SbsdkEvents = (_ISBSDKBaseClass2_4Events_Event)Sbsdk;
                //SbsdkHoverEvents = (_ISBSDKBaseClass2HoverEvents_Event)Sbsdk;
            }
            if (SbsdkEvents != null)
            {
                SbsdkEvents.OnEraser += SbsdkEvents_OnEraser;
                SbsdkEvents.OnNoTool += SbsdkEvents_OnNoTool;
                SbsdkEvents.OnPen += SbsdkEvents_OnPen;
            }
        }

        public Pen(object owner)
        {
            Owner = owner;
            Init();
        }

        private void SbsdkEvents_OnPen(int iPointerID)
        {
            if (PenSelected != null)
            {
                int iRed = 0, iGreen = 0, iBlue = 0;
                if (Sbsdk != null)
                {
                    Sbsdk.SBSDKGetToolColor(iPointerID, out iRed, out iGreen, out iBlue);
                }
                PenSelected(Owner, new PenEventArgs(Color.FromArgb(iRed, iGreen, iBlue)));
            }
        }

        private void SbsdkEvents_OnNoTool(int iPointerID)
        {
            if (FingerSelected != null)
            {
                FingerSelected(Owner, new PenEventArgs(Color.FromArgb(0, 0, 0)));
            }
        }

        private void SbsdkEvents_OnEraser(int iPointerID)
        {
            if (EraserSelected != null)
            {
                EraserSelected(Owner, new PenEventArgs(Color.FromArgb(0, 0, 0)));
            }
        }
    }
}
