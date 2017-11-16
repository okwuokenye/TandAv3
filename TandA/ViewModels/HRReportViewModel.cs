using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TandA.Models;
using TandA.DALs;
using TandA.ViewModels;
using System.Windows.Input;
using System.Data;

namespace TandA.ViewModels
{
    class HRReportViewModel : ObservableObject
    {
        #region Event declarations

        #endregion

        #region Private variables
        DALAdmin AdminDAL = new DALAdmin();
        DALEmployee EmployeeDAL = new DALEmployee();
        Visibility _WindowLoaderVisibility = Visibility.Collapsed;

        ObservableCollection<AbsenteeismModel> _ACodes = new ObservableCollection<AbsenteeismModel>();
        ObservableCollection<EmployeeModel> _Employees = new ObservableCollection<EmployeeModel>();


        ObservableCollection<PeriodModel> _Periods = new ObservableCollection<PeriodModel>();
        ObservableCollection<PeriodModel> _PeriodsToUse = new ObservableCollection<PeriodModel>();

        ObservableCollection<HRReportModel> _HRRItems = new ObservableCollection<HRReportModel>();
        PeriodModel _Period;
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Orders " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        public ObservableCollection<PeriodModel> Periods
        {
            get { return _Periods; }
        }

        public PeriodModel Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                }
            }
        }

        public ObservableCollection<HRReportModel> HRRItems
        {
            get { return _HRRItems; }
        }
        #endregion

        #region Constructors

        public HRReportViewModel()
        {
            try
            {
                Load_Async();
                // FillTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".HRReportViewModel\n" + ex.Message, "Error");
            }
        }
        #endregion


        #region Private functions
        async void Load_Async()
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");


                await Task.Run(() =>
                {
                    _Employees = EmployeeDAL.GetEmployees();
                    _Periods = AdminDAL.GetPeriods();
                });

                //Raise property changed for every property in view model
                foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties())
                {
                    RaisePropertyChanged(p.Name);
                }
                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".Load_Async\n" + ex.Message, "Error");
            }
        }

        private Decimal CalculatePunches(String p_EmployeeRef, Int32 p_Week)
        {
            Decimal l_Hours = 0;
            try
            {
                _PeriodsToUse = AdminDAL.GetPeriods();

                Int32 l_CurrentP = _PeriodsToUse.Where(m => m.StartDate == _Period.StartDate).FirstOrDefault().ID;

                PeriodModel l_Period_Week_Two = _PeriodsToUse.Where(m => m.ID == l_CurrentP + 1).FirstOrDefault();

                ObservableCollection<PunchesModel> _Punches = new ObservableCollection<PunchesModel>();

                if (p_Week == 1)
                {
                    _Punches = AdminDAL.GetEmployeePunches(p_EmployeeRef, _Period.ID);
                }

                if (p_Week == 2 && l_Period_Week_Two != null)
                {
                    _Punches = AdminDAL.GetEmployeePunches(p_EmployeeRef, l_Period_Week_Two.ID);
                }


                List<PunchesModel> l_ClockIns = _Punches.Where(m => m.PunchType == "1").ToList();
                List<PunchesModel> l_ClockOuts = _Punches.Where(m => m.PunchType == "2").ToList();

                DateTime l_FirstIn;
                DateTime l_NextIn;
                Int32 l_Increment = 0;

                foreach (var _c in l_ClockIns)
                {
                    l_FirstIn = l_ClockIns[l_Increment].PunchDate;

                    //check if another clockin exists on the same day
                    if (l_ClockIns.Where(m => m.PunchDate.Date == l_FirstIn.Date).Count() > l_Increment + 1)
                    {
                        l_NextIn = l_ClockIns[l_Increment + 1].PunchDate;
                        var l_PunchOut = l_ClockOuts.Where(m => m.PunchDate > l_FirstIn && m.PunchDate < l_NextIn).FirstOrDefault();
                        if (l_PunchOut != null)//if puch out exists
                        {
                            l_Hours += CalculateHoursBeforeBreak(l_FirstIn, l_PunchOut.PunchDate);
                            l_Hours += CalculateHoursAfterBreak(l_FirstIn, l_PunchOut.PunchDate);
                        }
                    }
                    else //no other punch in for the day
                    {
                        var l_PunchOut = l_ClockOuts.Where(m => m.PunchDate > l_FirstIn).FirstOrDefault();
                        if (l_PunchOut != null)//if puch out exists
                        {
                            l_Hours += CalculateHoursBeforeBreak(l_FirstIn, l_PunchOut.PunchDate);
                            l_Hours += CalculateHoursAfterBreak(l_FirstIn, l_PunchOut.PunchDate);
                        }
                    }
                    l_Increment++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CalculatePunches\n" + ex.Message, "Error");
            }

            return l_Hours;
        }

        private Decimal CalculateAbsenteeism(String p_EmployeeRef, Int32 p_Week, AbsenteeismModel p_Absent)
        {
            Decimal l_Hours = 0;
            try
            {
                _PeriodsToUse = AdminDAL.GetPeriods();

                Int32 l_CurrentP = _PeriodsToUse.Where(m => m.StartDate == _Period.StartDate).FirstOrDefault().ID;

                PeriodModel l_Period_Week_Two = _PeriodsToUse.Where(m => m.ID == l_CurrentP + 1).FirstOrDefault();

                List<EmployeeAbsenteeismModel> _Absents; // = new List<EmployeeAbsenteeismModel>();

                if (p_Week == 1)
                {
                    _Absents = AdminDAL.GetEmployeeHRRAbsenteeism(p_EmployeeRef, _Period.ID).Where(m => m.AbsentRef == p_Absent.Reference && m.IsPaid).ToList();
                    if (_Absents != null && _Absents.Count > 0)
                    {
                        foreach (var _c in _Absents)
                        {
                            if (_c.DateAbsent >= _Period.StartDate && _c.TimeTo < _Period.StartDate.AddDays(7))
                            {
                                //vacation was within current period. Calculate hours
                                l_Hours += CalculateHoursBeforeBreak(_c.DateAbsent, _c.TimeTo);
                                l_Hours += CalculateHoursAfterBreak(_c.DateAbsent, _c.TimeTo);
                            }
                            else if (_c.DateAbsent < _Period.StartDate && _c.TimeTo < _Period.StartDate.AddDays(7))
                            {
                                //vacation started in previous period but ended in current period
                                l_Hours += CalculateHoursBeforeBreak(_Period.StartDate, _c.TimeTo);
                                l_Hours += CalculateHoursAfterBreak(_Period.StartDate, _c.TimeTo);
                            }
                            else if (_c.DateAbsent >= _Period.StartDate && _c.TimeTo > _Period.StartDate.AddDays(7))
                            {
                                //vacation started in current period but ended in a period beyond current period
                                l_Hours += CalculateHoursBeforeBreak(_c.DateAbsent, _Period.StartDate.AddDays(7));
                                l_Hours += CalculateHoursAfterBreak(_c.DateAbsent, _Period.StartDate.AddDays(7));
                            }
                            else if (_c.DateAbsent < _Period.StartDate && _c.TimeTo > _Period.StartDate.AddDays(7))
                            {
                                //vacation started in previous period and ended in a period beyond current period
                                //add full period length
                                l_Hours += CalculateHoursBeforeBreak(_Period.StartDate, _Period.StartDate.AddDays(7));
                                l_Hours += CalculateHoursAfterBreak(_Period.StartDate, _Period.StartDate.AddDays(7));
                            }
                        }
                    }
                }

                if (p_Week == 2)
                {
                    _Absents = AdminDAL.GetEmployeeHRRAbsenteeism(p_EmployeeRef, l_Period_Week_Two.ID).Where(m => m.AbsentRef == p_Absent.Reference).ToList();
                    DateTime l_Period_NextWeek_Two = l_Period_Week_Two.StartDate.AddDays(7);
                    if (_Absents != null && _Absents.Count > 0)
                    {
                        foreach (var _c in _Absents)
                        {
                            if (_c.DateAbsent >= l_Period_Week_Two.StartDate && _c.TimeTo < l_Period_Week_Two.StartDate.AddDays(7))
                            {
                                //vacation was within current period. Calculate hours
                                l_Hours += CalculateHoursBeforeBreak(_c.DateAbsent, _c.TimeTo);
                                l_Hours += CalculateHoursAfterBreak(_c.DateAbsent, _c.TimeTo);
                            }
                            else if (_c.DateAbsent < l_Period_Week_Two.StartDate && _c.TimeTo < l_Period_NextWeek_Two)
                            {
                                //vacation started in previous period but ended in current period
                                l_Hours += CalculateHoursBeforeBreak(l_Period_Week_Two.StartDate, _c.TimeTo);
                                l_Hours += CalculateHoursAfterBreak(l_Period_Week_Two.StartDate, _c.TimeTo);
                            }
                            else if (_c.DateAbsent >= l_Period_Week_Two.StartDate && _c.TimeTo > l_Period_NextWeek_Two)
                            {
                                //vacation started in current period but ended in a period beyond current period
                                l_Hours += CalculateHoursBeforeBreak(_c.DateAbsent, l_Period_Week_Two.StartDate.AddDays(7));
                                l_Hours += CalculateHoursAfterBreak(_c.DateAbsent, l_Period_Week_Two.StartDate.AddDays(7));
                            }
                            else if (_c.DateAbsent < l_Period_Week_Two.StartDate && _c.TimeTo > l_Period_Week_Two.StartDate.AddDays(7))
                            {
                                //vacation started in previous period and ended in a period beyond current period
                                //add full period length
                                l_Hours += CalculateHoursBeforeBreak(l_Period_Week_Two.StartDate, l_Period_Week_Two.StartDate.AddDays(7));
                                l_Hours += CalculateHoursAfterBreak(l_Period_Week_Two.StartDate, l_Period_Week_Two.StartDate.AddDays(7));
                            }
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                // MessageBox.Show(this.ToString() + ".CalculateAbsenteeism\n" + ex.Message, "Error");
            }

            return l_Hours;
        }

        private Decimal CalculateHoursBeforeBreak(DateTime p_Start, DateTime p_End)
        {
            DateTime incidentStart = p_Start;
            DateTime incidentEnd = p_End;
            int minutes = (int)(incidentEnd - incidentStart).TotalMinutes;
            TimeSpan officeOpen = TimeSpan.FromHours(6);
            TimeSpan officeClosed = TimeSpan.FromHours(12);
            Decimal numHours = Enumerable.Range(0, minutes)
                .Select(min => incidentStart.AddMinutes(min))
                .Where(dt => dt.TimeOfDay >= officeOpen && dt.TimeOfDay < officeClosed)
                .GroupBy(dt => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, 0)) // round to hour
                .Count();

            return numHours;
        }

        private Decimal CalculateHoursAfterBreak(DateTime p_Start, DateTime p_End)
        {
            DateTime incidentStart = p_Start;
            DateTime incidentEnd = p_End;
            int minutes = (int)(incidentEnd - incidentStart).TotalMinutes;
            TimeSpan officeOpen = TimeSpan.FromHours(Convert.ToDouble("12.5"));
            TimeSpan officeClosed = TimeSpan.FromHours(Convert.ToDouble("14.5"));
            Decimal numHours = Enumerable.Range(0, minutes)
                .Select(min => incidentStart.AddMinutes(min))
                .Where(dt => dt.TimeOfDay >= officeOpen && dt.TimeOfDay < officeClosed)
                .GroupBy(dt => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0)) // round to minute
                .Count();

            return numHours / 60;
        }
        #endregion

        #region Public Functions
        public void FillTable()
        {
            try
            {
                ObservableCollection<GroupModel> _Groups = AdminDAL.GetGroups();

                foreach (var _g in _Groups)
                {
                    _ACodes = AdminDAL.GetACodes();
                    _Employees = EmployeeDAL.GetEmployeesInGroup(_g.GroupRef);

                    DataTable _HRR = new DataTable();

                    _HRR.Columns.Add("Employees", typeof(string));
                    _HRR.Columns.Add("Week 1", typeof(string));
                    _HRR.Columns.Add("Overtime 1", typeof(string));

                    foreach (AbsenteeismModel _a in _ACodes)
                    {
                        _HRR.Columns.Add(_a.Abbreviation + " 1", typeof(string));
                    }

                    _HRR.Columns.Add("Week 2", typeof(string));
                    _HRR.Columns.Add("Overtime 2", typeof(string));

                    foreach (AbsenteeismModel _a in _ACodes)
                    {
                        _HRR.Columns.Add(_a.Abbreviation + " 2", typeof(string));
                    }

                    _HRR.Columns.Add("Total", typeof(string));

                    Decimal l_Total = 0;
                    Decimal l_Abs = 0;
                    Decimal l_Week1 = 0;
                    Decimal l_Week2 = 0;

                    foreach (EmployeeModel _e in _Employees)
                    {
                        DataRow dr1 = _HRR.NewRow();
                        dr1["Employees"] = _e.Firstname + " " + _e.Lastname;

                        l_Week1 = CalculatePunches(_e.EmployeeNumber, 1);

                        dr1["Week 1"] = l_Week1 > 40 ? "40" : l_Week1.ToString();

                        l_Total += l_Week1;

                        dr1["Overtime 1"] = l_Week1 > 40 ? (l_Week1 - 40).ToString() : "0";
                        foreach (AbsenteeismModel _a in _ACodes)
                        {
                            l_Abs = CalculateAbsenteeism(_e.EmployeeNumber, 1, _a);
                            dr1[_a.Abbreviation.ToString() + " 1"] = l_Abs.ToString();
                            l_Total += l_Abs;
                        }


                        l_Week2 = CalculatePunches(_e.EmployeeNumber, 2);

                        dr1["Week 2"] = l_Week2 > 40 ? "40" : l_Week2.ToString();

                        l_Total += l_Week2;

                        dr1["Overtime 2"] = l_Week2 > 40 ? (l_Week2 - 40).ToString() : "0";
                        foreach (AbsenteeismModel _a in _ACodes)
                        {
                            l_Abs = CalculateAbsenteeism(_e.EmployeeNumber, 2, _a);
                            dr1[_a.Abbreviation.ToString() + " 2"] = l_Abs.ToString();
                            l_Total += l_Abs;
                        }

                        dr1["Total"] = l_Total.ToString();

                        _HRR.Rows.Add(dr1);

                    }

                    DataRow drSum = _HRR.NewRow();
                    drSum["Employees"] = "";

                    l_Week1 = 0;
                    l_Week2 = 0;
                    l_Abs = 0;
                    Decimal l_AbsSum = 0;
                    Decimal l_Abs2 = 0;
                    l_Total = 0;
                    foreach (EmployeeModel _e in _Employees)
                    {

                        l_Week1 += CalculatePunches(_e.EmployeeNumber, 1);
                        l_Week2 += CalculatePunches(_e.EmployeeNumber, 2);

                        
                        foreach (AbsenteeismModel _a in _ACodes)
                        {
                            l_Abs = CalculateAbsenteeism(_e.EmployeeNumber, 1, _a);
                            l_Abs2 = CalculateAbsenteeism(_e.EmployeeNumber, 2, _a);
                            l_AbsSum += l_Abs + l_Abs2;
                        }
                    }
                    l_Total += l_Week1 + l_Week2;
                   
                    drSum["Week 1"] = l_Week1 > 40 ? "40" : l_Week1.ToString();

                    drSum["Overtime 1"] = l_Week1 > 40 ? (l_Week1 - 40).ToString() : "0"; ;

                    foreach (AbsenteeismModel _a in _ACodes)
                    {
                        l_Abs2 = 0;
                        l_Abs = 0;
                        foreach (EmployeeModel _e in _Employees)
                        {
                                l_Abs += CalculateAbsenteeism(_e.EmployeeNumber, 1, _a);
                                l_Abs2 += CalculateAbsenteeism(_e.EmployeeNumber, 2, _a);
                        }

                        drSum[_a.Abbreviation.ToString() + " 1"] = l_Abs.ToString();
                        drSum[_a.Abbreviation.ToString() + " 2"] = l_Abs2.ToString();
                    }
                    l_Total += l_AbsSum;

                    drSum["Week 2"] = l_Week2 > 40 ? "40" : l_Week2.ToString();

                    drSum["Overtime 2"] = l_Week2 > 40 ? (l_Week2 - 40).ToString() : "0"; 
                    drSum["Total"] = l_Total.ToString();

                    _HRR.Rows.Add(drSum);
                    _HRRItems.Add(new HRReportModel(_HRR, _g.NameLabel));
                }
                RaisePropertyChanged("HRRItems");

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".FillTable\n" + ex.Message, "Error");
            }
        }
        #endregion

        #region Commands

        #endregion
    }
}
