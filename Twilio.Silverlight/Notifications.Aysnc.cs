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
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public void GetNotificationAsync(string notificationSid, Action<Notification> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Notifications/{NotificationSid}";
			request.RootElement = "Notification";

			request.AddParameter("NotificationSid", notificationSid, ParameterType.UrlSegment);

			ExecuteAsync<Notification>(request, (response) => callback(response));
		}

		public void GetNotificationsAsync(Action<NotificationResult> callback)
		{
			GetNotificationsAsync(null, null, null, null, callback);
		}

		public void GetNotificationsAsync(int? log, DateTime? messageDate, int? pageNumber, int? count, Action<NotificationResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Notifications";
			//request.RootElement = "Notifications";

			if (log.HasValue) request.AddParameter("Log", log);
			if (messageDate.HasValue) request.AddParameter("MessageDate", messageDate.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<NotificationResult>(request, (response) => callback(response));
		}

		public void DeleteNotificationAsync(string notificationSid, Action<RestResponse> callback)
		{
			Require.Argument("NotificationSid", notificationSid);
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/Notifications/{NotificationSid}";

			request.AddParameter("NotificationSid", notificationSid, ParameterType.UrlSegment);

			ExecuteAsync(request, (response) => callback(response));
		}
	}
}