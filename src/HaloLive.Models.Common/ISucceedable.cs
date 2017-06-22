using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Models
{
	/// <summary>
	/// Contract for objects that have a successful or unsuccessful state.
	/// </summary>
	public interface ISucceedable
	{
		/// <summary>
		/// Indicates if the object represents a successful state.
		/// </summary>
		bool isSuccessful { get; }
	}
}
