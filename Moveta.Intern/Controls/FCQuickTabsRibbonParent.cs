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


using Moveta.Intern;
using System.ComponentModel;

namespace PPJ.Runtime.Windows.QO
{
    /// <summary>
    /// Defines late bound functions in QuickTabs parent Form or Dialog Box that
    /// will be called when an event occurs in the tab control.
    /// </summary>
    [ApiCategory("QO")]
    [Alias("cQuickTabsParent")]
    public class FCQuickTabsRibbonParent : FCRibbonFormWindow
    {
        /// <summary>
        /// Indicates that a tab has been created.
        /// This function is called when the tab control receives SAM_Create.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public virtual SalBoolean TabCreate(SalWindowHandle hWnd)
        {
            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabCreate() is not implemented.");
            return true;
        }

        /// <summary>
        /// Indicates that a tab has been activated.
        /// This function is called before the child windows have been shown
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nTab"></param>
        /// <returns></returns>
        public virtual SalBoolean TabActivateStart(SalWindowHandle hWnd, SalNumber nTab)
        {
            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabActivateStart() is not implemented.");
            return true;
        }

        /// <summary>
        /// Indicates that a tab has been activated.
        /// This function is called after the child windows have been shown
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nTab"></param>
        /// <returns></returns>
        public virtual SalBoolean TabActivateFinish(SalWindowHandle hWnd, SalNumber nTab)
        {
            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabActivateFinish() is not implemented.");
            return true;
        }

        /// <summary>
        /// Indicates that user is attempting to change the current tab by clicking,
        ///  tabbing or some other user action.
        /// NOTE: Call CancelMode() to deny the user's request
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nTab"></param>
        /// <returns></returns>
        public virtual SalBoolean TabUserRequest(SalWindowHandle hWnd, SalNumber nTab)
        {
            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabUserRequest() is not implemented.");
            return true;
        }

        /// <summary>
        /// Indicates that the size of the tab frame may have changed
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public virtual SalBoolean TabFrameResize(SalWindowHandle hWnd)
        {
            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabFrameResize() is not implemented.");
            return true;
        }

        /// <summary>
        /// Define the page size for child forms created on the tab frame.
        /// This function can be overriden in the tab form or tab dialog box
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public virtual SalBoolean TabSetFormPageSize(SalWindowHandle hWnd)
        {
            SalQuickTabs picTabs = SalQuickTabs.FromHandle(hWnd);
            picTabs.SetPageSize(picTabs.PageRectangle, true);

            Sys.TraceWarning(Sys.TraceWindows, "FCQuickTabsRibbonParent.TabSetFormPageSize() is not implemented.");
            return true;
        }

        /// <summary>
        /// Fully qualified expressions operator.
        /// </summary>
        public static explicit operator FCQuickTabsRibbonParent(SalWindowHandle hWnd)
        {
            return FCQuickTabsRibbonParent.FromHandle(hWnd);
        }

        /// <summary>
        /// Returns the control associated with the handle.
        /// </summary>
        public static FCQuickTabsRibbonParent FromHandle(SalWindowHandle hWnd)
        {
            return (FCQuickTabsRibbonParent)SalWindow.FromHandle(hWnd, typeof(FCQuickTabsRibbonParent));
        }
    }
}
