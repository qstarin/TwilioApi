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
		public OutgoingCallerId GetOutgoingCallerId(string outgoingCallerIdSid) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";
			request.RootElement = "OutgoingCallerId";
			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);

			return Execute<OutgoingCallerId>(request);
		}

		public OutgoingCallerIdList GetOutgoingCallerIds() {
			return GetOutgoingCallerIds(null, null, null, null);
		}

		public OutgoingCallerIdList GetOutgoingCallerIds(string phoneNumber, string friendlyName, int? pageNumber, int? count) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/OutgoingCallerIds";
			request.RootElement = "OutgoingCallerIds";

			if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<OutgoingCallerIdList>(request);
		}

		public ValidationRequest AddOutgoingCallerId(string phoneNumber, string friendlyName, int? callDelay) {
			Require.Argument("PhoneNumber", phoneNumber);
			if (callDelay.HasValue) Validate.IsBetween(callDelay.Value, 0, 60);

			var request = new RestRequest(Method.POST);
			request.ActionFormat = "Accounts/{AccountSid}/OutgoingCallerIds";
			request.RootElement = "ValidationRequest";
			request.AddParameter("PhoneNumber", phoneNumber);

			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
			if (callDelay.HasValue) request.AddParameter("CallDelay", callDelay.Value);

			return Execute<ValidationRequest>(request);
		}

		public OutgoingCallerId UpdateOutgoingCallerIdName(string outgoingCallerIdSid, string friendlyName) {
			Require.Argument("OutgoingCallerIdSid", outgoingCallerIdSid);
			Require.Argument("FriendlyName", friendlyName);
			Validate.IsValidLength(friendlyName, 64);

			var request = new RestRequest(Method.POST);
			request.ActionFormat = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";
			request.RootElement = "OutgoingCallerId";

			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);
			request.AddParameter("FriendlyName", friendlyName);

			return Execute<OutgoingCallerId>(request);
		}

		public RestResponse DeleteOutgoingCallerId(string outgoingCallerIdSid) {
			Require.Argument("OutgoingCallerIdSid", outgoingCallerIdSid);
			var request = new RestRequest(Method.DELETE);
			request.ActionFormat = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";

			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);

			return Execute(request);
		}
	}
}