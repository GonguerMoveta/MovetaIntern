using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using PPJ.Runtime;
using PPJ.Runtime.Scripting;

namespace Moveta.Intern.Classes
{
	public class MailService
	{
		private SmtpClient Client;
		private ImapClient ImapClient;
		private Envelope envelop ;

		public MailService() { 
		  
			this.Client = new SmtpClient();
			this.ImapClient = new ImapClient();
			Envelope envelop = new Envelope();
		}

		public SalNumber smtpConnect(SalString param1, SalNumber param2, SalBoolean param3, ref SalNumber param4)
		{
		   SalNumber retVal = 0 ;
		   string _param1 = param1.Value;
		   int _param2 = (int)param2;
		   bool _param3 = (bool)param3;
		   uint _param4 = (uint)param4;
			try
			{
				this.Client.Connect(_param1, _param2, MailKit.Security.SecureSocketOptions.StartTls);
				if(this.Client.IsConnected)
				{
					retVal = 0;				
				}
				else
				{
					retVal = 1;
				}	
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);			
			}
			 
			return retVal;
		}

		public SalNumber smtpAuthenticate(SalString param3, SalString param4)
		{
			SalNumber retVal = 0;
			string _param3 = param3.Value;
			string _param4 = param4.Value;
			try
			{
				this.Client.Authenticate(_param3, _param4);
				if (this.Client.IsAuthenticated)
				{
					retVal = 0;
				}
				else
				{
					retVal = 1;
				}			
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				retVal = 1;
			}

			return retVal;
		}

		public SalNumber smtpMailDisconnect(Boolean param1)
		{
			SalNumber retVal = 0;
			
			try
			{
				this.Client.Disconnect(param1);
				if(!this.Client.IsConnected)
				{
					retVal = 0;
				}
				else
				{
					retVal = 1;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);		
			}

			return retVal;
		}

