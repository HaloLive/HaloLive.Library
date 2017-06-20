using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace HaloLive.Network.Common
{
	/// <summary>
	/// JWT Token model.
	/// </summary>
	[JsonObject]
	public sealed class JWTModel
	{
		/// <summary>
		/// JWT access token if authentication was successful.
		/// </summary>
		[CanBeNull]
		[JsonProperty(PropertyName = "access_token", Required = Required.Default)] //optional because could be an error
		public string AccessToken { get; private set; } //WARNING: Don't make these readonly. It breakes for some reason.

		/// <summary>
		/// Error type if an error was encountered.
		/// </summary>
		[CanBeNull]
		[JsonProperty(PropertyName = "error", Required = Required.Default)] //optional because could be a valid token
		public string Error { get; private set; } //WARNING: Don't make these readonly. It breakes for some reason.

		/// <summary>
		/// Humanreadable read description.
		/// </summary>
		[CanBeNull]
		[JsonProperty(PropertyName = "error_description", Required = Required.Default)] //optional because could be a valid token
		public string ErrorDescription { get; private set; } //WARNING: Don't make these readonly. It breakes for some reason.

		private Lazy<bool> _isTokenValid { get; }

		/// <summary>
		/// Indicates if the model contains a valid <see cref="AccessToken"/>.
		/// </summary>
		public bool isTokenValid => _isTokenValid.Value;

		/// <summary>
		/// Creates a JWTModel that contains an valid non-null <see cref="AccessToken"/>.
		/// </summary>
		/// <param name="accessToken"></param>
		public JWTModel([NotNull] string accessToken)
			: this()
		{
			if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(accessToken));

			AccessToken = accessToken;
		}

		/// <summary>
		/// Creates a JWTModel that contains an valid non-null <see cref="AccessToken"/>.
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="errorDescription"></param>
		public JWTModel([NotNull] string error, [NotNull] string errorDescription)
			: this()
		{
			if (string.IsNullOrWhiteSpace(error)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(error));
			if (string.IsNullOrWhiteSpace(errorDescription)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(errorDescription));

			Error = error;
			ErrorDescription = errorDescription;
		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		private JWTModel()
		{
			_isTokenValid = new Lazy<bool>(CheckIfTokenIsValid, true);
		}

		private bool CheckIfTokenIsValid()
		{
			return !String.IsNullOrEmpty(AccessToken) && String.IsNullOrEmpty(Error) && String.IsNullOrEmpty(ErrorDescription);
		}
	}
}
