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
	public class Notification : TwilioBase
	{
		public string Sid { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public string AccountSid { get; set; }
		public string CallSid { get; set; }
		public string ApiVersion { get; set; }
		public int Log { get; set; }
		public string ErrorCode { get; set; }
		public string MoreInfo { get; set; }
		public string MessageText { get; set; }
		public DateTime MessageDate { get; set; }
		public string RequestUrl { get; set; }
		public string RequestMethod { get; set; }
		public string RequestVariables { get; set; }
		public string ResponseHeaders { get; set; }
		public string ResponseBody { get; set; }
	}
}