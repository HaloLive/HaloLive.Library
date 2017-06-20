using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
			IEnumerable<Type> JSONDTOTypes = typeof(JWTModel).Assembly.GetTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>() != null);

			//assert
			foreach (Type t in JSONDTOTypes)
			{
				Assert.True(t.GetTypeInfo().DeclaredConstructors.Any(c => !c.GetParameters().Any()));
			}
		}
	}
}
