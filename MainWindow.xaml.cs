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
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		//ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "MessageBoxMessage", e_Exception);
		//throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);

		#region Property Changed Event Handler
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(String a_PropertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(a_PropertyName));
			}
		}
		#endregion

		#region Window
		private Boolean p_PreventCall;
		public Boolean PreventCall
		{
			get
			{
				try
				{
					return this.p_PreventCall;
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
					if (value != this.p_PreventCall)
					{
						this.p_PreventCall = value;
						OnPropertyChanged("PreventCall");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Load Window", e_Exception);
			}
		}
		#endregion

		#region Tab Control Main
		private void tabcontrol_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				if (this.tabitem_RunPrograms.IsSelected)
				{
					RunPrograms_Loaded();
				}
				if (this.tabitem_ProjectCodeAdmin.IsSelected)
				{
					ProjectCodeAdmin_Loaded();
				}
				if (this.tabitem_BilledTime.IsSelected)
				{
					BilledTime_Loaded();
				}
				if (this.tabitem_LeaveTime.IsSelected)
				{
					LeaveTime_Loaded();
				}
				if (this.tabitem_TimeSheet.IsSelected)
				{
					TimeSheet_Loaded();
				}

				if (this.tabitem_EmployeeList.IsSelected)
				{
					EmployeeList_Loaded();
				}

				if (this.tabitem_XMLFormat.IsSelected)
				{
					XMLFormat_Loaded();
				}
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Change Selected Tab Item", e_Exception);
			}
		}
		#endregion

		#region Run Programs
		private void RunPrograms_Loaded()
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void RunProgram(String _Program)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				Process.Start(@"C:\Windows\Explorer.exe", _Program);
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Run Program", e_Exception);
			}
		}

		private void button_RunPrograms_VersionControl_GetLatest_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\DrakeBlack\MasterUtility\VersionControl_GetLatest.bat");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Get Latest", e_Exception);
			}
		}

		private void button_RunPrograms_VersionControl_SourceSafe_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\Program Files (x86)\Microsoft Visual Studio\Common\VSS\win32\SSEXP.EXE");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Source Safe", e_Exception);
			}
		}

		private void button_RunPrograms_D21VisualStudios_D21Server_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21Server\D21Server.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Server", e_Exception);
			}
		}

		private void button_RunPrograms_D21VisualStudios_D21Incident_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21Incident\D21Incident.vbp");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Incident", e_Exception);
			}
		}

		private void button_RunPrograms_D21VisualStudios_D21Admin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21Admin\D21Admin.vbp");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Admin", e_Exception);
			}
		}

		private void button_RunPrograms_D21VisualStudios_D21DBUtility_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21DbUtil\D21DbUtil.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 DB Utility", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_DBConfigurator_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Products\DbConfigurator\DbConfigurator.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open DB Configurator", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Alpha_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Data\Alpha\Monaco.Common.Data.Alpha.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Alpha", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Gamma_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Data\Gamma\Monaco.Common.Data.Gamma.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Gamma", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Shared_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Shared\Shared.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Shared", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Controls_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Controls\Controls.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Controls", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_AdvancedControls_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Controls\AdvancedControls.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Advanced Controls", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Dialogs_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Dialogs\Dialogs.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Dialogs", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_AdvancedDialogs_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Dialogs\AdvancedDialogs.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Advanced Dialogs", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Reports_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Reports\Reports.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Reports", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Viewers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Viewers\Viewers.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Viewers", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_AdvancedViewers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Viewers\AdvancedViewers.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Advanced Viewers", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_AdminViewers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Viewers\AdminViewers.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Admin Viewers", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_AttributeViewers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Viewers\AttributeViewers.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Attribute Viewers", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_EntityViewers_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Common\Viewers\EntityViewers.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Entity Viewers", e_Exception);
			}
		}

		private void button_RunPrograms_EMVisualStudios_Angus_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Products\Angus\Angus.sln");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Angus", e_Exception);
			}
		}

		private void button_RunPrograms_D21Executable_D21Server_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21Server\bin\D21Server.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Server", e_Exception);
			}
		}

		private void button_RunPrograms_D21Executable_D21Incident_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21.bin\D21Incident.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Incident", e_Exception);
			}
		}

		private void button_RunPrograms_D21Executable_D21Admin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21.bin\D21Admin.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 Admin", e_Exception);
			}
		}

		private void button_RunPrograms_D21Executable_D21DBUtility_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21DbUtil\bin\DbUtil.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21 DB Utility", e_Exception);
			}
		}

		private void button_RunPrograms_EMExecutable_D21Config_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\D21\D21Config\D21Config.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open D21Config", e_Exception);
			}
		}

		private void button_RunPrograms_EMExecutable_DBConfigurator_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Products\DbConfigurator\DbConfigurator\bin\Release\DBConfigurator.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open DB Configurator", e_Exception);
			}
		}

		private void button_RunPrograms_EMExecutable_Angus_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Products\Angus\Angus\bin\Release\LE-21.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Angus", e_Exception);
			}
		}

		private void button_RunPrograms_EMExecutable_PurpleRain_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\MMSPrototype\Products\PurpleRain\PurpleRain\bin\Release\MMSAdmin.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Purple Rain", e_Exception);
			}
		}

		private void button_RunPrograms_EMExecutable_CodeSmith_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\Program Files (x86)\CodeSmith\v4.1\CodeSmithStudio.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Code Smith", e_Exception);
			}
		}

		private void button_RunPrograms_UtilityExecutable_Calculator_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				RunProgram(@"C:\Windows\System32\calc.exe");
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Open Code Smith", e_Exception);
			}
		}
		#endregion

		#region Project Code Admin
		private List<PersonalEntity.ProjectCode> p_ProjectCodeAdmin_ProjectCodes;
		public List<PersonalEntity.ProjectCode> ProjectCodeAdmin_ProjectCodes
		{
			get
			{
				try
				{
					return this.p_ProjectCodeAdmin_ProjectCodes;
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
					if (value != this.p_ProjectCodeAdmin_ProjectCodes)
					{
						this.p_ProjectCodeAdmin_ProjectCodes = value;
						OnPropertyChanged("ProjectCodeAdmin_ProjectCodes");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private List<PersonalEntity.ProjectSubCode> p_ProjectCodeAdmin_ProjectSubCodes;
		public List<PersonalEntity.ProjectSubCode> ProjectCodeAdmin_ProjectSubCodes
		{
			get
			{
				try
				{
					return this.p_ProjectCodeAdmin_ProjectSubCodes;
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
					if (value != this.p_ProjectCodeAdmin_ProjectSubCodes)
					{
						this.p_ProjectCodeAdmin_ProjectSubCodes = value;
						OnPropertyChanged("ProjectCodeAdmin_ProjectSubCodes");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private List<DataAccessLayer.ProjectSubCodesUsed> p_ProjectCodeAdmin_ProjectSubCodesUsed;
		public List<DataAccessLayer.ProjectSubCodesUsed> ProjectCodeAdmin_ProjectSubCodesUsed
		{
			get
			{
				try
				{
					return this.p_ProjectCodeAdmin_ProjectSubCodesUsed;
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
					if (value != this.p_ProjectCodeAdmin_ProjectSubCodesUsed)
					{
						this.p_ProjectCodeAdmin_ProjectSubCodesUsed = value;
						OnPropertyChanged("ProjectCodeAdmin_ProjectSubCodesUsed");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private void ProjectCodeAdmin_Loaded()
		{
			try
			{
				this.PreventCall = true;
				this.ProjectCodeAdmin_ProjectCodes = DataAccessLayer.ProjectCodes();
				this.ProjectCodeAdmin_ProjectSubCodes = DataAccessLayer.ProjectSubCodes();
				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void datagrid_ProjectCodeAdmin_ProjectCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;
				this.textbox_ProjectCodeAdmin_Description.Text = null;
				this.textbox_ProjectCodeAdmin_Value.Text = null;
				ProjectCodeAdmin_ProjectSubCodesUsed = null;

				if (e.AddedItems != null && e.AddedItems.Count > 0)
				{
					PersonalEntity.ProjectCode _Selected = (PersonalEntity.ProjectCode)e.AddedItems[0];

					if (_Selected != null)
					{
						this.PreventCall = true;
						this.textbox_ProjectCodeAdmin_Description.Text = _Selected.ProjectCodeDescription;
						this.textbox_ProjectCodeAdmin_Value.Text = _Selected.ProjectCodeValue;
						ProjectCodeAdmin_ProjectSubCodesUsed = new List<DataAccessLayer.ProjectSubCodesUsed>();

						foreach (PersonalEntity.ProjectSubCode fe_SubCode in this.ProjectCodeAdmin_ProjectSubCodes)
						{
							if (_Selected.SubCodes.FirstOrDefault(query => query.ProjectSubCodeID == fe_SubCode.ProjectSubCodeID) != null)
							{
								ProjectCodeAdmin_ProjectSubCodesUsed.Add(new DataAccessLayer.ProjectSubCodesUsed(fe_SubCode, true));
							}
							else
							{
								ProjectCodeAdmin_ProjectSubCodesUsed.Add(new DataAccessLayer.ProjectSubCodesUsed(fe_SubCode, false));
							}
						}

						this.PreventCall = false;
					}
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void button_ProjectCodeAdmin_Save_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;
				PersonalEntity.ProjectCode _ProjectCodeSave = null;

				if (this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems.Count == 1)
				{
					_ProjectCodeSave = (PersonalEntity.ProjectCode)this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems[0];

					if (ProjectCode_Validate(_ProjectCodeSave))
					{
						_ProjectCodeSave = DataAccessLayer.ProjectCode_Update(_ProjectCodeSave);
					}
				}
				else
				{
					if (ProjectCode_Validate())
					{
						_ProjectCodeSave = DataAccessLayer.ProjectCode_Add(this.textbox_ProjectCodeAdmin_Value.Text, this.textbox_ProjectCodeAdmin_Description.Text);
					}
				}

				ProjectCodeAdmin_Loaded();
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void button_ProjectCodeAdmin_Delete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;

				if (this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems.Count == 1)
				{
					DataAccessLayer.ProjectCode_Delete((PersonalEntity.ProjectCode)this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems[0]);
				}

				ProjectCodeAdmin_Loaded();
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private Boolean ProjectCode_Validate(PersonalEntity.ProjectCode _ProjectCode = null)
		{
			try
			{
				Boolean _Valid = true;

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_Description.Text))
				{
					_Valid = false;
				}

				this.textbox_ProjectCodeAdmin_Description.Text = this.textbox_ProjectCodeAdmin_Description.Text.Trim();

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_Description.Text))
				{
					_Valid = false;
				}

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_Value.Text))
				{
					_Valid = false;
				}

				this.textbox_ProjectCodeAdmin_Value.Text = this.textbox_ProjectCodeAdmin_Value.Text.Trim();

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_Value.Text))
				{
					_Valid = false;
				}

				if (_ProjectCode == null)
				{
					return _Valid;
				}

				return _Valid && !DataAccessLayer.ProjectCodeValue_Exists(this.textbox_ProjectCodeAdmin_Value.Text);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void datagrid_ProjectCodeAdmin_ProjectSubCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;
				this.textbox_ProjectCodeAdmin_SubDescription.Text = null;
				this.textbox_ProjectCodeAdmin_SubValue.Text = null;

				if (e.AddedItems != null && e.AddedItems.Count > 0)
				{
					DataAccessLayer.ProjectSubCodesUsed _Selected = (DataAccessLayer.ProjectSubCodesUsed)e.AddedItems[0];

					if (_Selected != null)
					{
						this.textbox_ProjectCodeAdmin_SubDescription.Text = _Selected.ProjectSubCodeDescription;
						this.textbox_ProjectCodeAdmin_SubValue.Text = _Selected.ProjectSubCodeValue;
					}
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		// DLB How To Get CheckBox Changed, I can catch when marked True but not when marked False?
		//private void ProjectCodeAdmin_ProjectSubCodesUsed_Click(object sender, RoutedEventArgs e)
		//{
		//	try
		//	{
		//		if (this.PreventCall)
		//		{
		//			return;
		//		}

		//		DataGridCell _Cell = (DataGridCell)sender;

		//		if (_Cell.Content.GetType() == typeof(CheckBox))
		//		{
		//			if (this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems.Count == 1 && this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems.Count == 1)
		//			{
		//				PersonalEntity.ProjectCode _Selected = (PersonalEntity.ProjectCode)this.datagrid_ProjectCodeAdmin_ProjectCodes.SelectedItems[0];
		//				DataAccessLayer.ProjectSubCodesUsed _SubSelected = (DataAccessLayer.ProjectSubCodesUsed)this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems[0];

		//				if (_SubSelected.ProjectSubCodeUsed)
		//				{
		//					//DataAccessLayer.ProjectCode_AddProjectSubCode(_Selected, _SubSelected.ProjectSubCode);
		//				}
		//				else
		//				{
		//					//DataAccessLayer.ProjectCode_RemoveProjectSubCode(_Selected, _SubSelected.ProjectSubCode);
		//				}

		//			}
		//		}
		//	}
		//	catch (Exception e_Exception)
		//	{
		//		throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
		//	}
		//}

		private void button_ProjectCodeAdmin_SubSave_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;
				PersonalEntity.ProjectSubCode _ProjectSubCodeSave = null;

				if (this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems.Count == 1)
				{
					_ProjectSubCodeSave = (PersonalEntity.ProjectSubCode)this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems[0];

					if (ProjectSubCode_Validate(_ProjectSubCodeSave))
					{
						_ProjectSubCodeSave = DataAccessLayer.ProjectSubCode_Update(_ProjectSubCodeSave);
					}
				}
				else
				{
					if (ProjectSubCode_Validate())
					{
						_ProjectSubCodeSave = DataAccessLayer.ProjectSubCode_Add(this.textbox_ProjectCodeAdmin_SubValue.Text, this.textbox_ProjectCodeAdmin_SubDescription.Text);
					}
				}

				ProjectCodeAdmin_Loaded();
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void button_ProjectCodeAdmin_SubDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;

				if (this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems != null && this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems.Count == 1)
				{
					DataAccessLayer.ProjectSubCode_Delete(((DataAccessLayer.ProjectSubCodesUsed)this.datagrid_ProjectCodeAdmin_ProjectSubCodes.SelectedItems[0]).ProjectSubCode);
				}

				ProjectCodeAdmin_Loaded();
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private Boolean ProjectSubCode_Validate(PersonalEntity.ProjectSubCode _ProjectSubCode = null)
		{
			try
			{
				Boolean _Valid = true;

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_SubDescription.Text))
				{
					_Valid = false;
				}

				this.textbox_ProjectCodeAdmin_SubDescription.Text = this.textbox_ProjectCodeAdmin_SubDescription.Text.Trim();

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_SubDescription.Text))
				{
					_Valid = false;
				}

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_SubValue.Text))
				{
					_Valid = false;
				}

				this.textbox_ProjectCodeAdmin_SubValue.Text = this.textbox_ProjectCodeAdmin_SubValue.Text.Trim();

				if (String.IsNullOrWhiteSpace(this.textbox_ProjectCodeAdmin_SubValue.Text))
				{
					_Valid = false;
				}

				if (_ProjectSubCode == null)
				{
					return _Valid;
				}

				return _Valid && !DataAccessLayer.ProjectSubCodeValue_Exists(this.textbox_ProjectCodeAdmin_SubValue.Text);
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
		#endregion

		#region Billed Time - Pending
		private void BilledTime_Loaded()
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
		#endregion

		#region Leave Time - Pending
		private void LeaveTime_Loaded()
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
		#endregion

		#region Time Sheet - Pending
		private void TimeSheet_Loaded()
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
		#endregion

		#region Employee List
		private List<PersonalEntity.PhoneList> p_EmployeeList_Entities;
		public List<PersonalEntity.PhoneList> EmployeeList_Entities
		{
			get
			{
				try
				{
					return this.p_EmployeeList_Entities;
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
					if (value != this.p_EmployeeList_Entities)
					{
						this.p_EmployeeList_Entities = value;
						OnPropertyChanged("EmployeeList_Entities");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private List<PersonalEntity.PhoneList> p_EmployeeList_Searched;
		public List<PersonalEntity.PhoneList> EmployeeList_Searched
		{
			get
			{
				try
				{
					return this.p_EmployeeList_Searched;
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
					if (value != this.p_EmployeeList_Searched)
					{
						this.p_EmployeeList_Searched = value;
						OnPropertyChanged("EmployeeList_Searched");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private void EmployeeList_Loaded()
		{
			try
			{
				this.PreventCall = true;

				if (this.EmployeeList_Entities == null || EmployeeList_Entities.Count == 0)
				{
					this.EmployeeList_Entities = DataAccessLayer.EmployeeList();
					this.EmployeeList_Searched = this.EmployeeList_Entities.ToList();
				}

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void textbox_EmployeeList_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;

				if (this.EmployeeList_Entities != null && EmployeeList_Entities.Count > 0)
				{
					this.EmployeeList_Searched = this.EmployeeList_Entities
						.Where(query =>
							query.Name.ToLower().Contains(this.textbox_EmployeeList_Search.Text)
							|| query.PhoneNumber.ToLower().Contains(this.textbox_EmployeeList_Search.Text)
							|| query.Department.ToLower().Contains(this.textbox_EmployeeList_Search.Text))
						.OrderBy(query => query.PhoneNumber).ToList();
				}
				else
				{
					this.EmployeeList_Searched = null;
				}
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Change Employee List Search Text", e_Exception);
			}
		}
		#endregion

		#region XML Format
		private List<String> p_XMLFormat_Results;
		public List<String> XMLFormat_Results
		{
			get
			{
				try
				{
					return this.p_XMLFormat_Results;
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
					if (value != this.p_XMLFormat_Results)
					{
						this.p_XMLFormat_Results = value;
						OnPropertyChanged("XMLFormat_Results");
					}
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		private void XMLFormat_Loaded()
		{
			try
			{
				this.PreventCall = true;

				this.PreventCall = false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		private void button_XMLFormat_OpenFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.PreventCall)
				{
					return;
				}

				e.Handled = true;
				Microsoft.Win32.OpenFileDialog l_OpenFile = new Microsoft.Win32.OpenFileDialog();
				l_OpenFile.DefaultExt = ".xml";
				l_OpenFile.Filter = "XML Files (*.xml;*.config)|*.xml;*.config";
				Nullable<Boolean> l_Result = l_OpenFile.ShowDialog();
				this.label_XMLFormat_FileName.Content = null;

				if (l_Result == true)
				{
					this.label_XMLFormat_FileName.Content = l_OpenFile.FileName;
					XMLFormat_Results_Add(XMLFormat.Format(this.label_XMLFormat_FileName.Content.ToString()));
				}
			}
			catch (Exception e_Exception)
			{
				ProgramMessage.Exception(MethodBase.GetCurrentMethod(), "Cannot Format XML", e_Exception);
			}
		}

		private void XMLFormat_Results_Add(List<String> _Results)
		{
			try
			{
				if (this.XMLFormat_Results == null)
				{
					this.XMLFormat_Results = new List<String>();
				}

				this.XMLFormat_Results.Clear();

				if (_Results == null || _Results.Count == 0)
				{
					return;
				}

				foreach (String fe_Result in _Results)
				{
					this.XMLFormat_Results.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " => " + fe_Result);
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
		#endregion
	}
}
