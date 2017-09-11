using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PersonalEntity;

namespace BilledTime
{
	/// <summary>
	/// Base Billed Time Entity
	/// </summary>
	public class BilledTimeEntity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BilledTimeEntity()
		{
			try
			{
				this.BilledTimeID = 0;
				this.BilledDate = DateTime.MinValue;
				this.BilledHours = 0;
				this.ProjectCode = null;
				this.ProjectCodeDescription = null;
				this.ProjectSubCode = null;
				this.ProjectSubCodeDescription = null;
				this.CombinedProjectCodeDescription = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Billed Time Collection
		/// </summary>
		/// <param name="_BilledTime">Personal Entities Billed Time Entity</param>
		public BilledTimeEntity(PersonalEntity.BilledTime _BilledTime)
		{
			try
			{
				this.BilledTimeID = _BilledTime.BilledTimeID;
				this.BilledDate = _BilledTime.BilledDate;
				this.BilledHours = _BilledTime.BilledHours;
				this.ProjectCode = _BilledTime.ProjectCode.ProjectCodeValue;
				this.ProjectCodeDescription = _BilledTime.ProjectCode.ProjectCodeDescription;
				this.ProjectSubCode = _BilledTime.ProjectSubCode.ProjectSubCodeValue;
				this.ProjectSubCodeDescription = _BilledTime.ProjectSubCode.ProjectSubCodeDescription;
				this.CombinedProjectCodeDescription = this.ProjectCode + "-" + this.ProjectSubCode + ": " + this.ProjectCodeDescription + "-" + this.ProjectSubCodeDescription;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time ID
		/// Primary Key
		/// </summary>
		public Int32 BilledTimeID
		{
			get;

			set;
		}

		/// <summary>
		/// Date
		/// </summary>
		public DateTime BilledDate
		{
			get;

			set;
		}

		/// <summary>
		/// Hours
		/// </summary>
		public Double BilledHours
		{
			get;

			set;
		}

		/// <summary>
		/// Notes
		/// </summary>
		public String Notes
		{
			get;

			set;
		}

		/// <summary>
		/// Project Code ID
		/// Foreign Key
		/// </summary>
		public Int32 ProjectCodeID
		{
			get;

			set;
		}

		/// <summary>
		/// Project Code Value
		/// </summary>
		public String ProjectCode
		{
			get;

			set;
		}

		/// <summary>
		/// Proejct Code Description
		/// </summary>
		public String ProjectCodeDescription
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code ID
		/// Foreign Key
		/// </summary>
		public Int32 ProjectSubCodeID
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code Value
		/// </summary>
		public String ProjectSubCode
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code Description
		/// </summary>
		public String ProjectSubCodeDescription
		{
			get;

			set;
		}

		/// <summary>
		/// Display Project Code And Project Sub Code, Value And Description
		/// </summary>
		public String CombinedProjectCodeDescription
		{
			get;

			set;
		}
	}

	/// <summary>
	/// Base Project Code Entity
	/// </summary>
	public class ProjectCodeEntity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ProjectCodeEntity()
		{
			try
			{
				this.ProjectCodeID = 0;
				this.ProjectCodeValue = null;
				this.ProjectCodeDescription = null;
				this.ProjectSubCodes = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Project Code
		/// </summary>
		/// <param name="_ProjectCode">Personal Entities Project Code Entity</param>
		public ProjectCodeEntity(PersonalEntity.ProjectCode _ProjectCode)
		{
			try
			{
				this.ProjectCodeID = _ProjectCode.ProjectCodeID;
				this.ProjectCodeValue = _ProjectCode.ProjectCodeValue;
				this.ProjectCodeDescription = _ProjectCode.ProjectCodeDescription;
				this.ProjectSubCodes = _ProjectCode.SubCodes.Select(query => new ProjectSubCodeEntity(query)).ToList();
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Project Code ID
		/// Primary Key
		/// </summary>
		public Int32 ProjectCodeID
		{
			get;

			set;
		}

		/// <summary>
		/// Project Code Value
		/// </summary>
		public String ProjectCodeValue
		{
			get;

			set;
		}

		/// <summary>
		/// Project Code Description
		/// </summary>
		public String ProjectCodeDescription
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code Collection For This Project Code
		/// </summary>
		public List<ProjectSubCodeEntity> ProjectSubCodes
		{
			get;

			set;
		}
	}

	/// <summary>
	/// Base Project Sub Code Entity
	/// </summary>
	public class ProjectSubCodeEntity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ProjectSubCodeEntity()
		{
			try
			{
				this.ProjectSubCodeID = 0;
				this.ProjectSubCodeValue = null;
				this.ProjectSubCodeDescription = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Project Sub Code
		/// </summary>
		/// <param name="_ProjectSubCode">Personal Entities Project Sub Code Entity</param>
		public ProjectSubCodeEntity(PersonalEntity.ProjectSubCode _ProjectSubCode)
		{
			try
			{
				this.ProjectSubCodeID = _ProjectSubCode.ProjectSubCodeID;
				this.ProjectSubCodeValue = _ProjectSubCode.ProjectSubCodeValue;
				this.ProjectSubCodeDescription = _ProjectSubCode.ProjectSubCodeDescription;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Project Sub Code ID
		/// Primary Key
		/// </summary>
		public Int32 ProjectSubCodeID
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code Value
		/// </summary>
		public String ProjectSubCodeValue
		{
			get;

			set;
		}

		/// <summary>
		/// Project Sub Code Description
		/// </summary>
		public String ProjectSubCodeDescription
		{
			get;

			set;
		}
	}

	/// <summary>
	/// Billed Time Grouped By Project Code And Project Sub Code
	/// </summary>
	public class ProjectGroupedBilledTime
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ProjectGroupedBilledTime()
		{
			try
			{
				this.BilledTimes = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Billed Time Collection
		/// </summary>
		/// <param name="_BilledTimes">List of Billed Time Entities</param>
		public ProjectGroupedBilledTime(List<BilledTimeEntity> _BilledTimes)
		{
			try
			{
				this.BilledTimes = _BilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Collection
		/// </summary>
		public List<BilledTimeEntity> BilledTimes
		{
			get;

			set;
		}

		/// <summary>
		/// Summed Hours From Billed Time Collection
		/// </summary>
		private Double _TotalHours;
		public Double TotalHours
		{
			get
			{
				try
				{
					this._TotalHours = 0;

					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._TotalHours = this.BilledTimes.Sum(query => query.BilledHours);
					}

					return this._TotalHours;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Display Project Code And Project Sub Code, Value And Description
		/// </summary>
		private String _CombinedProjectCodeDescription;
		public String CombinedProjectCodeDescription
		{
			get
			{
				try
				{
					this._CombinedProjectCodeDescription = null;

					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._CombinedProjectCodeDescription = this.BilledTimes[0].CombinedProjectCodeDescription;
					}

					return this._CombinedProjectCodeDescription;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}
	}

	/// <summary>
	/// Billed Time Grouped By Date
	/// </summary>
	public class DateGroupedBilledTime
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public DateGroupedBilledTime()
		{
			try
			{
				this._Date = null;
				this.BilledTimes = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Blank Date Time
		/// </summary>
		/// <param name="_DateBlank">Date used to create 'blank' Entity</param>
		public DateGroupedBilledTime(DateTime _DateBlank)
		{
			try
			{
				this.BilledTimes = null;
				this._Date = _DateBlank;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Billed Time Collection
		/// </summary>
		/// <param name="_BilledTimes">List of Billed Time Entities</param>
		public DateGroupedBilledTime(List<BilledTimeEntity> _BilledTimes)
		{
			try
			{
				this._Date = null;
				this.BilledTimes = _BilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Collection
		/// </summary>
		public List<BilledTimeEntity> BilledTimes
		{
			get;

			set;
		}

		/// <summary>
		/// Date
		/// </summary>
		private Nullable<DateTime> _Date;
		public Nullable<DateTime> Date
		{
			get
			{
				try
				{
					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._Date = null;
						this._Date = this.BilledTimes.FirstOrDefault().BilledDate.Date;
					}

					return this._Date;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Summed Hours From Billed Time Collection
		/// </summary>
		private Double _TotalHours;
		public Double TotalHours
		{
			get
			{
				try
				{
					this._TotalHours = 0;

					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._TotalHours = this.BilledTimes.Sum(query => query.BilledHours);
					}

					return this._TotalHours;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}

		/// <summary>
		/// Billed Time Grouped By Project Code Collection
		/// </summary>
		private List<ProjectGroupedBilledTime> _ProjectGroupedBilledTimes;
		public List<ProjectGroupedBilledTime> ProjectGroupedBilledTimes
		{
			get
			{
				try
				{
					this._ProjectGroupedBilledTimes = null;

					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._ProjectGroupedBilledTimes = (from BT in this.BilledTimes
													group BT by BT.CombinedProjectCodeDescription into BTG
													orderby BTG.Key
													select new ProjectGroupedBilledTime
													{
														BilledTimes = BTG.ToList(),
													}).ToList();
					}

					return this._ProjectGroupedBilledTimes;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}
	}

	/// <summary>
	/// Billed Time Grouped By Week
	/// </summary>
	public class WeekGroupBilledTime
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WeekGroupBilledTime()
		{
			try
			{
				this.BilledTimes = null;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Billed Time Collection
		/// </summary>
		/// <param name="_BilledTimes">List of Billed Time Entities</param>
		public WeekGroupBilledTime(List<BilledTimeEntity> _BilledTimes)
		{
			try
			{
				this.BilledTimes = _BilledTimes;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Billed Time Collection
		/// </summary>
		public List<BilledTimeEntity> BilledTimes
		{
			get;

			set;
		}

		/// <summary>
		/// Billed Time Grouped By Project Code Collection
		/// </summary>
		private List<ProjectGroupedBilledTime> _ProjectGroupedBilledTimes;
		public List<ProjectGroupedBilledTime> ProjectGroupedBilledTimes
		{
			get
			{
				try
				{
					this._ProjectGroupedBilledTimes = null;

					if (this.BilledTimes != null && this.BilledTimes.Count > 0)
					{
						this._ProjectGroupedBilledTimes = (from BT in this.BilledTimes
													group BT by BT.CombinedProjectCodeDescription into BTG
													orderby BTG.Key
													select new ProjectGroupedBilledTime
													{
														BilledTimes = BTG.ToList(),
													}).ToList();
					}

					return this._ProjectGroupedBilledTimes;
				}
				catch (Exception general_Exception)
				{
					throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
				}
			}
		}
	}

	/// <summary>
	/// Leave Entity
	/// </summary>
	public class LeaveEntity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LeaveEntity()
		{
			try
			{
				this.LeaveID = 0;
				this.LeaveTypeID = 0;
				this.LeaveTypeDescription = null;
				this.LeaveDate = DateTime.MinValue;
				this.LeaveHours = 0;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Constructor With Leave Entity
		/// </summary>
		/// <param name="_Leave">Personal Entity Leave</param>
		public LeaveEntity(PersonalEntity.Leave _Leave)
		{
			try
			{
				this.LeaveID = _Leave.LeaveID;
				this.LeaveTypeID = _Leave.LeaveTypeID;
				this.LeaveTypeDescription = _Leave.LeaveType.LeaveTypeDescription;
				this.LeaveDate = _Leave.LeaveDate;
				this.LeaveHours = _Leave.LeaveHours;
			}
			catch (Exception general_Exception)
			{
				throw ProgramMessage.Throw(MethodBase.GetCurrentMethod(), general_Exception);
			}
		}

		/// <summary>
		/// Leave ID
		/// Primary Key
		/// </summary>
		public Int32 LeaveID
		{
			get;

			set;
		}

		/// <summary>
		/// Leave Type ID
		/// Foreign Key
		/// </summary>
		public Int32 LeaveTypeID
		{
			get;

			set;
		}

		/// <summary>
		/// Leave Type Description
		/// </summary>
		public String LeaveTypeDescription
		{
			get;

			set;
		}

		/// <summary>
		/// Date
		/// </summary>
		public DateTime LeaveDate
		{
			get;

			set;
		}

		/// <summary>
		/// Hours
		/// </summary>
		public Double LeaveHours
		{
			get;

			set;
		}
	}

	/// <summary>
	/// Leave Type Enum
	/// </summary>
	public enum LeaveTypes
	{
		Absence = 0,
		Emergency = 1,
		Holiday = 2,
		Jury = 3,
		Personal = 4,
		Vacation = 5,
	}
}
