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
using NUnit.Framework.Internal;

namespace HaloLive.Models.Tests.UnitTests
{
	[TestFixture]
	public static class ResolveServiceEndpointRequestModelTests
	{
		[Test]
		[TestCase((ClientRegionLocale)int.MaxValue, (NetworkServiceType)int.MaxValue)]
		[TestCase((ClientRegionLocale)55555, (NetworkServiceType)6463646)]
		[TestCase((ClientRegionLocale)(-5), (NetworkServiceType)(-5))]
		[TestCase(ClientRegionLocale.CN, (NetworkServiceType)(-5))]
		[TestCase((ClientRegionLocale)(-5), NetworkServiceType.AuthenticationService)]
		public static void Test_Throws_On_Construction_With_Invalid_Argument(ClientRegionLocale region, NetworkServiceType service)
		{
			//assert
			Assert.Throws<ArgumentOutOfRangeException>(() => new ResolveServiceEndpointRequestModel(region, service));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.EU, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.KR, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.RU, NetworkServiceType.AuthenticationService)]
		public static void Test_Doesnt_Throw_On_Valid_Arguments(ClientRegionLocale region, NetworkServiceType service)
		{
			//assert
			Assert.DoesNotThrow(() => new ResolveServiceEndpointRequestModel(region, service));
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.EU, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.KR, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.RU, NetworkServiceType.AuthenticationService)]
		public static void Test_Can_JSON_Serialize_To_NonNull_Non_Whitespace(ClientRegionLocale region, NetworkServiceType service)
		{
			//arrange
			ResolveServiceEndpointRequestModel model = new ResolveServiceEndpointRequestModel(region, service);

			//act
			string serializedModel = JsonConvert.SerializeObject(model);

			//assert
			Assert.NotNull(serializedModel);
			Assert.IsNotEmpty(serializedModel);
		}

		[Test]
		[TestCase(ClientRegionLocale.CN, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.EU, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.KR, NetworkServiceType.AuthenticationService)]
		[TestCase(ClientRegionLocale.RU, NetworkServiceType.AuthenticationService)]
		public static void Test_Can_JSON_Serialize_Then_Deserialize_With_Preserved_Values(ClientRegionLocale region, NetworkServiceType service)
		{
			//arrange
			ResolveServiceEndpointRequestModel model = new ResolveServiceEndpointRequestModel(region, service);

			//act
			ResolveServiceEndpointRequestModel deserializedModel =
				JsonConvert.DeserializeObject<ResolveServiceEndpointRequestModel>(JsonConvert.SerializeObject(model));

			//assert
			Assert.NotNull(deserializedModel);
			Assert.True(Enum.IsDefined(typeof(ClientRegionLocale), deserializedModel.Region));
			Assert.True(Enum.IsDefined(typeof(NetworkServiceType), deserializedModel.ServiceType));
		}
	}
}
