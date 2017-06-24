using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
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
	}
}
