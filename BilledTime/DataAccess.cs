using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PersonalEntity;
using Microsoft.Office.Interop.Excel;

namespace BilledTime
{
	/// <summary>
	/// Utilities
	/// </summary>
	public static class Utility
	{
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Create Time Sheet
		/// </summary>
		/// <param name="_ProjectGroupedBilledTimes">All Project Grouped Billed Time For Week</param>
		/// <param name="_DateGroupedBilledTime_Monday">Monday Date Grouped Billed Time</param>
		/// <param name="_DateGroupedBilledTime_Tuesday">Tuesday Date Grouped Billed Time</param>
		/// <param name="_DateGroupedBilledTime_Wednesday">Wednesday Date Grouped Billed Time</param>
		/// <param name="_DateGroupedBilledTime_Thursday">Thursday Date Grouped Billed Time</param>
		/// <param name="_DateGroupedBilledTime_Friday">Friday Date Grouped Billed Time</param>
		public static void CreateExcel(List<ProjectGroupedBilledTime> _ProjectGroupedBilledTimes, DateGroupedBilledTime _DateGroupedBilledTime_Monday, DateGroupedBilledTime _DateGroupedBilledTime_Tuesday, DateGroupedBilledTime _DateGroupedBilledTime_Wednesday, DateGroupedBilledTime _DateGroupedBilledTime_Thursday, DateGroupedBilledTime _DateGroupedBilledTime_Friday)
		{
			try
			{
				// https://i.pinimg.com/736x/4a/5d/6d/4a5d6d3f89b4939390c36df466d86ef5--pantone-color-chart-color-charts.jpg
				DateTime _Monday = DateTime.Now.Date;

				while (_Monday.DayOfWeek != DayOfWeek.Sunday)
				{
					_Monday = _Monday.AddDays(-1);
				}

				_Monday.AddDays(1);
				_Monday = (_DateGroupedBilledTime_Monday != null && _DateGroupedBilledTime_Monday.Date.HasValue ? _DateGroupedBilledTime_Monday.Date.Value : _Monday);
				DateTime _Tuesday = (_DateGroupedBilledTime_Tuesday != null && _DateGroupedBilledTime_Tuesday.Date.HasValue ? _DateGroupedBilledTime_Tuesday.Date.Value : _Monday.AddDays(1));
				DateTime _Wednesday = (_DateGroupedBilledTime_Wednesday != null && _DateGroupedBilledTime_Wednesday.Date.HasValue ? _DateGroupedBilledTime_Wednesday.Date.Value : _Monday.AddDays(2));
				DateTime _Thursday = (_DateGroupedBilledTime_Thursday != null && _DateGroupedBilledTime_Thursday.Date.HasValue ? _DateGroupedBilledTime_Thursday.Date.Value : _Monday.AddDays(3));
				DateTime _Friday = (_DateGroupedBilledTime_Friday != null && _DateGroupedBilledTime_Friday.Date.HasValue ? _DateGroupedBilledTime_Friday.Date.Value : _Monday.AddDays(4));
				Microsoft.Office.Interop.Excel.Application _Excel = new Microsoft.Office.Interop.Excel.Application();
				_Excel.Visible = true;
				Workbook _Workbook = _Excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
				Worksheet _Worksheet = (Worksheet)_Workbook.Worksheets[1];
				_Worksheet.Rows[1].EntireRow.RowHeight = 13.5;
				_Worksheet.Rows[2].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[3].EntireRow.RowHeight = 30.75;
				_Worksheet.Rows[4].EntireRow.RowHeight = 30.75;
				_Worksheet.Rows[5].EntireRow.RowHeight = 9;
				_Worksheet.Rows[6].EntireRow.RowHeight = 10.5;
				_Worksheet.Rows[7].EntireRow.RowHeight = 21.75;
				_Worksheet.Rows[8].EntireRow.RowHeight = 6.75;
				_Worksheet.Rows[9].EntireRow.RowHeight = 19.5;
				_Worksheet.Rows[10].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[11].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[12].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[13].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[14].EntireRow.RowHeight = 27;
				_Worksheet.Rows[15].EntireRow.RowHeight = 27;
				_Worksheet.Rows[16].EntireRow.RowHeight = 27;
				_Worksheet.Rows[17].EntireRow.RowHeight = 27;
				_Worksheet.Rows[18].EntireRow.RowHeight = 27;
				_Worksheet.Rows[19].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[20].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[21].EntireRow.RowHeight = 6.75;
				_Worksheet.Rows[22].EntireRow.RowHeight = 24.75;
				_Worksheet.Rows[23].EntireRow.RowHeight = 6;
				_Worksheet.Rows[24].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[25].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[26].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[27].EntireRow.RowHeight = 9.75;
				_Worksheet.Rows[28].EntireRow.RowHeight = 15.75;
				_Worksheet.Rows[29].EntireRow.RowHeight = 13.5;
				_Worksheet.Rows[30].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[31].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[32].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[33].EntireRow.RowHeight = 12.75;
				_Worksheet.Rows[34].EntireRow.RowHeight = 13.5;
				_Worksheet.Rows[35].EntireRow.RowHeight = 15;
				_Worksheet.Rows[36].EntireRow.RowHeight = 15;
				_Worksheet.Rows[37].EntireRow.RowHeight = 15;
				_Worksheet.Rows[38].EntireRow.RowHeight = 15;
				_Worksheet.Rows[39].EntireRow.RowHeight = 15;
				_Worksheet.Columns[1].EntireColumn.ColumnWidth = 2.86;
				_Worksheet.Columns[2].EntireColumn.ColumnWidth = 13.29;
				_Worksheet.Columns[3].EntireColumn.ColumnWidth = 11.86;
				_Worksheet.Columns[4].EntireColumn.ColumnWidth = 0.92;
				_Worksheet.Columns[5].EntireColumn.ColumnWidth = 13.86;
				_Worksheet.Columns[6].EntireColumn.ColumnWidth = 3.86;
				_Worksheet.Columns[7].EntireColumn.ColumnWidth = 13.86;
				_Worksheet.Columns[8].EntireColumn.ColumnWidth = 3.71;
				_Worksheet.Columns[9].EntireColumn.ColumnWidth = 12.43;
				_Worksheet.Columns[10].EntireColumn.ColumnWidth = 3.14;
				_Worksheet.Columns[11].EntireColumn.ColumnWidth = 11.86;
				_Worksheet.Columns[12].EntireColumn.ColumnWidth = 3.86;
				_Worksheet.Columns[13].EntireColumn.ColumnWidth = 12.71;
				_Worksheet.Columns[14].EntireColumn.ColumnWidth = 1.14;
				_Worksheet.Columns[15].EntireColumn.ColumnWidth = 17.86;
				_Worksheet.Columns[16].EntireColumn.ColumnWidth = 9.71;
				_Worksheet.Columns[17].EntireColumn.ColumnWidth = 8.43;

				Range _Fill = _Worksheet.Range["A1", "P38"];
				_Fill.Interior.Color = XlRgbColor.rgbWhite;
				_Fill.Font.Name = "Arial";
				_Fill.NumberFormat = "@";

				_Worksheet.Shapes.AddPicture(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Logo.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 45, 20, (float)(117 * 0.75), (float)(95 * 0.75));

				Range _Left = _Worksheet.Range["B2", "B34"];
				Borders _Border_Left = _Left.Borders;
				_Border_Left[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_Left[XlBordersIndex.xlEdgeLeft].Weight = 4d;

				Range _Top = _Worksheet.Range["B2", "O2"];
				Borders _Border_Top = _Top.Borders;
				_Border_Top[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				_Border_Top[XlBordersIndex.xlEdgeTop].Weight = 4d;

				Range _Right = _Worksheet.Range["O2", "O34"];
				Borders _Border_Right = _Right.Borders;
				_Border_Right[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				_Border_Right[XlBordersIndex.xlEdgeRight].Weight = 4d;

				Range _Bottom = _Worksheet.Range["B34", "O34"];
				Borders _Border_Bottom = _Bottom.Borders;
				_Border_Bottom[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Bottom[XlBordersIndex.xlEdgeBottom].Weight = 4d;

				Range _MonacoTitle = _Worksheet.Range["E3"];
				_MonacoTitle.Cells.Font.Size = 24;
				_MonacoTitle.Cells.Font.Name = "Clarendon Condensed";
				_MonacoTitle.Cells.Font.Bold = true;
				_MonacoTitle.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_MonacoTitle.Value = "MONACO ENTERPRISES INC.";

				Range _Exempt = _Worksheet.Range["G4"];
				_Exempt.Cells.Font.Size = 24;
				_Exempt.Cells.Font.Name = "Clarendon Condensed";
				_Exempt.Cells.Font.Bold = true;
				_Exempt.Cells.Font.Color = XlRgbColor.rgbRoyalBlue;
				_Exempt.Value = "EXEMPT EMPLOYEE TIME SHEET";

				Range _NameHeader = _Worksheet.Range["C7", "D7"];
				_NameHeader.Merge();
				_NameHeader.Cells.Font.Size = 16;
				_NameHeader.Cells.Font.Bold = true;
				_NameHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_NameHeader.Interior.Color = XlRgbColor.rgbPaleTurquoise;
				_NameHeader.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				Borders _Border_NameHeader = _NameHeader.Borders;
				_Border_NameHeader.LineStyle = XlLineStyle.xlContinuous;
				_Border_NameHeader.Weight = 4d;
				_NameHeader.Value = "NAME:";

				Range _Name = _Worksheet.Range["E7", "I7"];
				_Name.Merge();
				_Name.Cells.Font.Size = 16;
				_Name.Cells.Font.Bold = true;
				_Name.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				Borders _Border_Name = _Name.Borders;
				_Border_Name.LineStyle = XlLineStyle.xlContinuous;
				_Border_Name.Weight = 4d;
				_Name.Value = ConfigurationManager.AppSettings["EmployeeName"];

				Range _IDHeader = _Worksheet.Range["J7", "L7"];
				_IDHeader.Merge();
				_IDHeader.Cells.Font.Size = 16;
				_IDHeader.Cells.Font.Bold = true;
				_IDHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_IDHeader.Interior.Color = XlRgbColor.rgbPaleTurquoise;
				_IDHeader.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				Borders _Border_IDHeader = _IDHeader.Borders;
				_Border_IDHeader.LineStyle = XlLineStyle.xlContinuous;
				_Border_IDHeader.Weight = 4d;
				_IDHeader.Value = "EMPLOYEE#:";

				Range _ID = _Worksheet.Range["M7", "N7"];
				_ID.Merge();
				_ID.Cells.Font.Size = 16;
				_ID.Cells.Font.Bold = true;
				_ID.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ID.Cells.Font.Color = XlRgbColor.rgbGreen;
				Borders _Border_ID = _ID.Borders;
				_Border_ID.LineStyle = XlLineStyle.xlContinuous;
				_Border_ID.Weight = 4d;
				_ID.Value = ConfigurationManager.AppSettings["EmployeeID"];

				Range _Instructions01 = _Worksheet.Range["E9", "M9"];
				_Instructions01.Merge();
				_Instructions01.Cells.Font.Size = 12;
				_Instructions01.Cells.Font.Bold = true;
				_Instructions01.Cells.Font.Italic = true;
				_Instructions01.Cells.Font.Color = XlRgbColor.rgbDarkOrchid;
				_Instructions01.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Instructions01.Value = "Please enter your non-regular hours under the appropriate category:";

				Range _DateHeader = _Worksheet.Range["C13"];
				_DateHeader.Cells.Font.Size = 12;
				_DateHeader.Cells.Font.Underline = true;
				_DateHeader.Cells.Font.Bold = true;
				_DateHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_DateHeader.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_DateHeader.Value = "Date";

				Range _Vacation1 = _Worksheet.Range["E12"];
				_Vacation1.Cells.Font.Size = 12;
				_Vacation1.Cells.Font.Bold = true;
				_Vacation1.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Vacation1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Vacation1.Value = "Vacation";

				Range _Vacation2 = _Worksheet.Range["E13"];
				_Vacation2.Cells.Font.Size = 12;
				_Vacation2.Cells.Font.Underline = true;
				_Vacation2.Cells.Font.Bold = true;
				_Vacation2.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Vacation2.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Vacation2.Value = "Taken";

				Range _Personal1 = _Worksheet.Range["G12"];
				_Personal1.Cells.Font.Size = 12;
				_Personal1.Cells.Font.Bold = true;
				_Personal1.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Personal1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Personal1.Value = "Personal";

				Range _Personal2 = _Worksheet.Range["G13"];
				_Personal2.Cells.Font.Size = 12;
				_Personal2.Cells.Font.Underline = true;
				_Personal2.Cells.Font.Bold = true;
				_Personal2.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Personal2.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Personal2.Value = "Leave";

				Range _Emergency1 = _Worksheet.Range["I12"];
				_Emergency1.Cells.Font.Size = 12;
				_Emergency1.Cells.Font.Bold = true;
				_Emergency1.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Emergency1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Emergency1.Value = "Emergency";

				Range _Emergency2 = _Worksheet.Range["I13"];
				_Emergency2.Cells.Font.Size = 12;
				_Emergency2.Cells.Font.Underline = true;
				_Emergency2.Cells.Font.Bold = true;
				_Emergency2.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Emergency2.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Emergency2.Value = "Leave";

				Range _Jury1 = _Worksheet.Range["K12"];
				_Jury1.Cells.Font.Size = 12;
				_Jury1.Cells.Font.Bold = true;
				_Jury1.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Jury1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Jury1.Value = "Jury";

				Range _Jury2 = _Worksheet.Range["K13"];
				_Jury2.Cells.Font.Size = 12;
				_Jury2.Cells.Font.Underline = true;
				_Jury2.Cells.Font.Bold = true;
				_Jury2.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Jury2.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Jury2.Value = "Duty";

				Range _Leave1 = _Worksheet.Range["M12"];
				_Leave1.Cells.Font.Size = 12;
				_Leave1.Cells.Font.Bold = true;
				_Leave1.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Leave1.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Leave1.Value = "Leave of";

				Range _Leave2 = _Worksheet.Range["M13"];
				_Leave2.Cells.Font.Size = 12;
				_Leave2.Cells.Font.Underline = true;
				_Leave2.Cells.Font.Bold = true;
				_Leave2.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Leave2.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Leave2.Value = "Absence**";

				Range _PlaceHolder01 = _Worksheet.Range["O12"];
				_PlaceHolder01.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;

				Range _Total = _Worksheet.Range["O13"];
				_Total.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				_Total.Cells.Font.Size = 12;
				_Total.Cells.Font.Underline = true;
				_Total.Cells.Font.Bold = true;
				_Total.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Total.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_Total.Value = "Total";

				Range _MondayHeader = _Worksheet.Range["B14"];
				_MondayHeader.Cells.Font.Size = 12;
				_MondayHeader.Cells.Font.Bold = true;
				_MondayHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_MondayHeader.Value = "Monday";

				Range _MondayDate = _Worksheet.Range["C14"];
				_MondayDate.Cells.Font.Size = 12;
				_MondayDate.Cells.Font.Bold = true;
				_MondayDate.Cells.Font.Color = XlRgbColor.rgbGreen;
				_MondayDate.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayDate.Value = _Monday.ToString("MM-dd-yy");

				Range _MondayVacation = _Worksheet.Range["E14"];
				_MondayVacation.NumberFormat = "0.0";
				_MondayVacation.Cells.Font.Size = 12;
				_MondayVacation.Cells.Font.Bold = true;
				_MondayVacation.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayVacation.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_MondayVacation = _MondayVacation.Borders;
				_Border_MondayVacation[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayVacation[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_MondayVacation[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _MondayPersonal = _Worksheet.Range["G14"];
				_MondayPersonal.NumberFormat = "0.0";
				_MondayPersonal.Cells.Font.Bold = true;
				_MondayPersonal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayPersonal.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_MondayPersonal = _MondayPersonal.Borders;
				_Border_MondayPersonal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayPersonal[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_MondayPersonal[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _MondayEmergency = _Worksheet.Range["I14"];
				_MondayEmergency.NumberFormat = "0.0";
				_MondayEmergency.Cells.Font.Size = 12;
				_MondayEmergency.Cells.Font.Bold = true;
				_MondayEmergency.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayEmergency.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_MondayEmergency = _MondayEmergency.Borders;
				_Border_MondayEmergency[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayEmergency[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_MondayEmergency[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _MondayJury = _Worksheet.Range["K14"];
				_MondayJury.NumberFormat = "0.0";
				_MondayJury.Cells.Font.Size = 12;
				_MondayJury.Cells.Font.Bold = true;
				_MondayJury.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayJury.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_MondayJury = _MondayJury.Borders;
				_Border_MondayJury[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayJury[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_MondayJury[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _MondayAbsence = _Worksheet.Range["M14"];
				_MondayAbsence.NumberFormat = "0.0";
				_MondayAbsence.Cells.Font.Size = 12;
				_MondayAbsence.Cells.Font.Bold = true;
				_MondayAbsence.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_MondayAbsence.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_MondayAbsence = _MondayAbsence.Borders;
				_Border_MondayAbsence[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayAbsence[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_MondayAbsence[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (DataAccess.IsPaidHoliday(_Monday))
				{
					_MondayVacation.Value = "HOLIDAY";
					_MondayPersonal.Value = "";
					_MondayEmergency.Value = "";
					_MondayJury.Value = "";
					_MondayAbsence.Value = "";
				}
				else
				{
					_MondayVacation.Value = DataAccess.LeaveHours(_Monday, LeaveTypes.Vacation);
					_MondayPersonal.Value = DataAccess.LeaveHours(_Monday, LeaveTypes.Personal);
					_MondayEmergency.Value = DataAccess.LeaveHours(_Monday, LeaveTypes.Emergency);
					_MondayJury.Value = DataAccess.LeaveHours(_Monday, LeaveTypes.Jury);
					_MondayAbsence.Value = DataAccess.LeaveHours(_Monday, LeaveTypes.Absence);
				}

				Range _MondayTotal = _Worksheet.Range["O14"];
				_MondayTotal.NumberFormat = "0.0";
				_MondayTotal.Cells.Font.Size = 12;
				_MondayTotal.Cells.Font.Bold = true;
				_MondayTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				_MondayTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				Borders _Border_MondayTotal = _MondayTotal.Borders;
				_Border_MondayTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_MondayTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_MondayTotal.Formula = "=SUM(E14:M14)";
				_MondayTotal.Calculate();

				Range _TuesdayHeader = _Worksheet.Range["B15"];
				_TuesdayHeader.Cells.Font.Size = 12;
				_TuesdayHeader.Cells.Font.Bold = true;
				_TuesdayHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_TuesdayHeader.Value = "Tuesday";

				Range _TuesdayDate = _Worksheet.Range["C15"];
				_TuesdayDate.Cells.Font.Size = 12;
				_TuesdayDate.Cells.Font.Bold = true;
				_TuesdayDate.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayDate.Cells.Font.Color = XlRgbColor.rgbGreen;
				_TuesdayDate.Value = _Tuesday.ToString("MM-dd-yy");

				Range _TuesdayVacation = _Worksheet.Range["E15"];
				_TuesdayVacation.NumberFormat = "0.0";
				_TuesdayVacation.Cells.Font.Size = 12;
				_TuesdayVacation.Cells.Font.Bold = true;
				_TuesdayVacation.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayVacation.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_TuesdayVacation = _TuesdayVacation.Borders;
				_Border_TuesdayVacation[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayVacation[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_TuesdayVacation[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _TuesdayPersonal = _Worksheet.Range["G15"];
				_TuesdayPersonal.NumberFormat = "0.0";
				_TuesdayPersonal.Cells.Font.Size = 12;
				_TuesdayPersonal.Cells.Font.Bold = true;
				_TuesdayPersonal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayPersonal.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_TuesdayPersonal = _TuesdayPersonal.Borders;
				_Border_TuesdayPersonal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayPersonal[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_TuesdayPersonal[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _TuesdayEmergency = _Worksheet.Range["I15"];
				_TuesdayEmergency.NumberFormat = "0.0";
				_TuesdayEmergency.Cells.Font.Size = 12;
				_TuesdayEmergency.Cells.Font.Bold = true;
				_TuesdayEmergency.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayEmergency.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_TuesdayEmergency = _TuesdayEmergency.Borders;
				_Border_TuesdayEmergency[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayEmergency[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_TuesdayEmergency[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _TuesdayJury = _Worksheet.Range["K15"];
				_TuesdayJury.NumberFormat = "0.0";
				_TuesdayJury.Cells.Font.Size = 12;
				_TuesdayJury.Cells.Font.Bold = true;
				_TuesdayJury.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayJury.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_TuesdayJury = _TuesdayJury.Borders;
				_Border_TuesdayJury[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayJury[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_TuesdayJury[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _TuesdayAbsence = _Worksheet.Range["M15"];
				_TuesdayAbsence.NumberFormat = "0.0";
				_TuesdayAbsence.Cells.Font.Size = 12;
				_TuesdayAbsence.Cells.Font.Bold = true;
				_TuesdayAbsence.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayAbsence.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_TuesdayAbsence = _TuesdayAbsence.Borders;
				_Border_TuesdayAbsence[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayAbsence[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_TuesdayAbsence[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (DataAccess.IsPaidHoliday(_Tuesday))
				{
					_TuesdayVacation.Value = "HOLIDAY";
					_TuesdayPersonal.Value = "";
					_TuesdayEmergency.Value = "";
					_TuesdayJury.Value = "";
					_TuesdayAbsence.Value = "";
				}
				else
				{
					_TuesdayVacation.Value = DataAccess.LeaveHours(_Tuesday, LeaveTypes.Vacation);
					_TuesdayPersonal.Value = DataAccess.LeaveHours(_Tuesday, LeaveTypes.Personal);
					_TuesdayEmergency.Value = DataAccess.LeaveHours(_Tuesday, LeaveTypes.Emergency);
					_TuesdayJury.Value = DataAccess.LeaveHours(_Tuesday, LeaveTypes.Jury);
					_TuesdayAbsence.Value = DataAccess.LeaveHours(_Tuesday, LeaveTypes.Absence);
				}

				Range _TuesdayTotal = _Worksheet.Range["O15"];
				_TuesdayTotal.NumberFormat = "0.0";
				_TuesdayTotal.Cells.Font.Size = 12;
				_TuesdayTotal.Cells.Font.Bold = true;
				_TuesdayTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_TuesdayTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_TuesdayTotal = _TuesdayTotal.Borders;
				_Border_TuesdayTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_TuesdayTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_TuesdayTotal.Formula = "=SUM(E15:M15)";
				_TuesdayTotal.Calculate();

				Range _WednesdayHeader = _Worksheet.Range["B16"];
				_WednesdayHeader.Cells.Font.Size = 12;
				_WednesdayHeader.Cells.Font.Bold = true;
				_WednesdayHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_WednesdayHeader.Value = "Wednesday";

				Range _WednesdayDate = _Worksheet.Range["C16"];
				_WednesdayDate.Cells.Font.Size = 12;
				_WednesdayDate.Cells.Font.Bold = true;
				_WednesdayDate.Cells.Font.Color = XlRgbColor.rgbGreen;
				_WednesdayDate.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayDate.Value = _Wednesday.ToString("MM-dd-yy");

				Range _WednesdayVacation = _Worksheet.Range["E16"];
				_WednesdayVacation.NumberFormat = "0.0";
				_WednesdayVacation.Cells.Font.Size = 12;
				_WednesdayVacation.Cells.Font.Bold = true;
				_WednesdayVacation.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayVacation.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_WednesdayVacation = _WednesdayVacation.Borders;
				_Border_WednesdayVacation[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayVacation[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_WednesdayVacation[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _WednesdayPersonal = _Worksheet.Range["G16"];
				_WednesdayPersonal.NumberFormat = "0.0";
				_WednesdayPersonal.Cells.Font.Size = 12;
				_WednesdayPersonal.Cells.Font.Bold = true;
				_WednesdayPersonal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayPersonal.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_WednesdayPersonal = _WednesdayPersonal.Borders;
				_Border_WednesdayPersonal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayPersonal[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_WednesdayPersonal[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _WednesdayEmergency = _Worksheet.Range["I16"];
				_WednesdayEmergency.NumberFormat = "0.0";
				_WednesdayEmergency.Cells.Font.Size = 12;
				_WednesdayEmergency.Cells.Font.Bold = true;
				_WednesdayEmergency.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayEmergency.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_WednesdayEmergency = _WednesdayEmergency.Borders;
				_Border_WednesdayEmergency[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayEmergency[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_WednesdayEmergency[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _WednesdayJury = _Worksheet.Range["K16"];
				_WednesdayJury.NumberFormat = "0.0";
				_WednesdayJury.Cells.Font.Size = 12;
				_WednesdayJury.Cells.Font.Bold = true;
				_WednesdayJury.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayJury.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_WednesdayJury = _WednesdayJury.Borders;
				_Border_WednesdayJury[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayJury[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_WednesdayJury[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _WednesdayAbsence = _Worksheet.Range["M16"];
				_WednesdayAbsence.NumberFormat = "0.0";
				_WednesdayAbsence.Cells.Font.Size = 12;
				_WednesdayAbsence.Cells.Font.Bold = true;
				_WednesdayAbsence.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayAbsence.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_WednesdayAbsence = _WednesdayAbsence.Borders;
				_Border_WednesdayAbsence[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayAbsence[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_WednesdayAbsence[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (DataAccess.IsPaidHoliday(_Wednesday))
				{
					_WednesdayVacation.Value = "HOLIDAY";
					_WednesdayPersonal.Value = "";
					_WednesdayEmergency.Value = "";
					_WednesdayJury.Value = "";
					_WednesdayAbsence.Value = "";
				}
				else
				{
					_WednesdayVacation.Value = DataAccess.LeaveHours(_Wednesday, LeaveTypes.Vacation);
					_WednesdayPersonal.Value = DataAccess.LeaveHours(_Wednesday, LeaveTypes.Personal);
					_WednesdayEmergency.Value = DataAccess.LeaveHours(_Wednesday, LeaveTypes.Emergency);
					_WednesdayJury.Value = DataAccess.LeaveHours(_Wednesday, LeaveTypes.Jury);
					_WednesdayAbsence.Value = DataAccess.LeaveHours(_Wednesday, LeaveTypes.Absence);
				}

				Range _WednesdayTotal = _Worksheet.Range["O16"];
				_WednesdayTotal.NumberFormat = "0.0";
				_WednesdayTotal.Cells.Font.Size = 12;
				_WednesdayTotal.Cells.Font.Bold = true;
				_WednesdayTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_WednesdayTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_WednesdayTotal = _WednesdayTotal.Borders;
				_Border_WednesdayTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_WednesdayTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_WednesdayTotal.Formula = "=SUM(E16:M16)";
				_WednesdayTotal.Calculate();

				Range _ThursdayHeader = _Worksheet.Range["B17"];
				_ThursdayHeader.Cells.Font.Size = 12;
				_ThursdayHeader.Cells.Font.Bold = true;
				_ThursdayHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_ThursdayHeader.Value = "Thursday";

				Range _ThursdayDate = _Worksheet.Range["C17"];
				_ThursdayDate.Cells.Font.Size = 12;
				_ThursdayDate.Cells.Font.Bold = true;
				_ThursdayDate.Cells.Font.Color = XlRgbColor.rgbGreen;
				_ThursdayDate.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayDate.Value = _Thursday.ToString("MM-dd-yy");

				Range _ThursdayVacation = _Worksheet.Range["E17"];
				_ThursdayVacation.NumberFormat = "0.0";
				_ThursdayVacation.Cells.Font.Size = 12;
				_ThursdayVacation.Cells.Font.Bold = true;
				_ThursdayVacation.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayVacation.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_ThursdayVacation = _ThursdayVacation.Borders;
				_Border_ThursdayVacation[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayVacation[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_ThursdayVacation[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _ThursdayPersonal = _Worksheet.Range["G17"];
				_ThursdayPersonal.NumberFormat = "0.0";
				_ThursdayPersonal.Cells.Font.Size = 12;
				_ThursdayPersonal.Cells.Font.Bold = true;
				_ThursdayPersonal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayPersonal.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_ThursdayPersonal = _ThursdayPersonal.Borders;
				_Border_ThursdayPersonal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayPersonal[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_ThursdayPersonal[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _ThursdayEmergency = _Worksheet.Range["I17"];
				_ThursdayEmergency.NumberFormat = "0.0";
				_ThursdayEmergency.Cells.Font.Size = 12;
				_ThursdayEmergency.Cells.Font.Bold = true;
				_ThursdayEmergency.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayEmergency.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_ThursdayEmergency = _ThursdayEmergency.Borders;
				_Border_ThursdayEmergency[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayEmergency[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_ThursdayEmergency[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _ThursdayJury = _Worksheet.Range["K17"];
				_ThursdayJury.NumberFormat = "0.0";
				_ThursdayJury.Cells.Font.Size = 12;
				_ThursdayJury.Cells.Font.Bold = true;
				_ThursdayJury.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayJury.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_ThursdayJury = _ThursdayJury.Borders;
				_Border_ThursdayJury[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayJury[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_ThursdayJury[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _ThursdayAbsence = _Worksheet.Range["M17"];
				_ThursdayAbsence.NumberFormat = "0.0";
				_ThursdayAbsence.Cells.Font.Size = 12;
				_ThursdayAbsence.Cells.Font.Bold = true;
				_ThursdayAbsence.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayAbsence.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_ThursdayAbsence = _ThursdayAbsence.Borders;
				_Border_ThursdayAbsence[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayAbsence[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_ThursdayAbsence[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (DataAccess.IsPaidHoliday(_Thursday))
				{
					_ThursdayVacation.Value = "HOLIDAY";
					_ThursdayPersonal.Value = "";
					_ThursdayEmergency.Value = "";
					_ThursdayJury.Value = "";
					_ThursdayAbsence.Value = "";
				}
				else
				{
					_ThursdayVacation.Value = DataAccess.LeaveHours(_Thursday, LeaveTypes.Vacation);
					_ThursdayPersonal.Value = DataAccess.LeaveHours(_Thursday, LeaveTypes.Personal);
					_ThursdayEmergency.Value = DataAccess.LeaveHours(_Thursday, LeaveTypes.Emergency);
					_ThursdayJury.Value = DataAccess.LeaveHours(_Thursday, LeaveTypes.Jury);
					_ThursdayAbsence.Value = DataAccess.LeaveHours(_Thursday, LeaveTypes.Absence);
				}

				Range _ThursdayTotal = _Worksheet.Range["O17"];
				_ThursdayTotal.NumberFormat = "0.0";
				_ThursdayTotal.Cells.Font.Size = 12;
				_ThursdayTotal.Cells.Font.Bold = true;
				_ThursdayTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_ThursdayTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_ThursdayTotal = _ThursdayTotal.Borders;
				_Border_ThursdayTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_ThursdayTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_ThursdayTotal.Formula = "=SUM(E17:M17)";
				_ThursdayTotal.Calculate();

				Range _FridayHeader = _Worksheet.Range["B18"];
				_FridayHeader.Cells.Font.Size = 12;
				_FridayHeader.Cells.Font.Bold = true;
				_FridayHeader.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_FridayHeader.Value = "Friday";

				Range _FridayDate = _Worksheet.Range["C18"];
				_FridayDate.Cells.Font.Size = 12;
				_FridayDate.Cells.Font.Bold = true;
				_FridayDate.Cells.Font.Color = XlRgbColor.rgbGreen;
				_FridayDate.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayDate.Value = _DateGroupedBilledTime_Friday.Date.Value.ToString("MM-dd-yy");
				_FridayDate.Value = _Friday.ToString("MM-dd-yy");

				Range _FridayVacation = _Worksheet.Range["E18"];
				_FridayVacation.NumberFormat = "0.0";
				_FridayVacation.Cells.Font.Size = 12;
				_FridayVacation.Cells.Font.Bold = true;
				_FridayVacation.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayVacation.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_FridayVacation = _FridayVacation.Borders;
				_Border_FridayVacation[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayVacation[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_FridayVacation[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _FridayPersonal = _Worksheet.Range["G18"];
				_FridayPersonal.NumberFormat = "0.0";
				_FridayPersonal.Cells.Font.Size = 12;
				_FridayPersonal.Cells.Font.Bold = true;
				_FridayPersonal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayPersonal.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_FridayPersonal = _FridayPersonal.Borders;
				_Border_FridayPersonal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayPersonal[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_FridayPersonal[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _FridayEmergency = _Worksheet.Range["I18"];
				_FridayEmergency.NumberFormat = "0.0";
				_FridayEmergency.Cells.Font.Size = 12;
				_FridayEmergency.Cells.Font.Bold = true;
				_FridayEmergency.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayEmergency.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_FridayEmergency = _FridayEmergency.Borders;
				_Border_FridayEmergency[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayEmergency[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_FridayEmergency[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _FridayJury = _Worksheet.Range["K18"];
				_FridayJury.NumberFormat = "0.0";
				_FridayJury.Cells.Font.Size = 12;
				_FridayJury.Cells.Font.Bold = true;
				_FridayJury.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayJury.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_FridayJury = _FridayJury.Borders;
				_Border_FridayJury[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayJury[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_FridayJury[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _FridayAbsence = _Worksheet.Range["M18"];
				_FridayAbsence.NumberFormat = "0.0";
				_FridayAbsence.Cells.Font.Size = 12;
				_FridayAbsence.Cells.Font.Bold = true;
				_FridayAbsence.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayAbsence.Cells.Font.Color = XlRgbColor.rgbRed;
				Borders _Border_FridayAbsence = _FridayAbsence.Borders;
				_Border_FridayAbsence[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayAbsence[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbLightGray;
				_Border_FridayAbsence[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (DataAccess.IsPaidHoliday(_Friday))
				{
					_FridayVacation.Value = "HOLIDAY";
					_FridayPersonal.Value = "";
					_FridayEmergency.Value = "";
					_FridayJury.Value = "";
					_FridayAbsence.Value = "";
				}
				else
				{
					_FridayVacation.Value = DataAccess.LeaveHours(_Friday, LeaveTypes.Vacation);
					_FridayPersonal.Value = DataAccess.LeaveHours(_Friday, LeaveTypes.Personal);
					_FridayEmergency.Value = DataAccess.LeaveHours(_Friday, LeaveTypes.Emergency);
					_FridayJury.Value = DataAccess.LeaveHours(_Friday, LeaveTypes.Jury);
					_FridayAbsence.Value = DataAccess.LeaveHours(_Friday, LeaveTypes.Absence);
				}

				Range _FridayTotal = _Worksheet.Range["O18"];
				_FridayTotal.NumberFormat = "0.0";
				_FridayTotal.Cells.Font.Size = 12;
				_FridayTotal.Cells.Font.Bold = true;
				_FridayTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_FridayTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_FridayTotal = _FridayTotal.Borders;
				_Border_FridayTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_FridayTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_FridayTotal.Formula = "=SUM(E18:M18)";
				_FridayTotal.Calculate();

				Range _PayPeriodTotal = _Worksheet.Range["B20", "C20"];
				_PayPeriodTotal.Merge();
				_PayPeriodTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				_PayPeriodTotal.Cells.Font.Size = 12;
				_PayPeriodTotal.Cells.Font.Bold = true;
				_PayPeriodTotal.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_PayPeriodTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_PayPeriodTotal.Value = "Pay Period Totals:";

				Range _PlaceHolder02 = _Worksheet.Range["D20"];
				_PlaceHolder02.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;

				Range _VacationTotal = _Worksheet.Range["E20"];
				_VacationTotal.NumberFormat = "0.0";
				_VacationTotal.Cells.Font.Size = 12;
				_VacationTotal.Cells.Font.Bold = true;
				_VacationTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_VacationTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_VacationTotal = _VacationTotal.Borders;
				_Border_VacationTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_VacationTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_VacationTotal.Formula = "=SUM(E14:E18)";
				_VacationTotal.Calculate();

				Range _PlaceHolder03 = _Worksheet.Range["F20"];
				_PlaceHolder03.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder03 = _PlaceHolder03.Borders;
				_Border_PlaceHolder03[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PlaceHolder03[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _PersonalTotal = _Worksheet.Range["G20"];
				_PersonalTotal.NumberFormat = "0.0";
				_PersonalTotal.Cells.Font.Size = 12;
				_PersonalTotal.Cells.Font.Bold = true;
				_PersonalTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_PersonalTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PersonalTotal = _PersonalTotal.Borders;
				_Border_PersonalTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PersonalTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_PersonalTotal.Formula = "=SUM(G14:G18)";
				_PersonalTotal.Calculate();

				Range _PlaceHolder04 = _Worksheet.Range["H20"];
				_PlaceHolder04.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder04 = _PlaceHolder04.Borders;
				_Border_PlaceHolder04[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PlaceHolder04[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _EmergencyTotal = _Worksheet.Range["I20"];
				_EmergencyTotal.NumberFormat = "0.0";
				_EmergencyTotal.Cells.Font.Size = 12;
				_EmergencyTotal.Cells.Font.Bold = true;
				_EmergencyTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_EmergencyTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_EmergencyTotal = _EmergencyTotal.Borders;
				_Border_EmergencyTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_EmergencyTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_EmergencyTotal.Formula = "=SUM(I14:I18)";
				_EmergencyTotal.Calculate();

				Range _PlaceHolder05 = _Worksheet.Range["J20"];
				_PlaceHolder05.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder05 = _PlaceHolder05.Borders;
				_Border_PlaceHolder05[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PlaceHolder05[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _JuryTotal = _Worksheet.Range["K20"];
				_JuryTotal.NumberFormat = "0.0";
				_JuryTotal.Cells.Font.Size = 12;
				_JuryTotal.Cells.Font.Bold = true;
				_JuryTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_JuryTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_JuryTotal = _JuryTotal.Borders;
				_Border_JuryTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_JuryTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_JuryTotal.Formula = "=SUM(K14:K18)";
				_JuryTotal.Calculate();

				Range _PlaceHolder06 = _Worksheet.Range["L20"];
				_PlaceHolder06.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder06 = _PlaceHolder06.Borders;
				_Border_PlaceHolder06[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PlaceHolder06[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AbsenceTotal = _Worksheet.Range["M20"];
				_AbsenceTotal.NumberFormat = "0.0";
				_AbsenceTotal.Cells.Font.Size = 12;
				_AbsenceTotal.Cells.Font.Bold = true;
				_AbsenceTotal.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				_AbsenceTotal.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_AbsenceTotal = _AbsenceTotal.Borders;
				_Border_AbsenceTotal[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AbsenceTotal[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_AbsenceTotal.Formula = "=SUM(M14:M18)";
				_AbsenceTotal.Calculate();

				Range _PlaceHolder07 = _Worksheet.Range["N20"];
				_PlaceHolder07.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder07 = _PlaceHolder07.Borders;
				_Border_PlaceHolder07[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_PlaceHolder07[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _PlaceHolder08 = _Worksheet.Range["B21", "N21"];
				_PlaceHolder08.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;
				Borders _Border_PlaceHolder08 = _PlaceHolder08.Borders;
				_Border_PlaceHolder08[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDouble;
				_Border_PlaceHolder08[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbRed;

				Range _PlaceHolder09 = _Worksheet.Range["O21"];
				Borders _Border_PlaceHolder09 = _PlaceHolder09.Borders;
				_Border_PlaceHolder09[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDouble;
				_Border_PlaceHolder09[XlBordersIndex.xlEdgeBottom].Color = XlRgbColor.rgbRed;

				Range _Instructions02 = _Worksheet.Range["E22"];
				_Instructions02.Cells.Font.Size = 10;
				_Instructions02.Cells.Font.Bold = true;
				_Instructions02.Cells.Font.Italic = true;
				_Instructions02.Cells.Font.Color = XlRgbColor.rgbDarkOrchid;
				_Instructions02.Value = "Please enter any time spent on Projects, Work Orders, or in another Department:";

				Range _DepartmentHeader01 = _Worksheet.Range["B24"];
				_DepartmentHeader01.Cells.Font.Size = 12;
				_DepartmentHeader01.Cells.Font.Bold = true;
				_DepartmentHeader01.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_DepartmentHeader01.Value = "Dept or Project #/Hours:";

				Range _DepartmentHeader02 = _Worksheet.Range["B26"];
				_DepartmentHeader02.Cells.Font.Size = 12;
				_DepartmentHeader02.Cells.Font.Bold = true;
				_DepartmentHeader02.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_DepartmentHeader02.Value = "Dept or Project #/Hours:";

				Range _Project01 = _Worksheet.Range["E24"];
				_Project01.Cells.Font.Size = 10;
				_Project01.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project01 = _Project01.Borders;
				_Border_Project01[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project01[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project02 = _Worksheet.Range["G24"];
				_Project02.Cells.Font.Size = 10;
				_Project02.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project02 = _Project02.Borders;
				_Border_Project02[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project02[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project03 = _Worksheet.Range["I24"];
				_Project03.Cells.Font.Size = 10;
				_Project03.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project03 = _Project03.Borders;
				_Border_Project03[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project03[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project04 = _Worksheet.Range["K24"];
				_Project04.Cells.Font.Size = 10;
				_Project04.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project04 = _Project04.Borders;
				_Border_Project04[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project04[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project05 = _Worksheet.Range["M24"];
				_Project05.Cells.Font.Size = 10;
				_Project05.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project05 = _Project05.Borders;
				_Border_Project05[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project05[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project06 = _Worksheet.Range["O24"];
				_Project06.Cells.Font.Size = 10;
				_Project06.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project06 = _Project06.Borders;
				_Border_Project06[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project06[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_Border_Project06[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project06[XlBordersIndex.xlEdgeRight].Weight = 4d;

				Range _Project11 = _Worksheet.Range["E26"];
				_Project11.Cells.Font.Size = 10;
				_Project11.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project11 = _Project11.Borders;
				_Border_Project11[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project11[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project12 = _Worksheet.Range["G26"];
				_Project12.Cells.Font.Size = 10;
				_Project12.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project12 = _Project12.Borders;
				_Border_Project12[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project12[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project13 = _Worksheet.Range["I26"];
				_Project13.Cells.Font.Size = 10;
				_Project13.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project13 = _Project13.Borders;
				_Border_Project13[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project13[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project14 = _Worksheet.Range["K26"];
				_Project14.Cells.Font.Size = 10;
				_Project14.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project14 = _Project14.Borders;
				_Border_Project14[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project14[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project15 = _Worksheet.Range["M26"];
				_Project15.Cells.Font.Size = 10;
				_Project15.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project15 = _Project15.Borders;
				_Border_Project15[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project15[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _Project16 = _Worksheet.Range["O26"];
				_Project16.Cells.Font.Size = 10;
				_Project16.Cells.Font.Color = XlRgbColor.rgbIndianRed;
				Borders _Border_Project16 = _Project16.Borders;
				_Border_Project16[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_Project11[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				if (_ProjectGroupedBilledTimes != null && _ProjectGroupedBilledTimes.Count > 0)
				{
					_ProjectGroupedBilledTimes = _ProjectGroupedBilledTimes.OrderBy(query => query.CombinedProjectCodeDescription).ToList();
					Int32 _Index = 0;

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project01.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project02.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project03.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project04.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project05.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project06.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project11.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project12.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project13.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project14.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project15.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}

					if (_ProjectGroupedBilledTimes.Count >= _Index + 1 && _ProjectGroupedBilledTimes[_Index].BilledTimes != null && _ProjectGroupedBilledTimes[_Index].BilledTimes.Count > 0)
					{
						_Project16.Value = _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectCode + "-" + _ProjectGroupedBilledTimes[_Index].BilledTimes.FirstOrDefault().ProjectSubCode + " / " + _ProjectGroupedBilledTimes[_Index].TotalHours.ToString("F2");
						_Index++;
					}
				}

				Range _Instructions03 = _Worksheet.Range["B28", "I28"];
				_Instructions03.Merge();
				_Instructions03.Cells.Font.Size = 8;
				_Instructions03.Cells.Font.Bold = true;
				_Instructions03.Cells.Font.Name = "Albertus Medium";
				_Instructions03.Cells.Font.Color = XlRgbColor.rgbGreen;
				Borders _Border_Instructions03 = _Instructions03.Borders;
				_Border_Instructions03.LineStyle = XlLineStyle.xlContinuous;
				_Border_Instructions03.Weight = 4d;
				_Instructions03.Value = "PLEASE SUBMIT THIS FORM BY E-MAIL TO PAT HANSEN NO LATER THAN 5:00 P.M. ON FRIDAY.";
				_Instructions03.Cells.Characters[62, 20].Font.Color = XlRgbColor.rgbDarkOrchid;

				Range _Instructions04 = _Worksheet.Range["B31"];
				_Instructions04.Cells.Font.Size = 8;
				_Instructions04.Cells.Font.Bold = true;
				_Instructions04.Cells.Font.Color = XlRgbColor.rgbRed;
				_Instructions04.Value = "*IMMEDIATELY REPORT ALL INJURIES TO YOUR MGR; FILE AN INJURY REPORT WITH SAFETY DIRECTOR THE SAME DAY AS THE INJURY.";

				Range _Instructions05 = _Worksheet.Range["B34"];
				_Instructions05.Cells.Font.Size = 8;
				_Instructions05.Cells.Font.Bold = true;
				_Instructions05.Cells.Font.Color = XlRgbColor.rgbMidnightBlue;
				_Instructions05.Value = "**Unpaid Leave of Absence Hours must be pre-approved by Human Resources Manager";

				Range _AdminHeader = _Worksheet.Range["M29", "O29"];
				_AdminHeader.Merge();
				_AdminHeader.Cells.Font.Size = 10;
				_AdminHeader.Cells.Font.Bold = true;
				_AdminHeader.Cells.Font.Color = XlRgbColor.rgbGreen;
				_AdminHeader.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
				Borders _Border_AdminHeader = _AdminHeader.Borders;
				_Border_AdminHeader.LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminHeader.Weight = 4d;
				_AdminHeader.Value = "PAYROLL CODING USE ONLY";

				Range _AdminADM = _Worksheet.Range["M30"];
				_AdminADM.Cells.Font.Size = 10;
				Borders _Border_AdminADM = _AdminADM.Borders;
				_Border_AdminADM[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminADM[XlBordersIndex.xlEdgeLeft].Weight = 4d;
				_Border_AdminADM[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminADM[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_AdminADM.Value = "ADM=";

				Range _AdminADM01 = _Worksheet.Range["N30"];
				Borders _Border_AdminADM01 = _AdminADM01.Borders;
				_Border_AdminADM01[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminADM01[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminADM02 = _Worksheet.Range["O30"];
				Borders _Border_AdminADM02 = _AdminADM02.Borders;
				_Border_AdminADM02[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminADM02[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminREG = _Worksheet.Range["M31"];
				_AdminREG.Cells.Font.Size = 10;
				Borders _Border_AdminREG = _AdminREG.Borders;
				_Border_AdminREG[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminREG[XlBordersIndex.xlEdgeLeft].Weight = 4d;
				_Border_AdminREG[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminREG[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_AdminREG.Value = "REG=";

				Range _AdminREG01 = _Worksheet.Range["N31"];
				Borders _Border_AdminREG01 = _AdminREG01.Borders;
				_Border_AdminREG01[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminREG01[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminREG02 = _Worksheet.Range["O31"];
				Borders _Border_AdminREG02 = _AdminREG02.Borders;
				_Border_AdminREG02[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminREG02[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminV05 = _Worksheet.Range["M32"];
				_AdminV05.Cells.Font.Size = 10;
				Borders _Border_AdminV05 = _AdminV05.Borders;
				_Border_AdminV05[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminV05[XlBordersIndex.xlEdgeLeft].Weight = 4d;
				_Border_AdminV05[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminV05[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_AdminV05.Value = "V05=";

				Range _AdminV0501 = _Worksheet.Range["N32"];
				Borders _Border_AdminV0501 = _AdminV0501.Borders;
				_Border_AdminV0501[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminV0501[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminV0502 = _Worksheet.Range["O32"];
				Borders _Border_AdminV0502 = _AdminV0502.Borders;
				_Border_AdminV0502[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminV0502[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminP07 = _Worksheet.Range["M33"];
				_AdminP07.Cells.Font.Size = 10;
				Borders _Border_AdminP07 = _AdminP07.Borders;
				_Border_AdminP07[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminP07[XlBordersIndex.xlEdgeLeft].Weight = 4d;
				_Border_AdminP07[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminP07[XlBordersIndex.xlEdgeBottom].Weight = 2d;
				_AdminP07.Value = "P07=";

				Range _AdminP0701 = _Worksheet.Range["N33"];
				Borders _Border_AdminP0701 = _AdminP0701.Borders;
				_Border_AdminP0701[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminP0701[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminP0702 = _Worksheet.Range["O33"];
				Borders _Border_AdminP0702 = _AdminP0702.Borders;
				_Border_AdminP0702[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminP0702[XlBordersIndex.xlEdgeBottom].Weight = 2d;

				Range _AdminH = _Worksheet.Range["M34"];
				_AdminH.Cells.Font.Size = 10;
				Borders _Border_AdminH = _AdminH.Borders;
				_Border_AdminH[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				_Border_AdminH[XlBordersIndex.xlEdgeLeft].Weight = 4d;
				_AdminH.Value = "H=";

				//_Workbook.SaveAs(ConfigurationManager.AppSettings["TimeSheetLocation"] + "ExemptTimeSheet_" + ConfigurationManager.AppSettings["EmployeeName"].Replace(" ", "") + "_" + _Friday.ToString("yyyy MM dd").Replace(" ", "") + @".xlsx");
				//_Workbook.Close();
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}

	/// <summary>
	/// Data Access
	/// </summary>
	public static class DataAccess
	{
		/// <summary>
		/// Billed Time Collection For Date
		/// </summary>
		/// <param name="_Date">Date to get Billed Times for</param>
		/// <returns></returns>
		public static List<BilledTimeEntity> BilledTimes(DateTime _Date)
		{
			try
			{
				List<BilledTimeEntity> _BilledTimes = null;
				DateTime _StartDate = _Date.Date;
				DateTime _EndDate = _StartDate.AddDays(1).AddTicks(-1);

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_BilledTimes = (from BT in Entity.BilledTimes
								 where BT.BilledDate >= _StartDate && BT.BilledDate <= _EndDate
								 orderby BT.BilledDate
								 select new BilledTimeEntity
								 {
									 BilledTimeID = BT.BilledTimeID,
									 BilledDate = BT.BilledDate,
									 BilledHours = BT.BilledHours,
									 ProjectCodeID = BT.ProjectCodeID,
									 ProjectCode = BT.ProjectCode.ProjectCodeValue,
									 ProjectCodeDescription = BT.ProjectCode.ProjectCodeDescription,
									 ProjectSubCodeID = BT.ProjectSubCodeID,
									 ProjectSubCode = BT.ProjectSubCode.ProjectSubCodeValue,
									 ProjectSubCodeDescription = BT.ProjectSubCode.ProjectSubCodeDescription,
									 CombinedProjectCodeDescription = BT.ProjectCode.ProjectCodeValue + "-" +
										BT.ProjectSubCode.ProjectSubCodeValue + ": " +
										BT.ProjectCode.ProjectCodeDescription + "-" +
										BT.ProjectSubCode.ProjectSubCodeDescription,
									 Notes = BT.Notes,
								 }).ToList();
				}

				return _BilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Collection For Date Range
		/// </summary>
		/// <param name="_StartDate">Start Date to get Billed Times for</param>
		/// <param name="_EndDate">End Date to get Billed Times Forparam>
		/// <returns></returns>
		public static List<BilledTimeEntity> BilledTimes(DateTime _StartDate, DateTime _EndDate)
		{
			try
			{
				List<BilledTimeEntity> _BilledTimes = null;
				_StartDate = _StartDate.Date;
				_EndDate = _EndDate.Date;

				if (_StartDate > _EndDate)
				{
					DateTime _TempDateTime = _StartDate;
					_StartDate = _EndDate;
					_EndDate = _TempDateTime;
				}

				if (_StartDate == _EndDate)
				{
					_EndDate = _StartDate.AddDays(1).AddTicks(-1);
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_BilledTimes = (from BT in Entity.BilledTimes
								 where BT.BilledDate >= _StartDate && BT.BilledDate <= _EndDate
								 orderby BT.BilledDate
								 select new BilledTimeEntity
								 {
									 BilledTimeID = BT.BilledTimeID,
									 BilledDate = BT.BilledDate,
									 BilledHours = BT.BilledHours,
									 ProjectCodeID = BT.ProjectCodeID,
									 ProjectCode = BT.ProjectCode.ProjectCodeValue,
									 ProjectCodeDescription = BT.ProjectCode.ProjectCodeDescription,
									 ProjectSubCodeID = BT.ProjectSubCodeID,
									 ProjectSubCode = BT.ProjectSubCode.ProjectSubCodeValue,
									 ProjectSubCodeDescription = BT.ProjectSubCode.ProjectSubCodeDescription,
									 CombinedProjectCodeDescription = BT.ProjectCode.ProjectCodeValue + "-" +
										BT.ProjectSubCode.ProjectSubCodeValue + ": " +
										BT.ProjectCode.ProjectCodeDescription + "-" +
										BT.ProjectSubCode.ProjectSubCodeDescription
								 }).ToList();
				}

				return _BilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Group By Date
		/// </summary>
		/// <param name="_Date">Date to get grouped Billed Times for</param>
		/// <returns></returns>
		public static DateGroupedBilledTime DateGroupedBilledTime(DateTime _Date)
		{
			try
			{
				DateGroupedBilledTime _DateGroupedBilledTime = null;
				List<BilledTimeEntity> _BilledTimes = BilledTimes(_Date);

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_DateGroupedBilledTime = (from BT in _BilledTimes
										 group BT by BT.BilledDate into BTG
										 select new DateGroupedBilledTime
										 {
											 BilledTimes = BTG.ToList(),
										 }).FirstOrDefault();
				}

				return _DateGroupedBilledTime;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Collection For Date Range Group By Date
		/// </summary>
		/// <param name="_StartDate">Start Date to get grouped Billed Times for</param>
		/// <param name="_EndDate">End Date to get grouped Billed Times for</param>
		/// <returns></returns>
		public static List<DateGroupedBilledTime> DateGroupedBilledTimes(DateTime _StartDate, DateTime _EndDate)
		{
			try
			{
				List<DateGroupedBilledTime> _DateGroupedBilledTimes = null;
				List<BilledTimeEntity> _BilledTimes = BilledTimes(_StartDate, _EndDate);

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_DateGroupedBilledTimes = (from BT in _BilledTimes
										  group BT by BT.BilledDate into BTG
										  select new DateGroupedBilledTime
										  {
											  BilledTimes = BTG.ToList(),
										  }).ToList();
				}

				return _DateGroupedBilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Project Code Collection
		/// </summary>
		/// <returns></returns>
		public static List<ProjectCodeEntity> ProjectCodes()
		{
			try
			{
				List<ProjectCodeEntity> _ProjectCodes = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_ProjectCodes = (from PC in Entity.ProjectCodes
								  orderby PC.ProjectCodeValue
								  select new ProjectCodeEntity
								  {
									  ProjectCodeID = PC.ProjectCodeID,
									  ProjectCodeValue = PC.ProjectCodeValue,
									  ProjectCodeDescription = PC.ProjectCodeDescription,
									  ProjectSubCodes = PC.SubCodes.Select(query => new ProjectSubCodeEntity()
									  {
										  ProjectSubCodeID = query.ProjectSubCodeID,
										  ProjectSubCodeValue = query.ProjectSubCodeValue,
										  ProjectSubCodeDescription = query.ProjectSubCodeDescription,
									  }).OrderBy(query => query.ProjectSubCodeValue).ToList(),
								  }).ToList();
				}

				return _ProjectCodes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Delete Billed Time
		/// </summary>
		/// <param name="_BilledTimeEntity">Billed Time Entity to delete</param>
		/// <returns></returns>
		public static Boolean BilledTimeEntity_Delete(BilledTimeEntity _BilledTimeEntity)
		{
			try
			{
				using (PersonalEntities Entity = new PersonalEntities())
				{
					var _BilledTimeDelete = (from BT in Entity.BilledTimes
										where BT.BilledTimeID == _BilledTimeEntity.BilledTimeID
										select BT).FirstOrDefault();

					if (_BilledTimeDelete != null)
					{
						Entity.BilledTimes.Remove(_BilledTimeDelete);
						Entity.SaveChanges();
					}
				}

				return true;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Update Billed Time
		/// </summary>
		/// <param name="_BilledTimeEntity">Billed Time Entity to be updated</param>
		/// <returns></returns>
		public static BilledTimeEntity BilledTimeEntity_Update(BilledTimeEntity _BilledTimeEntity)
		{
			try
			{
				return BilledTimeEntity_Update(_BilledTimeEntity.BilledTimeID, _BilledTimeEntity.BilledDate, _BilledTimeEntity.BilledHours, _BilledTimeEntity.Notes, _BilledTimeEntity.ProjectCodeID, _BilledTimeEntity.ProjectSubCodeID);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Update Billed Time
		/// </summary>
		/// <param name="_BilledTimeID">Billed Time ID</param>
		/// <param name="_BilledDate">Billed Time Date</param>
		/// <param name="_BilledHours">Billed Time Hours</param>
		/// <param name="_Notes">Billed Time Notes</param>
		/// <param name="_ProjectCodeID">Project Code ID</param>
		/// <param name="_ProjectSubCodeID">Project Sub Code ID</param>
		/// <returns></returns>
		public static BilledTimeEntity BilledTimeEntity_Update(Int32 _BilledTimeID, DateTime _BilledDate, Double _BilledHours, String _Notes, Int32 _ProjectCodeID, Int32 _ProjectSubCodeID)
		{
			try
			{
				PersonalEntity.BilledTime _BilledTimeUpdate = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_BilledTimeUpdate = (from BT in Entity.BilledTimes
									 where BT.BilledTimeID == _BilledTimeID
									 select BT).FirstOrDefault();

					if (_BilledTimeUpdate != null)
					{
						_BilledTimeUpdate.BilledDate = _BilledDate;
						_BilledTimeUpdate.BilledHours = _BilledHours;
						_BilledTimeUpdate.Notes = _Notes;
						_BilledTimeUpdate.ProjectCodeID = _ProjectCodeID;
						_BilledTimeUpdate.ProjectSubCodeID = _ProjectSubCodeID;
						Entity.SaveChanges();
					}
				}

				return new BilledTimeEntity(_BilledTimeUpdate);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Add Billed Time
		/// </summary>
		/// <param name="_BilledTimeEntity">Billed Time Entity to add</param>
		/// <returns></returns>
		public static BilledTimeEntity BilledTimeEntity_Add(BilledTimeEntity _BilledTimeEntity)
		{
			try
			{
				return BilledTimeEntity_Add(_BilledTimeEntity.BilledDate, _BilledTimeEntity.BilledHours, _BilledTimeEntity.Notes, _BilledTimeEntity.ProjectCodeID, _BilledTimeEntity.ProjectSubCodeID);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Add Billed Time
		/// </summary>
		/// <param name="_BilledDate">Billed Time Date</param>
		/// <param name="_BilledHours">Billed Time Hours</param>
		/// <param name="_Notes">Billed Time Notes</param>
		/// <param name="_ProjectCodeID">Proejct Code ID</param>
		/// <param name="_ProjectSubCodeID">Project Sub Code ID</param>
		/// <returns></returns>
		public static BilledTimeEntity BilledTimeEntity_Add(DateTime _BilledDate, Double _BilledHours, String _Notes, Int32 _ProjectCodeID, Int32 _ProjectSubCodeID)
		{
			try
			{
				PersonalEntity.BilledTime _BilledTimeUpdate = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_BilledTimeUpdate = new PersonalEntity.BilledTime();
					_BilledTimeUpdate.BilledDate = _BilledDate;
					_BilledTimeUpdate.BilledHours = _BilledHours;
					_BilledTimeUpdate.Notes = _Notes;
					_BilledTimeUpdate.ProjectCodeID = _ProjectCodeID;
					_BilledTimeUpdate.ProjectSubCodeID = _ProjectSubCodeID;
					_BilledTimeUpdate = Entity.BilledTimes.Add(_BilledTimeUpdate);
					Entity.SaveChanges();
				}

				return new BilledTimeEntity(_BilledTimeUpdate);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Week
		/// </summary>
		/// <param name="_DateGroupedBilledTimes">List of Billed Times to be grouped into date across a week</param>
		/// <returns></returns>
		public static WeekGroupBilledTime WeekGroupBilledTimes(List<DateGroupedBilledTime> _DateGroupedBilledTimes)
		{
			try
			{
				WeekGroupBilledTime _WeekGroupBilledTime = null;
				List<BilledTimeEntity> _BilledTimeEntities = null;

				if (_DateGroupedBilledTimes != null && _DateGroupedBilledTimes.Count > 0)
				{
					_BilledTimeEntities = new List<BilledTimeEntity>();

					foreach (DateGroupedBilledTime fe_Date in _DateGroupedBilledTimes)
					{
						if (fe_Date.BilledTimes != null && fe_Date.BilledTimes.Count > 0)
						{
							_BilledTimeEntities.AddRange(fe_Date.BilledTimes);
						}
					}

					if (_BilledTimeEntities != null && _BilledTimeEntities.Count > 0)
					{
						_WeekGroupBilledTime = new WeekGroupBilledTime(_BilledTimeEntities);
					}
				}

				return _WeekGroupBilledTime;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Determines If Date Is Paid Holiday
		/// </summary>
		/// <param name="_DateTime">Date To Check</param>
		/// <returns></returns>
		public static Boolean IsPaidHoliday(DateTime _DateTime)
		{
			try
			{
				using (PersonalEntities Entity = new PersonalEntities())
				{
					var _Vacation = (from V in Entity.Holidays
								  where V.HolidayDate == _DateTime.Date
								  select V).FirstOrDefault();

					return _Vacation != null;
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leaves For Date
		/// </summary>
		/// <param name="_Date">Date to get Leaves for</param>
		/// <returns></returns>
		public static List<LeaveEntity> Leave(DateTime _Date)
		{
			try
			{
				List<LeaveEntity> _Leaves = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Leaves = (from L in Entity.Leaves
							 where L.LeaveDate == _Date.Date
							 select new LeaveEntity
							 {
								 LeaveDate = L.LeaveDate,
								 LeaveHours = L.LeaveHours,
								 LeaveID = L.LeaveID,
								 LeaveTypeID = L.LeaveTypeID,
								 LeaveTypeDescription = L.LeaveType.LeaveTypeDescription,
							 }).ToList();
				}

				return _Leaves;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leaves For Date And Leave Type
		/// </summary>
		/// <param name="_Date">Date to get Leaves for</param>
		/// <param name="_LeaveType">Leave Type to get Leaves for</param>
		/// <returns></returns>
		public static List<LeaveEntity> Leave(DateTime _Date, LeaveTypes _LeaveType)
		{
			try
			{
				List<LeaveEntity> _Leaves = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Leaves = (from L in Entity.Leaves
							 where L.LeaveDate == _Date.Date && L.LeaveType.LeaveTypeDescription == _LeaveType.ToString()
							 select new LeaveEntity
							 {
								 LeaveDate = L.LeaveDate,
								 LeaveHours = L.LeaveHours,
								 LeaveID = L.LeaveID,
								 LeaveTypeID = L.LeaveTypeID,
								 LeaveTypeDescription = L.LeaveType.LeaveTypeDescription,
							 }).ToList();
				}

				return _Leaves;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leaves For Date Range
		/// </summary>
		/// <param name="_DateStart">Start Date to get Leaves for</param>
		/// <param name="_DateEnd">End Date to get Leaves for</param>
		/// <returns></returns>
		public static List<LeaveEntity> Leave(DateTime _DateStart, DateTime _DateEnd)
		{
			try
			{
				List<LeaveEntity> _Leaves = null;
				_DateStart = _DateStart.Date;
				_DateEnd = _DateEnd.Date;

				if (_DateStart > _DateEnd)
				{
					DateTime _TempDateTime = _DateStart;
					_DateStart = _DateEnd;
					_DateEnd = _TempDateTime;
				}

				if (_DateStart == _DateEnd)
				{
					_DateEnd = _DateStart.AddDays(1).AddTicks(-1);
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Leaves = (from L in Entity.Leaves
							 where L.LeaveDate >= _DateStart && L.LeaveDate <= _DateEnd
							 select new LeaveEntity
							 {
								 LeaveDate = L.LeaveDate,
								 LeaveHours = L.LeaveHours,
								 LeaveID = L.LeaveID,
								 LeaveTypeID = L.LeaveTypeID,
								 LeaveTypeDescription = L.LeaveType.LeaveTypeDescription,
							 }).ToList();
				}

				return _Leaves;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leaves For Date Range And Leave Type
		/// </summary>
		/// <param name="_DateStart">Start Date to get Leaves for</param>
		/// <param name="_DateEnd">End Date to get Leaves for</param>
		/// <param name="_LeaveType">Leave Type to get Leaves for</param>
		/// <returns></returns>
		public static List<LeaveEntity> Leave(DateTime _DateStart, DateTime _DateEnd, LeaveTypes _LeaveType)
		{
			try
			{
				List<LeaveEntity> _Leaves = null;
				_DateStart = _DateStart.Date;
				_DateEnd = _DateEnd.Date;

				if (_DateStart > _DateEnd)
				{
					DateTime _TempDateTime = _DateStart;
					_DateStart = _DateEnd;
					_DateEnd = _TempDateTime;
				}

				if (_DateStart == _DateEnd)
				{
					_DateEnd = _DateStart.AddDays(1).AddTicks(-1);
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Leaves = (from L in Entity.Leaves
							 where L.LeaveDate >= _DateStart && L.LeaveDate <= _DateEnd && L.LeaveType.LeaveTypeDescription == _LeaveType.ToString()
							 select new LeaveEntity
							 {
								 LeaveDate = L.LeaveDate,
								 LeaveHours = L.LeaveHours,
								 LeaveID = L.LeaveID,
								 LeaveTypeID = L.LeaveTypeID,
								 LeaveTypeDescription = L.LeaveType.LeaveTypeDescription,
							 }).ToList();
				}

				return _Leaves;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leave Hours For Date
		/// </summary>
		/// <param name="_Date">Date to get Leave Hours for</param>
		/// <returns></returns>
		public static String LeaveHours(DateTime _Date)
		{
			try
			{
				List<LeaveEntity> _Leaves = Leave(_Date);

				if (_Leaves != null && _Leaves.Count > 0)
				{
					return _Leaves.Sum(query => query.LeaveHours).ToString("F2");
				}
				else
				{
					return "";
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leave Hours For Date And Leave Type
		/// </summary>
		/// <param name="_Date">Date to get Leave Hours for</param>
		/// <param name="_LeaveType">Leave Type to get Leave Hours for</param>
		/// <returns></returns>
		public static String LeaveHours(DateTime _Date, LeaveTypes _LeaveType)
		{
			try
			{
				List<LeaveEntity> _Leaves = Leave(_Date, _LeaveType);

				if (_Leaves != null && _Leaves.Count > 0)
				{
					return _Leaves.Sum(query => query.LeaveHours).ToString("F2");
				}
				else
				{
					return "";
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leave Hours For Date Range
		/// </summary>
		/// <param name="_DateStart">Start Date to get Leave Hours for</param>
		/// <param name="_DateEnd">End Date to get Leave Hours for</param>
		/// <returns></returns>
		public static String LeaveHours(DateTime _DateStart, DateTime _DateEnd)
		{
			try
			{
				List<LeaveEntity> _Leaves = Leave(_DateStart, _DateEnd);

				if (_Leaves != null && _Leaves.Count > 0)
				{
					return _Leaves.Sum(query => query.LeaveHours).ToString("F2");
				}
				else
				{
					return "";
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leave Hours For Date Range And Leave Type
		/// </summary>
		/// <param name="_DateStart">Start Date to get Leave Hours for</param>
		/// <param name="_DateEnd">End Date to get Leave Hours for</param>
		/// <param name="_LeaveType">Leave Type to get Leave Hours for</param>
		/// <returns></returns>
		public static String LeaveHours(DateTime _DateStart, DateTime _DateEnd, LeaveTypes _LeaveType)
		{
			try
			{
				List<LeaveEntity> _Leaves = Leave(_DateStart, _DateEnd, _LeaveType);

				if (_Leaves != null && _Leaves.Count > 0)
				{
					return _Leaves.Sum(query => query.LeaveHours).ToString("F2");
				}
				else
				{
					return "";
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}

	/// <summary>
	/// Logging Program Message
	/// </summary>
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
					_SenderName = Utility.CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
				}
				catch
				{
					_SenderName = "Unknown";
				}

				MessageBox.Show(_Message, _SenderName, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
				_Level = Utility.CleanString(_Level, "Unknown", true);
				_Message = Utility.CleanString(_Message, "Unknown");

				try
				{
					_SenderName = Utility.CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
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
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
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
					_SenderName = Utility.CleanString(_Sender.Name + "." + _Sender.ReflectedType.Name, "Unknown", false, new Char[] { ' ', '.' });
				}
				catch
				{
					_SenderName = "Unknown";
				}

				return new Exception(_SenderName, _Exception);
			}
			catch (Exception general_Exception)
			{
				throw new System.Exception("Cannot Throw Exception", general_Exception);
			}
		}
	}
}