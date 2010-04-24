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
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		private const string BaseUrl = "https://api.twilio.com/2008-08-01";

		private readonly string _accountSid;
		private readonly string _secretKey;
		public TwilioApi(string accountSid, string secretKey) {
			_accountSid = accountSid;
			_secretKey = secretKey;
		}

		public T Execute<T>(RestRequest request) where T : new() {
			var client = new RestClient();
			client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
			client.BaseUrl = BaseUrl;
			request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
			var response = client.Execute<T>(request);
			return response.Data;
		}

		public RestResponse Execute(RestRequest request) {
			var client = new RestClient();
			client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
			client.BaseUrl = BaseUrl;
			request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
			return client.Execute(request);
		}

		private string GetParameterNameWithEquality(ComparisonType? comparisonType, string parameterName) {
			if (comparisonType.HasValue) {
				switch (comparisonType) {
					case ComparisonType.GreaterThanOrEqualTo:
						parameterName += ">";
						break;
					case ComparisonType.LessThanOrEqualTo:
						parameterName += "<";
						break;
				}
			}
			return parameterName;
		}
	}
}