using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authorization;
using HaloLive.Models.NameResolution;
using HaloLive.Network.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HaloLive.Models.Tests.UnitTests
{
	[TestFixture]
	public static class ResolveServiceEndpointResponseModelTests
	{
		[Test]
		[TestCase((NetworkServiceType)int.MaxValue)]
		[TestCase((NetworkServiceType)55555)]
		[TestCase((NetworkServiceType)(-5))]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_ServiceType(NetworkServiceType value)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new ResolveServiceEndpointResponseModel(value, new ResolvedEndpoint("test", 55)));
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService)]
		public static void Test_Throws_On_Construction_With_Invalid_Argument_Endpoint(NetworkServiceType value)
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new ResolveServiceEndpointResponseModel(value, null));
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService)]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(NetworkServiceType serviveType)
		{
			//assert
			Assert.DoesNotThrow(() => new ResolveServiceEndpointResponseModel(serviveType, new ResolvedEndpoint("test", 55)));
			Assert.DoesNotThrow(() => new ResolveServiceEndpointResponseModel(serviveType, ResolveServiceEndpointResponseCode.Success));
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		public static void Test_isSuccessful_False_On_Failed_ResponseCodes(NetworkServiceType serviceType, ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponseModel model = new ResolveServiceEndpointResponseModel(serviceType, resultCode);

			//assert
			Assert.False(model.isSuccessful);
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, "127.0.0.1", 55)]
		[TestCase(NetworkServiceType.AuthenticationService, "www.domain.com", 80)]
		public static void Test_isSuccessful_True_On_Success(NetworkServiceType serviceType, string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponseModel model = new ResolveServiceEndpointResponseModel(serviceType, new ResolvedEndpoint(endpoint, port));

			//assert
			Assert.True(model.isSuccessful);
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, "127.0.0.1", 55)]
		[TestCase(NetworkServiceType.AuthenticationService, "www.domain.com", 80)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(NetworkServiceType serviceType, string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponseModel authModel = new ResolveServiceEndpointResponseModel(serviceType, new ResolvedEndpoint(endpoint, port));

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(endpoint));
			Assert.True(serializedModel.Contains(port.ToString()));
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, "127.0.0.1", 55)]
		[TestCase(NetworkServiceType.AuthenticationService, "www.domain.com", 80)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(NetworkServiceType serviceType, string endpoint, int port)
		{
			//arrange
			ResolveServiceEndpointResponseModel authModel = new ResolveServiceEndpointResponseModel(serviceType, new ResolvedEndpoint(endpoint, port));

			//act
			ResolveServiceEndpointResponseModel deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointResponseModel>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.NotNull(deserializedModel.Endpoint);
			Assert.NotNull(deserializedModel.Endpoint.EndpointAddress);
			Assert.True(Enum.IsDefined(typeof(ResolveServiceEndpointResponseCode), deserializedModel.ResultCode));
			Assert.True(Enum.IsDefined(typeof(NetworkServiceType), deserializedModel.EndpointType), $"ServiceType {(int)deserializedModel.EndpointType}/{deserializedModel.EndpointType.ToString()} is not valid.");
			Assert.AreEqual(endpoint, deserializedModel.Endpoint.EndpointAddress);
			Assert.AreEqual(port, deserializedModel.Endpoint.EndpointPort);
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.Success)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnlisted)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(NetworkServiceType serviceType, ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponseModel authModel = new ResolveServiceEndpointResponseModel(serviceType, resultCode);

			//act
			string serializedModel = JsonConvert.SerializeObject(authModel);

			//assert
			Assert.NotNull(serializedModel);
			Assert.True(!serializedModel.Contains(nameof(authModel.isSuccessful)), $"JSON modle contains what should be unlisted field {nameof(authModel.isSuccessful)}. JSON: {serializedModel}");
			Assert.IsNotEmpty(serializedModel);
			Assert.True(serializedModel.Contains(((int)resultCode).ToString()));
			Assert.True(serializedModel.Contains(((int)serviceType).ToString()));
		}

		[Test]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.Success)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnavailable)]
		[TestCase(NetworkServiceType.AuthenticationService, ResolveServiceEndpointResponseCode.ServiceUnlisted)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(NetworkServiceType serviceType, ResolveServiceEndpointResponseCode resultCode)
		{
			//arrange
			ResolveServiceEndpointResponseModel authModel = new ResolveServiceEndpointResponseModel(serviceType, resultCode);

			//act
			ResolveServiceEndpointResponseModel deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointResponseModel>(JsonConvert.SerializeObject(authModel));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.Null(deserializedModel.Endpoint);
			Assert.True(Enum.IsDefined(typeof(ResolveServiceEndpointResponseCode), deserializedModel.ResultCode));
			Assert.True(Enum.IsDefined(typeof(NetworkServiceType), deserializedModel.EndpointType));
		}
	}
}
