using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HaloLive.Hosting.Tests.UnitTests
{
	[TestFixture]
	public static class X509Certificate2LoaderTests
	{
		[Test]
		public static void Test_Throws_On_Invalid_File()
		{
			Assert.Throws<InvalidOperationException>(() => X509Certificate2Loader.Create("nothing").Load());
		}

		[Test]
		public static void Test_Loads_NonNull_Cert_On_Valid_Location()
		{
			//arrange
			X509Certificate2 cert = X509Certificate2Loader.Create(FilePathToCurrentTestDirectory("TestCert.pfx")).Load();

			//assert
			Assert.NotNull(cert);
		}

		public static string FilePathToCurrentTestDirectory(string fileName)
		{
			return Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\", fileName);
		}
	}
}
