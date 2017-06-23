using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HaloLive.Hosting
{
	/// <summary>
	/// Strategy for loading the cert from the file store.
	/// It expects the cert to be loaded in the certs folder.
	/// </summary>
	public class FileStoreX509Certificate2LoadingStrategy : IX509Certificate2LoadingStrategy
	{
		/// <inheritdoc />
		public bool TryLoadCertificate(string name, out X509Certificate2 cert)
		{
			if (File.Exists(name))
			{
				cert = new X509Certificate2(name);
				return true;
			}

			cert = null;
			return false;
		}
	}
}
