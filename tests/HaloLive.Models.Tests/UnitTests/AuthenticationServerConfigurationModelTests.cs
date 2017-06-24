using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using HaloLive.Models.NameResolution;
using HaloLive.Network.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HaloLive.Models.Tests.UnitTests
{
	[TestFixture]
	public static class AuthenticationServerConfigurationModelTests
	{
		/// <summary>
		/// Asp Core IOptions paradigm requires a public parameterless ctor
		/// due to new() constraint.
		/// </summary>
		[Test]
		public static void Test_Model_Had_Public_Parameterless_Ctor()
		{
			//assert
			Assert.True(typeof(AuthenticationServerConfigurationModel).GetTypeInfo().GetConstructors().Any(c => !c.GetParameters().Any()));
		}

		[Test]
		[TestCase(@"api/auth", "Certs/TestCert.pfx", "Test")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(string endpoint, string path, string dbString)
		{
			//arrange
			AuthenticationServerConfigurationModel model = new AuthenticationServerConfigurationModel(endpoint, path, dbString);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(endpoint));
			Assert.True(serializedModel.Contains(path));
			Assert.True(serializedModel.Contains(dbString));
		}

		[Test]
		[TestCase(@"api/auth", "Certs/TestCert.pfx", "Test")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(string endpoint, string path, string dbString)
		{
			//arrange
			AuthenticationServerConfigurationModel model = new AuthenticationServerConfigurationModel(endpoint, path, dbString);

			//act
			AuthenticationServerConfigurationModel deserializedModel =
				JsonConvert.DeserializeObject<AuthenticationServerConfigurationModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);

			Assert.NotNull(deserializedModel.AuthenticationControllerEndpoint);
			Assert.IsNotEmpty(deserializedModel.AuthenticationControllerEndpoint);
			Assert.AreEqual(endpoint, deserializedModel.AuthenticationControllerEndpoint);

			Assert.NotNull(deserializedModel.AuthenticationDatabaseString);
			Assert.IsNotEmpty(deserializedModel.AuthenticationDatabaseString);
			Assert.AreEqual(path, deserializedModel.JwtSigningX509Certificate2Path);

			Assert.NotNull(deserializedModel.JwtSigningX509Certificate2Path);
			Assert.IsNotEmpty(deserializedModel.JwtSigningX509Certificate2Path);
			Assert.AreEqual(dbString, deserializedModel.AuthenticationDatabaseString);
		}
	}
}
