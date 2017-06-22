using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Network.Common;
using Newtonsoft.Json;

namespace HaloLive.Models.NameResolution
{
	/// <summary>
	/// Response to <see cref="ResolveServiceEndpointRequestModel"/> containing the endpoint of the requested service.
	/// </summary>
	[JsonObject]
	public sealed class ResolveServiceEndpointResponseModel : IResponseModel<ResolveServiceEndpointResponseCode>, ISucceedable
	{
		/// <summary>
		/// The resolved endpoint.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)] //can be null if no endpoint is available
		public ResolvedEndpoint Endpoint { get; private set; } //JSON requires it to have a setter for some reason

		/// <summary>
		/// The type of the endpoint. This is sent back and can be used to verify the correct data was sent.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public NetworkServiceType EndpointType { get; private set; } //JSON requires it to have a setter for some reason

		/// <inheritdoc />
		[JsonProperty(Required = Required.Default)]
		public ResolveServiceEndpointResponseCode ResultCode { get; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ResolveServiceEndpointResponseCode.Success;

		public ResolveServiceEndpointResponseModel(ResolvedEndpoint endpoint, NetworkServiceType endpointType)
		{
			if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
			if (!Enum.IsDefined(typeof(NetworkServiceType), endpointType)) throw new ArgumentOutOfRangeException(nameof(endpointType), "Value should be defined in the NetworkServiceType enum.");

			//We allow null on the endpoint but not if they pass this DTO one
			Endpoint = endpoint;
			EndpointType = endpointType;

			//We can assume success since they provided an endpoint
			ResultCode = ResolveServiceEndpointResponseCode.Success;
		}

		public ResolveServiceEndpointResponseModel(NetworkServiceType endpointType, ResolveServiceEndpointResponseCode resultCode)
		{
			if (!Enum.IsDefined(typeof(NetworkServiceType), endpointType)) throw new ArgumentOutOfRangeException(nameof(endpointType), "Value should be defined in the NetworkServiceType enum.");
			if (!Enum.IsDefined(typeof(ResolveServiceEndpointResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the ResolveServiceEndpointResponseCode enum.");

			EndpointType = endpointType;
			ResultCode = resultCode;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected ResolveServiceEndpointResponseModel()
		{
			
		}
	}
}
