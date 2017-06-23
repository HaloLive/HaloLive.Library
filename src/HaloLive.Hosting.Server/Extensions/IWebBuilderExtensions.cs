using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace HaloLive.Hosting
{
	public static class IWebBuilderExtensions
	{
		/// <summary>
		/// Configure the server host with the arguments encoded in the <see cref="args"/> mapped to
		/// an options instance of <see cref="DefaultWebHostingArgumentsModel"/>.
		/// </summary>
		/// <param name="builder">The web host builder.</param>
		/// <param name="args">The commandline args.</param>
		/// <returns>The provided <see cref="IWebHostBuilder"/>for fluent chaining.</returns>
		public static IWebHostBuilder ConfigureKestrelHostWithCommandlinArgs(this IWebHostBuilder builder, string[] args)
		{
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					if (model.isHttpsEnabled)
					{
						//If https is enabled then a certifcate should be available for loading.
						builder.UseKestrel(options =>
						{
							options.UseHttps(new HttpsConnectionFilterOptions()
							{
								SslProtocols = System.Security.Authentication.SslProtocols.Tls
									| System.Security.Authentication.SslProtocols.Tls11
									| System.Security.Authentication.SslProtocols.Tls12,

								//Load the cert with the cert loader
								ServerCertificate = X509Certificate2Loader.Create(model.HttpsCertificateName).Load()
							});
						});
					}
					else
						builder.UseKestrel();

					if (model.isCustomUrlDefined)
					{
						builder.UseUrls(model.isHttpsEnabled
							? model.Url
								.ToLower()
								.Replace("http://", "https://")
							: model.Url);
					}
				});

			return builder;
		}
	}
}
