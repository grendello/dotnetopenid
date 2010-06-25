using System;

namespace DotNetOpenAuth
{
	public class Mono
	{
		public static bool RunningMono { get; private set; }
		
		static Mono ()
		{
			Type t = Type.GetType ("Mono.Runtime", false);
			RunningMono = t != null;
		}
	}
}

