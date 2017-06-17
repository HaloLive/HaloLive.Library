using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Common
{
	/// <summary>
	/// Enumeration of valid party modes for an Xboxlive party.
	/// </summary>
	public enum LobbyPartyMode : int
	{
		/// <summary>
		/// Indicates that the lobby is open to all.
		/// </summary>
		OpenParty = 0,

		/// <summary>
		/// Indicates that this is a friends only lobby.
		/// </summary>
		FriendsOnly = 1,

		/// <summary>
		/// Indicates that the lobby can only be joined by invite.
		/// </summary>
		InviteOnly = 2,

		//TODO: What is this? Local?
		Online = 3,

		//TODO: What is this? Local?
		Offline = 4
	}
}
