using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	public partial class UserControl_Day : UserControl, INotifyPropertyChanged
	{
		#region Routed Event Bubble
		/// <summary>
		/// Selected Billed Time Routed Event
		/// </summary>
		public static readonly RoutedEvent BilledTimeSelectedEvent = EventManager.RegisterRoutedEvent("BilledTimeSelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_Day));
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

		/// <summary>
		/// Selected Leave Routed Event
		/// </summary>
		public static readonly RoutedEvent LeaveSelectedEvent = EventManager.RegisterRoutedEvent("LeaveSelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_Day));
		public event RoutedEventHandler LeaveSelected
		{
			add
			{
				AddHandler(LeaveSelectedEvent, value);
			}

			remove
			{
				RemoveHandler(LeaveSelectedEvent, value);
			}
		}
		#endregion

		#region DependencyProperties
		/// <summary>
		/// DateGroupedBilledTime Object
		/// </summary>
		public static readonly DependencyProperty DateGroupedBilledTimeProperty = DependencyProperty.Register("DateBilledTime", typeof(DateGroupedBilledTime), typeof(UserControl_Day), new FrameworkPropertyMetadata(null, DateGroupedBilledTimeChanged));
		private static void DateGroupedBilledTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_Data((UserControl_Day)sender, (DateGroupedBilledTime)e.NewValue);
		}
		public DateGroupedBilledTime DateBilledTime
		{
			get
			{
				return (DateGroupedBilledTime)GetValue(DateGroupedBilledTimeProperty);
			}

			set
			{
				SetValue(DateGroupedBilledTimeProperty, value);
			}
		}

		/// <summary>
		/// Leaves Object
		/// </summary>
		public static readonly DependencyProperty LeavesProperty = DependencyProperty.Register("Leaves", typeof(List<LeaveEntity>), typeof(UserControl_Day), new FrameworkPropertyMetadata(null, LeavesChanged));
		private static void LeavesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_Leave((UserControl_Day)sender, (List<LeaveEntity>)e.NewValue);
		}
		public List<LeaveEntity> Leaves
		{
			get
			{
				return (List<LeaveEntity>)GetValue(LeavesProperty);
			}

			set
			{
				SetValue(LeavesProperty, value);
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
		/// Current Date For This Day
		/// </summary>
		private Nullable<DateTime> _CurrentDate;
		public Nullable<DateTime> CurrentDate
		{
			get
			{
				try
				{
					return this._CurrentDate;
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
					if (value != this._CurrentDate)
					{
						this._CurrentDate = value;
						OnPropertyChanged("CurrentDate");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Total Billed Hours For This Day
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
		/// Individual BilledTimeEntity For This Day
		/// </summary>
		private List<BilledTimeEntity> _BilledTimeEntities;
		public List<BilledTimeEntity> BilledTimeEntities
		{
			get
			{
				try
				{
					return this._BilledTimeEntities;
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
					if (value != this._BilledTimeEntities)
					{
						this._BilledTimeEntities = value;
						OnPropertyChanged("BilledTimeEntities");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// List Of Billed Times Grouped By ProjectCode-ProjectSubCode For This Day
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
		/// Individual Leaves For This Day
		/// </summary>
		private List<LeaveEntity> _LeavesEntities;
		public List<LeaveEntity> LeavesEntities
		{
			get
			{
				try
				{
					return this._LeavesEntities;
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
					if (value != this._LeavesEntities)
					{
						this._LeavesEntities = value;
						OnPropertyChanged("LeavesEntities");
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
		public UserControl_Day()
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
		/// User Selected BilledTimeEntity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void datagrid_BilledTimes_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
		{
			try
			{
				e.Handled = false;
				RaiseEvent(new RoutedEventArgs(UserControl_Day.BilledTimeSelectedEvent));
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Select Billed Time", general_Exception);
			}
		}

		/// <summary>
		/// Just Clears Billed Time Entity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void datagrid_GroupedProjectCodeBilledTimes_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
		{
			try
			{
				e.Handled = false;
				this.datagrid_BilledTimes.SelectedItem = null;
				RaiseEvent(new RoutedEventArgs(UserControl_Day.BilledTimeSelectedEvent));
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Selected Billed Time", general_Exception);
			}
		}

		/// <summary>
		/// User Selected LeaveEntity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void datagrid_Leave_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
		{
			try
			{
				e.Handled = false;
				RaiseEvent(new RoutedEventArgs(UserControl_Day.LeaveSelectedEvent));
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Select Leave", general_Exception);
			}
		}

		/// <summary>
		/// This is called by Dependency Property Change
		/// </summary>
		/// <param name="this_UserControl"></param>
		/// <param name="this_DateGroupedBilledTime"></param>
		public static void Show_Data(UserControl_Day this_UserControl, DateGroupedBilledTime this_DateGroupedBilledTime)
		{
			try
			{
				this_UserControl.Load_Data(this_DateGroupedBilledTime);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_DateGroupedBilledTime"></param>
		private void Load_Data(DateGroupedBilledTime _DateGroupedBilledTime)
		{
			try
			{
				if (_DateGroupedBilledTime != null)
				{
					this.CurrentDate = _DateGroupedBilledTime.Date.Value;
					this.TotalHours = _DateGroupedBilledTime.TotalHours;

					if (_DateGroupedBilledTime.ProjectGroupedBilledTimes != null && _DateGroupedBilledTime.ProjectGroupedBilledTimes.Count > 0)
					{
						this.ProjectGroupedBilledTimes = _DateGroupedBilledTime.ProjectGroupedBilledTimes.OrderBy(query => query.CombinedProjectCodeDescription).ToList();
					}
					else
					{
						this.ProjectGroupedBilledTimes = null;
					}

					if (_DateGroupedBilledTime.BilledTimes != null && _DateGroupedBilledTime.BilledTimes.Count > 0)
					{
						this.BilledTimeEntities = _DateGroupedBilledTime.BilledTimes.OrderBy(query => query.BilledDate).ToList();
					}
					else
					{
						this.ProjectGroupedBilledTimes = null;
					}
				}
				else
				{
					this.CurrentDate = null;
					this.TotalHours = 0;
					this.ProjectGroupedBilledTimes = null;
					this.BilledTimeEntities = null;
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
		/// <param name="this_Leaves"></param>
		public static void Show_Leave(UserControl_Day this_UserControl, List<LeaveEntity> this_Leaves)
		{
			try
			{
				this_UserControl.Load_Leaves(this_Leaves);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_Leaves"></param>
		private void Load_Leaves(List<LeaveEntity> _Leaves)
		{
			try
			{
				if (_Leaves != null)
				{
					this.Leaves = _Leaves;
				}
				else
				{
					this.Leaves = null;
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}
}
