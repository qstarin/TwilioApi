.NET wrapper for Twilio REST API

Uses RestSharp: http://restsharp.org

Twilio Documentation: http://www.twilio.com/docs/index

Sample Usage:

using Twilio;
var twilio = new TwilioApi("accountSid", "secretKey");
twilio.InitiateOutboundCall("123456790", "5555551212", "http://example.com/handleCall");
twilio.SendSmsMessage("5555551212", "1234567890", "Can you believe it's this easy to send an SMS?!");