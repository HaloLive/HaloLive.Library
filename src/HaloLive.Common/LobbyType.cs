using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Common
{
	/// <summary>
	/// Enumeration of the lobby types in Halo.
	/// </summary>
	public enum LobbyType : int
	{
		/// <summary>
		/// The campaign lobby.
		/// </summary>
		Campaign = 0,

		/// <summary>
		/// The Xboxlive matchmaking lobby.
		/// </summary>
		Matchmaking = 1,

		//TODO: What is this really? Maybe custom games?
		/// <summary>
		/// Systemlink lobby? Not sure.
		/// </summary>
		Multiplayer = 2,

		/// <summary>
		/// The Forge lobby.
		/// </summary>
		Forge = 3,

		/// <summary>
		/// The replay theater lobby.
		/// </summary>
		Theater = 4
	}
}
