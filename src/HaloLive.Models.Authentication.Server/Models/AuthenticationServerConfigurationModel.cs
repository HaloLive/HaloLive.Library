using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HaloLive.Models.Authentication
{
	//TODO: Test, lazy today want to move quickly
	/// <summary>
	/// The configuration options for the authentication server.
	/// </summary>
	[JsonObject]
	public sealed class AuthenticationServerConfigurationModel
	{
		/// <summary>
		/// Should be set to the endpoint/action specified in the HaloLive documentation https://github.com/HaloLive/Documentation
		/// which is /api/auth but it can be configured, however it shouldn't.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public string AuthenticationControllerEndpoint { get; }

		/// <summary>
		/// Should be the path to the X509Certificate2's path that will be used in the signing/issuing of
		/// JWT from the authentication server.
		/// </summary>
		[JsonProperty(Required = Required.Default)]
		public string JwtSigningX509Certificate2Path { get; }

		/// <summary>
		/// Creates a new configuration for the authentication server.
		/// </summary>
		/// <param name="endpoint">The endpoint/controller/action used for authentication.</param>
		/// <param name="certPath">The path to the JWT signing certificate.</param>
		public AuthenticationServerConfigurationModel(string endpoint, string certPath)
		{
			if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpoint));
			if (string.IsNullOrWhiteSpace(certPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(certPath));

			AuthenticationControllerEndpoint = endpoint;
			JwtSigningX509Certificate2Path = certPath;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		public AuthenticationServerConfigurationModel()
		{
			
		}
	}
}
