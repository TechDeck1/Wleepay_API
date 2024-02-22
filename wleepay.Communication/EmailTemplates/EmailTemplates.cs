using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Communication.EmailTemplates
{
    public class EmailTemplates
    {
        public class EmailHtmlTemplates
        {
            public string EmailTemplate(string email, string uri, string username)
            {
                return $@"
                    <html>
                    <head>
                        <style>
                           body {{
                            font-family: 'Arial', sans-serif;
                            margin: 0; /* Remove default body margin */
                            padding: 0; /* Remove default body padding */
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 0;
                        }}
                        p {{
                            margin-top: 0; /* Remove top margin for paragraphs */
                            margin-bottom: 1em; /* Add a bottom margin for paragraphs for spacing */
                        }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <p>Dear {username},</p>
                            <p>Please click the following link to reset your password</p>
                            <p>{uri}</p>
                        </div>
                    </body>
                    </html>
                ";

            }

            public string EmailTemplateForAccountSetup(string email, string uri, string username)
            {
                return $@"
                    <html>
                    <head>
                        <style>
                           body {{
                            font-family: 'Arial', sans-serif;
                            margin: 0; /* Remove default body margin */
                            padding: 0; /* Remove default body padding */
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 0;
                        }}
                        p {{
                            margin-top: 0; /* Remove top margin for paragraphs */
                            margin-bottom: 1em; /* Add a bottom margin for paragraphs for spacing */
                        }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <p>Dear {username},</p>
                            <p>Please click the following link to complete your signup process:</p>
                            <p>{uri}</p>
                        </div>
                    </body>
                    </html>
                ";

            }
        }
    }
}
