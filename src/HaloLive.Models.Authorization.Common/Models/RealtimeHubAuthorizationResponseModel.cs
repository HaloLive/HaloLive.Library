using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HaloLive.Models.Authorization
{
	/// <summary>
	/// Model/response sent back as a response to the <see cref="RealtimeHubAuthorizationEventModel"/>.
	/// </summary>
	[JsonObject]
	public sealed class RealtimeHubAuthorizationResponseModel : IResponseModel<RealtimeHubAuthorizationResponseCode>, ISucceedable
	{
		/// <summary>
		/// Indicates the result of the <see cref="RealtimeHubAuthorizationEventModel"/>
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		public RealtimeHubAuthorizationResponseCode ResultCode { get; }

		/// <summary>
		/// Indicates if the authorization request is successful.
		/// </summary>
		public bool isSuccessful => ResultCode == RealtimeHubAuthorizationResponseCode.Success;

		public RealtimeHubAuthorizationResponseModel(RealtimeHubAuthorizationResponseCode resultCode)
		{
			if (!Enum.IsDefined(typeof(RealtimeHubAuthorizationResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the RealtimeHubAuthorizationResponseCode enum.");

			ResultCode = resultCode;
		}

		/// <summary>
		/// Protected serializer ctor.
		/// </summary>
		protected RealtimeHubAuthorizationResponseModel()
		{
			
		}
	}
}
