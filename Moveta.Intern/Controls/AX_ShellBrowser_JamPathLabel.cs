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
	public class AX_ShellBrowser_JamPathLabel : SalActiveX
	{
		
		/// <summary>
		/// This is the real COM object class.
		/// </summary>
		internal ShellBrowser_JamPathLabel _CoClass = new ShellBrowser_JamPathLabel();
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public AX_ShellBrowser_JamPathLabel(): base("{980884E4-742F-4D24-A1F5-CE8B04B8AC15}")
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
			// AX_ShellBrowser_JamPathLabel
			// 
			this.Name = "AX_ShellBrowser_JamPathLabel";
			this.Size = new System.Drawing.Size(72, 21);
			this.TabStop = false;
		}
		#endregion
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue">Important: this is one of the ShellBrowser_TAlignment constants</param>
		/// <returns></returns>
		public SalBoolean PropGetAlignment(ref SalNumber returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetAlignment(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs">Important: this is one of the ShellBrowser_TAlignment constants</param>
		/// <returns></returns>
		public SalBoolean PropSetAlignment(SalNumber rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetAlignment(rhs);
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
		public SalBoolean PropGetCaption(ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetCaption(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetVersion(ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetVersion(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetVersion(SalString rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetVersion(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetAutoSize(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetAutoSize(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetAutoSize(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetAutoSize(rhs);
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
		/// <returns></returns>
		public SalBoolean AboutBox()
		{
			using (new SalContext(this))
			{
				return _CoClass.AboutBox();
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetTransparent(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetTransparent(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetTransparent(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetTransparent(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetShowAccelChar(ref SalBoolean returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetShowAccelChar(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetShowAccelChar(SalBoolean rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetShowAccelChar(rhs);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetPath(ref SalString returnValue)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropGetPath(ref returnValue);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetPath(SalString rhs)
		{
			using (new SalContext(this))
			{
				return _CoClass.PropSetPath(rhs);
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
		
		#region System Methods/Properties
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public new static AX_ShellBrowser_JamPathLabel FromHandle(SalWindowHandle handle)
		{
			return ((AX_ShellBrowser_JamPathLabel)SalWindow.FromHandle(handle, typeof(AX_ShellBrowser_JamPathLabel)));
		}
		#endregion
	}
}
