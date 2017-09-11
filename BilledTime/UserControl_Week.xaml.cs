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
	public partial class UserControl_Week : UserControl, INotifyPropertyChanged
	{
		#region Routed Event Bubble
		/// <summary>
		/// Day Selected Routed Event
		/// </summary>
		public static readonly RoutedEvent BilledTimeSelectedEvent = EventManager.RegisterRoutedEvent("BilledTimeDaySelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_Week));
		public event RoutedEventHandler BilledTimeSelected
		{
			add
			{
				AddHandler(BilledTimeSelectedEvent, value);
			}

			remove
			{
				RemoveHandler(BilledTimeSelectedEvent, value);
			}
		}
		#endregion

		#region DependencyProperties
		/// <summary>
		/// List<DateGroupedBilledTime> Object
		/// </summary>
		public static readonly DependencyProperty DateGroupedBilledTimesProperty = DependencyProperty.Register("DateBilledTimes", typeof(List<DateGroupedBilledTime>), typeof(UserControl_Week), new FrameworkPropertyMetadata(null, DateGroupedBilledTimesChanged));
		private static void DateGroupedBilledTimesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_Data((UserControl_Week)sender, (List<DateGroupedBilledTime>)e.NewValue);
		}
		public List<DateGroupedBilledTime> DateBilledTimes
		{
			get
			{
				return (List<DateGroupedBilledTime>)GetValue(DateGroupedBilledTimesProperty);
			}

			set
			{
				SetValue(DateGroupedBilledTimesProperty, value);
			}
		}

		/// <summary>
		/// Leaves Grouped Object
		/// </summary>
		public static readonly DependencyProperty LeavesGroupedProperty = DependencyProperty.Register("LeavesGrouped", typeof(List<LeaveEntity>), typeof(UserControl_Week), new FrameworkPropertyMetadata(null, LeavesGroupedChanged));
		private static void LeavesGroupedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_Leave((UserControl_Week)sender, (List<LeaveEntity>)e.NewValue);
		}
		public List<LeaveEntity> LeavesGrouped
		{
			get
			{
				return (List<LeaveEntity>)GetValue(LeavesGroupedProperty);
			}

			set
			{
				SetValue(LeavesGroupedProperty, value);
			}
		}
		#endregion

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
		/// Total Billed Hours For This Week
		/// </summary>
		private Double _TotalHours;
		public Double TotalHours
		{
			get
			{
				try
				{
					return this._TotalHours;
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
					if (value != this._TotalHours)
					{
						this._TotalHours = value;
						OnPropertyChanged("TotalHours");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// List Of Billed Times Grouped By ProjectCode-ProjectSubCode For This Week
		/// </summary>
		private List<ProjectGroupedBilledTime> _ProjectGroupedBilledTimes;
		public List<ProjectGroupedBilledTime> ProjectGroupedBilledTimes
		{
			get
			{
				try
				{
					return this._ProjectGroupedBilledTimes;
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
					if (value != this._ProjectGroupedBilledTimes)
					{
						this._ProjectGroupedBilledTimes = value;
						OnPropertyChanged("ProjectGroupedBilledTimes");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Date Grouped Billed Time For This Monday
		/// </summary>
		private DateGroupedBilledTime _DateGroupedBilledTime_Monday;
		public DateGroupedBilledTime DateGroupedBilledTime_Monday
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTime_Monday;
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
					if (value != this._DateGroupedBilledTime_Monday)
					{
						this._DateGroupedBilledTime_Monday = value;
						OnPropertyChanged("DateGroupedBilledTime_Monday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Individual Leaves For Monday
		/// </summary>
		private List<LeaveEntity> _LeavesEntities_Monday;
		public List<LeaveEntity> LeavesEntities_Monday
		{
			get
			{
				try
				{
					return this._LeavesEntities_Monday;
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
					if (value != this._LeavesEntities_Monday)
					{
						this._LeavesEntities_Monday = value;
						OnPropertyChanged("LeavesEntities_Monday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Date Grouped Billed Time For This Tuesday
		/// </summary>
		private DateGroupedBilledTime _DateGroupedBilledTime_Tuesday;
		public DateGroupedBilledTime DateGroupedBilledTime_Tuesday
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTime_Tuesday;
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
					if (value != this._DateGroupedBilledTime_Tuesday)
					{
						this._DateGroupedBilledTime_Tuesday = value;
						OnPropertyChanged("DateGroupedBilledTime_Tuesday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Individual Leaves For Tuesday
		/// </summary>
		private List<LeaveEntity> _LeavesEntities_Tuesday;
		public List<LeaveEntity> LeavesEntities_Tuesday
		{
			get
			{
				try
				{
					return this._LeavesEntities_Tuesday;
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
					if (value != this._LeavesEntities_Tuesday)
					{
						this._LeavesEntities_Tuesday = value;
						OnPropertyChanged("LeavesEntities_Tuesday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Date Grouped Billed Time For This Wednesday
		/// </summary>
		private DateGroupedBilledTime _DateGroupedBilledTime_Wednesday;
		public DateGroupedBilledTime DateGroupedBilledTime_Wednesday
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTime_Wednesday;
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
					if (value != this._DateGroupedBilledTime_Wednesday)
					{
						this._DateGroupedBilledTime_Wednesday = value;
						OnPropertyChanged("DateGroupedBilledTime_Wednesday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Individual Leaves For Wednesday
		/// </summary>
		private List<LeaveEntity> _LeavesEntities_Wednesday;
		public List<LeaveEntity> LeavesEntities_Wednesday
		{
			get
			{
				try
				{
					return this._LeavesEntities_Wednesday;
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
					if (value != this._LeavesEntities_Wednesday)
					{
						this._LeavesEntities_Wednesday = value;
						OnPropertyChanged("LeavesEntities_Wednesday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Date Grouped Billed Time For This Thursday
		/// </summary>
		private DateGroupedBilledTime _DateGroupedBilledTime_Thursday;
		public DateGroupedBilledTime DateGroupedBilledTime_Thursday
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTime_Thursday;
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
					if (value != this._DateGroupedBilledTime_Thursday)
					{
						this._DateGroupedBilledTime_Thursday = value;
						OnPropertyChanged("DateGroupedBilledTime_Thursday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Individual Leaves For Thursday
		/// </summary>
		private List<LeaveEntity> _LeavesEntities_Thursday;
		public List<LeaveEntity> LeavesEntities_Thursday
		{
			get
			{
				try
				{
					return this._LeavesEntities_Thursday;
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
					if (value != this._LeavesEntities_Thursday)
					{
						this._LeavesEntities_Thursday = value;
						OnPropertyChanged("LeavesEntities_Thursday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Date Grouped Billed Time For This Friday
		/// </summary>
		private DateGroupedBilledTime _DateGroupedBilledTime_Friday;
		public DateGroupedBilledTime DateGroupedBilledTime_Friday
		{
			get
			{
				try
				{
					return this._DateGroupedBilledTime_Friday;
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
					if (value != this._DateGroupedBilledTime_Friday)
					{
						this._DateGroupedBilledTime_Friday = value;
						OnPropertyChanged("DateGroupedBilledTime_Friday");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Individual Leaves For Friday
		/// </summary>
		private List<LeaveEntity> _LeavesEntities_Friday;
		public List<LeaveEntity> LeavesEntities_Friday
		{
			get
			{
				try
				{
					return this._LeavesEntities_Friday;
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
					if (value != this._LeavesEntities_Friday)
					{
						this._LeavesEntities_Friday = value;
						OnPropertyChanged("LeavesEntities_Friday");
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
		private List<LeaveEntity> _LeavesGroupedEntities;
		public List<LeaveEntity> LeavesGroupedEntities
		{
			get
			{
				try
				{
					return this._LeavesGroupedEntities;
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
					if (value != this._LeavesGroupedEntities)
					{
						this._LeavesGroupedEntities = value;
						OnPropertyChanged("LeavesGroupedEntities");
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
		public UserControl_Week()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		/// <summary>
		/// Loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_Loaded(Object sender, RoutedEventArgs e)
		{
			try
			{

			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load", general_Exception);
			}
		}

		/// <summary>
		/// Routed Event For Edit Billed Time Entity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void day_BilledTimeSelected(Object sender, RoutedEventArgs e)
		{
			try
			{
				e.Handled = false;
				RaiseEvent(new RoutedEventArgs(UserControl_Week.BilledTimeSelectedEvent, sender));
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Select Billed Time", general_Exception);
			}
		}

		/// <summary>
		/// Create Excel Time Sheet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_CreateTimeSheet_Click(Object sender, RoutedEventArgs e)
		{
			try
			{
				e.Handled = true;
				Utility.CreateExcel(this.ProjectGroupedBilledTimes, this.DateGroupedBilledTime_Monday, this.DateGroupedBilledTime_Tuesday, this.DateGroupedBilledTime_Wednesday, this.DateGroupedBilledTime_Thursday, this.DateGroupedBilledTime_Friday);
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Create Time Sheet", general_Exception);
			}
		}



		/// <summary>
		/// This is called by Dependency Property Change
		/// </summary>
		/// <param name="this_UserControl"></param>
		/// <param name="this_DateGroupedBilledTimes"></param>
		public static void Show_Data(UserControl_Week this_UserControl, List<DateGroupedBilledTime> this_DateGroupedBilledTimes)
		{
			try
			{
				this_UserControl.Load_Data(this_DateGroupedBilledTimes);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_DateGroupedBilledTimes"></param>
		private void Load_Data(List<DateGroupedBilledTime> _DateGroupedBilledTimes)
		{
			try
			{
				if (_DateGroupedBilledTimes != null && _DateGroupedBilledTimes.Count > 0)
				{
					DateGroupedBilledTime _NextDay = null;
					DateTime _DateMonday = _DateGroupedBilledTimes.Min(query => query.Date.Value);
					this.TotalHours = _DateGroupedBilledTimes.Sum(query => query.TotalHours);

					while (_DateMonday.DayOfWeek != DayOfWeek.Monday)
					{
						_DateMonday = _DateMonday.AddDays(-1);
					}

					_NextDay = _DateGroupedBilledTimes.FirstOrDefault(query => query.Date.Value.DayOfWeek == DayOfWeek.Monday);

					if (_NextDay == null)
					{
						_NextDay = new DateGroupedBilledTime(_DateMonday);
					}

					this.DateGroupedBilledTime_Monday = _NextDay;
					_NextDay = _DateGroupedBilledTimes.FirstOrDefault(query => query.Date.Value.DayOfWeek == DayOfWeek.Tuesday);

					if (_NextDay == null)
					{
						_NextDay = new DateGroupedBilledTime(_DateMonday.AddDays(1));
					}

					this.DateGroupedBilledTime_Tuesday = _NextDay;
					_NextDay = _DateGroupedBilledTimes.FirstOrDefault(query => query.Date.Value.DayOfWeek == DayOfWeek.Wednesday);

					if (_NextDay == null)
					{
						_NextDay = new DateGroupedBilledTime(_DateMonday.AddDays(2));
					}

					this.DateGroupedBilledTime_Wednesday = _NextDay;
					_NextDay = _DateGroupedBilledTimes.FirstOrDefault(query => query.Date.Value.DayOfWeek == DayOfWeek.Thursday);

					if (_NextDay == null)
					{
						_NextDay = new DateGroupedBilledTime(_DateMonday.AddDays(3));
					}

					this.DateGroupedBilledTime_Thursday = _NextDay;
					_NextDay = _DateGroupedBilledTimes.FirstOrDefault(query => query.Date.Value.DayOfWeek == DayOfWeek.Friday);

					if (_NextDay == null)
					{
						_NextDay = new DateGroupedBilledTime(_DateMonday.AddDays(4));
					}

					this.DateGroupedBilledTime_Friday = _NextDay;

					// Week Totals
					var var_ProjectGroupedBilledTimes = DataAccess.WeekGroupBilledTimes(_DateGroupedBilledTimes).ProjectGroupedBilledTimes;

					if (var_ProjectGroupedBilledTimes != null && var_ProjectGroupedBilledTimes.Count > 0)
					{
						this.ProjectGroupedBilledTimes = var_ProjectGroupedBilledTimes.OrderBy(query => query.CombinedProjectCodeDescription).ToList();
					}
					else
					{
						this.ProjectGroupedBilledTimes = null;
					}
				}
				else
				{
					this.TotalHours = 0;
					this.ProjectGroupedBilledTimes = null;

					DateTime _DateMonday = DateTime.Now.Date;

					while (_DateMonday.DayOfWeek != DayOfWeek.Sunday)
					{
						_DateMonday = _DateMonday.AddDays(-1);
					}

					_DateMonday = _DateMonday.AddDays(1);
					this.DateGroupedBilledTime_Monday = new DateGroupedBilledTime(_DateMonday);
					this.DateGroupedBilledTime_Tuesday = new DateGroupedBilledTime(_DateMonday.AddDays(1));
					this.DateGroupedBilledTime_Wednesday = new DateGroupedBilledTime(_DateMonday.AddDays(2));
					this.DateGroupedBilledTime_Thursday = new DateGroupedBilledTime(_DateMonday.AddDays(3));
					this.DateGroupedBilledTime_Friday = new DateGroupedBilledTime(_DateMonday.AddDays(4));
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// This is called by Dependency Property Change
		/// </summary>
		/// <param name="this_UserControl"></param>
		/// <param name="this_LeavesGrouped"></param>
		public static void Show_Leave(UserControl_Week this_UserControl, List<LeaveEntity> this_LeavesGrouped)
		{
			try
			{
				this_UserControl.Load_Leaves(this_LeavesGrouped);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_LeavesGrouped"></param>
		private void Load_Leaves(List<LeaveEntity> _LeavesGrouped)
		{
			try
			{
				if (_LeavesGrouped != null)
				{
					this.LeavesGrouped = _LeavesGrouped;

					var _LeavesDateGrouped = this.LeavesGrouped.GroupBy(query => query.LeaveDate).ToList();

					if (_LeavesDateGrouped != null && _LeavesDateGrouped.Count > 0)
					{
						this.LeavesEntities_Monday = _LeavesDateGrouped.FirstOrDefault(query => query.Key.DayOfWeek == DayOfWeek.Monday).ToList();
						this.LeavesEntities_Tuesday = _LeavesDateGrouped.FirstOrDefault(query => query.Key.DayOfWeek == DayOfWeek.Tuesday).ToList();
						this.LeavesEntities_Wednesday = _LeavesDateGrouped.FirstOrDefault(query => query.Key.DayOfWeek == DayOfWeek.Wednesday).ToList();
						this.LeavesEntities_Thursday = _LeavesDateGrouped.FirstOrDefault(query => query.Key.DayOfWeek == DayOfWeek.Thursday).ToList();
						this.LeavesEntities_Friday = _LeavesDateGrouped.FirstOrDefault(query => query.Key.DayOfWeek == DayOfWeek.Friday).ToList();
					}
					else
					{
						this.LeavesEntities_Monday = null;
						this.LeavesEntities_Tuesday = null;
						this.LeavesEntities_Wednesday = null;
						this.LeavesEntities_Thursday = null;
						this.LeavesEntities_Friday = null;
					}
				}
				else
				{
					this.LeavesGrouped = null;
					this.LeavesEntities_Monday = null;
					this.LeavesEntities_Tuesday = null;
					this.LeavesEntities_Wednesday = null;
					this.LeavesEntities_Thursday = null;
					this.LeavesEntities_Friday = null;
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}
}
