using System.Text;

namespace PublicRecords.Infraestructure.Services.SendEmail
{
    public class CreateMailMessageBody
    {
        public static string GenerateEmailHtmlTemplate(string title, Dictionary<string, string> body)
        {
            string responseMailBody = MailBody(body);

            return $@"
                    <html lang=""pt-br"">
                    <head>
                        <style>

                            body {{
                                font - family: Arial, sans-serif;
                                font-size: 1.1em;
                                background-color: #f4f4f4;
                                padding-left: 10px;
                                padding-right: 10px;
                                padding-bottom: 5px;
                                padding-top: 1px;
                                border-radius: 5px;
                            }}

                            h3 {{
                                color: black;
                                display: flex;
                                justify-content: center;
                            }}

                            
                            footer p {{
                                justify-content: center;
                                font-size: 0.6em;
                            }}


                        </style>    
                    </head>
                    <body>" +
                    responseMailBody
                    +
                    @"</body>
                    <footer>
                        <p>Este é um e-mail automático, favor não responder.</p>
                    </footer>
                    </html>";
        }

        internal static string MailBody(Dictionary<string, string> body)
        {
            if (body == null || body.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            foreach (var (key, value) in body)
            {
                if (key.Contains("titulo", StringComparison.OrdinalIgnoreCase))
                {
                    stringBuilder.AppendLine($"<h3>{value}</h3>\n");
                }
                else
                {
                    stringBuilder.AppendLine($"<p>{value}</p>");
                }
            }

            return stringBuilder.ToString();
        }

    }
}
