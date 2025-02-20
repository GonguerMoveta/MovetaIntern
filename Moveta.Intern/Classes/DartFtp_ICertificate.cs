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
	public class DartFtp_ICertificate : SalObject
	{
		
		/// <summary>
		/// This is the real COM interface.
		/// </summary>
		internal COMInterface _Interface = null;
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public DartFtp_ICertificate(){ }
		public DartFtp_ICertificate(COMInterface obj) : this()
		{
			this._Interface = obj;
		}
		#endregion
		
		/// <summary>
		/// </summary>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetIssuedTo(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.IssuedTo;
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
		public SalBoolean PropGetIssuedBy(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.IssuedBy;
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
		public SalBoolean PropGetValidFrom(ref SalDateTime returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.ValidFrom;
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
		public SalBoolean PropGetValidTo(ref SalDateTime returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.ValidTo;
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
		public SalBoolean PropGetKeySize(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.KeySize;
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
		/// <param name="Usage">Important: this is one of the DartFtp_KeyUsageConstants constants</param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean PropGetKeyUsage(SalNumber Usage, ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				int param1 = (int)Usage;
				returnValue = _Interface.KeyUsage(param1);
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
		public SalBoolean PropGetVersion(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Version;
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
		public SalBoolean PropGetHandle(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Handle;
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
		public SalBoolean PropSetHandle(SalNumber rhs)
		{
			#region Actions
			try
			{
				int param1 = (int)rhs;
				_Interface.Handle = param1;
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
		public SalBoolean PropGetKeyName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.KeyName;
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
		public SalBoolean PropGetSerialNumber(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.SerialNumber;
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
		public SalBoolean PropGetFriendlyName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.FriendlyName;
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
		/// <param name="returnValue">Important: this is one of the DartFtp_StoreLocationConstants constants</param>
		/// <returns></returns>
		public SalBoolean PropGetStoreLocation(ref SalNumber returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.StoreLocation;
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
		public SalBoolean PropGetStoreName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.StoreName;
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
		[Guid("93D500FD-C927-11D3-912C-00105A17B608")]
		[InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
		public interface COMInterface
		{
			string IssuedTo { get; }
			string IssuedBy { get; }
			System.DateTime ValidFrom { get; }
			System.DateTime ValidTo { get; }
			int KeySize { get; }
			bool KeyUsage(int Usage);
			int Version { get; }
			int Handle { get; set; }
			string KeyName { get; }
			string SerialNumber { get; }
			string FriendlyName { get; }
			int StoreLocation { get; }
			string StoreName { get; }
		}
	}
}
