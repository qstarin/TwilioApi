﻿#region License
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
		public void GetIncomingPhoneNumberAsync(string incomingPhoneNumberSid, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		public void GetIncomingPhoneNumbersAsync(Action<IncomingPhoneNumberResult> callback)
		{
			GetIncomingPhoneNumbersAsync(null, null, null, null, callback);
		}

		public void GetIncomingPhoneNumbersAsync(string phoneNumber, string friendlyName, int? pageNumber, int? count, Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";
			//request.RootElement = "IncomingPhoneNumbers";

			if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		public void GetLocalIncomingPhoneNumbersAsync(Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/Local";
			request.RootElement = "IncomingPhoneNumbers";

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		public void AddLocalPhoneNumberAsync(PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/Local";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);
			AddSmsOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		public void GetTollFreeIncomingPhoneNumbersAsync(Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/TollFree";
			request.RootElement = "IncomingPhoneNumbers";

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		public void AddTollFreePhoneNumberAsync(PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/TollFree";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		public void UpdateIncomingPhoneNumberAsync(string incomingPhoneNumberSid, PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);
			AddPhoneNumberOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		public void DeleteIncomingPhoneNumberAsync(string incomingPhoneNumberSid, Action<RestResponse> callback)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			ExecuteAsync(request, (response) => callback(response));
		}

#if !FRAMEWORK
		private void AddPhoneNumberOptionsToRequest(RestRequest request, PhoneNumberOptions options)
		{
			if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
			if (options.FriendlyName.HasValue())
			{
				Validate.IsValidLength(options.FriendlyName, 64);
				request.AddParameter("FriendlyName", options.FriendlyName);
			}
			if (options.Url.HasValue()) request.AddParameter("Url", options.Url);
			if (options.Method.HasValue) request.AddParameter("Method", options.Method.ToString());
			if (options.VoiceFallbackUrl.HasValue()) request.AddParameter("VoiceFallbackUrl", options.VoiceFallbackUrl);
			if (options.VoiceFallbackMethod.HasValue) request.AddParameter("VoiceFallbackMethod", options.VoiceFallbackMethod.ToString());
			if (options.VoiceCallerIdLookup.HasValue) request.AddParameter("VoiceCallerIdLookup", options.VoiceCallerIdLookup.Value);
		}

		private void AddSmsOptionsToRequest(RestRequest request, PhoneNumberOptions options)
		{
			if (options.SmsUrl.HasValue()) request.AddParameter("SmsUrl", options.SmsUrl);
			if (options.SmsMethod.HasValue) request.AddParameter("SmsMethod", options.SmsMethod.ToString());
			if (options.SmsFallbackUrl.HasValue()) request.AddParameter("SmsFallbackUrl", options.SmsFallbackUrl);
			if (options.SmsFallbackMethod.HasValue) request.AddParameter("SmsFallbackMethod", options.SmsFallbackMethod.ToString());
		}
#endif
	}
}