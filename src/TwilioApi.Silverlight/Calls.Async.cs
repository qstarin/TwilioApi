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
		public void GetCallsAsync(Action<CallResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";

			ExecuteAsync<CallResult>(request, (response) => callback(response));
		}

		public void GetCallsAsync(CallListRequest options, Action<CallResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";

			if (options.From.HasValue()) request.AddParameter("From", options.From);
			if (options.To.HasValue()) request.AddParameter("To", options.To);
			if (options.Status.HasValue) request.AddParameter("Status", options.Status);
			if (options.StartTime.HasValue) request.AddParameter("StartTime", options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter("EndTime", options.EndTime.Value.ToString("yyyy-MM-dd"));

			var startTimeParameterName = GetParameterNameWithEquality(options.StartTimeComparison, "StartTime");
			var endTimeParameterName = GetParameterNameWithEquality(options.EndTimeComparison, "EndTime");

			if (options.StartTime.HasValue) request.AddParameter(startTimeParameterName, options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter(endTimeParameterName, options.EndTime.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("num", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("page", options.PageNumber.Value);

			ExecuteAsync<CallResult>(request, (response) => callback(response));
		}

		public void GetCallAsync(string callSid, Action<Call> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			ExecuteAsync<Call>(request, (response) => callback(response));
		}

		public void GetCallSegmentsAsync(string callSid, Action<CallResult> callback)
		{
			GetCallSegmentsAsync(callSid, null, null, callback);
		}

		public void GetCallSegmentsAsync(string callSid, int? pageNumber, int? count, Action<CallResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}/Segments";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<CallResult>(request, (response) => callback(response));
		}

		public void GetCallSegmentAsync(string callSid, string callSegmentSid, Action<Call> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}/Segments/{CallSegmentSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("CallSegmentSid", callSegmentSid, ParameterType.UrlSegment);

			ExecuteAsync<Call>(request, (response) => callback(response));
		}

		public void InitiateOutboundCallAsync(string from, string to, string url, Action<Call> callback)
		{
			InitiateOutboundCallAsync(from, to, url, null, callback);
		}

		public void InitiateOutboundCallAsync(string from, string to, string url, string statusCallback, Action<Call> callback)
		{
			InitiateOutboundCallAsync(new CallOptions
			{
				From = from,
				To = to,
				Url = url,
				StatusCallback = statusCallback
			},
			callback);
		}

		public void InitiateOutboundCallAsync(CallOptions options, Action<Call> callback)
		{
			Require.Argument("From", options.From);
			Require.Argument("To", options.To);
			Require.Argument("Url", options.Url);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls";
			request.RootElement = "Calls";

			request.AddParameter("From", options.From);
			request.AddParameter("To", options.To);
			request.AddParameter("Url", options.Url);

			if (options.StatusCallback.HasValue()) request.AddParameter("StatusCallback", options.StatusCallback);
			if (options.StatusCallbackMethod.HasValue) request.AddParameter("StatusCallbackMethod", options.StatusCallbackMethod);
			if (options.FallbackUrl.HasValue()) request.AddParameter("FallbackUrl", options.FallbackUrl);
			if (options.FallbackMethod.HasValue) request.AddParameter("FallbackMethod", options.FallbackMethod);
			if (options.Method.HasValue) request.AddParameter("Method", options.Method);
			if (options.SendDigits.HasValue()) request.AddParameter("SendDigits", options.SendDigits);
			if (options.IfMachine.HasValue) request.AddParameter("IfMachine", options.IfMachine.Value);
			if (options.Timeout.HasValue) request.AddParameter("Timeout", options.Timeout.Value);
			ExecuteAsync<Call>(request, (response) => callback(response));
		}

		public void HangupCallAsync(string callSid, HangupStyle style, Action<Call> callback)
		{
			Require.Argument("CallSid", callSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddUrlSegment("CallSid", callSid);
			request.AddParameter("Status", style.ToString().ToLower());

			ExecuteAsync<Call>(request, (response) => callback(response));
		}

		public void RedirectCallAsync(string callSid, string currentUrl, Method? currentMethod, Action<Call> callback)
		{
			Require.Argument("CallSid", callSid);
			Require.Argument("CurrentUrl", currentUrl);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("CurrentUrl", currentUrl);
			if (currentMethod.HasValue) request.AddParameter("CurrentMethod", currentMethod.Value);

			ExecuteAsync<Call>(request, (response) => callback(response));
		}
	}
}