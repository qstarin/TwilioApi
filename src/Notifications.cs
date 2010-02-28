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
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public Notification GetNotification(string notificationSid) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/Notifications/{NotificationSid}";
			request.RootElement = "Notification";

			request.AddParameter("NotificationSid", notificationSid, ParameterType.UrlSegment);

			return Execute<Notification>(request);
		}

		public NotificationList GetNotifications() {
			return GetNotifications(null, null, null, null);
		}

		public NotificationList GetNotifications(int? log, DateTime? messageDate, int? pageNumber, int? count) {
			var request = new RestRequest();
			request.ActionFormat = "Accounts/{AccountSid}/Notifications";
			request.RootElement = "Notifications";

			if (log.HasValue) request.AddParameter("Log", log);
			if (messageDate.HasValue) request.AddParameter("MessageDate", messageDate.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<NotificationList>(request);
		}

		public RestResponse DeleteNotification(string notificationSid) {
			Require.Argument("NotificationSid", notificationSid);
			var request = new RestRequest(Method.DELETE);
			request.ActionFormat = "Accounts/{AccountSid}/Notifications/{NotificationSid}";

			request.AddParameter("NotificationSid", notificationSid, ParameterType.UrlSegment);

			return Execute(request);
		}
	}
}