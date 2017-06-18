using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace HaloLive.Network.Common
{
	/// <summary>
	/// The authentication request model.
	/// </summary>
	[JsonObject]
	public sealed class AuthenticationRequestModel
	{
		/// <summary>
		/// Username for the authentication request.
		/// </summary>
		[NotNull]
		[JsonProperty(PropertyName = "username", Required = Required.Always)]
		public string UserName { get; }

		/// <summary>
		/// Password for the authentication request.
		/// </summary>
		[NotNull]
		[JsonProperty(PropertyName = "password", Required = Required.Always)]
		public string Password { get; }

		/// <summary>
		/// The OAuth grant type.
		/// </summary>
		[NotNull]
		[JsonProperty(PropertyName = "grant_type", Required = Required.Always)]
		private string GrantType => "password";

		/// <summary>
		/// Creates a new Authentication Request Model.
		/// </summary>
		/// <param name="userName">The non-null username.</param>
		/// <param name="password">The non-null password.</param>
		public AuthenticationRequestModel([NotNull] string userName, [NotNull] string password)
		{
			if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

			UserName = userName;
			Password = password;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			//Just return the body that will likely be used for the auth
			return $"grant_type=password&username={UserName}&password={Password}";
		}
	}
}
