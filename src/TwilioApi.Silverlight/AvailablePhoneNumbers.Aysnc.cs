using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		public void ListAvailableLocalPhoneNumbers(string isoCountryCode, AvailablePhoneNumberListRequest options, Action<AvailablePhoneNumberResult> callback)
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

			ExecuteAsync<AvailablePhoneNumberResult>(request, (response) => callback(response));
		}

		public void ListAvailableTollFreePhoneNumbers(string isoCountryCode, Action<AvailablePhoneNumberResult> callback)
		{
			Require.Argument("isoCountryCode", isoCountryCode);

			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/TollFree";
			request.AddUrlSegment("IsoCountryCode", isoCountryCode);

			ExecuteAsync<AvailablePhoneNumberResult>(request, (response) => callback(response));
		}

		public void ListAvailableTollFreePhoneNumbers(string isoCountryCode, string contains, Action<AvailablePhoneNumberResult> callback)
		{
			Require.Argument("isoCountryCode", isoCountryCode);
			Require.Argument("contains", contains);

			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/TollFree";
			request.AddUrlSegment("IsoCountryCode", isoCountryCode);

			request.AddParameter("Contains", contains);

			ExecuteAsync<AvailablePhoneNumberResult>(request, (response) => callback(response));
		}
	}
}
