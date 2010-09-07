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
using Twilio.Model;

#if WINDOWS_PHONE
using System.Windows;
#endif

namespace Twilio
{
	public partial class TwilioApi
	{
		public void ExecuteAsync<T>(RestRequest request, Action<T> callback) where T : new()
		{
			request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
			_client.ExecuteAsync<T>(request, (response) =>
			{
#if WINDOWS_PHONE
				//var dispatcher = Deployment.Current.Dispatcher;
				//dispatcher.BeginInvoke(() => {
#endif
					callback(response.Data);
#if WINDOWS_PHONE
				//});
#endif
			});
		}

		public void ExecuteAsync(RestRequest request, Action<RestResponse> callback)
		{
			request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
			_client.ExecuteAsync(request, (response) =>
			{
#if WINDOWS_PHONE
				//var dispatcher = Deployment.Current.Dispatcher;
				//dispatcher.BeginInvoke(() =>
				//{
#endif
					callback(response);
#if WINDOWS_PHONE
				//});
#endif
			});
		}
	}
}