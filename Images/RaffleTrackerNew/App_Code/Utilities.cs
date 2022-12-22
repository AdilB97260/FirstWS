using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Utilities
/// </summary>

public static class Utilities
{

    private static readonly Random _rng = new Random();
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789"; //Added 1-9

    #region[MESSAGES]

    public static void ShowMessage(string Message, string Title, System.Web.UI.Page Page)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "_ShowMessage", "jAlert('" + Message + "','" + Title + "');", true);
    }

    public static void ShowSuccessMessage(string Message, string Title, System.Web.UI.Page Page, string RedirectPage)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "jAlert('" + Message + "','" + Title + "',true, '" + RedirectPage + "');", true);
    }

    public static void ShowImportantMessage(string Message, string Title, System.Web.UI.Page Page)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "jImportant('" + Message + "','" + Title + "');", true);
    }

       

    #endregion

    #region [CROPE IMAGE]
    public static void CropImageFixedSize(HttpPostedFile pf, Size _Size, string SavePath, string ImageName)
    {
        System.Drawing.Image OriginalImage = System.Drawing.Image.FromStream(pf.InputStream);
        System.Drawing.Image ResizeImage = System.Drawing.Image.FromStream(pf.InputStream);


        System.Drawing.Imaging.ImageFormat imgFormate = System.Drawing.Imaging.ImageFormat.Jpeg;
        string ext = System.IO.Path.GetExtension(pf.FileName);

        ResizeImage = ResizeBitmap((Bitmap)ResizeImage, _Size.Width, _Size.Height);
        if (ext.ToLower() == ".jpg")
        {
            imgFormate = System.Drawing.Imaging.ImageFormat.Jpeg;
        }
        else if (ext.ToLower() == ".gif")
        {
            imgFormate = System.Drawing.Imaging.ImageFormat.Gif;
        }
        else if (ext.ToLower() == ".png")
        {
            imgFormate = System.Drawing.Imaging.ImageFormat.Png;
        }
        else if (ext.ToLower() == ".bmp")
        {
            imgFormate = System.Drawing.Imaging.ImageFormat.Bmp;
        }

        ResizeImage.Save(HttpContext.Current.Server.MapPath(SavePath + ImageName), imgFormate);
        ResizeImage.Dispose();
    }

    private static Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
    {
        Bitmap result = new Bitmap(nWidth, nHeight);
        using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
            g.DrawImage(b, 0, 0, nWidth, nHeight);
        return result;
    }

    #endregion

    public static int GetLoginKeyUserKey()
    {
        int retKey = UserSession.Inst.UserPK;
        if (UserSession.Inst.UserType == "CHURCH")
        {
            retKey = UserSession.Inst.ChurchPK;
        }
        if (UserSession.Inst.UserType == "RAFFLE")
        {
            retKey = UserSession.Inst.RafflePK;
        }
        if (UserSession.Inst.UserType == "MEMBER")
        {
            retKey = UserSession.Inst.MemberPK;
        }
        return retKey;
    }



    public enum PAGEID
    {
        SALESDISTREP,
        SALESDATERANGEREP,
        DISTREP,
        TICKDISTREP
    }


    public static int GetAccessedUserKey()
    {
        int retKey = UserSession.Inst.UserPK;
        if (UserSession.Inst.Member2MemberPK > 0)
        {
            retKey = UserSession.Inst.Member2MemberPK;
        }
        else if (UserSession.Inst.MemberPK > 0)
        {
            retKey = UserSession.Inst.MemberPK; 
        }
        else if (UserSession.Inst.ChurchPK > 0)
        {
            retKey = UserSession.Inst.ChurchPK; 
        }
        else if (UserSession.Inst.RafflePK > 0)
        {
            retKey = UserSession.Inst.RafflePK; 
        }

        return retKey;

    }


    public static string GetActicationCode()
    {

        return string.Format("{0}-{1}-{2}", RandomString(4), RandomString(5), RandomString(8));
    }


    public static string RandomString(int size)
    {
        char[] buffer = new char[size];

        for (int i = 0; i < size; i++)
        {
            buffer[i] = _chars[_rng.Next(_chars.Length)];
        }
        return new string(buffer);
    }

    #region [ENCRIPTION DECRIPTION]

    //static byte[] Key_64 = new byte[] { 255, 8, 90, 100, 11, 12, 13, 14 };
    //static byte[] Iv_64 = new byte[] { 65, 110, 68, 26, 69, 178, 200, 219 };

    //public static string Encrypt(string value)
    //{
    //    if (value != "")
    //    {
    //        DESCryptoServiceProvider CryptoProvidor = new DESCryptoServiceProvider();
    //        CryptoProvidor.Padding = PaddingMode.PKCS7;
    //        MemoryStream ms = new MemoryStream();
    //        CryptoStream cs = new CryptoStream(ms, CryptoProvidor.CreateEncryptor(Key_64, Iv_64), CryptoStreamMode.Write);
    //        StreamWriter sw = new StreamWriter(cs);
    //        sw.Write(value);
    //        sw.Flush();
    //        cs.FlushFinalBlock();
    //        ms.Flush();
    //        return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
    //    }
    //    return string.Empty;
    //}

    //public static string Descrypt(string value)
    //{
    //    if (value != "")
    //    {
    //        value = value.Replace(" ", "+");
    //        DESCryptoServiceProvider CryptoProvidor = new DESCryptoServiceProvider();
    //        CryptoProvidor.Padding = PaddingMode.PKCS7;
    //        Byte[] buf = Convert.FromBase64String(value);
    //        MemoryStream ms = new MemoryStream(buf);
    //        CryptoStream cs = new CryptoStream(ms, CryptoProvidor.CreateDecryptor(Key_64, Iv_64), CryptoStreamMode.Read);
    //        StreamReader sr = new StreamReader(cs);
    //        return sr.ReadToEnd();
    //    }
    //    return string.Empty;
    //}
    #endregion

    #region [SEND MAIL]


    //public static void SendToMailWithAttachment(string From, string To, string Body, string Subject, bool IsHtmlBody = false, string AttachedFileContent = "")
    //{
    //    try
    //    {


    //        string SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];
    //        string SMTPUserName = WebConfigurationManager.AppSettings["SMTPUserName"];
    //        string SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];


    //        MailMessage objMailMessage = new MailMessage();
    //        MailAddress objMailAddress = new MailAddress(From);

    //        objMailMessage.From = objMailAddress;
    //        string[] toIds = To.Split(';');
    //        if (toIds.Length > 1)
    //        {
    //            foreach (var id in toIds)
    //            {
    //                if (id != "")
    //                    objMailMessage.To.Add(new MailAddress(id));
    //            }
    //        }
    //        else
    //            objMailMessage.To.Add(new MailAddress(To));

    //        objMailMessage.Subject = Subject;
    //        if (IsHtmlBody)
    //            objMailMessage.IsBodyHtml = true;
    //        else
    //            objMailMessage.IsBodyHtml = false;

    //        objMailMessage.Body = Body;
    //        objMailMessage.Priority = MailPriority.Normal;

    //        StringReader sr = new StringReader(AttachedFileContent.ToString());
    //        Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);

    //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
    //            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/" + PdfName + ".pdf"), FileMode.Create));
    //            pdfDoc.Open();
    //            htmlparser.Parse(sr);
    //            pdfDoc.Close();
    //            byte[] bytes = memoryStream.ToArray();
    //            memoryStream.Close();
    //            objMailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "Temp-NAA-Card.pdf"));
    //        }

    //        if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
    //        {
    //            using (var client = new SmtpClient("smtp.gmail.com", 587)
    //            {
    //                Credentials = new NetworkCredential("hbagwan21@gmail.com", "Optimist9"),
    //                EnableSsl = true
    //            })
    //            {
    //                client.Send(objMailMessage);
    //            }
    //        }

    //        else
    //        {
    //            using (var client = new SmtpClient(SMTPServer, 25)
    //            {
    //                Credentials = new NetworkCredential(SMTPUserName, SMTPPassword),
    //                //EnableSsl = true
    //            })
    //            {
    //                client.Send(objMailMessage);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}



    public static void SendToMail(string From, string To, string Body, string Subject, bool IsHtmlBody = false)
    {
        try
        {


            string SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];
            string SMTPUserName = WebConfigurationManager.AppSettings["SMTPUserName"];
            string SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];


            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAddress = new MailAddress(From);

            objMailMessage.From = objMailAddress;
            string[] toIds = To.Split(';');
            if (toIds.Length > 1)
            {
                foreach (var id in toIds)
                {
                    if (id != "")
                        objMailMessage.To.Add(new MailAddress(id));
                }
            }
            else
                objMailMessage.To.Add(new MailAddress(To));

            objMailMessage.Subject = Subject;
            if (IsHtmlBody)
                objMailMessage.IsBodyHtml = true;
            else
                objMailMessage.IsBodyHtml = false;

            objMailMessage.Body = Body;
            objMailMessage.Priority = MailPriority.Normal;


            if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("hbagwan21@gmail.com", "Optimist9"),
                    EnableSsl = true
                })
                {
                    client.Send(objMailMessage);
                }
            }

            else
            {
                using (var client = new SmtpClient(SMTPServer, 25)
                {
                    Credentials = new NetworkCredential(SMTPUserName, SMTPPassword),
                    //EnableSsl = true
                })
                {
                    client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                    client.Send(objMailMessage);
                }
            }







            //SmtpClient objSmtpClient = new SmtpClient();
            //objSmtpClient.Host = "smtp.gmail.com";
            //objSmtpClient.Port = 587;
            //objSmtpClient.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);


            ////SmtpClient objSmtpClient = new SmtpClient(SMTPServer);



            //MailMessage objMailMessage = new MailMessage();
            //MailAddress objMailAddress = new MailAddress(From);

            //objMailMessage.From = objMailAddress;
            //string[] toIds = To.Split(';');
            //if (toIds.Length > 1)
            //{
            //    foreach (var id in toIds)
            //    {
            //        if (id != "")
            //            objMailMessage.To.Add(new MailAddress(id));
            //    }
            //}
            //else
            //    objMailMessage.To.Add(new MailAddress(To));

            //objMailMessage.Subject = Subject;
            //if (IsHtmlBody)
            //    objMailMessage.IsBodyHtml = true;
            //else
            //    objMailMessage.IsBodyHtml = false;

            //objMailMessage.Body = Body;
            //objMailMessage.Priority = MailPriority.Normal;
            //objSmtpClient.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
            //objSmtpClient.EnableSsl = true;
            //objSmtpClient.UseDefaultCredentials = false;
            //objSmtpClient.Send(objMailMessage);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   public static void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Cancelled == true || e.Error != null)
        {
            throw new Exception(e.Cancelled ? "EMail sedning was canceled." : "Error: " + e.Error.ToString());
        }
    }


    public static string GetFileContents(string FullPath)
    {
        string strContents = string.Empty;
        StreamReader objReader;
        try
        {
            objReader = new StreamReader(FullPath);
            strContents = objReader.ReadToEnd();
            objReader.Close();
            return strContents;
        }
        catch { }
        return strContents;
    }
    #endregion
}
