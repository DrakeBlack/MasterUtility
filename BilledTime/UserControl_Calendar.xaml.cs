using System;
using System.Collections.Generic;
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

namespace BilledTime
{
	public partial class UserControl_Calendar : UserControl
	{
		#region Routed Event Bubble
		/// <summary>
		/// Selected Week Routed Event
		/// </summary>
		public static readonly RoutedEvent WeekSelectedEvent = EventManager.RegisterRoutedEvent("WeekSelectedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_Calendar));
		public event RoutedEventHandler WeekSelected
		{
			add
			{
				AddHandler(WeekSelectedEvent, value);
			}

			remove
			{
				RemoveHandler(WeekSelectedEvent, value);
			}
		}
		#endregion

		/// <summary>
		/// Prevent Selected Dates Changed From Running When Selected Week Based On Selected Day
		/// </summary>
		private Boolean _PreventCall = false;

		/// <summary>
		/// Constructor
		/// </summary>
		public UserControl_Calendar()
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
				this.calendar_Week.SelectedDate = DateTime.Now;
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load", general_Exception);
			}
		}

		/// <summary>
		/// Change Selected Day
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void calendar_Week_SelectedDatesChanged(Object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (e.AddedItems == null || e.AddedItems.Count == 0)
				{
					return;
				}

				if (_PreventCall)
				{
					return;
				}

				e.Handled = false;
				SelectWeek((DateTime)e.AddedItems[0]);
				Mouse.Capture(null);
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Change Selected Dates", general_Exception);
			}
		}

		/// <summary>
		/// Selected Week Based On Selected Date
		/// </summary>
		/// <param name="_Date"></param>
		private void SelectWeek(DateTime _Date)
		{
			try
			{
				_PreventCall = true;
				DateTime _MoveDate = _Date;
				this.calendar_Week.SelectedDates.Clear();
				this.calendar_Week.SelectedDates.Add(_Date);

				while (_MoveDate.DayOfWeek != DayOfWeek.Sunday)
				{
					_MoveDate = _MoveDate.AddDays(-1);
				}

				for (Int32 _Counter = 0; _Counter < 7; _Counter++)
				{
					if (_MoveDate != _Date)
					{
						this.calendar_Week.SelectedDates.Add(_MoveDate);
					}

					_MoveDate = _MoveDate.AddDays(1);
				}

				_PreventCall = false;
				RaiseEvent(new RoutedEventArgs(UserControl_Calendar.WeekSelectedEvent));
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}
}
