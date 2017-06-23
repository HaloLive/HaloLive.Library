using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Network.Common;
using Newtonsoft.Json;

namespace HaloLive.Models.NameResolution
{
	/// <summary>
	/// Format for the endpoint map storage.
	/// </summary>
	[JsonObject]
	public sealed class NameEndpointResolutionStorageModel
	{
		/// <summary>
		/// The region these endpoints are for.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public ClientRegionLocale Region { get; }

		/// <summary>
		/// The Dictionary of services and endpoints.
		/// </summary>
		[JsonProperty(nameof(ServiceEndpoints), Required = Required.Default)]
		private Dictionary<NetworkServiceType, ResolvedEndpoint> _ServiceEndpoints { get; }

		/// <summary>
		/// The Dictionary of services and endpoints
		/// </summary>
		[JsonIgnore]
		public IReadOnlyDictionary<NetworkServiceType, ResolvedEndpoint> ServiceEndpoints => _ServiceEndpoints;

		//We don't need a real ctor because we load a file
		public NameEndpointResolutionStorageModel(ClientRegionLocale region, Dictionary<NetworkServiceType, ResolvedEndpoint> serviceEndpoints)
		{
			if (serviceEndpoints == null) throw new ArgumentNullException(nameof(serviceEndpoints));
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");

			Region = region;
			_ServiceEndpoints = serviceEndpoints;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected NameEndpointResolutionStorageModel()
		{
			
		}
	}
}
