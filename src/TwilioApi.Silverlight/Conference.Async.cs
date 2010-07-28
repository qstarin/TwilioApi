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
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public void GetConferencesAsync(Action<ConferenceResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";
			//request.RootElement = "Conferences";

			ExecuteAsync<ConferenceResult>(request, (response) => callback(response));
		}

		public void GetConferencesAsync(ConferenceListRequest options, Action<ConferenceResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";
			//request.RootElement = "Conferences";

			if (options.Status.HasValue) request.AddParameter("Status", options.Status);
			if (options.FriendlyName.HasValue()) request.AddParameter("FriendlyName", options.FriendlyName);

			var dateCreatedParameterName = GetParameterNameWithEquality(options.DateCreatedComparison, "DateCreated");
			var dateUpdatedParameterName = GetParameterNameWithEquality(options.DateUpdatedComparison, "DateUpdated");

			if (options.DateCreated.HasValue) request.AddParameter(dateCreatedParameterName, options.DateCreated.Value.ToString("yyyy-MM-dd"));
			if (options.DateUpdated.HasValue) request.AddParameter(dateUpdatedParameterName, options.DateUpdated.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("num", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("page", options.PageNumber.Value);

			ExecuteAsync<ConferenceResult>(request, (response) => callback(response));
		}

		public void GetConferenceAsync(string conferenceSid, Action<Conference> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}";
			request.RootElement = "Conference";

			request.AddParameter("ConferenceSid", conferenceSid);

			ExecuteAsync<Conference>(request, (response) => callback(response));
		}

		public void GetConferenceParticipantsAsync(string conferenceSid, bool? muted, Action<ParticipantResult> callback)
		{
			GetConferenceParticipantsAsync(conferenceSid, muted, null, null, callback);
		}

		public void GetConferenceParticipantsAsync(string conferenceSid, bool? muted, int? pageNumber, int? count, Action<ParticipantResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants";
			//request.RootElement = "Participants";

			request.AddParameter("ConferenceSid", conferenceSid);
			if (muted.HasValue) request.AddParameter("Muted", muted.Value);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<ParticipantResult>(request, (response) => callback(response));
		}

		public void GetConferenceParticipantAsync(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		public void MuteConferenceParticipantAsync(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", true);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		public void UnmuteConferenceParticipantAsync(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", false);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		public void KickParticipantFromConferenceAsync(string conferenceSid, string callSid, Action<bool> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			//var response = Execute(request);
			ExecuteAsync(request, (response) => callback(response.StatusCode == System.Net.HttpStatusCode.NoContent));
		}
	}
}