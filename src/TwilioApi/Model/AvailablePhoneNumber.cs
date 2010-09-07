using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twilio.Model
{
	public class AvailablePhoneNumber : TwilioBase
	{
		public string FriendlyName { get; set; }
		public string PhoneNumber { get; set; }
		public string Lata { get; set; }
		public string RateCenter { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string IsoCountry { get; set; }
	}

	public class AvailablePhoneNumberResult
	{
		public AvailablePhoneNumberList AvailablePhoneNumbers { get; set; }
	}

	public class AvailablePhoneNumberList : TwilioListBase<AvailablePhoneNumber>
	{
	}

	public class AvailablePhoneNumberListRequest
	{
		public string AreaCode { get; set; }
		public string Contains { get; set; }
		public string InRegion { get; set; }
		public string InPostalCode { get; set; }
		public string NearLatLong { get; set; }
		public int? Distance { get; set; }
		public string NearNumber { get; set; }
		public string InLata { get; set; }
		public string InRateCenter { get; set; }
	}
}
