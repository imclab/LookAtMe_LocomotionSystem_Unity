using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Constants 
{
	[Flags]
	public enum FilteringMethod
	{
		None,
		Basic,
		LinearRegression
	}

	[Flags]
	public enum FootIKComputingType
	{
		Disabled,
		Enabled,
		Automatic
	}
}