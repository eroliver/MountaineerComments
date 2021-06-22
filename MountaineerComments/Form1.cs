using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MountaineerComments
{
    public partial class Form1 : Form
    {
        MailMessage message;
        SmtpClient smtp;
        string commentRecipient = "mtr.misdepartment@cnty.com";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try

            {

                SendButton.Enabled = false;


                message = new MailMessage();


                //no need for this, we will pass the email
                //if (IsValidEmail(txtTo.Text))

                //{

                message.To.Add(commentRecipient);

                //}

                //if (IsValidEmail(txtCC.Text))

                //{

                //message.CC.Add(txtCC.Text);

                //}


                message.Subject = "Message from MountaineerComments Application";

                message.From = new MailAddress("mountaineercomments@cnty.com");

                if (CheckField(Comment) && CheckField(CommenterName))
                {

                    string comment = ($"{CommenterName.Text}:    {Comment.Text}");
                    message.Body = comment;
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("Name and Comment are BOTH required" +
                        "\n and can NOT contain 5 consecutive characters.");


                    SendButton.Enabled = true;

                    ResetFields();

                    return;
                }

                //if (lblAttachment.Text.Length > 0)

                //{

                //if (System.IO.File.Exists(lblAttachment.Text))

                //{

                //message.Attachments.Add(new Attachment(lblAttachment.Text));

                //}

                //}



                // set smtp details

                smtp = new SmtpClient("smtp-relay.gmail.com");

                smtp.Port = 25;

                smtp.EnableSsl = false;

                //smtp.Credentials = new NetworkCredential(emailSender, "nsmldcwqslfijndu");

                smtp.SendAsync(message, message.Subject);

                smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

                ResetFields();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);


                SendButton.Enabled = true;

            }
        }

        private void CommentBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)

        {

            if (e.Cancelled == true)

            {

                MessageBox.Show("Comment sending cancelled!");

            }

            else if (e.Error != null)

            {

                MessageBox.Show(e.Error.Message);

            }

            else

            {

                MessageBox.Show("Comment sent sucessfully! Thank you!");

            }




            SendButton.Enabled = true;

        }



        private void btnCancel_Click(object sender, EventArgs e)

        {

            smtp.SendAsyncCancel();

            MessageBox.Show("Comment sending cancelled!");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private bool CheckField(TextBox textBox)
        {
            //Verifies user input in text fields, checking for empty string or repeating characters.
            if (textBox.Text != String.Empty && !HasConsecutiveChars(textBox.Text, 5))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool HasConsecutiveChars(string source, int sequenceLength)
        {
            // should check sequence length, and return true if it finds consecutive repeating characters.

            // just repeating letters
            //return Regex.IsMatch(source, "([a-zA-Z])\\1{" + (sequenceLength - 1) + "}");

            // any character version
            return Regex.IsMatch(source,"(.)\\1{"+ (sequenceLength - 1) + "}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CommenterNamesucks_TextChanged(object sender, EventArgs e)
        {

        }

        private void ResetFields()
        {
            Comment.Text = string.Empty;
            CommenterName.Text = string.Empty;

            //foreach (Control control in this.Controls)
            //{
            //    if (control is TextBox)
            //        if ((control as TextBox).Text != string.Empty)
            //        {
            //            Text = string.Empty;
            //        }
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        



        //SmtpClient.SendAsyncCancel() method is used to cancel the email send operation.



        //implement to add attachments along with message
        //private void lnkAttachFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)

        //{

        //if (openFileDialog1.ShowDialog() != DialogResult.Cancel)

        //{

        //lblAttachment.Text = openFileDialog1.FileName;

        //lblAttachment.Visible = true;

        //lnkAttachFile.Visible = false;

        //}
    }
}

