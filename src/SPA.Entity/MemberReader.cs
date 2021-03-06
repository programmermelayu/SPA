﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPA.Data;
using System.Data.SqlClient;
using SPA.Core;

namespace SPA.Entity 
{
    public class MemberReader : IEntityReader<Member>
    {
        #region IEntityReader<Member> Members

        public string ErrorMessage { get; set; }

        public string SystemErrorMessage { get; set; }

        private Member _singleRecord;
        public Member SingleRecord
        {
            get
            {
                if (_singleRecord == null)
                {
                    _singleRecord = new Member();
                }
                return _singleRecord;
            }
            set
            {
                _singleRecord = value;
            }
        }

        private List<Member> _multipleRecords;
        public List<Member> MultipleRecords
        {
            get
            {
                if (_multipleRecords == null)
                {
                    _multipleRecords = new List<Member>();
                }
                return _multipleRecords;
            }

            set
            {
                _multipleRecords = value;
            }
        }

        public bool ReadSingle(FilterElement filter)
        {
            SqlDataReader dr = DbReader.Execute(GetQuery(filter));
            return CreateSingleRecordDataReader(dr);
        }

        public bool ReadSingle(List<FilterElement> filterCollection)
        {
            SqlDataReader dr = DbReader.Execute(GetQuery(filterCollection));
            return CreateSingleRecordDataReader(dr);
        }

        public bool ReadMultiple()
        {
            SqlDataReader dr = DbReader.Execute(GetQuery());
            return CreateMultipleRecordsDataReader(dr);
        }

        public bool ReadMultiple(List<FilterElement> filterCollection)
        {
            SqlDataReader dr = DbReader.Execute(GetQuery(filterCollection));
            return CreateMultipleRecordsDataReader(dr);
        }

        public bool ReadMultiple(FilterElement filter)
        {
            SqlDataReader dr = DbReader.Execute(GetQuery(filter));
            return CreateMultipleRecordsDataReader(dr);
        }

        private string GetQuery(FilterElement filter)
        {
            SelectBuilder sb = new SelectBuilder();
            CreateQueryHeader(sb);
            if (filter != null)
            {
                switch (filter.Key)
                {
                    case Enums.Data.KeyElements.MemberCode:
                        sb.AddWhereField("Code", filter.Value);
                        break;
                    case Enums.Data.KeyElements.MemberKP:
                        sb.AddWhereField("NewIC", filter.Value);
                        break;
                    case Enums.Data.KeyElements.MemberID:
                        sb.AddWhereField("ID", filter.Value, SPA.Enums.Data.DataType.Number);
                        break;
                    case Enums.Data.KeyElements.UseColumnName:
                        sb.AddWhereField(filter.ColumnName, filter.Value);
                        break;
                }
            }

            return sb.Sql;
        }

        private string GetQuery(List<FilterElement> filterCollection)
        {
            SelectBuilder sb = new SelectBuilder();
            CreateQueryHeader(sb);

            if (filterCollection != null)
            {
                foreach (var filter in filterCollection)
                {
                    switch (filter.Key)
                    {
                        case Enums.Data.KeyElements.MemberCode:
                            sb.AddWhereField("Code", filter.Value);
                            break;
                        case Enums.Data.KeyElements.MemberKP:
                            sb.AddWhereField("NewIC", filter.Value);
                            break;
                        case Enums.Data.KeyElements.MemberID:
                            sb.AddWhereField("ID", filter.Value, SPA.Enums.Data.DataType.Number);
                            break;
                        case Enums.Data.KeyElements.UseColumnName:
                            sb.AddWhereField(filter.ColumnName, filter.Value);
                            break;
                    }
                }
            }
            return sb.Sql;
        }

        private string GetQuery()
        {
            SelectBuilder sb = new SelectBuilder();
            CreateQueryHeader(sb);
            return sb.Sql;
        }

