using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HaloLive.Network.Common.Tests
{
	[TestFixture]
	public static class AuthenticationRequestModelTests
	{
		[Test]
		[TestCase(null, null)]
		[TestCase("Username", null)]
		[TestCase(null, "Password")]
		[TestCase(null, "")]
		[TestCase("", null)]
		[TestCase(null, "  ")]
		[TestCase("  ", null)]
		[TestCase("User", "  ")]
		[TestCase("  ", "Password")]
		[TestCase("   ", "   ")]
		[TestCase("", "   ")]
		[TestCase("   ", "")]
		public static void Test_Throws_On_Construction_With_Invalid_Arguments(string username, string password)
		{
			//assert
			Assert.Throws<ArgumentException>(() => new AuthenticationRequestModel(username, password));
		}

		[Test]
		[TestCase("Username", "Password")]
		[TestCase("Username55", "Password55")]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(string username, string password)
		{
			//assert
			Assert.DoesNotThrow(() => new AuthenticationRequestModel(username, password));
		}

		[Test]
		[TestCase("Username", "Password")]
		[TestCase("Username55", "Password55")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(string username, string password)
		{
			//arrange
			AuthenticationRequestModel authModel = new AuthenticationRequestModel(username, password);

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(username));
			
			//password may not be contained depending
		}

		[Test]
		[TestCase("Username", "Password")]
		[TestCase("Username55", "Password55")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(string username, string password)
		{
			//arrange
			AuthenticationRequestModel authModel = new AuthenticationRequestModel(username, password);

			//act
			AuthenticationRequestModel deserializedModel = 
				JsonConvert.DeserializeObject<AuthenticationRequestModel>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.UserName);
			Assert.NotNull(deserializedModel.Password);
			Assert.AreEqual(username, deserializedModel.UserName);

			//password may not be contained depending
		}
	}
}
