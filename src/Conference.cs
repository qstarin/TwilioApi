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
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public ConferenceList GetConferences() {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";
			request.RootElement = "Conferences";

			return Execute<ConferenceList>(request);
		}

		public ConferenceList GetConferences(ConferenceListRequest options) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";
			request.RootElement = "Conferences";

			if (options.Status.HasValue) request.AddParameter("Status", options.Status);
			if (options.FriendlyName.HasValue()) request.AddParameter("FriendlyName", options.FriendlyName);

			var dateCreatedParameterName = GetParameterNameWithEquality(options.DateCreatedComparison, "DateCreated");
			var dateUpdatedParameterName = GetParameterNameWithEquality(options.DateUpdatedComparison, "DateUpdated");

			if (options.DateCreated.HasValue) request.AddParameter(dateCreatedParameterName, options.DateCreated.Value.ToString("yyyy-MM-dd"));
			if (options.DateUpdated.HasValue) request.AddParameter(dateUpdatedParameterName, options.DateUpdated.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("num", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("page", options.PageNumber.Value);

			return Execute<ConferenceList>(request);
		}

		public Conference GetConference(string conferenceSid) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}";
			request.RootElement = "Conference";

			request.AddParameter("ConferenceSid", conferenceSid);

			return Execute<Conference>(request);
		}

		public ParticipantList GetConferenceParticipants(string conferenceSid, bool? muted) {
			return GetConferenceParticipants(conferenceSid, muted, null, null);
		}

		public ParticipantList GetConferenceParticipants(string conferenceSid, bool? muted, int? pageNumber, int? count) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants";
			request.RootElement = "Participants";

			request.AddParameter("ConferenceSid", conferenceSid);
			if (muted.HasValue) request.AddParameter("Muted", muted.Value);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<ParticipantList>(request);
		}

		public Participant GetConferenceParticipant(string conferenceSid, string callSid) {
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			return Execute<Participant>(request);
		}

		public Participant MuteConferenceParticipant(string conferenceSid, string callSid) {
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", true);

			return Execute<Participant>(request);
		}

		public Participant UnmuteConferenceParticipant(string conferenceSid, string callSid) {
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", false);

			return Execute<Participant>(request);
		}

		public bool KickParticipantFromConference(string conferenceSid, string callSid) {
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			var response = Execute(request);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}
	}
}