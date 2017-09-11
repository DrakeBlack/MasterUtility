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
	public static class DataAccessLayer
	{
		public class ProjectSubCodesUsed
		{
			public ProjectSubCodesUsed(PersonalEntity.ProjectSubCode _ProjectSubCode, Boolean _ProjectSubCodeUsed)
			{
				this.ProjectSubCode = _ProjectSubCode;
				this.ProjectSubCodeUsed = _ProjectSubCodeUsed;
			}

			public PersonalEntity.ProjectSubCode ProjectSubCode
			{
				get;

				private set;
			}

			public Boolean ProjectSubCodeUsed
			{
				get;

				set;
			}

			public String ProjectSubCodeValue
			{
				get
				{
					if (this.ProjectSubCode != null)
					{
						return this.ProjectSubCode.ProjectSubCodeValue;
					}
					else
					{
						return null;
					}
				}
			}

			public String ProjectSubCodeDescription
			{
				get
				{
					if (this.ProjectSubCode != null)
					{
						return this.ProjectSubCode.ProjectSubCodeDescription;
					}
					else
					{
						return null;
					}
				}
			}
		}

		public static List<PersonalEntity.PhoneList> EmployeeList()
		{
			try
			{
				List<PersonalEntity.PhoneList> l_EmployeeList = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					l_EmployeeList = (from EL in Entity.PhoneLists
								   orderby EL.PhoneNumber, EL.Name, EL.Department
								   select EL).ToList();
				}

				return l_EmployeeList;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static List<PersonalEntity.ProjectCode> ProjectCodes()
		{
			try
			{
				List<PersonalEntity.ProjectCode> l_ProjectCodes = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					l_ProjectCodes = (from PC in Entity.ProjectCodes.Include("SubCodes")
								   orderby PC.ProjectCodeValue
								   select PC).ToList();
				}

				return l_ProjectCodes;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectCode ProjectCode_Add(String _Value, String _Description, List<PersonalEntity.ProjectSubCode> _ProjectSubCodes = null)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(_Value) || String.IsNullOrWhiteSpace(_Description))
				{
					throw new Exception("Invalid Project Code Data");
				}

				PersonalEntity.ProjectCode _Add = new ProjectCode();

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Add.ProjectCodeDescription = _Description;
					_Add.ProjectCodeValue = _Value;

					if (_ProjectSubCodes != null && _ProjectSubCodes.Count > 0)
					{
						foreach (PersonalEntity.ProjectSubCode fe_SubCode in _ProjectSubCodes)
						{
							_Add.SubCodes.Add(fe_SubCode);
						}
					}

					Entity.ProjectCodes.Add(_Add);
					Entity.SaveChanges();
				}

				return _Add;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectCode ProjectCode_Update(PersonalEntity.ProjectCode _ProjectCode, List<PersonalEntity.ProjectSubCode> _ProjectSubCodes = null)
		{
			try
			{
				if (_ProjectCode == null)
				{
					throw new Exception("Invalid Project Code");
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					if (_ProjectSubCodes != null && _ProjectSubCodes.Count > 0)
					{
						_ProjectCode.SubCodes.Clear();

						foreach (PersonalEntity.ProjectSubCode fe_SubCode in _ProjectSubCodes)
						{
							_ProjectCode.SubCodes.Add(fe_SubCode);
						}
					}

					Entity.ProjectCodes.Attach(_ProjectCode);
					var _ProjectCodeChange = Entity.Entry(_ProjectCode);
					_ProjectCodeChange.State = EntityState.Modified;
					Entity.SaveChanges();
				}

				return _ProjectCode;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectCode ProjectCode_AddProjectSubCode(PersonalEntity.ProjectCode _ProjectCode, PersonalEntity.ProjectSubCode _ProjectSubCode)
		{
			try
			{
				if (_ProjectCode == null)
				{
					throw new Exception("Invalid Project Code");
				}

				if (_ProjectSubCode == null)
				{
					throw new Exception("Invalid Project Sub Code");
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_ProjectCode.SubCodes.Add(_ProjectSubCode);
					Entity.ProjectCodes.Attach(_ProjectCode);
					var _ProjectCodeChange = Entity.Entry(_ProjectCode);
					_ProjectCodeChange.State = EntityState.Modified;
					Entity.SaveChanges();
				}

				return _ProjectCode;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectCode ProjectCode_RemoveProjectSubCode(PersonalEntity.ProjectCode _ProjectCode, PersonalEntity.ProjectSubCode _ProjectSubCode)
		{
			try
			{
				if (_ProjectCode == null)
				{
					throw new Exception("Invalid Project Code");
				}

				if (_ProjectSubCode == null)
				{
					throw new Exception("Invalid Project Sub Code");
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_ProjectCode.SubCodes.Remove(_ProjectSubCode);
					Entity.ProjectCodes.Attach(_ProjectCode);
					var _ProjectCodeChange = Entity.Entry(_ProjectCode);
					_ProjectCodeChange.State = EntityState.Modified;
					Entity.SaveChanges();
				}

				return _ProjectCode;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static Boolean ProjectCodeValue_Exists(String _ProjectCodeValue)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(_ProjectCodeValue))
				{
					return false;
				}

				_ProjectCodeValue = _ProjectCodeValue.Trim();

				if (String.IsNullOrWhiteSpace(_ProjectCodeValue))
				{
					return false;
				}

				_ProjectCodeValue = _ProjectCodeValue.Trim().ToUpper();

				using (PersonalEntities Entity = new PersonalEntities())
				{
					return Entity.ProjectCodes.Any(query => query.ProjectCodeValue == _ProjectCodeValue);
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static Boolean ProjectCode_Delete(PersonalEntity.ProjectCode _ProjectCode)
		{
			try
			{
				if (_ProjectCode != null)
				{
					using (PersonalEntities Entity = new PersonalEntities())
					{
						Entity.ProjectCodes.Remove(Entity.ProjectCodes.FirstOrDefault(query => query.ProjectCodeID == _ProjectCode.ProjectCodeID));
						Entity.SaveChanges();
					}

					return true;
				}

				return false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static List<PersonalEntity.ProjectSubCode> ProjectSubCodes()
		{
			try
			{
				List<PersonalEntity.ProjectSubCode> l_ProjectSubCodes = null;

				using (PersonalEntities Entity = new PersonalEntities())
				{
					l_ProjectSubCodes = (from PSC in Entity.ProjectSubCodes
								   orderby PSC.ProjectSubCodeValue
								   select PSC).ToList();
				}

				return l_ProjectSubCodes;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectSubCode ProjectSubCode_Add(String _Value, String _Description)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(_Value) || String.IsNullOrWhiteSpace(_Description))
				{
					throw new Exception("Invalid Project Sub Code Data");
				}

				PersonalEntity.ProjectSubCode _Add = new ProjectSubCode();

				using (PersonalEntities Entity = new PersonalEntities())
				{
					_Add.ProjectSubCodeDescription = _Description;
					_Add.ProjectSubCodeValue = _Value;
					Entity.ProjectSubCodes.Add(_Add);
					Entity.SaveChanges();
				}

				return _Add;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static PersonalEntity.ProjectSubCode ProjectSubCode_Update(PersonalEntity.ProjectSubCode _ProjectSubCode)
		{
			try
			{
				if (_ProjectSubCode == null)
				{
					throw new Exception("Invalid Project Sub Code");
				}

				using (PersonalEntities Entity = new PersonalEntities())
				{
					Entity.ProjectSubCodes.Attach(_ProjectSubCode);
					var _ProjectSubCodeChange = Entity.Entry(_ProjectSubCode);
					_ProjectSubCodeChange.State = EntityState.Modified;
					Entity.SaveChanges();
				}

				return _ProjectSubCode;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static Boolean ProjectSubCode_Delete(PersonalEntity.ProjectSubCode _ProjectSubCode)
		{
			try
			{
				if (_ProjectSubCode != null)
				{
					using (PersonalEntities Entity = new PersonalEntities())
					{
						Entity.ProjectSubCodes.Remove(Entity.ProjectSubCodes.FirstOrDefault(query => query.ProjectSubCodeID == _ProjectSubCode.ProjectSubCodeID));
						Entity.SaveChanges();
					}

					return true;
				}

				return false;
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}

		public static Boolean ProjectSubCodeValue_Exists(String _ProjectSubCodeValue)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(_ProjectSubCodeValue))
				{
					return false;
				}

				_ProjectSubCodeValue = _ProjectSubCodeValue.Trim();

				if (String.IsNullOrWhiteSpace(_ProjectSubCodeValue))
				{
					return false;
				}

				_ProjectSubCodeValue = _ProjectSubCodeValue.Trim().ToUpper();

				using (PersonalEntities Entity = new PersonalEntities())
				{
					return Entity.ProjectSubCodes.Any(query => query.ProjectSubCodeValue == _ProjectSubCodeValue);
				}
			}
			catch (Exception e_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), e_Exception);
			}
		}
	}
}
