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

using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public CallResult GetCalls() {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";
			//request.RootElement = "Calls";

			return Execute<CallResult>(request);
		}

		public CallResult GetCalls(CallListRequest options) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";
			//request.RootElement = "Calls";

			if (options.Called.HasValue()) request.AddParameter("Called", options.Called);
			if (options.Caller.HasValue()) request.AddParameter("Caller", options.Caller);
			if (options.Status.HasValue) request.AddParameter("Status", options.Status);
			if (options.StartTime.HasValue) request.AddParameter("StartTime", options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter("EndTime", options.EndTime.Value.ToString("yyyy-MM-dd"));

			var startTimeParameterName = GetParameterNameWithEquality(options.StartTimeComparison, "StartTime");
			var endTimeParameterName = GetParameterNameWithEquality(options.EndTimeComparison, "EndTime");

			if (options.StartTime.HasValue) request.AddParameter(startTimeParameterName, options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter(endTimeParameterName, options.EndTime.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("num", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("page", options.PageNumber.Value);

			return Execute<CallResult>(request);
		}

		public Call GetCall(string callSid) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			return Execute<Call>(request);
		}

		public CallResult GetCallSegments(string callSid) {
			return GetCallSegments(callSid, null, null);
		}

		public CallResult GetCallSegments(string callSid, int? pageNumber, int? count) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}/Segments";
			//request.RootElement = "Calls";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<CallResult>(request);
		}

		public Call GetCallSegment(string callSid, string callSegmentSid) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}/Segments/{CallSegmentSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("CallSegmentSid", callSegmentSid, ParameterType.UrlSegment);

			return Execute<Call>(request);
		}

		public Call InitiateOutboundCall(string caller, string called, string url) {
			return InitiateOutboundCall(new CallOptions {
												Caller = caller,
												Called = called,
												Url = url
			                                });
		}

		public Call InitiateOutboundCall(CallOptions options) {
			Require.Argument("Caller", options.Caller);
			Require.Argument("Called", options.Called);
			Require.Argument("Url", options.Url);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls";
			request.RootElement = "Calls";

			request.AddParameter("Caller", options.Caller);
			request.AddParameter("Called", options.Called);
			request.AddParameter("Url", options.Url);

			if (options.Method.HasValue) request.AddParameter("Method", options.Method);
			if (options.SendDigits.HasValue()) request.AddParameter("SendDigits", options.SendDigits);
			if (options.IfMachine.HasValue) request.AddParameter("IfMachine", options.IfMachine.Value);
			if (options.Timeout.HasValue) request.AddParameter("Timeout", options.Timeout.Value);

			return Execute<Call>(request);
		}

		public Call RedirectCall(string callSid, string currentUrl, Method? currentMethod) {
			Require.Argument("CallSid", callSid);
			Require.Argument("CurrentUrl", currentUrl);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("CurrentUrl", currentUrl);
			if (currentMethod.HasValue) request.AddParameter("CurrentMethod", currentMethod.Value);

			return Execute<Call>(request);
		}
	}
}