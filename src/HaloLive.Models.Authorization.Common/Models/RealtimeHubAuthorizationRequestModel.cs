using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Metadata.Common;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace HaloLive.Models.Authorization
{
	//Security-wise this is exploitable but only in beign ways. A user with the same public IP that knows a user's hub connectionId could theoratically
	//authorize the hub to publish for an account THEY control. This is an exploit but pointless.
	/// <summary>
	/// Model/request that should be sent to preform the authorization step for a realtime hub session.
	/// </summary>
	[JsonObject]
	[RequiresAuthorization]
	public sealed class RealtimeHubAuthorizationRequestModel
	{
		//TODO: Security audit
		//To understand this you must know that at this point a JWT must have been issued to this user during authentication.
		//We can send this JWT to identify and authorize users for actions. However, we use a realtime hub to publish events to them
		//but we need a way to know what account data should be published and we must authorize that first. To do this we require the client to
		//send their hub connection ID along with the JWT in the header allowing for the hub connection to go live.
		/// <summary>
		/// The connection ID of the hub to authorize.
		/// </summary>
		[NotNull]
		[JsonProperty(Required = Required.Always)]
		public string HubConnectionId { get; }

		public RealtimeHubAuthorizationRequestModel([NotNull] string hubConnectionId)
		{
			if (string.IsNullOrWhiteSpace(hubConnectionId)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(hubConnectionId));

			HubConnectionId = hubConnectionId;
		}

		/// <summary>
		/// Protected serializer ctor
		/// </summary>
		protected RealtimeHubAuthorizationRequestModel()
		{
			
		}
	}
}
