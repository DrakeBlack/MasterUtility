using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace RunAll
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Button button_Sender = (Button)sender;

			switch (button_Sender.Name)
			{
				case "button_Exit":
					Application.Current.Shutdown();
					break;
				case "button_GetLatest":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\DrakeBlack\RunAll\GetLatest.bat");
					break;
				case "button_SourceSafe":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\Program Files (x86)\Microsoft Visual Studio\Common\VSS\win32\SSEXP.EXE");
					break;
				case "button_VS_D21Admin":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21Admin\D21Admin.vbp");
					break;
				case "button_VS_D21Incident":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21Incident\D21Incident.vbp");
					break;
				case "button_VS_D21DBUtil":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21DbUtil\D21DbUtil.sln");
					break;
				case "button_VS_D21Server":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21Server\D21Server.sln");
					break;
				case "button_D21Server":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21Server\bin\D21Server.exe");
					break;
				case "button_D21Admin":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21.bin\D21Admin.exe");
					break;
				case "button_D21Incident":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21.bin\D21Incident.exe");
					break;
				case "button_D21DBUtil":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21DbUtil\bin\DbUtil.exe");
					break;
				case "button_D21Config":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\D21\D21Config\D21Config.exe");
					break;
				case "button_VS_DBConfigurator":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Products\DbConfigurator\DbConfigurator.sln");
					break;
				case "button_VS_Angus":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Products\Angus\Angus.sln");
					break;
				case "button_VS_AdminViewers":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Viewers\AdminViewers.sln");
					break;
				case "button_VS_Alpha":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Data\Alpha\Monaco.Common.Data.Alpha.sln");
					break;
				case "button_VS_Gamma":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Data\Gamma\Monaco.Common.Data.Gamma.sln");
					break;
				case "button_VS_Shared":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Shared\Shared.sln");
					break;
				case "button_VS_Controls":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Controls\Controls.sln");
					break;
				case "button_VS_AdvancedControls":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Controls\AdvancedControls.sln");
					break;
				case "button_VS_Dialogs":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Dialogs\Dialogs.sln");
					break;
				case "button_VS_AdvancedDialogs":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Dialogs\AdvancedDialogs.sln");
					break;
				case "button_VS_Reports":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Reports\Reports.sln");
					break;
				case "button_VS_Viewers":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Viewers\Viewers.sln");
					break;
				case "button_VS_AdvancedViewers":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Viewers\AdvancedViewers.sln");
					break;
				case "button_VS_AttributeViewers":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Viewers\AttributeViewers.sln");
					break;
				case "button_VS_EntityViewers":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Common\Viewers\EntityViewers.sln");
					break;
				case "button_Angus":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Products\Angus\Angus\bin\Release\LE-21.exe");
					break;
				case "button_DBConfigurator":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Products\DbConfigurator\DbConfigurator\bin\Release\DBConfigurator.exe");
					break;
				case "button_CodeSmith":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\Program Files (x86)\CodeSmith\v4.1\CodeSmithStudio.exe");
					break;
				case "button_PurpleRain":
					Process.Start(@"C:\Windows\explorer.exe", @"C:\MMSPrototype\Products\PurpleRain\PurpleRain\bin\Release\MMSAdmin.exe");
					break;
				default:

					break;
			}
		}
	}
}