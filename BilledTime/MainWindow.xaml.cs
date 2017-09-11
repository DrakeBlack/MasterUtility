using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using PersonalEntity;

namespace BilledTime
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		#region Bound Objects
		/// <summary>
		/// Property Change Event
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(String a_PropertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(a_PropertyName));
			}
		}

		/// <summary>
		/// Billed Times Grouped By Date
		/// </summary>
		private List<DateGroupedBilledTime> _DateGroupedBilledTimes;
		public List<DateGroupedBilledTime> DateGroupedBilledTimes
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTimes;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._DateGroupedBilledTimes)
					{
						this._DateGroupedBilledTimes = value;
						OnPropertyChanged("DateGroupedBilledTimes");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Billed Time For Edit
		/// </summary>
		private BilledTimeEntity _BilledTimeEntityEdit;
		public BilledTimeEntity BilledTimeEntityEdit
		{
			get
			{
				try
				{
					return this._BilledTimeEntityEdit;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._BilledTimeEntityEdit)
					{
						this._BilledTimeEntityEdit = value;
						OnPropertyChanged("BilledTimeEntityEdit");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Calendar Selected Date
		/// </summary>
		private DateTime _SelectedDate;
		public DateTime SelectedDate
		{
			get
			{
				try
				{
					return this._SelectedDate;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._SelectedDate)
					{
						this._SelectedDate = value;
						OnPropertyChanged("SelectedDate");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Start Of Week For Billed Time Data
		/// </summary>
		private DateTime _WeekStart;
		public DateTime WeekStart
		{
			get
			{
				try
				{
					return this._WeekStart;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._WeekStart)
					{
						this._WeekStart = value;
						OnPropertyChanged("WeekStart");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// End Of Week For Billed Time Data
		/// </summary>
		private DateTime _WeekEnd;
		public DateTime WeekEnd
		{
			get
			{
				try
				{
					return this._WeekEnd;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._WeekEnd)
					{
						this._WeekEnd = value;
						OnPropertyChanged("WeekEnd");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Project Codes For ComboBox
		/// </summary>
		private List<ProjectCodeEntity> _ProjectCodeEntities;
		public List<ProjectCodeEntity> ProjectCodeEntities
		{
			get
			{
				try
				{
					return this._ProjectCodeEntities;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._ProjectCodeEntities)
					{
						this._ProjectCodeEntities = value;
						OnPropertyChanged("ProjectCodeEntities");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Grouped Leaves For Week
		/// </summary>
		private List<LeaveEntity> _LeavesGrouped;
		public List<LeaveEntity> LeavesGrouped
		{
			get
			{
				try
				{
					return this._LeavesGrouped;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}

			set
			{
				try
				{
					if (value != this._LeavesGrouped)
					{
						this._LeavesGrouped = value;
						OnPropertyChanged("LeavesGrouped");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		/// <summary>
		/// Loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(Object sender, RoutedEventArgs e)
		{
			try
			{
				this.Title = "Billed Time - " + ConfigurationManager.AppSettings["EmployeeName"] + " (" + ConfigurationManager.AppSettings["EmployeeID"] + ")";
				this.ProjectCodeEntities = DataAccess.ProjectCodes();
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load", general_Exception);
			}
		}

		/// <summary>
		/// Calendar Control Date Changed Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_Calendar_WeekSelected(Object sender, RoutedEventArgs e)
		{
			try
			{
				e.Handled = true;
				UserControl_Calendar _CalendarUserControl = (UserControl_Calendar)sender;
				Calendar _Calendar = _CalendarUserControl.calendar_Week;
				this.WeekStart = _Calendar.SelectedDates.Min();
				this.WeekEnd = _Calendar.SelectedDates.Max();
				this.DateGroupedBilledTimes = DataAccess.DateGroupedBilledTimes(_Calendar.SelectedDates.Min(), _Calendar.SelectedDates.Max());
				this.SelectedDate = _Calendar.SelectedDates[0];
				this.LeavesGrouped = DataAccess.Leave(_Calendar.SelectedDates.Min(), _Calendar.SelectedDates.Max());
				this.BilledTimeEntityEdit = null;
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Set Selected Week", general_Exception);
			}
		}

		/// <summary>
		/// Day Control Selected Billed Time Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void week_BilledTimeSelected(Object sender, RoutedEventArgs e)
		{
			try
			{
				e.Handled = true;
				UserControl_Day _Day = (UserControl_Day)e.OriginalSource;
				this.BilledTimeEntityEdit = (BilledTimeEntity)_Day.datagrid_BilledTimes.SelectedItem;
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load Billed Time For Edit", general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Control Delete Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void control_BilledTime_BilledTimeDelete(Object sender, RoutedEventArgs e)
		{
			try
			{
				this.DateGroupedBilledTimes = DataAccess.DateGroupedBilledTimes(this.WeekStart, this.WeekEnd);
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Delete Billed Time", general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Control Save Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void control_BilledTime_BilledTimeUpdate(Object sender, RoutedEventArgs e)
		{
			try
			{
				this.DateGroupedBilledTimes = DataAccess.DateGroupedBilledTimes(this.WeekStart, this.WeekEnd);
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Udpate Billed Time", general_Exception);
			}
		}
	}
}