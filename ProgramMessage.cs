using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using PersonalEntity;

namespace MasterUtility
{
    public static class ProgramMessage
    {
		/// <summary>
		/// Information
		/// </summary>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Message">Message to log</param>
		public static void Information(MethodBase _Sender, String _Message)
		{
			try
			{
				Log("Information", _Sender, _Message, null);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Warning
		/// </summary>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Message">Message to log</param>
		public static void Warning(MethodBase _Sender, String _Message)
		{
			try
			{
				Log("Warning", _Sender, _Message, null);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Exception
		/// </summary>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Message">Message to log</param>
		/// <param name="_Exception">Exception to log</param>
		public static void Exception(MethodBase _Sender, String _Message, Exception _Exception)
		{
			try
			{
				Log("Exception", _Sender, _Message, _Exception);
				String _SenderName = "Unknown";

				try
				{
					_SenderName = CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
				}
				catch
				{
					_SenderName = "Unknown";
				}

				MessageBox.Show(_Message, _SenderName, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Debug
		/// </summary>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Message">Message to log</param>
		public static void Debug(MethodBase _Sender, String _Message)
		{
			try
			{
				Log("Debug", _Sender, _Message, null);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Logs Program Message
		/// </summary>
		/// <param name="_Level"></param>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Message">Message to log</param>
		/// <param name="_Exception">Exception to log</param>
		private static void Log(String _Level, MethodBase _Sender, String _Message, Exception _Exception)
		{
			try
			{
				DateTime _DateTimeNow = DateTime.Now;
				String _SenderName = "Unknown";
				_Level = CleanString(_Level, "Unknown", true);
				_Message = CleanString(_Message, "Unknown");

				try
				{
					_SenderName = CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
				}
				catch
				{
					_SenderName = "Unknown";
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					PersonalEntity.ProgramMessage _ProgramMessage = new PersonalEntity.ProgramMessage();

					using (TextWriter _Writer = new StreamWriter("BilledTime_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", true))
					{
						_Writer.WriteLine(_DateTimeNow.ToString("yyyy-MM-dd HH:mm:ss"));
						_ProgramMessage.DateTime = _DateTimeNow;
						_Writer.WriteLine("Level: " + _Level);
						_ProgramMessage.LevelCategory = _Level;
						_Writer.WriteLine("Sender: " + _SenderName);
						_ProgramMessage.Sender = _SenderName;
						_Writer.WriteLine("Message: " + _Message);
						_ProgramMessage.Message = _Message;

						if (_Exception != null)
						{
							PersonalEntity.ProgramMessageException _ProgramMessageException = new PersonalEntity.ProgramMessageException();
							Int32 _Order = 0;
							_ProgramMessageException.ExceptionOrder = _Order++;
							_Writer.WriteLine("Exception: " + _Exception.Message);
							_ProgramMessageException.ExceptionMessage = _Exception.Message;
							_ProgramMessage.Exceptions.Add(_ProgramMessageException);

							while (_Exception.InnerException != null)
							{
								_Exception = _Exception.InnerException;
								_ProgramMessageException.ExceptionOrder = _Order++;
								_Writer.WriteLine("Exception: " + _Exception.Message);
								_ProgramMessageException.ExceptionMessage = _Exception.Message;
								_ProgramMessage.Exceptions.Add(_ProgramMessageException);
							}
						}

						_Writer.WriteLine();
						_Writer.Flush();
						Entity.ProgramMessages.Add(_ProgramMessage);
						Entity.SaveChanges();
					}
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Exception To Throw
		/// </summary>
		/// <param name="_Sender">Method Base of sender</param>
		/// <param name="_Exception">Base Exception</param>
		/// <returns>Throws Exception</returns>
		public static Exception Throw(MethodBase _Sender, Exception _Exception)
		{
			try
			{
				String _SenderName = "Unknown";

				try
				{
					_SenderName = CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
				}
				catch
				{
					_SenderName = "Unknown";
				}

				return new Exception(_SenderName, _Exception);
			}
			catch (Exception e_Exception)
			{
				throw new System.Exception("Cannot Throw Exception", e_Exception);
			}
		}

		/// <summary>
		/// Clean String
		/// </summary>
		/// <param name="_String">String to be cleaned</param>
		/// <returns></returns>
		public static String CleanString(String _String)
		{
			try
			{
				return CleanString(_String, null, false, new Char[] { });
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Clean String
		/// </summary>
		/// <param name="_String">String to be cleaned</param>
		/// <param name="_Default">Default for String if it is null or empty</param>
		/// <returns></returns>
		public static String CleanString(String _String, String _Default)
		{
			try
			{
				return CleanString(_String, _Default, false, new Char[] { });
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Clean String
		/// </summary>
		/// <param name="_String">String to be cleaned</param>
		/// <param name="_Default">Default for String if it is null or empty</param>
		/// <param name="_CamelCase">Should the String have Camel Case</param>
		/// <returns></returns>
		public static String CleanString(String _String, String _Default, Boolean _CamelCase)
		{
			try
			{
				return CleanString(_String, _Default, _CamelCase, new Char[] { });
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		/// <summary>
		/// Clean String
		/// </summary>
		/// <param name="_String">String to be cleaned</param>
		/// <param name="_Default">Default for String if it is null or empty</param>
		/// <param name="_CamelCase">Should the String have Camel Case</param>
		/// <param name="_SplitCharacters">List of characters to split String for Camel Case</param>
		/// <returns></returns>
		public static String CleanString(String _String, String _Default, Boolean _CamelCase, Char[] _SplitCharacters)
		{
			try
			{
				String _Return = null;

				if (String.IsNullOrWhiteSpace(_String))
				{
					return _Default;
				}

				_String = _String.Trim();

				if (String.IsNullOrWhiteSpace(_String))
				{
					return _Default;
				}

				if (_SplitCharacters == null || _SplitCharacters.Length == 0)
				{
					_SplitCharacters = new Char[] { ' ' };
				}

				if (_CamelCase)
				{
					_Return = String.Empty;
					Boolean _CapitalLetter = true;
					Char[] _Letters = _String.ToArray();

					foreach (Char fe_Letter in _Letters)
					{
						if (_SplitCharacters.Contains(fe_Letter))
						{
							_Return += fe_Letter.ToString();
							_CapitalLetter = true;
						}
						else if (_CapitalLetter)
						{
							_Return += fe_Letter.ToString().ToUpper();
							_CapitalLetter = false;
						}
						else
						{
							_Return += fe_Letter.ToString().ToLower();
						}
					}
				}
				else
				{
					_Return = _String;
				}

				return _Return;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
	}
}