		public SalNumber SmtpSendMail(SalNumber param1, SalNumber param2, SalString param3, SalString param4, SalString param5, SalString param6, SalString param7, SalString param8, SalBoolean param9, SalNumber param10, SalString param11, SalString param12, SalString param13, SalString param14)
		{
			SalNumber retVal = 0;
			uint _param1 = (uint)param1;
			byte _param2 = (byte)param2;
			string _param3 = param3.Value;
			string _param4 = param4.Value;
			string _param5 = param5.Value;
			string _param6 = param6.Value;
			string _param7 = param7.Value;
			string _param8 = param8.Value;
			bool _param9 = (bool)param9;
			byte _param10 = (byte)param10;
			string _param11 = param11.Value;	
			string _param12 = param12.Value;

			string _param13 = param13.Value;
			string _param14 = param14.Value;

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_param11, _param4));
			message.To.Add(new MailboxAddress(_param12, _param5));
			message.Subject = _param3;
			message.Body = new TextPart("plain")
			{
				Text = _param7
		    };

			try
			{
				if(this.smtpAuthenticate(_param13, _param14) == 0)
				{
					this.Client.Send(message);					
					retVal = 0;
				}
				else
				{
					retVal = 7;
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);			
			}

			return retVal;

		}

        /// <summary>
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <returns></returns>
        public SalNumber ImapConnect(SalString param1, SalNumber param2, SalString param3, SalString param4, SalBoolean param5)
        {
            SalNumber retVal = 0;
            string _param1 = param1.Value;
            int _param2 = (int)param2;
            string _param3 = param3.Value;
            string _param4 = param4.Value;
            bool _param5 = (bool)param5;

			try
			{
                this.ImapClient.Connect(_param1, _param2, MailKit.Security.SecureSocketOptions.StartTls);
                if (this.ImapClient.IsConnected)
                {
                    retVal = 0;
                }
                else
                {
                    retVal = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return retVal;
        }

		

        public SalNumber ImapAuthenticate(SalString param3, SalString param4)
        {
            SalNumber retVal = 0;
            string _param3 = param3.Value;
            string _param4 = param4.Value;
            try
            {
                this.ImapClient.Authenticate(_param3, _param4);
                if (this.ImapClient.IsAuthenticated)
                {
                    retVal = 0;
                }
                else
                {
                    retVal = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retVal;
        }

        /// <summary>
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <param name="param6"></param>
        /// <param name="param7"></param>
        /// <param name="param8"></param>
        /// <param name="param9"></param>
        /// <returns></returns>
        public SalNumber ImapGetMail(SalNumber param1, SalNumber param2, ref SalString param3, ref SalString param4, ref SalString param5, ref SalString param6, ref SalString param7, ref SalString param8, ref SalString param9)
        {
            SalNumber retVal = 0;
			int i ;
            uint _param1 = (uint)param1;
            byte _param2 = (byte)param2;
            string _param3 = param3.Value;
            string _param4 = param4.Value;
            string _param5 = param5.Value;
            string _param6 = param6.Value;
            string _param7 = param7.Value;
            string _param8 = param8.Value;
            bool _param9 = (bool)param9;
           
            this.ImapAuthenticate(_param5, _param6);
			
            this.ImapClient.Inbox.Open(MailKit.FolderAccess.ReadOnly);
			if(this.ImapClient.Inbox.IsOpen)
			{
                for (i = 0; i < ImapClient.Inbox.Count; i++)
                {
                    Console.WriteLine("From: {0}", ImapClient.Inbox.GetMessage(i).From);
                    Console.WriteLine("Subject: {0}", ImapClient.Inbox.GetMessage(i).Subject);
                    Console.WriteLine("Date: {0}", ImapClient.Inbox.GetMessage(i).Date);
                    Console.WriteLine("TextBody: {0}", ImapClient.Inbox.GetMessage(i).TextBody);
                }
				retVal = 0;
            }
			else
			{
				retVal = 1;
			}
		
            return retVal;
        }

        /// <summary>
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <param name="param6"></param>
        /// <param name="param7"></param>
        /// <param name="param8"></param>
        /// <param name="param9"></param>
        /// <param name="param10"></param>
        /// <returns></returns>
        public static SalNumber ImapGetNextMail(SalNumber param1, SalNumber param2, ref SalNumber param3, ref SalString param4, ref SalString param5, ref SalString param6, ref SalString param7, ref SalString param8, ref SalString param9, ref SalString param10)
        {
            SalNumber retVal = 0;
            // TODO: Implement the external function.
            return retVal;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public SalNumber ImapGetNumberMessages()
        {
            SalNumber retVal = 0;

			if(this.ImapClient.Inbox != null)
			{
                retVal = this.ImapClient.Inbox.Count();
            }
			
            return retVal;
        }

		public SalNumber ImapGetNumberMessages(SalNumber param1, SalNumber param2, ref SalNumber param3, ref SalString param4, ref SalString param5)
		{
			SalNumber retVal = 0;
			

		  return retVal;
		}

		public SalNumber ImapGetEnvelopes(SalNumber param1, SalNumber param2, SalArray<SalNumber> param3, SalArray<SalString> param4, SalArray<SalString> param5, SalArray<SalString> param6)
		{
			SalNumber retVal = 0;
			int i;
			uint _param1 = (uint)param1;
			uint _param2 = (uint)param2;
			InternetAddressList _param6 = new InternetAddressList();
			//List<int> _param3 = param3.;
			//string _param4 = param4.Value;
			//string _param5 = param5.Value;
			//string _param6 = param6.Value;
			Envelope envelop = new Envelope();

			for (i = 0; i < ImapClient.Inbox.Count; i++)
			{

				//ImapClient.Inbox.GetMessage(i).From.ToString();
				//ImapClient.Inbox.GetMessage(i).Sender.ToString();
				//ImapClient.Inbox.GetMessage(i).InReplyTo.ToString();
				//ImapClient.Inbox.GetMessage(i).To.ToString();
				//ImapClient.Inbox.GetMessage(i).Bcc.ToString();
				//ImapClient.Inbox.GetMessage(i).Cc.ToString();

				// ImapClient.Inbox.GetMessage(i).From.ToString();
				//Console.WriteLine("Subject: {0}", );
				//Console.WriteLine("Date: {0}", );
				//Console.WriteLine("TextBody: {0}", );
			}


			return retVal;

		}

	}
}
