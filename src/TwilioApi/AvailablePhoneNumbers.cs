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
		public AvailablePhoneNumberResult ListAvailableLocalPhoneNumbers(string isoCountryCode, AvailablePhoneNumberListRequest options)
		{
			Require.Argument("isoCountryCode", isoCountryCode);

			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/Local";
			request.AddUrlSegment("IsoCountryCode", isoCountryCode);

			if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
			if (options.Contains.HasValue()) request.AddParameter("Contains", options.AreaCode);
			if (options.Distance.HasValue) request.AddParameter("Distance", options.AreaCode);
			if (options.InLata.HasValue()) request.AddParameter("InLata", options.AreaCode);
			if (options.InPostalCode.HasValue()) request.AddParameter("InPostalCode", options.AreaCode);
			if (options.InRateCenter.HasValue()) request.AddParameter("InRateCenter", options.AreaCode);
			if (options.InRegion.HasValue()) request.AddParameter("InRegion", options.AreaCode);
			if (options.NearLatLong.HasValue()) request.AddParameter("NearLatLong", options.AreaCode);
			if (options.NearNumber.HasValue()) request.AddParameter("NearNumber", options.AreaCode);

			return Execute<AvailablePhoneNumberResult>(request);
		}

		public AvailablePhoneNumberResult ListAvailableTollFreePhoneNumbers(string isoCountryCode)
		{
			Require.Argument("isoCountryCode", isoCountryCode);

			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/TollFree";
			request.AddUrlSegment("IsoCountryCode", isoCountryCode);

			return Execute<AvailablePhoneNumberResult>(request);
		}

		public AvailablePhoneNumberResult ListAvailableTollFreePhoneNumbers(string isoCountryCode, string contains)
		{
			Require.Argument("isoCountryCode", isoCountryCode);
			Require.Argument("contains", contains);

			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/TollFree";
			request.AddUrlSegment("IsoCountryCode", isoCountryCode);

			request.AddParameter("Contains", contains);

			return Execute<AvailablePhoneNumberResult>(request);
		}
	}
}