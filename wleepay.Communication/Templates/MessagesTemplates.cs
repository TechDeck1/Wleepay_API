using Newtonsoft.Json;
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
            return $"Your WleeSend security code is {otp}.";
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

        public static string TransactionCompleted(string amount, string currency, long txNumber)
        {
            return $"A transction of {amount} {currency} with transaction number {txNumber} is completed.";
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
        public static string CustomerTicketReplyMessageSmsTemplate(string Username, string message)
        {
            return $" Dear {Username}, \n\n Reply from system administrator:\n\n{message}";
        }

        public static string AirtimeAndLecPaymentTemplate(string operatorName, string transactionId , string referenceId, string date, string amount)
        {
            return $"You received {amount} airtime credit from WleeSend.com. Thank you.";
        }

        public static string LecCreditPurchaseReceiptTemplate(string paidAmount, string units, string tokenNumber, string meterNumber, string lecCustomerName, string transactionId, string receiptNumber, string date)
        {
            return $"You received ${paidAmount} LEC credit from WleeSend.com. Meter {meterNumber}. Token {tokenNumber}.";
        }

        // ADD THIS: HTML Template for Mobile Recharge Receipt
        public static string MobileRechargeReceiptEmail(string customerName, string totalAmount, string creditAmount, string msisdn, string txId, string refId, string date, string operatorName, string productType, string token, string validUntil)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: 'Arial', sans-serif; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
                    h2 {{ color: #0056b3; }}
                    .details {{ margin-top: 20px; }}
                    .label {{ font-weight: bold; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Transaction Receipt</h2>
                    <p>Dear {customerName},</p>
                    <p>Your mobile airtime recharge was <strong>Successful</strong>.</p>
                    
                    <div class='details'>
                        <p><span class='label'>Date:</span> {date}</p>
                        <p><span class='label'>Transaction ID:</span> {txId}</p>
                        <p><span class='label'>Reference:</span> {refId}</p>
                        <p><span class='label'>Receiver Phone Number:</span> {msisdn}</p>
                        <p><span class='label'>Credit Type:</span> {operatorName} - {productType}</p>
                        <p><span class='label'>Credit Amount:</span> ${creditAmount}</p>
                        <p><span class='label'>Airtime Token:</span> {token}</p>
                        <p><span class='label'>Valid Until:</span> {validUntil}</p>
                        <p><span class='label'>Total Amount:</span> ${totalAmount}</p>
                    </div>

                    <p>Thank you for using WleeSend.</p>
                </div>
            </body>
            </html>";
        }

        // ADD THIS: HTML Template for LEC Credit Receipt
        public static string LecCreditReceiptEmail(string customerName, string totalAmount, string creditAmount, string receiverPhoneNumber, string token, string meterNumber, string units, string txId, string date)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: 'Arial', sans-serif; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
                    h2 {{ color: #0056b3; }}
                    .token-box {{ background-color: #f8f9fa; padding: 15px; border: 1px dashed #0056b3; text-align: center; font-size: 18px; font-weight: bold; margin: 20px 0; }}
                    .label {{ font-weight: bold; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>LEC Credit Receipt</h2>
                    <p>Dear {customerName},</p>
                    <p>Your LEC Credit Purchase was <strong>Successful</strong>.</p>

                    <div class='token-box'>
                        TOKEN: {token}
                    </div>
                    
                    <div class='details'>
                        <p><span class='label'>Date:</span> {date}</p>
                        <p><span class='label'>Transaction ID:</span> {txId}</p>
                        <p><span class='label'>Receiver Phone Number:</span> {receiverPhoneNumber}</p>
                        <p><span class='label'>Meter Number:</span> {meterNumber}</p>
                        <p><span class='label'>Units:</span> {units} kWh</p>
                        <p><span class='label'>Credit Amount:</span> ${creditAmount}</p>
                        <p><span class='label'>Total Amount:</span> ${totalAmount}</p>
                    </div>

                    <p>Thank you for using WleeSend.</p>
                </div>
            </body>
            </html>";
        }


    }
}
