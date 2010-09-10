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

			AddNumberSearchParameters(options, request);

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

		private void AddNumberSearchParameters(AvailablePhoneNumberListRequest options, RestRequest request)
		{
			if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
			if (options.Contains.HasValue()) request.AddParameter("Contains", options.Contains);
			if (options.Distance.HasValue) request.AddParameter("Distance", options.Distance);
			if (options.InLata.HasValue()) request.AddParameter("InLata", options.InLata);
			if (options.InPostalCode.HasValue()) request.AddParameter("InPostalCode", options.InPostalCode);
			if (options.InRateCenter.HasValue()) request.AddParameter("InRateCenter", options.InRateCenter);
			if (options.InRegion.HasValue()) request.AddParameter("InRegion", options.InRegion);
			if (options.NearLatLong.HasValue()) request.AddParameter("NearLatLong", options.NearLatLong);
			if (options.NearNumber.HasValue()) request.AddParameter("NearNumber", options.NearNumber);
		}

	}
}
