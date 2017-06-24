using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HaloLive.Models.Authentication
{
	/// <summary>
	/// HaloLive OpenIddict app role.
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class HaloLiveApplicationRole : IdentityRole<int> { } //we don't need any additional data; we rely directly on identity
}
