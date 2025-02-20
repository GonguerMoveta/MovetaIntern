// <ppj name="Moveta.Intern" date="1/29/2024 3:39:48 AM" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
// ======================================================================================================
// This code was generated by the Ice Porter(tm) Tool version 4.8.15.0
// Ice Porter is part of The Porting Project (PPJ) by Ice Tea Group, LLC.
// The generated code is not guaranteed to be accurate and to compile without
// manual modifications.
// 
// ICE TEA GROUP LLC SHALL IN NO EVENT BE LIABLE FOR ANY DAMAGES WHATSOEVER
// (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS
// INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER LOSS OF ANY KIND)
// ARISING OUT OF THE USE OR INABILITY TO USE THE GENERATED CODE, WHETHER
// DIRECT, INDIRECT, INCIDENTAL, CONSEQUENTIAL, SPECIAL OR OTHERWISE, REGARDLESS
// OF THE FORM OF ACTION, EVEN IF ICE TEA GROUP LLC HAS BEEN ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGES.
// =====================================================================================================
using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MT;
using PPJ.Runtime;
using PPJ.Runtime.Com;
using PPJ.Runtime.Sql;
using PPJ.Runtime.Vis;
using PPJ.Runtime.Windows;
using PPJ.Runtime.Windows.QO;
using PPJ.Runtime.XSal;

namespace Moveta.Intern
{
	
	/// <summary>
	/// </summary>
	public class AX_ShellBrowser_JamDriveList : SalActiveX
	{
		
