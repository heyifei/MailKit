//Thanks to jstedfast 
public static void SaveSentMailToSentFloder(MimeMessage msg) 
        {
            ImapClient client = new ImapClient();
            
            client.Connect(Form1.SMTPServer, Form1.ReceivePort,SecureSocketOptions.None);
            
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(Form1.IMAPAccount, Form1.IMAPPWD);
            
            //��ȡ�����ļ���
            //List<IMailFolder> mailFolderList = client.GetFolders(client.PersonalNamespaces[0]).ToList();
            IMailFolder folder;
            if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
            {
                folder = client.GetFolder(SpecialFolder.Sent);
            }
            else 
            { 
                var personal = client.GetFolder(client.PersonalNamespaces[0]);
                folder = personal.GetSubfolder("Sent");
            }
 
            folder.Append(msg, MessageFlags.Seen);
            client.Disconnect(true);
            client.Dispose();
            
        }