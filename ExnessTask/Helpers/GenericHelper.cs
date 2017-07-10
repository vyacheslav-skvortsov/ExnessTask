using System.ComponentModel;

namespace ExnessTask.Helpers
{
	public static class GenericHelper
	{
		public static T Convert<T>(string s)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			return (T)converter.ConvertFromString(s);
		}
	}
}
