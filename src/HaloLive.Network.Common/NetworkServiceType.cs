using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HaloLive.Network.Common
{
	/// <summary>
	/// Enumeration of network services in the backend.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NetworkServiceType
	{
		/// <summary>
		/// The authentication service.
		/// </summary>
		AuthenticationService = 1,

		/// <summary>
		/// Service that provides a list of game servers.
		/// </summary>
		GameServerListService = 2
	}
}
