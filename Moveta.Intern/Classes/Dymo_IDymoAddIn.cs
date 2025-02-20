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
	public class Dymo_IDymoAddIn : SalObject
	{
		
		/// <summary>
		/// This is the real COM interface.
		/// </summary>
		internal COMInterface _Interface = null;
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public Dymo_IDymoAddIn(){ }
		public Dymo_IDymoAddIn(COMInterface obj) : this()
		{
			this._Interface = obj;
		}
		#endregion
		
		/// <summary>
		/// </summary>
		/// <param name="FileName"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean Open(SalString FileName, ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				string param1 = (string)FileName;
				returnValue = _Interface.Open(param1);
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
		public SalBoolean Save(ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.Save();
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
		/// <param name="FileName"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean SaveAs(SalString FileName, ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				string param1 = (string)FileName;
				returnValue = _Interface.SaveAs(param1);
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
		/// <param name="Copies"></param>
		/// <param name="bShowDialog"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean Print(SalNumber Copies, SalBoolean bShowDialog, ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				int param1 = (int)Copies;
				bool param2 = (bool)bShowDialog;
				returnValue = _Interface.Print(param1, param2);
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
		/// <returns></returns>
		public SalBoolean Hide()
		{
			#region Actions
			try
			{
				_Interface.Hide();
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
		/// <returns></returns>
		public SalBoolean Show()
		{
			#region Actions
			try
			{
				_Interface.Show();
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
		/// <param name="State"></param>
		/// <returns></returns>
		public SalBoolean SysTray(SalBoolean State)
		{
			#region Actions
			try
			{
				bool param1 = (bool)State;
				_Interface.SysTray(param1);
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
		/// <returns></returns>
		public SalBoolean Quit()
		{
			#region Actions
			try
			{
				_Interface.Quit();
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
		public SalBoolean PropGetFileName(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.FileName;
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
		/// <param name="Printer"></param>
		/// <param name="returnValue"></param>
		/// <returns></returns>
		public SalBoolean SelectPrinter(SalString Printer, ref SalBoolean returnValue)
		{
			#region Actions
			try
			{
				string param1 = (string)Printer;
				returnValue = _Interface.SelectPrinter(param1);
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
		public SalBoolean GetDymoPrinters(ref SalString returnValue)
		{
			#region Actions
			try
			{
				returnValue = _Interface.GetDymoPrinters();
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
		[Guid("00020400-0000-0000-C000-000000000046")]
		[InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
		public interface COMInterface
		{
			bool Open(string FileName);
			bool Save();
			bool SaveAs(string FileName);
			bool Print(int Copies, bool bShowDialog);
			void Hide();
			void Show();
			void SysTray(bool State);
			void Quit();
			string FileName { get; }
			bool SelectPrinter(string Printer);
			string GetDymoPrinters();
		}
	}
}
