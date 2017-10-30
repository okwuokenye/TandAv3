﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    class EmployeeModel
    {
        public String EmployeeNumber { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String EmailAddress { get; set; }
        public String NameLabel { get; set; }
        public String MemberType { get; set; }

        public EmployeeModel(String p_EmployeeNumber, String p_Firstname, String p_Lastname, String p_EmailAddress)
        {
            EmployeeNumber = p_EmployeeNumber;
            Firstname = p_Firstname;
            Lastname = p_Lastname;
            EmailAddress = p_EmailAddress;
            NameLabel = p_Firstname + ", " + p_Lastname + " (" + p_EmployeeNumber + ")";
        }

        public EmployeeModel(String p_EmployeeNumber, String p_Firstname, String p_Lastname, String p_EmailAddress, String p_MemberStatus)
        {
            EmployeeNumber = p_EmployeeNumber;
            Firstname = p_Firstname;
            Lastname = p_Lastname;
            EmailAddress = p_EmailAddress;
            NameLabel = p_Firstname + ", " + p_Lastname + " (" + p_EmployeeNumber + ")";
            MemberType = p_MemberStatus == "Active" ? "Employee" : "Supervisor";
        }
    }
}
