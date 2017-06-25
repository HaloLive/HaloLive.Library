using System;
using System.Collections.Generic;
using System.Text;
using AspNet.Security.OpenIdConnect.Primitives;
using HaloLive.Models.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace HaloLive.Hosting
{
	public static class HaloLiveAuthorizationServerIServiceCollectionExtensions
	{
		public static IServiceCollection AddHaloLiveAuthorization(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			//Service required for reading the JWT claims.
			services.AddSingleton<IClaimsPrincipalReader, ClaimsPrincipalReader>();

			//We also need to enable identity
			services.AddIdentity<HaloLiveApplicationUser, HaloLiveApplicationRole>(options =>
			{
				//These disable the ridiculous requirements that the defauly password scheme has
				options.Password.RequireNonAlphanumeric = false;

				//For some reason I can't figure out how to get the JWT middleware to spit out sub claims
				//so we need to map the Identity to expect nameidentifier
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
				options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
			});

			return services;
		}
	}
}
