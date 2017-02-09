using PlacementCell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using PlacementCell.PlacementCellDBModel;
using HiQPdf;


namespace PlacementCell.BusinessLogicClasses
{
    public class SendAutomatedMails
    {
        PlacementCellDBContext dbObj = new PlacementCellDBContext();
        string offerLetter = "";
        public void SendMailFromApplication(PlacementDBTestForClass modelObj)
        {
            string From = "OmniCorporates Careers";
            string FromEmail = "rajat.sedate@gmail.com";

            var candidate = dbObj.CandidateProfileTable.Find(modelObj.SelectCandidate);
            string toMail = candidate.MailAddress;
            string messageContent = "";

            messageContent = "Hi, " + "<br/>" +
                "Congratulations, Your profile has been shortlisted for recruitment drive. Following is the schedule" + "<br/>" +
                "Technical Round :" + modelObj.TechnicalInterviewDate.ToString() + "<br/>" +
                "Written Round :" + modelObj.TestDate.ToString() + "<br/>" +
                "HR Interview Round :" + modelObj.HrInterviewDate.ToString() + "<br/>" +
                "Regards, " + "<br/>" +
                "OmniCorporates";


            // var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(toMail));  // replace with valid value 
            message.From = new MailAddress("rajat.sedate@gmail.com");  // replace with valid value
            message.Subject = "OmniKart Recruitments";
            message.Body = string.Format(messageContent, From, FromEmail);
            message.IsBodyHtml = true;

            SendMailMainFunction(message);


        }

        public void SendMailMainFunction(MailMessage message)
        {

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "",  // replace with valid value
                    Password = ""  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
        }
        public void SendOfferLetterFromMail(EmployeeModelForClass empModel)
        {
            string From = "OmniCorporates Careers";
            string FromEmail = "rajat.sedate@gmail.com";

            var employee = dbObj.EmployeeTable.Find(empModel.EmployeeID);
            string toMail = employee.Email;

            int index = empModel.msg.IndexOf('{');
            offerLetter = empModel.msg.Substring(0, index - 1);

            //How to get the offer letter here

            // var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(toMail));  // replace with valid value 
            message.From = new MailAddress("rajat.sedate@gmail.com");  // replace with valid value
            message.Subject = "OmniKart Recruitments";
            message.Body = string.Format(offerLetter, From, FromEmail);


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "rajat.sedate@gmail.com",  // replace with valid value
                    Password = "Mygmail@2016"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
        }

        public void ConvertHTMLToPdf(EmployeeModelForClass empModel)
        {
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // set browser width 
            //htmlToPdfConverter.BrowserWidth = int.Parse(textBoxBrowserWidth.Text);

            // set browser height if specified, otherwise use the default 
            //if (textBoxBrowserHeight.Text.Length > 0)
            //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

            //// set HTML Load timeout 
            //htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

            //// set PDF page size and orientation 
            //htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
            //htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation();

            // set PDF page margins 
            htmlToPdfConverter.Document.Margins = new PdfMargins(0);

            //// set a wait time before starting the conversion 
            //htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);

            // convert HTML to PDF 
            byte[] pdfBuffer = null;

            //if (radioButtonConvertUrl.Checked)
            //{
            //    // convert URL to a PDF memory buffer 
            //    string url = textBoxUrl.Text;

            //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
            //}
            //else
            //{
            // convert HTML code 
            string htmlCode = empModel.msg;
            string baseUrl = "Home/GenerateOfferLetter";

            // convert HTML code to a PDF memory buffer 
            pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
            //}

            // inform the browser about the binary data format 
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

            // instruct browser how to open PDF as attachment or inline and file name 
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                true ? "inline" : "attachment",
                pdfBuffer.Length.ToString()));

            // write the PDF buffer to HTTP response 
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            // call End() method of HTTP response to stop ASP.NET page processing 
            HttpContext.Current.Response.End();
        }

        public void SendMailToEmployee(string to, string subject, string messageBody)
        {
            string From = "OmniCorporates Careers";
            string FromEmail = "rajat.sedate@gmail.com";

            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(to));  // replace with valid value 
            message.From = new MailAddress("rajat.sedate@gmail.com");  // replace with valid value
            message.Subject = subject;
            message.Body = string.Format(messageBody, From, FromEmail);
            message.IsBodyHtml = true;

            SendMailMainFunction(message);

        }



        internal void SendPasswordOnEmail(LoginDBModel user)
        {
            string From = "OmniCorporates Careers";
            string FromEmail = "rajat.sedate@gmail.com";

            string messageBody = "Your Omnikart Password is:" +
                Environment.NewLine +
                "\"" + user.Password + "\"" +
                Environment.NewLine;

            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(user.UserName));  // replace with valid value 
            message.From = new MailAddress("rajat.sedate@gmail.com");  // replace with valid value
            message.Subject = "Omnikart Password Request";
            message.Body = string.Format(messageBody, From, FromEmail);
            message.IsBodyHtml = true;

            SendMailMainFunction(message);
        }
    }
}