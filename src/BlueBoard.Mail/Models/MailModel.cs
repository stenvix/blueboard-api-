namespace BlueBoard.Mail.Models
{
    public class MailModel
    {
        public MailModel(string mailTo, string subject, string text)
        {
            this.MailTo = mailTo;
            this.Subject = subject;
            this.Text = text;
        }
        
        public string MailTo { get; }
        public string Subject { get; }
        public string Text { get; }
    }
}
