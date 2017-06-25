using System;
using System.Collections.Generic;
using System.Text;
using HaloLive.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HaloLive.Hosting
{
	/// <summary>
	/// Base Halo Live MVC controller that is ready to handle authorization related functions.
	/// </summary>
	public abstract class AuthorizationReadyController : Controller
	{
		//Use GetUserId to get the HaloLive account ID.
		//USe GetUserName to get the HaloLive account name.
		protected IClaimsPrincipalReader HaloLiveUserManager { get; }

		/// <summary>
		/// Creates a new authorization ready controller.
		/// </summary>
		/// <param name="haloLiveUserManager">The user manager to be used for identity/user authorization functions.</param>
		protected AuthorizationReadyController([FromServices] IClaimsPrincipalReader haloLiveUserManager)
		{
			if (haloLiveUserManager == null) throw new ArgumentNullException(nameof(haloLiveUserManager));

			//We NEED the usermanager because the easiest and cleanest way to access the identity claims such as ID is through
			//the user manager
			HaloLiveUserManager = haloLiveUserManager;
		}
	}
}