		/// <summary>
		/// This is the real COM object class.
		/// </summary>
		internal ShellBrowser_JamDriveList _CoClass = new ShellBrowser_JamDriveList();
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public AX_ShellBrowser_JamDriveList(): base("{48EBB627-AB10-45F8-91B2-0C8C7C849FE5}")
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}
		#endregion
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// AX_ShellBrowser_JamDriveList
			// 
			this.Name = "AX_ShellBrowser_JamDriveList";
			this.Size = new System.Drawing.Size(72, 21);
			this.TabStop = false;
		}
		#endregion
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean DeleteSelected()
		{
			using (new SalContext(this))
			{
				return _CoClass.DeleteSelected();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetFontByRef(stdole_Font rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetFontByRef(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetShowColumnHeaders(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetShowColumnHeaders(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Paths"></param>
		/// <param name="Checked"></param>
		/// <returns></returns>
		public SalBoolean AddStrings(ShellBrowser_IStrings Paths, SalBoolean Checked)
		{
			using (new SalContext(this))
			{
				return _CoClass.AddStrings(Paths, Checked);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean InitiateAction()
		{
			using (new SalContext(this))
			{
				return _CoClass.InitiateAction();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetShowShellObjectNames(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetShowShellObjectNames(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean AlphaSort(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.AlphaSort(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetDetails(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetDetails(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetDetails(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetDetails(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetCheckboxes(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetCheckboxes(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetCheckboxes(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetCheckboxes(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue">Important: this is one of the ShellBrowser_TScrollStyle constants</param>
		/// <returns></returns>
		public SalBoolean PropGetScrollBars(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetScrollBars(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs">Important: this is one of the ShellBrowser_TScrollStyle constants</param>
		/// <returns></returns>
		public SalBoolean PropSetScrollBars(SalNumber rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetScrollBars(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetRowSelect(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetRowSelect(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetRowSelect(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetRowSelect(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean SelectAll()
		{
			using (new SalContext(this))
			{
				return _CoClass.SelectAll();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetFullDrag(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetFullDrag(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetFullDrag(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetFullDrag(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean FullRefresh()
		{
			using (new SalContext(this))
			{
				return _CoClass.FullRefresh();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="S"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean StringWidth(SalString S, ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.StringWidth(S, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetEnabled(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetEnabled(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetEnabled(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetEnabled(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetCheckedDrives(ShellBrowser_IStrings returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetCheckedDrives(returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean GetSearchString(ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.GetSearchString(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean SmartRefresh()
		{
			using (new SalContext(this))
			{
				return _CoClass.SmartRefresh();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Index"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean GetFullPath(SalNumber Index, ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.GetFullPath(Index, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetVisible(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetVisible(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetVisible(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetVisible(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetOleDragDrop(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetOleDragDrop(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetOleDragDrop(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetOleDragDrop(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetColor(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetColor(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetColor(SalNumber rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetColor(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean ShowContextMenu(SalNumber x, SalNumber y, ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.ShowContextMenu(x, y, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean UnCheckAll()
		{
			using (new SalContext(this))
			{
				return _CoClass.UnCheckAll();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Filename"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean IsItemInList(SalString Filename, ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.IsItemInList(Filename, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Command"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean InvokeContextMenuCommand(SalString Command, ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.InvokeContextMenuCommand(Command, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean Clear()
		{
			using (new SalContext(this))
			{
				return _CoClass.Clear();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetHotTrack(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetHotTrack(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetHotTrack(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetHotTrack(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="DX"></param>
		/// <param name="DY"></param>
		/// <returns></returns>
		public SalBoolean Scroll(SalNumber DX, SalNumber DY)
		{
			using (new SalContext(this))
			{
				return _CoClass.Scroll(DX, DY);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetShowShellObjectNames(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetShowShellObjectNames(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Path"></param>
		/// <param name="Checked"></param>
		/// <returns></returns>
		public SalBoolean AddEditable(SalString Path, SalBoolean Checked)
		{
			using (new SalContext(this))
			{
				return _CoClass.AddEditable(Path, Checked);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetSelectedPaths(ShellBrowser_IStrings returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetSelectedPaths(returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetShellContextMenu(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetShellContextMenu(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetShellContextMenu(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetShellContextMenu(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetStatusBarColor(SalNumber rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetStatusBarColor(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean ClearSelection()
		{
			using (new SalContext(this))
			{
				return _CoClass.ClearSelection();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetStatusBarColor(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetStatusBarColor(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Code">Important: this is one of the ShellBrowser_TListArrangement constants</param>
		/// <returns></returns>
		public SalBoolean Arrange(SalNumber Code)
		{
			using (new SalContext(this))
			{
				return _CoClass.Arrange(Code);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="FirstIndex"></param>
		/// <param name="LastIndex"></param>
		/// <returns></returns>
		public SalBoolean UpdateItems(SalNumber FirstIndex, SalNumber LastIndex)
		{
			using (new SalContext(this))
			{
				return _CoClass.UpdateItems(FirstIndex, LastIndex);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean CancelEdit()
		{
			using (new SalContext(this))
			{
				return _CoClass.CancelEdit();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean IsEditing(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.IsEditing(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetExcludeList(ShellBrowser_IStrings rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetExcludeList(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetMultiSelect(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetMultiSelect(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetMultiSelect(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetMultiSelect(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetExcludeList(ShellBrowser_IStrings returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetExcludeList(returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetHideSelection(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetHideSelection(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetHideSelection(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetHideSelection(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Index"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetItems(SalNumber Index, ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetItems(Index, ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="Filename"></param>
		/// <returns></returns>
		public SalBoolean DeleteItem(SalString Filename)
		{
			using (new SalContext(this))
			{
				return _CoClass.DeleteItem(Filename);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetSelected(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetSelected(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetUseSystemFont(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetUseSystemFont(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetUseSystemFont(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetUseSystemFont(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetAutomaticRefresh(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetAutomaticRefresh(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetAutomaticRefresh(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetAutomaticRefresh(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetShowColumnHeaders(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetShowColumnHeaders(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetFont(stdole_Font returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetFont(returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetFont(stdole_Font rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetFont(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean CheckAll()
		{
			using (new SalContext(this))
			{
				return _CoClass.CheckAll();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs">Important: this is one of the ShellBrowser_TBorderStyle constants</param>
		/// <returns></returns>
		public SalBoolean PropSetBorderStyle(SalNumber rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetBorderStyle(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue">Important: this is one of the ShellBrowser_TBorderStyle constants</param>
		/// <returns></returns>
		public SalBoolean PropGetBorderStyle(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetBorderStyle(ref returnValue);
			}
		}
		
		#region System Methods/Properties
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public new static AX_ShellBrowser_JamDriveList FromHandle(SalWindowHandle handle)
		{
			return ((AX_ShellBrowser_JamDriveList)SalWindow.FromHandle(handle, typeof(AX_ShellBrowser_JamDriveList)));
		}
		#endregion
	}
}
