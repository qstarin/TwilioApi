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
	public class Call : TwilioBase
	{
		public string Sid { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public string AccountSid { get; set; }
		public string To { get; set; }
		public string From { get; set; }
		public string PhoneNumberSid { get; set; }
		public int Status { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int? Duration { get; set; }
		public decimal? Price { get; set; }
		public string Direction { get; set; }
		public string AnsweredBy { get; set; }
		public string ForwardedFrom { get; set; }
		public string CallerName { get; set; }
		public string GroupSid { get; set; }
	}
}