using System;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using TypeSafe.Http.Net;

namespace HaloLive.Network.Auth.TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Task.Run(AsyncMain).Wait();

			Console.ReadKey();
		}

		public static async Task AsyncMain()
		{
			Console.WriteLine("Starting.");
			Console.ReadKey();

			try
			{
				IAuthenticationService apiInterface = RestServiceBuilder<IAuthenticationService>.Create()
					.RegisterDotNetHttpClient(@"http://localhost.fiddler:5000")
					.RegisterDefaultSerializers()
					.RegisterJsonNetSerializer()
					.Build();

				JWTModel authResult = await apiInterface.TryAuthenticate(new AuthenticationRequestModel("admin", "Password69"));

				Console.WriteLine($"IsSuccessful: {authResult.isTokenValid} Token: {authResult.AccessToken}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}