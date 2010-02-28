#region License
//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public SmsMessage GetSmsMessage(string smsMessageSid) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/SMS/Messages/{SMSMessageSid}";
			request.RootElement = "SMSMessage";
			request.AddParameter("SMSMessageSid", smsMessageSid);

			return Execute<SmsMessage>(request);
		}

		public SmsMessageList GetSmsMessages() {
			return GetSmsMessages(null, null, null, null, null);
		}

		public SmsMessageList GetSmsMessages(string to, string from, DateTime? dateSent, int? pageNumber, int? count) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/SMS/Messages";
			request.RootElement = "SMSMessages";

			if (to.HasValue()) request.AddParameter("To", to);
			if (from.HasValue()) request.AddParameter("From", from);
			if (dateSent.HasValue) request.AddParameter("DateSent", dateSent.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<SmsMessageList>(request);
		}

		public SmsMessage SendSmsMessage(string from, string to, string body, string statusCallback) {
			Validate.IsValidLength(body, 160);
			Require.Argument("from", from);
			Require.Argument("to", to);
			Require.Argument("body", body);

			var request = new RestRequest(Method.POST);
			request.ActionFormat = "Accounts/{AccountSid}/SMS/Messages";
			request.AddParameter("From", from);
			request.AddParameter("To", to);
			request.AddParameter("Body", body);
			if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);

			return Execute<SmsMessage>(request);
		}
	}
}