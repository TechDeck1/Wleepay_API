using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Communication.Templates
{
    public static class MessagesTemplates
    {
        public static string RegistrationSuccessMessage(string accountNumber, string level) {
            return $"Your Wlee account is now active. Account #{accountNumber}, Level {level}";
        }

        public static string PinResetOTPMessage(string otp)
        {
            return $"Your Wlee account is now active. Account #{otp}";
        }

        public static string ValidateNumberOTPMessage(string otp)
        {
            return $"Your Wleepay security code is {otp}.";
        }

        public static string PaymentApprovedRejected(string amount, string currency, string agent, long requestNumber, bool isCashOut = false, bool rejected = false)
        {
            if(rejected)
                return isCashOut ? $"Your CashOut request for {amount} {currency} is rejected" : $"Your TopUp request for {amount} {currency} is rejected";

            return isCashOut ? $"Your CashOut request for {amount} {currency} is approved": $"Your TopUp request for {amount} {currency} is approved";
        }

        public static string PaymentRequestReceived(string amount, string currency, string customer, long requestNumber, bool isCashOut = false)
        {
            return isCashOut ? $"A CashOut request for {amount} {currency} received from {customer}.": $"A TopUp request for {amount} {currency} received from {customer}.";
        }

        public static string PaymentReceived(string amount, string currency, string customer, long txNumber)
        {
            return $"A payment of {amount} {currency} received from {customer}.";
        }

        public static string CustomerTicketClosedMessage(string issueType, int ticketId)
        {
            return $" Your support ticket for {issueType} with TicketID:{ticketId} has been closed.";

        }

        public static string CustomerTicketCreatedMessage(string issueType, int ticketId)
        {
            return $" Your support ticket for {issueType} with TicketID:{ticketId} has been created.";
        }

        public static string CustomerTicketReplyMessage(string Username, string message)
        {
            //return $" Dear {Username}, \n\n Reply from system administrator:{message}\n\n Your support ticket has been closed.";
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
                            <p>Dear {Username},</p>
                            <p>Reply from system administrator: {message}</p>
                            <p><b>Your support ticket has been closed.<b></p>
                        </div>
                    </body>
                    </html>
                ";

        }

        public static string PaymentReceivedEmail(string amount, string currency, string customer, long txNumber, string ReceiverName)
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
                            <p>Dear {ReceiverName},</p>
                            <p>A payment of {amount} {currency} received from {customer}.</p>
                        </div>
                    </body>
                    </html>
                ";

        }

        public static string PaymentReceivedInCashOnHandAccount(string amount, string currency, string SmsReciverName)
        {
            return $"Dear {SmsReciverName},\nA payment of {amount} {currency} is credited to cash on hand account.";

        }

        public static string TaxRateUpdateMessage(string taxRate, string SmsReciverName, string Taxtype)
        {
            return $"Dear {SmsReciverName}, \n A tax rate for {Taxtype} is changed by {taxRate}%. ";

        }
    }
}
