using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Network.Common;
using Newtonsoft.Json;

namespace HaloLive.Models.NameResolution
{
	/// <summary>
	/// Request that resolves a specific service's endpoint.
	/// Many services do not and should not have static endpoints. This also allows for the ability to
	/// naively loadbalance, though it's unlikely that it will be done this way, or direct users by region.
	/// </summary>
	[JsonObject]
	public sealed class ResolveServiceEndpointRequestModel
	{
		/// <summary>
		/// Indicates the region of the client requesting.
		/// This parameter can be used to connect clients to closer services. For example
		/// there may be a voice chat service and connecting them to a closer endpoint in their country
		/// may serve better quality.
		/// </summary>
		public ClientRegionLocale Region { get; }

		/// <summary>
		/// Indicates the service requested for resolution.
		/// </summary>
		public NetworkServiceType ServiceType { get; }

		public ResolveServiceEndpointRequestModel(ClientRegionLocale region, NetworkServiceType serviceType)
		{
			if (!Enum.IsDefined(typeof(ClientRegionLocale), region)) throw new ArgumentOutOfRangeException(nameof(region), "Value should be defined in the ClientRegionLocale enum.");
			if (!Enum.IsDefined(typeof(NetworkServiceType), serviceType)) throw new ArgumentOutOfRangeException(nameof(serviceType), "Value should be defined in the NetworkServiceType enum.");

			Region = region;
			ServiceType = serviceType;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected ResolveServiceEndpointRequestModel()
		{
			
		}
	}
}
