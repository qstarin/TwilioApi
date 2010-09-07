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
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public void GetRecordingsAsync(Action<RecordingResult> callback)
		{
			GetRecordingsAsync(null, null, null, null, callback);
		}

		public void GetRecordingsAsync(string callSid, DateTime? dateCreated, int? pageNumber, int? count, Action<RecordingResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings";
			//request.RootElement = "Recordings";

			if (callSid.HasValue()) request.AddParameter("CallSid", callSid);
			if (dateCreated.HasValue) request.AddParameter("DateCreated", dateCreated.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<RecordingResult>(request, (response) => callback(response));
		}

		public void GetRecordingAsync(string recordingSid, Action<Recording> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}.xml";
			request.RootElement = "Recording";

			request.AddParameter("RecordingSid", recordingSid, ParameterType.UrlSegment);

			ExecuteAsync<Recording>(request, (response) => callback(response));
		}

		public void DeleteRecordingAsync(string recordingSid, Action<RestResponse> callback)
		{
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}.xml";
			request.RootElement = "Recording";

			request.AddParameter("RecordingSid", recordingSid, ParameterType.UrlSegment);

			ExecuteAsync(request, (response) => callback(response));
		}
	}
}