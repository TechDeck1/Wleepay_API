using System;
using System.Net;
using System.Text;
using QRCoder;

namespace wleepay.Communication.Templates
{
    public static class GiftVoucherReceiptTemplate
    {
        public const string QrContentId = "wleesend-voucher-qr";

        public static byte[] CreateQrCode(string voucherCode)
        {
            if (string.IsNullOrWhiteSpace(voucherCode))
                throw new ArgumentException("A voucher code is required to create its QR code.", nameof(voucherCode));

            using (var generator = new QRCodeGenerator())
            using (var data = generator.CreateQrCode(voucherCode.Trim(), QRCodeGenerator.ECCLevel.Q))
            {
                var renderer = new PngByteQRCode(data);
                return renderer.GetGraphic(10);
            }
        }

        public static string BuildHtml(
            string firstName, string operatorName, string productName,
            string faceValue, string amountPaid, string voucherCode,
            string validUntil, string orderReference, string purchaseDate,
            string providerInstructions)
        {
            var sb = new StringBuilder();
            sb.Append("<!doctype html><html><body style=\"margin:0;background:#f4f6f8;font-family:Arial,sans-serif;color:#202631\">");
            sb.Append("<div style=\"max-width:620px;margin:24px auto;padding:28px;background:#fff;border:1px solid #e3e7eb;border-radius:12px\">");
            sb.Append("<h1 style=\"margin:0 0 12px;color:#153b68;font-size:24px\">Your gift voucher</h1>");
            sb.AppendFormat("<p>Hi {0},</p>", E(firstName));
            sb.Append("<p>Your voucher purchase was successful. Keep this email so you can redeem your voucher.</p>");
            sb.Append("<table role=\"presentation\" style=\"width:100%;border-collapse:collapse;margin:20px 0\">");
            Row(sb, "Operator", operatorName);
            Row(sb, "Voucher", productName);
            Row(sb, "Voucher value", "$" + faceValue);
            Row(sb, "Amount paid", "$" + amountPaid);
            Row(sb, "Purchased", purchaseDate);
            Row(sb, "Order reference", orderReference);
            Row(sb, "Valid until", validUntil);
            sb.Append("</table>");

            if (!string.IsNullOrWhiteSpace(voucherCode) && !string.Equals(voucherCode, "N/A", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append("<div style=\"margin:24px 0;padding:20px;text-align:center;background:#f6f9fc;border:1px dashed #9aafc4;border-radius:8px\">");
                sb.Append("<p style=\"margin:0 0 8px;color:#596779\">Voucher code / PIN</p>");
                sb.AppendFormat("<div style=\"font-size:22px;font-weight:bold;letter-spacing:1px;word-break:break-word\">{0}</div>", E(voucherCode));
                sb.AppendFormat("<p style=\"margin:16px 0 4px\"><img src=\"cid:{0}\" width=\"180\" height=\"180\" alt=\"QR code for your voucher\" style=\"display:block;margin:auto;width:180px;height:180px\"></p>", QrContentId);
                sb.Append("<p style=\"margin:0;color:#596779;font-size:13px\">Scan the QR code to read the voucher code.</p>");
                sb.Append("</div>");
            }

            if (!string.IsNullOrWhiteSpace(providerInstructions))
            {
                sb.Append("<h2 style=\"font-size:16px;color:#153b68\">Redemption instructions</h2><p style=\"white-space:pre-line\">");
                sb.Append(E(providerInstructions));
                sb.Append("</p>");
            }

            sb.Append("<p style=\"margin-top:24px;color:#596779;font-size:13px\">Treat this code like cash. Anyone who has it may be able to redeem the voucher.</p>");
            sb.Append("<p style=\"margin-bottom:0\">Thank you for using WleeSend.</p></div></body></html>");
            return sb.ToString();
        }

        private static void Row(StringBuilder sb, string label, string value)
        {
            sb.AppendFormat("<tr><td style=\"padding:8px 10px;border-bottom:1px solid #edf0f2;color:#596779\">{0}</td><td style=\"padding:8px 10px;border-bottom:1px solid #edf0f2;text-align:right;font-weight:bold\">{1}</td></tr>", E(label), E(value));
        }

        private static string E(string value) => WebUtility.HtmlEncode(value ?? string.Empty);
    }
}
