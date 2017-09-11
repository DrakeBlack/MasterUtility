using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PersonalEntity;

namespace BilledTime
{
	/// <summary>
	/// Format Converter For Date
	/// </summary>
	public class DateConverter : IValueConverter
	{
		/// <summary>
		/// Convert To Date
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Language"></param>
		/// <returns></returns>
		public Object Convert(Object _Value, Type _TargetType, Object _Parameter, String _Language)
		{
			try
			{
				DateTime _Date = (DateTime)_Value;

				return _Date.ToString("dddd yyyy-MM-dd");
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert To Date
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Culture"></param>
		/// <returns></returns>
		public Object Convert(Object _Value, Type _TargetType, Object _Parameter, CultureInfo _Culture)
		{
			try
			{
				return Convert(_Value, _TargetType, _Parameter, _Culture.Name);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert From Date
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Language"></param>
		/// <returns></returns>
		public Object ConvertBack(Object _Value, Type _TargetType, Object _Parameter, String _Language)
		{
			try
			{
				String _String = (String)_Value;

				return System.Convert.ToDateTime(_String);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert From Date
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Culture"></param>
		/// <returns></returns>
		public Object ConvertBack(Object _Value, Type _TargetType, Object _Parameter, CultureInfo _Culture)
		{
			try
			{
				return ConvertBack(_Value, _TargetType, _Parameter, _Culture.Name);
			}
			catch
			{
				return null;
			}
		}
	}

	/// <summary>
	/// Format Converter For Hours
	/// </summary>
	public class HourConverter : IValueConverter
	{
		/// <summary>
		/// Convert To Hours
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Language"></param>
		/// <returns></returns>
		public Object Convert(Object _Value, Type _TargetType, Object _Parameter, String _Language)
		{
			try
			{
				Double _Hours = (Double)_Value;

				return String.Format("{0:0.00}", _Hours);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert To Hours
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Culture"></param>
		/// <returns></returns>
		public Object Convert(Object _Value, Type _TargetType, Object _Parameter, CultureInfo _Culture)
		{
			try
			{
				return Convert(_Value, _TargetType, _Parameter, _Culture.Name);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert From Hours
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Language"></param>
		/// <returns></returns>
		public Object ConvertBack(Object _Value, Type _TargetType, Object _Parameter, String _Language)
		{
			try
			{
				String _String = (String)_Value;

				return System.Convert.ToDouble(_String);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Convert From Hours
		/// </summary>
		/// <param name="_Value"></param>
		/// <param name="_TargetType"></param>
		/// <param name="_Parameter"></param>
		/// <param name="_Culture"></param>
		/// <returns></returns>
		public Object ConvertBack(Object _Value, Type _TargetType, Object _Parameter, CultureInfo _Culture)
		{
			try
			{
				return ConvertBack(_Value, _TargetType, _Parameter, _Culture.Name);
			}
			catch
			{
				return null;
			}
		}
	}
}