        private void CreateQueryHeader(SelectBuilder sb)
        {
            sb.Fields.Add("ID");
            sb.Fields.Add("NewIC");
            sb.Fields.Add("Name");
            sb.Fields.Add("Code");
            sb.Fields.Add("Age");
            sb.Fields.Add("Birthdate");

            sb.Fields.Add("CitizenshipID");
            sb.Fields.Add("StatusID");
            sb.Fields.Add("RaceID");
            sb.Fields.Add("ReligionID");
            sb.Fields.Add("SexID");
            sb.Fields.Add("MaritalStatusID");
            sb.Fields.Add("CategoryID");

            sb.Fields.Add("PermanentAddress");
            sb.Fields.Add("PermanentDistrict");
            sb.Fields.Add("PermanentPostcode");
            sb.Fields.Add("PermanentStateID");

            sb.Fields.Add("CurrentAddress");
            sb.Fields.Add("CurrentDistrict");
            sb.Fields.Add("CurrentPostcode");
            sb.Fields.Add("CurrentStateID");

            sb.Fields.Add("OfficePositionTitle");
            sb.Fields.Add("OfficeAddress");
            sb.Fields.Add("OfficeDistrict");
            sb.Fields.Add("OfficePostcode");
            sb.Fields.Add("OfficeStateID");
            sb.Fields.Add("OfficePhone");
            sb.Fields.Add("OfficeFax");
            sb.Fields.Add("OfficeEmail");
            sb.Fields.Add("OfficeRetiredDate");

            sb.Fields.Add("PersonalEmail");
            sb.Fields.Add("PersonalHomePhone");
            sb.Fields.Add("PersonalMobilePhone");

            sb.Fields.Add("StartMembership");
            sb.Fields.Add("MembershipDate");
            sb.Fields.Add("EndMembership");
            sb.Fields.Add("EndMembershipDate");

            sb.Table = "Members";
            sb.OrderBy = "Code ASC";

        }

