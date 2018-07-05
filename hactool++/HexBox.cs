namespace System.Windows.Forms
{
    public class HexBox : TextBox
    {
        #region Constructors
        /// <summary> 
        /// The default constructor
        /// </summary>
        public HexBox()
        {
            TextChanged += new EventHandler(OnTextChanged);
            KeyDown += new KeyEventHandler(OnKeyDown);
        }
        #endregion

        #region Properties
        new public String Text
        {
            get { return base.Text; }
            set
            {
                base.Text = LeaveOnlyHex(value);
            }
        }
        #endregion

        #region OnTextChange
        /// <summary>
        /// If TextBox Input Changes, Validate the Input!
        /// </summary>
        protected void OnTextChanged(object sender, EventArgs e)
        {
            Text = LeaveOnlyHex(Text);
            Select(Text.Length, 0);
        }
        #endregion

        #region LeaveOnlyHex
        /// <summary>
        /// Validate that HEX Input!
        /// </summary>
        private string LeaveOnlyHex(String inString)
        {
            String tmp = inString;
            foreach (char c in inString.ToCharArray())
            {
                if (!IsHex(c))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp;
        }

        public bool IsHex(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'a' && c <= 'f') ||
                   (c >= 'A' && c <= 'F');
        }
        #endregion

        #region OnKeyDown
        /// <summary>
        /// Checks for a Number (or Numpad) Key or Action Key
        /// </summary>
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.KeyData) && !IsActionKey(e.KeyData);
        }

        public bool IsActionKey(Keys inKey)
        {
            return inKey == Keys.Delete || inKey == Keys.Back;
        }

        public bool IsNumberKey(Keys inKey)
        {
            return (inKey < Keys.D0 || inKey > Keys.D9) ||
                   (inKey > Keys.NumPad0 || inKey < Keys.NumPad9);
        }
        #endregion
    }
}