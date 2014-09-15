using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ATM
{
    public class Screen
    {
        public Screen( System.Windows.Forms.RichTextBox obj )
        {
            if( TextBox == null )
                TextBox = obj;
        }

        private static System.Windows.Forms.RichTextBox TextBox = null;
        private static String ScreenBuffer = "";

        delegate void textDelegate(String text);

        private void ChangeText(String text)
        {
            if (TextBox.InvokeRequired)
            {
                TextBox.BeginInvoke(new textDelegate(ChangeText), new object[] { text });
                return;
            }
            else
            {
                TextBox.Text = text;
            }
        }

        public void Clear()
        {
            ScreenBuffer = "";
            ChangeText(ScreenBuffer);
        }

        public void Backspace()
        {
            ScreenBuffer = ScreenBuffer.Substring( 0, ScreenBuffer.Length - 1);
            ChangeText(ScreenBuffer);
        }

        public void displayMessage(String message)
        {
            ScreenBuffer += message;
            ChangeText(ScreenBuffer);
        }



        public void displayMessageLine(String message)
        {
            ScreenBuffer += (message + "\n");
            ChangeText(ScreenBuffer);
        }



        public void displayDollarAmount(double amount)
        {
            ScreenBuffer += amount.ToString("F2", CultureInfo.GetCultureInfo("en-US")) + " руб.";
            ChangeText(ScreenBuffer);
        }
    }
}
