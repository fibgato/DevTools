using System.Net;
using System.Net.Mail;
using System.Text;

namespace NetDevTools.Core.Framework
{
    public interface IEmailService
    {
        Task EnviarEmail(string remetente, string assunto, string mensagem, string destinatario, int timeOut = 60000);
        string RetornarBody(string url, string titulo, string mensagem, string botaoLink = "", string codRedefinicao = "");
    }

    public class EmailService : IEmailService
    {
        private readonly string _usuarioGestao;
        private readonly string _senhaGestao;
        private readonly string _smtpGestao;
        private readonly int _portaGestao;
        private readonly bool _sslGestao;

        public EmailService(string usuario, string senha, string smtp, int porta, bool ssl)
        {
            _usuarioGestao = usuario;
            _senhaGestao = senha;
            _smtpGestao = smtp;
            _portaGestao = porta;
            _sslGestao = ssl;
        }

        private async Task EnviarEmail(SmtpClient client, string remetente, string assunto, string mensagem, string destinatario)
        {
            if (client.Credentials == null) throw new Exception("Credenciais para enviar o email não configuradas");

            var mail = new MailMessage()
            {
                From = new MailAddress(((NetworkCredential)client.Credentials).UserName, remetente),
            };

            mail.To.Add(destinatario);
            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                await client.SendMailAsync(mail);
            }
            catch (SmtpFailedRecipientException)
            {
                //EMAIL INVALIDO
            }
            catch (Exception)
            {
                //CATCH POR ALGUM MOTIVO
            }
        }

        /// <summary>
        /// Enviar um email
        /// </summary>
        /// <param name="remetente">Nome do Rementente. Ex: João da Silva</param>
        /// <param name="assunto">Assunto do Email</param>
        /// <param name="mensagem">Conteudo da Mensagem (HTML)</param>
        /// <param name="destinatario">Email dos Destinatários</param>
        /// <returns></returns>
        public async Task EnviarEmail(string remetente, string assunto, string mensagem, string destinatario, int timeOut = 60000)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.Port = _portaGestao;
                smtp.Host = _smtpGestao;
                smtp.EnableSsl = _sslGestao;
                smtp.Timeout = timeOut;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_usuarioGestao, _senhaGestao);

                await EnviarEmail(smtp, remetente, assunto, mensagem, destinatario);
            }
        }
        public string RetornarBody(string url, string titulo, string mensagem, string botaoLink = "", string codRedefinicao = "")
        {
            var html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"utf-8\">");
            html.AppendLine("<title>@TituloPagina</title>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
            html.AppendLine("<tbody>");
            html.AppendLine("<tr>");
            html.AppendLine("<td>");
            html.AppendLine("<img src=\"@LogoEmail\" alt=\"img1 saurus\" width=\"600\" height=\"200\" style=\"display:block\"/>");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"padding-left:40px; padding-right:40px;\">");
            html.AppendLine("<br/>");
            html.AppendLine("<br/>");
            html.AppendLine("<h2 style=\"font-family:'Century Gothic', Arial, 'Lucida Sans'; color:#246C97; text-align:center\">@TituloEmail</h2>");
            html.AppendLine("<p style=\"font-family: 'Century Gothic', Arial, 'Lucida Sans'; font-size: 14px; color:#555555; text-align:center\">");
            html.AppendLine("@TextoEmail");

            if (!string.IsNullOrEmpty(botaoLink))
            {
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");
                html.AppendLine("<center>");
                html.AppendLine("<a href=\"@URLBotao\" style=\"border:10px solid #246C97; text-decoration: none; background:#246C97; margin-top:50px; color:#fff\">@TextoBotao</a>");
                html.AppendLine("</center>");
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");

            }

            if (!string.IsNullOrEmpty(codRedefinicao))
            {
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");
                html.AppendLine("<center>");
                html.AppendLine("<h4 style=\"font-family:'Century Gothic', Arial, 'Lucida Sans'; color:#ffffff;\">Seu código de redefinição:</h4>");
                html.AppendLine($"<h1 style=\"font-family:'Century Gothic', Arial, 'Lucida Sans';padding: 10px 10px 10px 10px;color:#EB7922;text-decoration:none;margin-top:50px;\">{codRedefinicao}</h1>");
                html.AppendLine("</center>");
                html.AppendLine("<br/>");
                html.AppendLine("<br/>");
            }

            html.AppendLine("</p>");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");
            html.AppendLine("<tr>");
            html.AppendLine("<td colspan=\"3\">");
            html.AppendLine("<img src =\"@LogoRodaPeEmail\" alt=\"img1 saurus\" width=\"600\" height=\"150\" style=\"display:block\" />");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");
            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("<br/>");
            html.AppendLine("<br/>");
            html.AppendLine("<br/>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            var editado = html.ToString();

            editado = editado.Replace("@TituloPagina", titulo);
            editado = editado.Replace("@TituloEmail", titulo);
            editado = editado.Replace("@TextoEmail", mensagem);
            editado = editado.Replace("@TextoBotao", "Clique aqui");
            editado = editado.Replace("@URLBotao", botaoLink);

            editado = editado.Replace("@LogoSistema", url + "style/email/img_01.jpg");
            editado = editado.Replace("@LogoEmail", url + "style/email/img_01_logo.jpg");
            editado = editado.Replace("@LogoRodaPeEmail", url + "style/email/img_03.jpg");

            return editado;
        }
    }
}