        private bool CreateMultipleRecordsDataReader(SqlDataReader dr)
        {
            if (dr.HasRecord())
            {
                while (dr.Read())
                {
                    Member member = new Member();
                    member.ID = dr.GetValueInt("ID"); ;
                    member.NewIC = dr.GetValueString("NewIC");
                    member.Name = dr.GetValueString("Name");
                    member.Code = dr.GetValueString("Code");
                    member.Age = dr.GetValueInt("Age");
                    member.Birthdate = dr.GetValueDate("Birthdate").ToString();

                    member.CitizenshipID = dr.GetValueInt("CitizenshipID");
                    member.StatusID = dr.GetValueInt("StatusID");
                    member.RaceID = dr.GetValueInt("RaceID");
                    member.ReligionID = dr.GetValueInt("ReligionID");
                    member.SexID = dr.GetValueInt("SexID");
                    member.MaritalStatusID = dr.GetValueInt("MaritalStatusID");
                    member.CategoryID = dr.GetValueInt("CategoryID");

                    member.PermanentAddress = dr.GetValueString("PermanentAddress");
                    member.PermanentDistrict = dr.GetValueString("PermanentDistrict");
                    member.PermanentPostcode = dr.GetValueString("PermanentPostcode");
                    member.PermanentStateID = dr.GetValueInt("PermanentStateID");

                    member.CurrentAddress = dr.GetValueString("CurrentAddress");
                    member.CurrentDistrict = dr.GetValueString("CurrentDistrict");
                    member.CurrentPostcode = dr.GetValueString("CurrentPostcode");
                    member.CurrentStateID = dr.GetValueInt("CurrentStateID");

                    member.OfficePositionTitle = dr.GetValueString("OfficePositionTitle");
                    member.OfficeAddress = dr.GetValueString("OfficeAddress");
                    member.OfficeDistrict = dr.GetValueString("OfficeDistrict");
                    member.OfficePostcode = dr.GetValueString("OfficePostcode");
                    member.OfficeStateID = dr.GetValueInt("OfficeStateID");
                    member.OfficePhone = dr.GetValueString("OfficePhone");
                    member.OfficeFax = dr.GetValueString("OfficeFax");
                    member.OfficeEmail = dr.GetValueString("OfficeEmail");
                    member.OfficeRetiredDate = dr.GetValueDate("OfficeRetiredDate").ToString();

                    member.PersonalEmail = dr.GetValueString("PersonalEmail");
                    member.PersonalHomePhone = dr.GetValueString("PersonalHomePhone");
                    member.PersonalMobilePhone = dr.GetValueString("PersonalMobilePhone");

                    member.MemberStart = dr.GetValueBoolean("StartMembership");
                    member.MemberStartDate = dr.GetValueDate("MembershipDate").ToString();
                    member.MemberEnd = dr.GetValueBoolean("EndMembership");
                    member.MemberEndDate = dr.GetValueDate("EndMembershipDate").ToString();

                    MultipleRecords.Add(member);
                }
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CreateSingleRecordDataReader(SqlDataReader dr)
        {
            if (dr.HasRecord())
            {
                while (dr.Read())
                {
                    SingleRecord.ID = dr.GetValueInt("ID");
                    SingleRecord.NewIC = dr.GetValueString("NewIC");
                    SingleRecord.Name = dr.GetValueString("Name");
                    SingleRecord.Code = dr.GetValueString("Code");
                    SingleRecord.Age = dr.GetValueInt("Age");
                    SingleRecord.Birthdate = dr.GetValueDate("Birthdate").ToString();

                    SingleRecord.CitizenshipID = dr.GetValueInt("CitizenshipID");
                    SingleRecord.StatusID = dr.GetValueInt("StatusID");
                    SingleRecord.RaceID = dr.GetValueInt("RaceID");
                    SingleRecord.ReligionID = dr.GetValueInt("ReligionID");
                    SingleRecord.SexID = dr.GetValueInt("SexID");
                    SingleRecord.MaritalStatusID = dr.GetValueInt("MaritalStatusID");
                    SingleRecord.CategoryID = dr.GetValueInt("CategoryID");

                    SingleRecord.PermanentAddress = dr.GetValueString("PermanentAddress");
                    SingleRecord.PermanentDistrict = dr.GetValueString("PermanentDistrict");
                    SingleRecord.PermanentPostcode = dr.GetValueString("PermanentPostcode");
                    SingleRecord.PermanentStateID = dr.GetValueInt("PermanentStateID");

                    SingleRecord.CurrentAddress = dr.GetValueString("CurrentAddress");
                    SingleRecord.CurrentDistrict = dr.GetValueString("CurrentDistrict");
                    SingleRecord.CurrentPostcode = dr.GetValueString("CurrentPostcode");
                    SingleRecord.CurrentStateID = dr.GetValueInt("CurrentStateID");

                    SingleRecord.OfficePositionTitle = dr.GetValueString("OfficePositionTitle");
                    SingleRecord.OfficeAddress = dr.GetValueString("OfficeAddress");
                    SingleRecord.OfficeDistrict = dr.GetValueString("OfficeDistrict");
                    SingleRecord.OfficePostcode = dr.GetValueString("OfficePostcode");
                    SingleRecord.OfficeStateID = dr.GetValueInt("OfficeStateID");
                    SingleRecord.OfficePhone = dr.GetValueString("OfficePhone");
                    SingleRecord.OfficeFax = dr.GetValueString("OfficeFax");
                    SingleRecord.OfficeEmail = dr.GetValueString("OfficeEmail");
                    SingleRecord.OfficeRetiredDate = dr.GetValueDate("OfficeRetiredDate").ToString();

                    SingleRecord.PersonalEmail = dr.GetValueString("PersonalEmail");
                    SingleRecord.PersonalHomePhone = dr.GetValueString("PersonalHomePhone");
                    SingleRecord.PersonalMobilePhone = dr.GetValueString("PersonalMobilePhone");

                    SingleRecord.MemberStart = dr.GetValueBoolean("StartMembership");
                    SingleRecord.MemberStartDate = dr.GetValueDate("MembershipDate").ToString();
                    SingleRecord.MemberEnd = dr.GetValueBoolean("EndMembership");
                    SingleRecord.MemberEndDate = dr.GetValueDate("EndMembershipDate").ToString();
                }
                dr.Close();
                return true;
            }
            else
            {
                return false;
            }

        }


       

        #endregion
    }
}
