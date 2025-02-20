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
	/// </summary>
	public class XMLNamedNodeMap : XMLNodeList
	{
		#region Constructors/Destructors
		#endregion
		
		#region Methods
		
		/// <summary>
		/// </summary>
		/// <param name="sName"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		public SalBoolean getNamedItem(SalString sName, XMLNode node)
		{
			#region Local Variables
			SalNumber i = 0;
			SalNumber n = 0;
			#endregion
			
			#region Actions
			i = 0;
			n = length();
			while (i < n) 
			{
				if (children[i].nodeName() == sName) 
				{
					node = children[i];
					return true;
				}
				i = i + 1;
			}
			node = null;
			return false;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="sNamespace"></param>
		/// <param name="sName"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		public SalBoolean getNamedItemNS(SalString sNamespace, SalString sName, XMLNode node)
		{
			#region Local Variables
			SalNumber i = 0;
			SalNumber n = 0;
			SalBoolean bFoundName = false;
			SalBoolean bFoundNamespace = false;
			#endregion
			
			#region Actions
			i = 0;
			n = length();
			while (i < n) 
			{
				bFoundName = false;
				bFoundNamespace = false;

				if (children[i].nodeName() == sName) 
				{
					bFoundName = true;
				}

				if (children[i].getNamespaceURI() == sNamespace) 
				{
					bFoundNamespace = true;
				}

				// are we searching for the actual namespace or the prefix for the namespace
				// If children[i].getPrefix() = sNamespace
				// Set bFoundNamespace = TRUE

				if (bFoundName && bFoundNamespace) 
				{
					node = children[i];
					return true;
				}
				i = i + 1;
			}
			node = null;
			return false;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public SalBoolean add(XMLNode node)
		{
			#region Local Variables
			SalNumber bound = 0;
			SalBoolean bReturn = false;
			#endregion
			
			#region Actions
			bReturn = children.GetUpperBound(1, ref bound);
			children[bound + 1] = node;
			return bReturn;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="sName"></param>
		/// <returns></returns>
		public SalBoolean remove(SalString sName)
		{
			#region Local Variables
			SalNumber i = 0;
			SalNumber n = 0;
			SalBoolean bFound = false;
			XMLNode oParentNode = new XMLNode();
			XMLAttr oAttr = new XMLAttr();
			XMLElement oElement = new XMLElement();
			SalBoolean bReturn = false;
			SalNumber nType = 0;
			#endregion
			
			#region Actions
			i = 0;
			bFound = false;
			n = length();
			while (i < n && !(bFound)) 
			{
				if (children[i].nodeName() == sName) 
				{
					bFound = true;
				}
				else
				{
					i = i + 1;
				}
			}
			if (bFound) 
			{
				// remove the node from the document
				nType = children[i].nodeType();
				if (nType == Const.XML_ATTRIBUTE_NODE) 
				{
					oAttr.castToAttribute(children[i]);
					if (oAttr.getOwnerElement(oElement)) 
					{
						bReturn = oElement.removeAttributeNode(oAttr);
					}
				}
				else
				{
					if (children[i].parentNode(oParentNode)) 
					{
						bReturn = oParentNode.removeChild(children[i]);
					}
				}
				// If children[i].nodeType( ) > 0
				// If children[i].parentNode( oParentNode )
				// Call oParentNode.removeChild( children[i] )
				if (bReturn) 
				{
					// if the only item in the list clear it completly
					if (n == 1) 
					{
						children.SetUpperBound(1, -1);
					}
					// else shift them all
					else
					{
						while (i < n) 
						{
							children[i] = children[i + 1];
							i = i + 1;
						}
						children.SetUpperBound(1, n - 1);
					}
				}
			}
			return bReturn;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <param name="sNamespace"></param>
		/// <param name="sName"></param>
		/// <returns></returns>
		public SalBoolean removeNS(SalString sNamespace, SalString sName)
		{
			#region Local Variables
			SalNumber i = 0;
			SalNumber n = 0;
			SalBoolean bFoundName = false;
			SalBoolean bFoundNamespace = false;
			SalBoolean bRemoveNode = false;
			XMLNode oParentNode = new XMLNode();
			XMLAttr oAttr = new XMLAttr();
			XMLElement oElement = new XMLElement();
			SalBoolean bReturn = false;
			SalNumber nType = 0;
			#endregion
			
			#region Actions
			i = 0;
			bReturn = true;
			bRemoveNode = false;
			n = length();
			while (i < n) 
			{
				bFoundName = false;
				bFoundNamespace = false;

				if (children[i].nodeName() == sName) 
				{
					bFoundName = true;
				}

				if (children[i].getNamespaceURI() == sNamespace) 
				{
					bFoundNamespace = true;
				}

				// are we searching for the actual namespace or the prefix for the namespace
				// If children[i].getPrefix() = sNamespace
				// Set bFoundNamespace = TRUE

				// if we have a match break out of while
				if (bFoundName && bFoundNamespace) 
				{
					bRemoveNode = true;
					break;
				}
				// Else continue with next
				i = i + 1;
			}

			if (bRemoveNode) 
			{
				// remove the node from the document
				nType = children[i].nodeType();
				if (nType == Const.XML_ATTRIBUTE_NODE) 
				{
					oAttr.castToAttribute(children[i]);
					if (oAttr.getOwnerElement(oElement)) 
					{
						bReturn = oElement.removeAttributeNode(oAttr);
					}
				}
				else
				{
					if (children[i].parentNode(oParentNode)) 
					{
						bReturn = oParentNode.removeChild(children[i]);
					}
				}
				if (bReturn) 
				{
					// if the only item in the list clear it completly
					if (n == 1) 
					{
						children.SetUpperBound(1, -1);
					}
					// else shift them all
					else
					{
						while (i < n) 
						{
							children[i] = children[i + 1];
							i = i + 1;
						}
						children.SetUpperBound(1, n - 1);
					}
				}
			}
			return bReturn;
			#endregion
		}
		#endregion
	}
}
