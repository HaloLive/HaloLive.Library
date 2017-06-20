using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authorization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HaloLive.Models.Tests
{
	[TestFixture]
	public static class RealtimeHubAuthorizationResponseModelTests
	{
		[Test]
		[TestCase((RealtimeHubAuthorizationResponseCode)int.MaxValue)]
		[TestCase((RealtimeHubAuthorizationResponseCode)55555)]
		[TestCase((RealtimeHubAuthorizationResponseCode)(-5))]
		public static void Test_Throws_On_Construction_With_Invalid_Argument(RealtimeHubAuthorizationResponseCode value)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new RealtimeHubAuthorizationResponseModel(value));
		}

		[Test]
		[TestCase(RealtimeHubAuthorizationResponseCode.InvalidAuthorizationCredentials)]
		[TestCase(RealtimeHubAuthorizationResponseCode.ConnectionAuthorizedToDifferentClient)]
		[TestCase(RealtimeHubAuthorizationResponseCode.AlreadyAuthorized)]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(RealtimeHubAuthorizationResponseCode value)
		{
			//assert
			Assert.DoesNotThrow(() => new RealtimeHubAuthorizationResponseModel(value));
		}

		[Test]
		[TestCase(RealtimeHubAuthorizationResponseCode.AlreadyAuthorized)]
		[TestCase(RealtimeHubAuthorizationResponseCode.ConnectionAuthorizedToDifferentClient)]
		[TestCase(RealtimeHubAuthorizationResponseCode.InvalidAuthorizationCredentials)]
		[TestCase(RealtimeHubAuthorizationResponseCode.GeneralServiceUnavailable)]
		[TestCase(RealtimeHubAuthorizationResponseCode.HubConnectionAuthRequestAddressMismatch)]
		public static void Test_isSuccessful_False_On_Failed_ResponseCodes(RealtimeHubAuthorizationResponseCode value)
		{
			//arrange
			RealtimeHubAuthorizationResponseModel model = new RealtimeHubAuthorizationResponseModel(value);

			//assert
			Assert.False(model.isSuccessful);
		}

		[Test]
		[TestCase(RealtimeHubAuthorizationResponseCode.Success)]
		public static void Test_isSuccessful_True_On_Success(RealtimeHubAuthorizationResponseCode value)
		{
			//arrange
			RealtimeHubAuthorizationResponseModel model = new RealtimeHubAuthorizationResponseModel(value);

			//assert
			Assert.True(model.isSuccessful);
		}

		[Test]
		[TestCase(RealtimeHubAuthorizationResponseCode.InvalidAuthorizationCredentials)]
		[TestCase(RealtimeHubAuthorizationResponseCode.ConnectionAuthorizedToDifferentClient)]
		[TestCase(RealtimeHubAuthorizationResponseCode.AlreadyAuthorized)]
		[TestCase(RealtimeHubAuthorizationResponseCode.Success)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(RealtimeHubAuthorizationResponseCode value)
		{
			//arrange
			RealtimeHubAuthorizationResponseModel authModel = new RealtimeHubAuthorizationResponseModel(value);

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(((int)value).ToString()));
		}

		[Test]
		[TestCase(RealtimeHubAuthorizationResponseCode.InvalidAuthorizationCredentials)]
		[TestCase(RealtimeHubAuthorizationResponseCode.ConnectionAuthorizedToDifferentClient)]
		[TestCase(RealtimeHubAuthorizationResponseCode.AlreadyAuthorized)]
		[TestCase(RealtimeHubAuthorizationResponseCode.Success)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(RealtimeHubAuthorizationResponseCode value)
		{
			//arrange
			RealtimeHubAuthorizationResponseModel authModel = new RealtimeHubAuthorizationResponseModel(value);

			//act
			RealtimeHubAuthorizationResponseModel deserializedModel =
				JsonConvert.DeserializeObject<RealtimeHubAuthorizationResponseModel>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.True(Enum.IsDefined(typeof(RealtimeHubAuthorizationResponseCode), deserializedModel.ResultCode));
			Assert.AreEqual(value, deserializedModel.ResultCode);
		}
	}
}
