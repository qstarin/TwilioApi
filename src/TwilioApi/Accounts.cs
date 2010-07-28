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
		public Account GetAccount()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}";
			request.RootElement = "Account";

			return Execute<Account>(request);
		}

		public Account UpdateAccountName(string friendlyName)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}";
			request.RootElement = "Account";
			request.AddParameter("FriendlyName", friendlyName);

			return Execute<Account>(request);
		}
	}
}