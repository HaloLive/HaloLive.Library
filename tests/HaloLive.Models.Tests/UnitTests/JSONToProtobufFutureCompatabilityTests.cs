using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using HaloLive.Models.Authorization;
using HaloLive.Models.NameResolution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HaloLive.Network.Common.Tests
{
	[TestFixture]
	public static class JSONToProtobufFutureCompatabilityTests
	{
		[Test]
		public static void Test_All_JSON_DTOs_Have_Parameterless_Constructors()
		{
			//arrange
			IEnumerable<Type> JSONDTOTypes = GetAllJsonObjectTypesFromAssemblyType(typeof(JWTModel));

			JSONDTOTypes = JSONDTOTypes.Concat(GetAllJsonObjectTypesFromAssemblyType(typeof(RealtimeHubAuthorizationEventModel)));
			JSONDTOTypes = JSONDTOTypes.Concat(GetAllJsonObjectTypesFromAssemblyType(typeof(ResolveServiceEndpointRequestModel)));

			//assert
			foreach (Type t in JSONDTOTypes)
			{
				Assert.True(t.GetTypeInfo().DeclaredConstructors.Any(c => !c.GetParameters().Any()), $"The type {t.FullName} does not have a parameterless constructor.");
			}
		}

		private static IEnumerable<Type> GetAllJsonObjectTypesFromAssemblyType(Type assType)
		{
			return assType.Assembly.GetTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>() != null);
		}
	}
}
