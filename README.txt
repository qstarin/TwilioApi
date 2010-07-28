.NET wrapper for Twilio REST API

Uses RestSharp: http://restsharp.org

Twilio Documentation: http://www.twilio.com/docs/index

Sample Usage:

using Twilio;
var twilio = new TwilioApi("accountSid", "secretKey");
var call = twilio.InitiateOutboundCall("123456790", "5555551212", "http://example.com/handleCall");
var msg = twilio.SendSmsMessage("5555551212", "1234567890", "Can you believe it's this easy to send an SMS?!");

Silverlight/Windows Phone 7/Asynchronous Requests Sample Usage
- You can declare async callbacks inline or in a separate method

using Twilio;
var twilio = new TwilioApi("accountSid", "secretKey");
twilio.InitiateOutboundCallAsync("123456790", "5555551212", "http://example.com/handleCall", callSent);
twilio.SendSmsMessage("5555551212", "1234567890", "Hello!", (msg) => {
    // Console.WriteLine(msg.Sid);
});

public void callSent(Call call) {
    // Console.WriteLog(call.Sid);
}