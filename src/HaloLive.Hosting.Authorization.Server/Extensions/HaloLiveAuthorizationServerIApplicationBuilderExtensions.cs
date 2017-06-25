using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace HaloLive.Hosting
{
	public static class HaloLiveAuthorizationServerIApplicationBuilderExtensions
	{
		//TODO: Test this; Can't with moq due to extension methods
		/// <summary>
		/// Registers JWT authorization in the <see cref="IApplicationBuilder"/> middleware pipeline.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <param name="jwtCertificate">The certificate used to check the signature of the JWT.</param>
		/// <returns>The application builder.</returns>
		[Obsolete("This is technically obsolete. In .NET CORE 2.0 it will be removed/changed. See https://github.com/aspnet/Home/issues/2007")]
		public static IApplicationBuilder UseHaloLiveAuthorization(this IApplicationBuilder builder, X509Certificate2 jwtCertificate)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (jwtCertificate == null) throw new ArgumentNullException(nameof(jwtCertificate));

			//This is CRITICAL for now.
			//See https://github.com/aspnet/Security/issues/1043
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			JwtBearerOptions bearerOptions = new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				RequireHttpsMetadata = true,
				TokenValidationParameters = new TokenValidationParameters()
				{
					IssuerSigningKey = new X509SecurityKey(jwtCertificate),
					ValidateIssuerSigningKey = false, //WARNING: This is bad. We should validate the signing key in the future
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateLifetime = false, //temporary until we come up with a solution

					NameClaimType = OpenIdConnectConstants.Claims.Name,
					RoleClaimType = OpenIdConnectConstants.Claims.Role
				}
			};

			//TODO: THIS IS OBSOLETE in .NET CORE 2.0 https://github.com/aspnet/Home/issues/2007
			//fluently return
			return builder.UseJwtBearerAuthentication(bearerOptions)
				.UseIdentity();
		}
	}
}
