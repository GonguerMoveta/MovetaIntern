// <ppj name="Moveta.Intern" date="1/17/2024 7:59:41 AM" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
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
	/// Progress Bar Control
	/// </summary>
	public class cccMsctlsProgressBar32 : SalCustomControl, gwcCommonControlClass.LateBind
	{
		// Multiple Inheritance: Base class instance.
		protected gwcCommonControlClass _gwcCommonControlClass;
		
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public cccMsctlsProgressBar32()
		{
			// Initialize second-base instances.
			InitializeMultipleInheritance();
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}
		#endregion
		
		#region Multiple Inheritance Fields
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalBoolean iv_bCreated
		{
			get { return _gwcCommonControlClass.iv_bCreated; }
			set { _gwcCommonControlClass.iv_bCreated = value; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalNumber iv_nStyleEx
		{
			get { return _gwcCommonControlClass.iv_nStyleEx; }
			set { _gwcCommonControlClass.iv_nStyleEx = value; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalNumber iv_nStyle
		{
			get { return _gwcCommonControlClass.iv_nStyle; }
			set { _gwcCommonControlClass.iv_nStyle = value; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalWindowHandle iv_hWndControl
		{
			get { return _gwcCommonControlClass.iv_hWndControl; }
			set { _gwcCommonControlClass.iv_hWndControl = value; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalWindowHandle iv_hWndParent
		{
			get { return _gwcCommonControlClass.iv_hWndParent; }
			set { _gwcCommonControlClass.iv_hWndParent = value; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SalWindowHandle iv_hWndSelf
		{
			get { return _gwcCommonControlClass.iv_hWndSelf; }
			set { _gwcCommonControlClass.iv_hWndSelf = value; }
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
			// cccMsctlsProgressBar32
			// 
			this.Name = "cccMsctlsProgressBar32";
			this.Text = "Progress Bar Control";
			this.Size = new System.Drawing.Size(72, 21);
			this.WindowsDLLName = "comctl32.dll";
			this.WindowsClassName = "msctls_progress32";
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.cccMsctlsProgressBar32_WindowActions);
		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Latebound function that creates the progress bar control.
		/// </summary>
		/// <param name="p_nStyle"></param>
		/// <param name="p_nStyleEx"></param>
		/// <returns></returns>
		public SalNumber lbfCreateControl(SalNumber p_nStyle, SalNumber p_nStyleEx)
		{
			#region Local Variables
			fcStructRectangle RECT = new fcStructRectangle();
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				// //-- set the styles --//
				if (p_nStyle == 0) 
				{
					p_nStyle = getStyle();
				}
				if (p_nStyleEx == 0) 
				{
					p_nStyleEx = getStyleEx();
				}

				this.lb_lbfGetInitialObjectRect(RECT);

				// //-- destroy the control if already created --//
				if (iv_hWndControl != SalWindowHandle.Null) 
				{
					iv_hWndControl.DestroyWindow();
					iv_hWndControl = SalWindowHandle.Null;
				}

				// //-- create the control --//
				iv_hWndControl = Ext.CreateWindowExA(p_nStyleEx, Const.PROGRESS_CLASS, SalString.Null, p_nStyle, 0, 0, RECT.iv_nRight - RECT.iv_nLeft, RECT.iv_nBottom - RECT.iv_nTop, getHandleSelf(), 0, Ext.GetWindowLongA(this, Const.GWL_HINSTANCE), 0);

				if (getHandleControl() != Sys.hWndNULL) 
				{

					iv_bCreated = true;

				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Initialize the members and create the control.
		/// </summary>
		/// <param name="p_nwParam"></param>
		/// <param name="p_nlParam"></param>
		/// <returns></returns>
		public SalNumber lbfOn_SAM_Create(SalNumber p_nwParam, SalNumber p_nlParam)
		{
			#region Actions
			using (new SalContext(this))
			{
				// //-- initialize base class --//
				((gwcCommonControlClass)this).lbfOn_SAM_Create(p_nwParam, p_nlParam);

				// //-- initialize the styles --//
				if (getStyle() == 0) 
				{
					setStyle((Const.WS_CHILD | Const.WS_VISIBLE));
				}

				// //-- create the control --//
				this.lb_lbfCreateControl(0, 0);
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Get the position
		/// </summary>
		/// <returns></returns>
		public SalNumber GetPos()
		{
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					return getHandleControl().SendMessage(Const.PBM_GETPOS, 0, 0);
				}
				return 0;
			}
			#endregion
		}
		
		/// <summary>
		/// Set the color of indicator bar
		/// </summary>
		/// <param name="p_nColor"></param>
		/// <returns></returns>
		public SalNumber SetBarColor(SalNumber p_nColor)
		{
			#region Local Variables
			SalNumber nR = 0;
			SalNumber nG = 0;
			SalNumber nB = 0;
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					Sal.ColorToRGB(p_nColor, ref nR, ref nG, ref nB);
					p_nColor = (nR | (nG * 256)) | (nB * 65536);
					getHandleControl().SendMessage(Const.PBM_SETBARCOLOR, 0, p_nColor);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Set the color of background
		/// </summary>
		/// <param name="p_nColor"></param>
		/// <returns></returns>
		public SalNumber SetBackgroundColor(SalNumber p_nColor)
		{
			#region Local Variables
			SalNumber nR = 0;
			SalNumber nG = 0;
			SalNumber nB = 0;
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					Sal.ColorToRGB(p_nColor, ref nR, ref nG, ref nB);
					p_nColor = (nR | (nG * 256)) | (nB * 65536);
					getHandleControl().SendMessage(Const.PBM_SETBKCOLOR, 0, p_nColor);
					getHandleControl().UpdateWindow();
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Set the indicator position
		/// </summary>
		/// <param name="p_nPos"></param>
		/// <returns></returns>
		public SalNumber SetPos(SalNumber p_nPos)
		{
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					getHandleControl().SendMessage(Const.PBM_SETPOS, p_nPos, 0);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Set minimum and maximum
		/// </summary>
		/// <param name="p_nMin"></param>
		/// <param name="p_nMax"></param>
		/// <returns></returns>
		public SalNumber SetRange(SalNumber p_nMin, SalNumber p_nMax)
		{
			#region Local Variables
			SalString sBuffer = "";
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{

					Sal.StrSetBufferLength(ref sBuffer, 8);
					// Call CStructPutInt( sBuffer, 0, p_nMin )
					// Call CStructPutInt( sBuffer, 4, p_nMax )
					Ext.CStructPutLong(ref sBuffer, 0, p_nMin);
					Ext.CStructPutLong(ref sBuffer, 4, p_nMax);

					Vis.SendMsgString(getHandleControl(), Const.PBM_SETRANGE, 0, sBuffer);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Specify the step increment
		/// </summary>
		/// <param name="p_nStep"></param>
		/// <returns></returns>
		public SalNumber SetStep(SalNumber p_nStep)
		{
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					getHandleControl().SendMessage(Const.PBM_SETSTEP, p_nStep, 0);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// Advance current position by step and redraw
		/// </summary>
		/// <returns></returns>
		public SalNumber StepIt()
		{
			#region Local Variables
			SalNumber nLastPos = 0;
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					nLastPos = getHandleControl().SendMessage(Const.PBM_STEPIT, 0, 0);
				}
			}

			return 0;
			#endregion
		}
		// //-- own functions --//
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber On_PBMLoop()
		{
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					SetPos(0);
					getHandleSelf().SetTimer(1, 100);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber On_PBMLoopStop()
		{
			#region Actions
			using (new SalContext(this))
			{
				if (IsControlCreated()) 
				{
					getHandleSelf().KillTimer(1);
					SetPos(0);
				}
			}

			return 0;
			#endregion
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// cccMsctlsProgressBar32 WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cccMsctlsProgressBar32_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Const.PAM_PBM_LOOP:
					this.cccMsctlsProgressBar32_OnPAM_PBM_LOOP(sender, e);
					break;
				
				case Const.PAM_PBM_LOOPSTOP:
					this.cccMsctlsProgressBar32_OnPAM_PBM_LOOPSTOP(sender, e);
					break;
				
				case Sys.SAM_Timer:
					this.cccMsctlsProgressBar32_OnSAM_Timer(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// PAM_PBM_LOOP event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cccMsctlsProgressBar32_OnPAM_PBM_LOOP(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.On_PBMLoop();
			#endregion
		}
		
		/// <summary>
		/// PAM_PBM_LOOPSTOP event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cccMsctlsProgressBar32_OnPAM_PBM_LOOPSTOP(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.On_PBMLoopStop();
			#endregion
		}
		
		/// <summary>
		/// SAM_Timer event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cccMsctlsProgressBar32_OnSAM_Timer(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.StepIt();
			#endregion
		}
		#endregion
		
		#region Multiple Inheritance Methods
		
		/// <summary>
		/// Get the iv_nStyle member.
		/// </summary>
		/// <returns></returns>
		public SalNumber getStyle()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.getStyle();
			}
		}
		
		/// <summary>
		/// Set the iv_nStyle member.
		/// </summary>
		/// <param name="p_nArg"></param>
		/// <returns></returns>
		public SalNumber setStyle(SalNumber p_nArg)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.setStyle(p_nArg);
			}
		}
		
		/// <summary>
		/// Returns the windows extended style
		/// </summary>
		/// <returns></returns>
		public SalNumber GetWindowStyleEx()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.GetWindowStyleEx();
			}
		}
		
		/// <summary>
		/// Set a windows extended style
		/// </summary>
		/// <param name="p_nStyle"></param>
		/// <returns></returns>
		public SalNumber SetWindowStyleEx(SalNumber p_nStyle)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.SetWindowStyleEx(p_nStyle);
			}
		}
		
		/// <summary>
		/// Returns the parent window handle
		/// </summary>
		/// <returns></returns>
		public SalWindowHandle getHandleParent()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.getHandleParent();
			}
		}
		
		/// <summary>
		/// Positions the created control.
		/// </summary>
		/// <returns></returns>
		public SalBoolean lbfResizeControl()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.lbfResizeControl();
			}
		}
		
		/// <summary>
		/// Returns the window rectangle size for the hWndItem (not the control).
		/// </summary>
		/// <param name="p_RECT"></param>
		/// <returns></returns>
		public SalNumber lbfGetInitialObjectRect(fcStructRectangle p_RECT)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.lbfGetInitialObjectRect(p_RECT);
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="p_nwParam"></param>
		/// <param name="p_nlParam"></param>
		/// <returns></returns>
		public SalNumber lbfOn_SAM_Destroy(SalNumber p_nwParam, SalNumber p_nlParam)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.lbfOn_SAM_Destroy(p_nwParam, p_nlParam);
			}
		}
		
		/// <summary>
		/// Returns the control window handle
		/// </summary>
		/// <returns></returns>
		public SalWindowHandle getHandleControl()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.getHandleControl();
			}
		}
		
		/// <summary>
		/// Returns TRUE if control is created otherwise FALSE
		/// </summary>
		/// <returns></returns>
		public SalNumber IsControlCreated()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.IsControlCreated();
			}
		}
		
		/// <summary>
		/// Returns the item window handle
		/// </summary>
		/// <returns></returns>
		public SalWindowHandle getHandleSelf()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.getHandleSelf();
			}
		}
		
		/// <summary>
		/// Returns the windows style
		/// </summary>
		/// <returns></returns>
		public SalNumber GetWindowStyle()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.GetWindowStyle();
			}
		}
		
		/// <summary>
		/// Set a windows style
		/// </summary>
		/// <param name="p_nStyle"></param>
		/// <returns></returns>
		public SalNumber SetWindowStyle(SalNumber p_nStyle)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.SetWindowStyle(p_nStyle);
			}
		}
		
		/// <summary>
		/// Get the iv_nStyleEx member.
		/// </summary>
		/// <returns></returns>
		public SalNumber getStyleEx()
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.getStyleEx();
			}
		}
		
		/// <summary>
		/// Set the iv_nStyleEx member.
		/// </summary>
		/// <param name="p_nArg"></param>
		/// <returns></returns>
		public SalNumber setStyleEx(SalNumber p_nArg)
		{
			using (new SalContext(this))
			{
				return _gwcCommonControlClass.setStyleEx(p_nArg);
			}
		}
		#endregion
		
		#region Multiple Inheritance Operators
		
		/// <summary>
		/// Multiple Inheritance: Cast operator from type cccMsctlsProgressBar32 to type gwcCommonControlClass.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static implicit operator gwcCommonControlClass(cccMsctlsProgressBar32 self)
		{
			return self._gwcCommonControlClass;
		}
		
		/// <summary>
		/// Multiple Inheritance: Cast operator from type gwcCommonControlClass to type cccMsctlsProgressBar32.
		/// </summary>
		/// <param name="super"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static implicit operator cccMsctlsProgressBar32(gwcCommonControlClass super)
		{
			return ((cccMsctlsProgressBar32)super._derived);
		}
		#endregion
		
		#region System Methods/Properties
		
		/// <summary>
		/// Multiple Inheritance: Initialize delegate instances.
		/// </summary>
		private void InitializeMultipleInheritance()
		{
			this._gwcCommonControlClass = new gwcCommonControlClass(this);
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static cccMsctlsProgressBar32 FromHandle(SalWindowHandle handle)
		{
			return ((cccMsctlsProgressBar32)SalWindow.FromHandle(handle, typeof(cccMsctlsProgressBar32)));
		}
		#endregion
		
		#region Late Bind Methods
		
		/// <summary>
		/// Virtual wrapper replacement for late-bound (..) calls.
		/// </summary>
		public virtual SalNumber lb_lbfOn_SAM_Create(SalNumber p_nwParam, SalNumber p_nlParam)
		{
			return this.lbfOn_SAM_Create(p_nwParam, p_nlParam);
		}
		
		/// <summary>
		/// Virtual wrapper replacement for late-bound (..) calls.
		/// </summary>
		public virtual SalNumber lb_lbfOn_SAM_Destroy(SalNumber p_nwParam, SalNumber p_nlParam)
		{
			return this.lbfOn_SAM_Destroy(p_nwParam, p_nlParam);
		}
		
		/// <summary>
		/// Virtual wrapper replacement for late-bound (..) calls.
		/// </summary>
		public virtual SalNumber lb_lbfGetInitialObjectRect(fcStructRectangle p_RECT)
		{
			return this.lbfGetInitialObjectRect(p_RECT);
		}
		
		/// <summary>
		/// Virtual wrapper replacement for late-bound (..) calls.
		/// </summary>
		public virtual SalNumber lb_lbfCreateControl(SalNumber p_nStyle, SalNumber p_nStyleEx)
		{
			return this.lbfCreateControl(p_nStyle, p_nStyleEx);
		}
		#endregion
	}
}
