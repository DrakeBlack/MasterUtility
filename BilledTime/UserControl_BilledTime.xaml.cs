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
	public partial class UserControl_BilledTime : UserControl, INotifyPropertyChanged
	{
		#region Routed Event Bubble
		/// <summary>
		/// Delete Billed Time Routed Event
		/// </summary>
		public static readonly RoutedEvent BilledTimeDeleteEvent = EventManager.RegisterRoutedEvent("BilledTimeDeleteEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_BilledTime));
		public event RoutedEventHandler BilledTimeDelete
		{
			add
			{
				AddHandler(BilledTimeDeleteEvent, value);
			}

			remove
			{
				RemoveHandler(BilledTimeDeleteEvent, value);
			}
		}

		/// <summary>
		/// Update Billed Time Routed Event
		/// </summary>
		public static readonly RoutedEvent BilledTimeUpdateEvent = EventManager.RegisterRoutedEvent("BilledTimeUpdateEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserControl_BilledTime));
		public event RoutedEventHandler BilledTimeUpdate
		{
			add
			{
				AddHandler(BilledTimeUpdateEvent, value);
			}

			remove
			{
				RemoveHandler(BilledTimeUpdateEvent, value);
			}
		}
		#endregion

		#region DependencyProperties
		/// <summary>
		/// BilledTimeEntity Object
		/// </summary>
		public static readonly DependencyProperty BilledTimeEntityProperty = DependencyProperty.Register("BilledTimeEntity", typeof(BilledTimeEntity), typeof(UserControl_BilledTime), new FrameworkPropertyMetadata(null, BilledTimeEntityChanged));
		private static void BilledTimeEntityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_Data((UserControl_BilledTime)sender, (BilledTimeEntity)e.NewValue);
		}
		public BilledTimeEntity BilledTimeEntity
		{
			get
			{
				return (BilledTimeEntity)GetValue(BilledTimeEntityProperty);
			}

			set
			{
				SetValue(BilledTimeEntityProperty, value);
			}
		}

		/// <summary>
		/// ProjectCodes Object
		/// </summary>
		public static readonly DependencyProperty ProjectCodesProperty = DependencyProperty.Register("ProjectCodes", typeof(List<ProjectCodeEntity>), typeof(UserControl_BilledTime), new FrameworkPropertyMetadata(null, ProjectCodesChanged));
		private static void ProjectCodesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Show_ProjectCodes((UserControl_BilledTime)sender, (List<ProjectCodeEntity>)e.NewValue);
		}
		public List<ProjectCodeEntity> ProjectCodes
		{
			get
			{
				return (List<ProjectCodeEntity>)GetValue(ProjectCodesProperty);
			}

			set
			{
				SetValue(ProjectCodesProperty, value);
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
		/// Billed Time Entity For Edit
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
		/// Project Sub Codes For ComboBox
		/// </summary>
		private List<ProjectSubCodeEntity> _ProjectSubCodeEntities;
		public List<ProjectSubCodeEntity> ProjectSubCodeEntities
		{
			get
			{
				try
				{
					return this._ProjectSubCodeEntities;
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
					if (value != this._ProjectSubCodeEntities)
					{
						this._ProjectSubCodeEntities = value;
						OnPropertyChanged("ProjectSubCodeEntities");
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
		public UserControl_BilledTime()
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
				this.BilledTimeEntityEdit = null;
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load", general_Exception);
			}
		}

		/// <summary>
		/// Changing Project Code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void combobox_Project_SelectionChanged(Object sender, SelectionChangedEventArgs e)
		{
			try
			{
				ProjectCodeEntity _ProjectCode = (ProjectCodeEntity)e.AddedItems[0];
				ProjectSubCodeEntities = _ProjectCode.ProjectSubCodes;
				this.combobox_Code.SelectedIndex = 0;
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Change Project Code", general_Exception);
			}
		}

		/// <summary>
		/// Delete Billed Time Entity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Delete_Click(Object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.BilledTimeEntityEdit == null)
				{
					return;
				}

				Boolean _Result = DataAccess.BilledTimeEntity_Delete(this.BilledTimeEntityEdit);

				if (_Result)
				{
					this.BilledTimeEntityEdit = null;
					this.combobox_Project.SelectedIndex = 0;
					this.combobox_Code.SelectedIndex = 0;
					this.datepicker_Date.SelectedDate = DateTime.Now;
					this.button_Delete.Visibility = Visibility.Collapsed;
					RaiseEvent(new RoutedEventArgs(UserControl_BilledTime.BilledTimeDeleteEvent));
				}
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Delete", general_Exception);
			}
		}

		/// <summary>
		/// Save Billed Time Entity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Save_Click(Object sender, RoutedEventArgs e)
		{
			try
			{
				BilledTimeEntity _Validate = Validate();
				Boolean _Result = false;

				if (_Validate != null)
				{
					if (this.BilledTimeEntityEdit == null)
					{
						DataAccess.BilledTimeEntity_Add(_Validate);
						_Result = true;
					}
					else
					{
						DataAccess.BilledTimeEntity_Update(this.BilledTimeEntityEdit.BilledTimeID, _Validate.BilledDate, _Validate.BilledHours, _Validate.Notes, _Validate.ProjectCodeID, _Validate.ProjectSubCodeID);
						_Result = true;
					}
				}

				if (_Result)
				{
					this.BilledTimeEntityEdit = null;
					this.combobox_Project.SelectedIndex = 0;
					this.combobox_Code.SelectedIndex = 0;
					this.datepicker_Date.SelectedDate = DateTime.Now;
					this.button_Delete.Visibility = Visibility.Collapsed;
					RaiseEvent(new RoutedEventArgs(UserControl_BilledTime.BilledTimeUpdateEvent));
				}
			}
			catch (Exception general_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Save", general_Exception);
			}
		}

		/// <summary>
		/// Validate Data Entry
		/// </summary>
		/// <returns>BilledTimeEntity</returns>
		private BilledTimeEntity Validate()
		{
			try
			{
				Boolean _Valid = true;
				BilledTimeEntity _New = new BilledTimeEntity();

				if (!this.datepicker_Date.SelectedDate.HasValue)
				{
					_Valid = false;
				}
				else
				{
					_New.BilledDate = this.datepicker_Date.SelectedDate.Value;
				}

				if (this.combobox_Project.SelectedIndex < 0)
				{
					_Valid = false;
				}
				else
				{
					_New.ProjectCodeID = (Int32)this.combobox_Project.SelectedValue;
				}

				if (this.combobox_Code.SelectedIndex < 0)
				{
					_Valid = false;
				}
				else
				{
					_New.ProjectSubCodeID = (Int32)this.combobox_Code.SelectedValue;
				}

				if (String.IsNullOrWhiteSpace(this.textbox_Hours.Text))
				{
					_Valid = false;
				}

				this.textbox_Hours.Text = this.textbox_Hours.Text.Trim();

				if (String.IsNullOrWhiteSpace(this.textbox_Hours.Text))
				{
					_Valid = false;
				}

				try
				{
					_New.BilledHours = Convert.ToDouble(this.textbox_Hours.Text);
				}
				catch
				{
					_Valid = false;
				}

				if (_Valid)
				{
					return _New;
				}
				else
				{
					return null;
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
		/// <param name="this_BilledTimeEntity"></param>
		public static void Show_Data(UserControl_BilledTime this_UserControl, BilledTimeEntity this_BilledTimeEntity)
		{
			try
			{
				this_UserControl.Load_Data(this_BilledTimeEntity);
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
		/// <param name="this_BilledTimeEntity"></param>
		public static void Show_ProjectCodes(UserControl_BilledTime this_UserControl, List<ProjectCodeEntity> this_ProjectCodeEntities)
		{
			try
			{
				this_UserControl.Load_ProjectCodes(this_ProjectCodeEntities);
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_BilledTimeEntity"></param>
		private void Load_Data(BilledTimeEntity _BilledTimeEntity)
		{
			try
			{
				if (_BilledTimeEntity != null)
				{
					this.BilledTimeEntityEdit = _BilledTimeEntity;
					this.combobox_Project.SelectedValue = _BilledTimeEntity.ProjectCodeID;
					this.combobox_Code.SelectedValue = _BilledTimeEntity.ProjectSubCodeID;
					this.button_Delete.Visibility = Visibility.Visible;
				}
				else
				{
					this.BilledTimeEntityEdit = null;
					this.combobox_Project.SelectedIndex = 0;
					this.combobox_Code.SelectedIndex = 0;
					this.datepicker_Date.SelectedDate = DateTime.Now;
					this.button_Delete.Visibility = Visibility.Collapsed;
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Actually Sets Object Values
		/// </summary>
		/// <param name="_BilledTimeEntity"></param>
		private void Load_ProjectCodes(List<ProjectCodeEntity> _ProjectCodeEntities)
		{
			try
			{
				if (_ProjectCodeEntities != null)
				{
					this.ProjectCodeEntities = _ProjectCodeEntities;
				}
				else
				{
					this.ProjectCodeEntities = null;
				}
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}
	}
}
