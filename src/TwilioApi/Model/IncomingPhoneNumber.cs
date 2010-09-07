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

namespace Twilio.Model
{
	public class IncomingPhoneNumber : TwilioBase
	{
		public string Sid { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public string FriendlyName { get; set; }
		public string AccountSid { get; set; }
		public string PhoneNumber { get; set; }
		public string ApiVersion { get; set; }
		public bool VoiceCallerIdLookup { get; set; }
		public string VoiceUrl { get; set; }
		public string VoiceMethod { get; set; }
		public string VoiceFallbackUrl { get; set; }
		public string VoiceFallbackMethod { get; set; }
		public string StatusCallback { get; set; }
		public string StatusCallbackMethod { get; set; }
		public string SmsUrl { get; set; }
		public string SmsMethod { get; set; }
		public string SmsFallbackUrl { get; set; }
		public string SmsFallbackMethod { get; set; }
	}
}