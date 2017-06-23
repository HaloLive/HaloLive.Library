using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using HaloLive.Models.Hosting;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HaloLive.Models.Tests.UnitTests
{
	[TestFixture]
	public static class DefaultWebHostingArgumentsModelTests
	{
		[Test]
		public static void Test_Can_Parse_From_Empty_Args()
		{
			//arrange
			string[] args = new string[0];

			//act
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args);
			//.WithParsed(model => Assert.Fail("There are no parsed options. It should never reach here."))
			//.WithNotParsed(errors => Assert.Fail("There should be no incorrectly parsed options. It should never reach here."));
		}

		[Test]
		[TestCase()]
		[TestCase("", "hi", "test", "   ")]
		public static void Test_Can_Parse_From_Empty_Args_Without_Producing_Invalid_State(params string[] args)
		{
			//act
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model => Assert.False(model.isCustomUrlDefined || model.isHttpsEnabled || model.isJwtEnabled))
				.WithNotParsed(Assert.IsEmpty);
		}

		[Test]
		[TestCase("--usehttps=TestCert.pfx")]
		public static void Test_Can_Parse_Https_Option_from_Args(params string[] args)
		{
			//assert
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					Assert.False(model.isCustomUrlDefined || model.isJwtEnabled || !model.isHttpsEnabled);
					Assert.AreEqual("TestCert.pfx", model.HttpsCertificateName);
				})
				.WithNotParsed(errors => Assert.IsEmpty(errors, $"Errors {errors.Aggregate("", (s, error) => $"{s}\n{error}")}"));
		}

		[Test]
		[TestCase(@"--url=http://127.0.0.1:80")]
		public static void Test_Can_Parse_Url_Option_from_Args(params string[] args)
		{
			//assert
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					Assert.False(model.isHttpsEnabled || model.isJwtEnabled || !model.isCustomUrlDefined);
					Assert.AreEqual(@"http://127.0.0.1:80", model.Url);
				})
				.WithNotParsed(errors => Assert.IsEmpty(errors, $"Errors {errors.Aggregate("", (s, error) => $"{s}\n{error}")}"));
		}

		[Test]
		[TestCase("--usejwt=TestCert.pfx")]
		public static void Test_Can_Parse_Jwt_Option_from_Args(params string[] args)
		{
			//assert
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					Assert.False(model.isCustomUrlDefined || model.isHttpsEnabled || !model.isJwtEnabled);
					Assert.AreEqual("TestCert.pfx", model.JwtCertificateName);
				})
				.WithNotParsed(errors => Assert.IsEmpty(errors, $"Errors {errors.Aggregate("", (s, error) => $"{s}\n{error}")}"));
		}

		[Test]
		[TestCase("--usejwt=TestCert.pfx", @"--url=http://127.0.0.1:80", "--usehttps=TestCert.pfx")]
		public static void Test_Can_Parse_All_Option_from_Args(params string[] args)
		{
			//assert
			Parser.Default.ParseArguments<DefaultWebHostingArgumentsModel>(args)
				.WithParsed(model =>
				{
					Assert.True(model.isCustomUrlDefined && model.isHttpsEnabled && model.isJwtEnabled);
					Assert.AreEqual("TestCert.pfx", model.JwtCertificateName);
					Assert.AreEqual(@"http://127.0.0.1:80", model.Url);
					Assert.AreEqual("TestCert.pfx", model.HttpsCertificateName);
				})
				.WithNotParsed(errors => Assert.IsEmpty(errors, $"Errors {errors.Aggregate("", (s, error) => $"{s}\n{error}")}"));
		}
	}
}
