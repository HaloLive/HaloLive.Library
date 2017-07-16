using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.NameResolution;
using TypeSafe.Http.Net;

namespace HaloLive.Network
{
	public interface IServiceDiscoveryService
	{
		/// <summary>
		/// Attempts to discover the service specified in the <see cref="ResolveServiceEndpointRequestModel"/>.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>The result of the resolution request.</returns>
		[Post("/api/ServiceDiscovery/Discover")]
		Task<ResolveServiceEndpointResponseModel> Discover([JsonBody] ResolveServiceEndpointRequestModel request);
	}
}
