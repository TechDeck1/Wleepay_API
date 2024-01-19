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

        public static string CustomerTicketClosedMessage( int ticketId)
        {
            return $"Your Customer ticket with ticket id #{ticketId} is closed.";
        }

        public static string CustomerTicketCreatedMessage(int ticketId)
        {
            return $"Your Customer ticket with ticket id #{ticketId} is created.";
        }
    }
}
