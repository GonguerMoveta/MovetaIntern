//=================================================================================
// Copyright (C) Ice Tea Group, LLC
// 
// By using this code you have agreed to the terms and conditions set forth
// in the "PPJ Framework EULA" and "PPJ Framework Source EULA" licenses that
// have been delivered to you. In case you do not have these two documents
// you are hereby notified that you are not permitted to use this code.
// Please contact us at info@iceteagroup.com for further assistance.
// EULA stands for End User License Agreement.
// 
// ICE TEA GROUP LLC SHALL IN NO EVENT BE LIABLE FOR ANY DAMAGES WHATSOEVER
// (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS
// INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER LOSS OF ANY KIND)
// ARISING OUT OF THE USE OR INABILITY TO USE THE GENERATED CODE, WHETHER
// DIRECT, INDIRECT, INCIDENTAL, CONSEQUENTIAL, SPECIAL OR OTHERWISE, REGARDLESS
// OF THE FORM OF ACTION, EVEN IF ICE TEA GROUP LLC HAS BEEN ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGES.
//=================================================================================

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace PPJ.Runtime.Windows.QO
{
    /// <summary>
    /// Form window with a tab frame.  In addition to
    /// containing a built in tab frame.  This class also
    /// has the smarts to manage other child windows
    /// on the form so that they can be associated with
    /// specific tabs.
    /// </summary>
    [ApiCategory("QO")]
    [Alias("cQuickTabsForm")]
    public class FCQuickTabsRibbonForm : FCQuickTabsRibbonParentForm
    {
        /// <summary>
        /// tab container
        /// </summary>
        public SalQuickTabs picTabs;

        /// <summary>
        /// Form window with a tab frame.  In addition to
        /// containing a built in tab frame.  This class also
        /// has the smarts to manage other child windows
        /// on the form so that they can be associated with
        /// specific tabs.
        /// </summary>
        public FCQuickTabsRibbonForm()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picTabs = new PPJ.Runtime.Windows.QO.SalQuickTabs();
            this.SuspendLayout();
            // 
            // picTabs
            // 
            this.picTabs.Name = "picTabs";
            this.picTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTabs.Size = new System.Drawing.Size(264, 190);
            // 
            // FCQuickTabsRibbonForm
            // 
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(264, 190);
            this.Controls.Add(this.picTabs);
            this.Name = "FCQuickTabsRibbonForm";
            this.Text = "(Untitled)";
            this.ResumeLayout(true);

        }

        /// <summary>
        /// Processes Control+Tab to change the current tab also when the focus
        /// is on a different control.
        /// 
        /// Doesn't process Accept and Cancel buttons on hidden controls. PPJ controls 
        /// should process mnemonics when hidden unless they are hidden because they are
        /// bound to a tab page.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Control control = null;
            IButtonControl button = null;
            switch (keyData)
            {
                case Keys.Tab | Keys.Control:
                case Keys.Tab | Keys.Control | Keys.Shift:
                    if (!picTabs.Focused && picTabs.ProcessKeys(keyData))
                        return true;
                    break;

                case Keys.Enter:
                    if (!SalWindow.MapEnterToTab)
                    {
                        button = this.AcceptButton;
                        if (button != null)
                        {
                            control = (Control)button;
                            if (!control.Visible && picTabs.GetControlTabPages(control) != "")
                                return false;
                        }
                    }
                    break;

                case Keys.Escape:
                    button = this.CancelButton;
                    if (button != null)
                    {
                        control = (Control)button;
                        if (!control.Visible && picTabs.GetControlTabPages(control) != "")
                            return false;
                    }
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        private static MethodInfo _processMnemonic;

        /// <summary>
        /// Takes over processing mnemonics.
        /// PPJ controls should process mnemonics when hidden unless they are hidden
        /// because they are bound to a tab page.
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            if (!this.Enabled || !this.Visible && !this.HasChildren)
                return false;

            // loop through the controls starting at the control next to the current Active control in the Tab order
            // till we find someone willing to process this mnemonic. we don't start the search on the Active control 
            // to allow controls in the same container with the same mnemonic to be processed sequentially.
            bool wrapped = false;
            Control activeControl = this.ActiveControl;
            if (activeControl == this.picTabs && Control.ModifierKeys != Keys.Alt)
                return false;

            Control control = activeControl;
            do
            {
                // start from the 
                control = base.GetNextControl(control, true);
                if (control != null)
                {
                    if (!control.Visible && picTabs.GetControlTabPages(control) != "")
                        continue;
                    if (ProcessMnemonic(control, charCode))
                        return true;
                }
                else
                {
                    if (wrapped)
                        return false;

                    wrapped = true;
                }
            }
            while (control != activeControl);

            return false;
        }

        private static bool ProcessMnemonic(Control control, char charCode)
        {
            if (_processMnemonic == null)
                _processMnemonic = typeof(Control).GetMethod("ProcessMnemonic", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(char) }, null);

            if (_processMnemonic != null)
                return (bool)_processMnemonic.Invoke(control, new object[] { charCode });
            else
                Sys.TraceError(Sys.TraceSystem, "GetMethod: MethodNotFound: {0}.ProcessMnemonic", control.GetType().FullName);

            return false;
        }

        /// <summary>
        /// Fully qualified expressions operator.
        /// </summary>
        public static explicit operator FCQuickTabsRibbonForm(SalWindowHandle hWnd)
        {
            return FCQuickTabsRibbonForm.FromHandle(hWnd);
        }

        /// <summary>
        /// Returns the control associated with the handle.
        /// </summary>
        public static new FCQuickTabsRibbonForm FromHandle(SalWindowHandle hWnd)
        {
            Control ctrl = Control.FromHandle(hWnd);
            return ((FCQuickTabsRibbonForm)ctrl);
        }
    }
}
