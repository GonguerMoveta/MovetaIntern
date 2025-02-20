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
	public class DartFtp_IListEntry : SalObject
	{
		
		/// <summary>
		/// This is the real COM interface.
		/// </summary>
		internal COMInterface _Interface = null;
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public DartFtp_IListEntry(){ }
		public DartFtp_IListEntry(COMInterface obj) : this()
		{
			this._Interface = obj;
		}
		#endregion
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Name;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetType(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Type;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetPermissions(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Permissions;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetDirectory(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Directory;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetOwner(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Owner;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetGroupName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.GroupName;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetSize(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Size;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetTimeStamp(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.TimeStamp;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetLinks(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Links;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetDestination(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Destination;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetText(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Text;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public SalBoolean PropSetText(SalString rhs)
		{
			#region Actions
			try
			{
				string param1 = (string)rhs;
				_Interface.Text = param1;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetSizeHigh(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.SizeHigh;
				return true;
			}
			catch (COMException ex)
			{
				return HandleException(ex);
			}
			#endregion
		}
		
		/// <summary>
		/// This is the real COM interface declaration.
		/// </summary>
		[Guid("F6241E16-C4F7-11D2-AD9C-00105A17B608")]
		[InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
		public interface COMInterface
		{
			string Name { get; }
			string Type { get; }
			int Permissions { get; }
			string Directory { get; }
			string Owner { get; }
			string GroupName { get; }
			int Size { get; }
			string TimeStamp { get; }
			int Links { get; }
			string Destination { get; }
			string Text { get; set; }
			int SizeHigh { get; }
		}
	}
}
