using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HaloLive.Hosting
{
	public class X509Certificate2Loader
	{
		public static X509Certificate2Loader Create(string certName)
		{
			return new X509Certificate2Loader(certName);
		}

		private string CertName { get; }

		public X509Certificate2Loader(string certName)
		{
			if (string.IsNullOrWhiteSpace(certName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(certName));

			CertName = certName;
		}

		/// <summary>
		/// Loads the <see cref="X509Certificate2"/>
		/// </summary>
		/// <returns>The certificate loaded.</returns>
		public X509Certificate2 Load()
		{
			X509Certificate2 cert = null;

			//We only try the file store for now
			new FileStoreX509Certificate2LoadingStrategy().TryLoadCertificate(CertName, out cert);

			if(cert == null)
				throw new InvalidOperationException($"Failed to load the certifcate {CertName}.");

			return cert;
		}
	}
}
