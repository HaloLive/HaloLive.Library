using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Metadata.Common
{
	/// <summary>
	/// Meta-data marker to indicate that a model requires authorization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class RequiresAuthorizationAttribute : Attribute
	{
		public RequiresAuthorizationAttribute()
		{
			
		}
	}
}
