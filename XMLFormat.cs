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
	public static class XMLFormat
	{
		/// <summary>
		/// Formats An XML File
		/// </summary>
		/// <param name="_FileName">File To Format</param>
		/// <returns></returns>
		public static List<String> Format(String _FileName)
		{
			try
			{
				String l_Format = "[FILENAME]_[DATETIME]";
				FileInfo l_OriginalFile = new FileInfo(_FileName);
				FileInfo l_NewFile = new FileInfo(_FileName);
				List<String> l_Results = new List<String>();
				l_Results.Add("File Name [" + _FileName + "]");

				if (!l_OriginalFile.Exists)
				{
					l_Results.Add("File Does Not Exist");

					return l_Results;
				}

				if (l_OriginalFile.Extension.ToLower() != ".xml" && l_OriginalFile.Extension.ToLower() != ".config")
				{
					l_Results.Add("Invalid File Format");

					return l_Results;
				}

				l_Format = l_Format.Replace("[FILENAME]", System.IO.Path.GetFileNameWithoutExtension(l_OriginalFile.Name));
				l_Format = l_Format.Replace("[DATETIME]", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
				l_Format += l_OriginalFile.Extension;
				l_Results.Add("File Backup [" + l_Format + "]");
				l_OriginalFile.MoveTo(l_OriginalFile.DirectoryName + "\\" + l_Format);
				XmlDocument l_Document = new XmlDocument();
				l_Results.Add("File Loading");
				l_Document.Load(l_OriginalFile.FullName);
				XmlWriterSettings l_Settings = new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "\t",
					NewLineChars = "\r\n",
					NewLineHandling = NewLineHandling.Entitize,
					NewLineOnAttributes = true,
					OmitXmlDeclaration = false,
					Encoding = new UTF8Encoding(true),
				};

				l_Results.Add("File Saving");

				using (XmlWriter xml_Writer = XmlTextWriter.Create(l_NewFile.FullName, l_Settings))
				{
					l_Document.Save(xml_Writer);
				}

				l_Results.Add("File Formatted [" + l_NewFile.Name + "]");

				if (l_Results == null || l_Results.Count == 0)
				{
					l_Results = null;
				}

				return l_Results;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
	}
}
