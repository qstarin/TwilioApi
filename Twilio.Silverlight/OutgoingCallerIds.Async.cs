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
		public void GetOutgoingCallerIdAsync(string outgoingCallerIdSid, Action<OutgoingCallerId> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";
			request.RootElement = "OutgoingCallerId";
			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);

			ExecuteAsync<OutgoingCallerId>(request, (response) => callback(response));
		}

		public void GetOutgoingCallerIdsAsync(Action<OutgoingCallerIdResult> callback)
		{
			GetOutgoingCallerIdsAsync(null, null, null, null, callback);
		}

		public void GetOutgoingCallerIdsAsync(string phoneNumber, string friendlyName, int? pageNumber, int? count, Action<OutgoingCallerIdResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/OutgoingCallerIds";
			//request.RootElement = "OutgoingCallerIds";

			if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<OutgoingCallerIdResult>(request, (response) => callback(response));
		}

		public void AddOutgoingCallerIdAsync(string phoneNumber, string friendlyName, int? callDelay, Action<ValidationRequest> callback)
		{
			Require.Argument("PhoneNumber", phoneNumber);
			if (callDelay.HasValue) Validate.IsBetween(callDelay.Value, 0, 60);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/OutgoingCallerIds";
			request.RootElement = "ValidationRequest";
			request.AddParameter("PhoneNumber", phoneNumber);

			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
			if (callDelay.HasValue) request.AddParameter("CallDelay", callDelay.Value);

			ExecuteAsync<ValidationRequest>(request, (response) => callback(response));
		}

		public void UpdateOutgoingCallerIdNameAsync(string outgoingCallerIdSid, string friendlyName, Action<OutgoingCallerId> callback)
		{
			Require.Argument("OutgoingCallerIdSid", outgoingCallerIdSid);
			Require.Argument("FriendlyName", friendlyName);
			Validate.IsValidLength(friendlyName, 64);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";
			request.RootElement = "OutgoingCallerId";

			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);
			request.AddParameter("FriendlyName", friendlyName);

			ExecuteAsync<OutgoingCallerId>(request, (response) => callback(response));
		}

		public void DeleteOutgoingCallerIdAsync(string outgoingCallerIdSid, Action<RestResponse> callback)
		{
			Require.Argument("OutgoingCallerIdSid", outgoingCallerIdSid);
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/OutgoingCallerIds/{OutgoingCallerIdSid}";

			request.AddParameter("OutgoingCallerIdSid", outgoingCallerIdSid, ParameterType.UrlSegment);

			ExecuteAsync(request, (response) => callback(response));
		}
	}
}