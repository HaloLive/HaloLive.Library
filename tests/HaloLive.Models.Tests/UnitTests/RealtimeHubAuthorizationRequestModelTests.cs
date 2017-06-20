using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authorization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HaloLive.Models.Tests.UnitTests
{
	[TestFixture]
	public static class RealtimeHubAuthorizationRequestModelTests
	{
		[Test]
		[TestCase("")]
		[TestCase("  ")]
		[TestCase(null)]
		public static void Test_Throws_On_Construction_With_Invalid_Argument(string value)
		{
			//assert
			Assert.Throws<ArgumentException>(() => new RealtimeHubAuthorizationRequestModel(value));
		}

		[Test]
		[TestCase("123123-adsuu241234e-dfgfg")]
		[TestCase("Blahblah blah")]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(string value)
		{
			//assert
			Assert.DoesNotThrow(() => new RealtimeHubAuthorizationRequestModel(value));
		}

		[Test]
		[TestCase("123123-adsuu241234e-dfgfg")]
		[TestCase("Blahblah blah")]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(string value)
		{
			//arrange
			RealtimeHubAuthorizationRequestModel authModel = new RealtimeHubAuthorizationRequestModel(value);

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(value));
		}

		[Test]
		[TestCase("123123-adsuu241234e-dfgfg")]
		[TestCase("Blahblah blah")]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(string value)
		{
			//arrange
			RealtimeHubAuthorizationRequestModel authModel = new RealtimeHubAuthorizationRequestModel(value);

			//act
			RealtimeHubAuthorizationRequestModel deserializedModel =
				JsonConvert.DeserializeObject<RealtimeHubAuthorizationRequestModel>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.HubConnectionId);
			Assert.AreEqual(value, deserializedModel.HubConnectionId);
		}
	}
}
