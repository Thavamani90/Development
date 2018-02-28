﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FingerprintsModel
{
    public class Nurse
    {
        public string CAge { get; set; }
        public int SingleParent { get; set; }
        public int TwoParent { get; set; }
        public int English { get; set; }
        public int Other { get; set; }
        public int HomelessYes { get; set; }
        public int HomelessNo { get; set; }
        public int TANF_sel { get; set; }
        public int SSI_sel { get; set; }
        public int SNAP_sel { get; set; }
        public int WIC_sel { get; set; }
        public int None_sel { get; set; }
        public int Teenager { get; set; }
        //Guardian1
        public int Age20 { get; set; }
        public int Age30over { get; set; }
        public int MilitaryStatusNone { get; set; }
        public int MilitaryStatusActive { get; set; }
        public int MilitaryStatusVeteran { get; set; }
        public int CurrentlyWorkYes { get; set; }
        public int CurrentlyWorkNo { get; set; }
        public int JobTrainingyes { get; set; }
        public int JobTrainingno { get; set; }
        //Guardian2
        public int G2Teenager { get; set; }
        public int G2Age20 { get; set; }
        public int G2Age30over { get; set; }
        public int G2MilitaryStatusNone { get; set; }
        public int G2MilitaryStatusActive { get; set; }
        public int G2MilitaryStatusVeteran { get; set; }
        public int G2CurrentlyWorkYes { get; set; }
        public int G2CurrentlyWorkNo { get; set; }
        public int G2JobTrainingyes { get; set; }
        public int G2JobTrainingno { get; set; }
        //Child

        public int Ageless10weeks { get; set; }
        public int Agegreater10weeks { get; set; }
        public int Age3Months { get; set; }
        public int Age6Months { get; set; }
        public int Age1yr { get; set; }
        public int Age2yr { get; set; }
        public int Age3yr { get; set; }
        public int Age4yr { get; set; }
        public int Age5yr { get; set; }
        public int Age6yr { get; set; }
        public int Age6yrorgreater { get; set; }
        public int MedicalHomeYes { get; set; }
        public int MedicalHomeNo { get; set; }
        public int DentalHomeYes { get; set; }
        public int DentalHomeNo { get; set; }
        public int InsuranceYes { get; set; }
        public int InsuranceNo { get; set; }
        public int SuspecteddocumentofdisabiltyYes { get; set; }
        public int SuspecteddocumentofdisabiltyNo { get; set; }
        public int SuspecteddisabiltyYes { get; set; }
        public int SuspecteddisabiltyNo { get; set; }
        public int DocumentofdisabiltyYes { get; set; }
        public int DocumentofdisabiltyNo { get; set; }
        public int ChildWlfareYes { get; set; }
        public int ChildWlfareNo { get; set; }
        public int FosterChildYes { get; set; }
        public int FosterChildNo { get; set; }
        public int DualCustYes { get; set; }
        public int DualCustNo { get; set; }
        //Poverty Points
        public int poverty0to25 { get; set; }
        public int poverty26to50 { get; set; }
        public int poverty51to75 { get; set; }
        public int poverty76to100 { get; set; }
        public int poverty100to130 { get; set; }
        public int povertygreater130 { get; set; }
        //PM
        public int PMInsuranceYes { get; set; }
        public int PMInsuranceNo { get; set; }
        public int PMMedicalHomeYes { get; set; }
        public int PMMedicalHomeNo { get; set; }
        public int Trimester1 { get; set; }
        public int Trimester2 { get; set; }
        public int Trimester3 { get; set; }
            //View Client Details
             public string CenterID { get; set; }
             public string CenterName { get; set; }
            public int AcceptID { get; set; }
            public string AcceptApplicant { get; set; }
            public string AcceptReason { get; set; }
        public string RejectDesc { get; set; }
            public int HealthReview { get; set; }
            public List<Nurse> getList { get; set; }
            public calculateincome calcualteincome { get; set; }
            public string AmountNo { get; set; }
            public string DocumentDesc { get; set; }
            public string HWInput { get; set; }
            public string AssessmentDate { get; set; }
            public string Areadesignation { get; set; }
            public string Areabreakdown { get; set; }
            public string AHeight { get; set; }
            public string AWeight { get; set; }
            public string HeadCircle { get; set; }
            public string ClientFname { get; set; }
            public string ClientMname { get; set; }
            public string ClientLName { get; set; }
            public int ClientID { get; set; }
            public string ClientType { get; set; }
            //public string isFamily { get; set; }
            public int HouseholdId { get; set; }
            public Guid UserId { get; set; }
            public Int32 StreetID { get; set; }
            public string Street { get; set; }
            public string County { get; set; }
            public string StreetName { get; set; }
            public string Apartmentno { get; set; }
            public string ZipCode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public bool AdresssverificationinPaper { get; set; }
            public byte[] HImageByte { get; set; }
            public string HFileName { get; set; }
            public string HFileExtension { get; set; }
            public string HFileInString { get; set; }
            public string nationality { get; set; }
            public string PrimaryLanguauge { get; set; }
            public Int32 FamilyHouseholdID { get; set; }
            public int FamilyType { get; set; }
            public bool TANF { get; set; }
            public bool SSI { get; set; }
            public bool NONE { get; set; }
            public bool WIC { get; set; }
            public bool SNAP { get; set; }
            public int HomeType { get; set; }
            public int RentType { get; set; }
            public int Interpretor { get; set; }
            public string InsuranceOption { get; set; }
            public string MedicalNote { get; set; }
            public int Medicalhome { get; set; }
            public int MedicalhomePreg1 { get; set; }
            public int MedicalhomePreg2 { get; set; }
            public int MedicalService { get; set; }
            public bool BMIStatus1 { get; set; }
            public int BMIStatus { get; set; }
            public int BMIStatus2 { get; set; }
            public int ImmunizationService { get; set; }
            public string CreatedOn { get; set; }
            public int SchoolDistrict { get; set; }
            public string OtherLanguageDetail { get; set; }
            public int ParentRelatioship { get; set; }
            public string ParentRelatioshipOther { get; set; }
            public string Povertypercentage { get; set; }
            public int docstorage { get; set; }
            //Parent Details 1
            public Int32 Parent { get; set; }
            public Int32 ParentID { get; set; }
            public Int32 ParentOriginalId { get; set; }//changes
            public HttpPostedFileBase PAvatar { get; set; }
            public string PAvatarUrl { get; set; }
            public string Pfirstname { get; set; }
            public string Plastname { get; set; }
            public string Pmidddlename { get; set; }
            public string PDOB { get; set; }
            public string Pphoneno { get; set; }
            public string PphoneType { get; set; }
            public bool PState { get; set; }
            public bool PSms { get; set; }
            public string Pnotes { get; set; }
            public string Pnotesother { get; set; }
            public string Pemailid { get; set; }
            public string PGender { get; set; }
            public int PMilitaryStatus { get; set; }
            public string PEnrollment { get; set; }
            public string PCurrentlyWorking { get; set; }
            public string PDegreeEarned { get; set; }
            public string PRole { get; set; }
            public string PQuestion { get; set; }
            public string PGuardiannotes { get; set; }
            public byte[] PImageByte { get; set; }
            public string PImagejson { get; set; }
            public string PFileName { get; set; }
            public string PFileExtension { get; set; }
            public int Parentsecondexist { get; set; }
            public decimal Parentincome1 { get; set; }
            public string Parentfirstname { get; set; }
            public string ParentLastname { get; set; }
            public string ParentStrretaddress { get; set; }
            public string Parentcity { get; set; }
            public string Parentstate { get; set; }
            public string Parentzipcode { get; set; }
            public string ParentSSN1 { get; set; }
            public int ExistPmprogram { get; set; }
            public bool Noemail1 { get; set; }
            //Parent Details 2
            public Int32 ParentID1 { get; set; }
            public HttpPostedFileBase P1Avatar { get; set; }
            public string P1AvatarUrl { get; set; }
            public string P1firstname { get; set; }
            public string P1lastname { get; set; }
            public string P1midddlename { get; set; }
            public string P1DOB { get; set; }
            public string P1phoneno { get; set; }
            public string P1phoneType { get; set; }
            public bool P1State { get; set; }
            public bool P1Sms { get; set; }
            public string P1notes { get; set; }
            public string P1notesother { get; set; }
            public string P1emailid { get; set; }
            public int P1Gender { get; set; }
            public int P1MilitaryStatus { get; set; }
            public string P1Enrollment { get; set; }
            public string P1CurrentlyWorking { get; set; }
            public string P1DegreeEarned { get; set; }
            public string P1Role { get; set; }
            public string P1Question { get; set; }
            public string P1Guardiannotes { get; set; }
            public byte[] P1ImageByte { get; set; }
            public string P1Imagejson { get; set; }
            public string P1FileName { get; set; }
            public string P1FileExtension { get; set; }
            public decimal Parentincome2 { get; set; }
            public string ParentSSN2 { get; set; }
            public string ParentSSN3 { get; set; }
            public bool Noemail2 { get; set; }
            //Child Details

            //added by santosh
            public bool IsIEP { get; set; }
            public bool IsIFSP { get; set; }
            public bool IsExpired { get; set; }
            //
            public Int32 ChildId { get; set; }
            public string Cfirstname { get; set; }
            public string Clastname { get; set; }
            public string Cmiddlename { get; set; }
            public string CDOB { get; set; }
            public string CProgramType { get; set; }
            public string CGender { get; set; }
            public int CParentdisable { get; set; }
            public int CDentalhome { get; set; }
            public int IsFoster { get; set; }
            public string CRace { get; set; }
            public string CRaceSubCategory { get; set; }
            public int CEthnicity { get; set; }
            public HttpPostedFileBase CAvatar { get; set; }
            public bool DobverificationinPaper { get; set; }
            public string CAvatarUrl { get; set; }
            public string CFileName { get; set; }
            public string CFileExtension { get; set; }
            public string DOBverifiedBy { get; set; }
            public string CSSN { get; set; }
            public byte[] CImageByte { get; set; }
            public string Imagejson { get; set; }
            public string CDoctor { get; set; }
            public string CDoctorP1 { get; set; }
            public string CDoctorP2 { get; set; }
            public string CDentist { get; set; }
            public string CDoctorddl { get; set; }
            public int Doctor { get; set; }
            public int Dentist { get; set; }
            public int P1Doctor { get; set; }
            public int P2Doctor { get; set; }
            public HttpPostedFileBase FiledobRAvatar { get; set; }
            public string DobFileName { get; set; }
            public string DobFileExtension { get; set; }
            public byte[] Dobaddressform { get; set; }
            public string Dobaddressformjson { get; set; }
            public HttpPostedFileBase FileImmunization { get; set; }
            public string ImmunizationFileName { get; set; }
            public string ImmunizationFileExtension { get; set; }
            public byte[] Immunizationfileinbytes { get; set; }
            public bool ImmunizationinPaper { get; set; }
            public int InDualcustody { get; set; }
            public int Inwalfareagency { get; set; }
            public string Raceother { get; set; }
            public int Yakkrid { get; set; }
            //30082016
            public bool Healthreviewnurse { get; set; }
            
            
            //Emergency Contacts 
            public Int32 EmegencyId { get; set; }
            public string Efirstname { get; set; }
            public string Elastname { get; set; }
            public string Emiddlename { get; set; }
            public string EDOB { get; set; }
            public string EGender { get; set; }
            public string Ephoneno { get; set; }
            public string Eworkno { get; set; }
            public string Ehomeno { get; set; }
            public string EAddress1 { get; set; }
            public string EAddress2 { get; set; }
            public string ECity { get; set; }
            public string EZipcode { get; set; }
            public string EEmail { get; set; }
            public bool IsPickupContact { get; set; }
            public string EState { get; set; }
            public string Enotes { get; set; }
            public HttpPostedFileBase EAvatar { get; set; }
            public string EFileName { get; set; }
            public string EFileExtension { get; set; }
            public string EImagejson { get; set; }
            public byte[] EImageByte { get; set; }
            public string EAvatarUrl { get; set; }
            public string ERelationwithchild { get; set; }
            //Restricted Details
            public Int32 RestrictedId { get; set; }
            public string Rfirstname { get; set; }
            public string Rlastname { get; set; }
            public string Rmiddlename { get; set; }
            public string PrimaryLang { get; set; }
            public string RPhoneno { get; set; }
            public HttpPostedFileBase RAvatar { get; set; }
            public string RFileName { get; set; }
            public string RFileExtension { get; set; }
            public string RImagejson { get; set; }
            public string RAvatarUrl { get; set; }
            public byte[] RImageByte { get; set; }
            public string RDescription { get; set; }
            //Others in house hold 
            public Int32 OthersId { get; set; }
            public string Ofirstname { get; set; }
            public string Olastname { get; set; }
            public string Omiddlename { get; set; }
            public string ODOB { get; set; }
            public string OGender { get; set; }
            public bool Oemergencycontact { get; set; }
            public int Alreadyemergencycontact { get; set; }
            public List<PrimarylangInfo> langList = new List<PrimarylangInfo>();
            public List<phone> phoneList = new List<phone>();
            public List<NationalityInfo> nationList = new List<NationalityInfo>();
            public List<RaceInfo> raceList = new List<RaceInfo>();
            public List<RaceSubCategory> raceCategory = new List<RaceSubCategory>();
            public List<Relationship> relationship = new List<Relationship>();
            public List<Parentphone1> phoneListParent = new List<Parentphone1>();
            public List<Parentphone2> phoneListParent1 = new List<Parentphone2>();
            public HttpPostedFileBase FileaddressAvatar { get; set; }
            public HttpPostedFileBase FileothersRAvatar { get; set; }
            public HttpPostedFileBase fileDescription { get; set; }
            public SelectPoints _Selectionpoints { get; set; }
            public List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords { get; set; }
            public List<Qualifier> QualifierRecords { get; set; }
            public int ReceivedServicesPrenatal { get; set; }
            public int ReceivedServicesPostpartum { get; set; }
            public int ReceivedServicesMentalHealth { get; set; }
            public int ReceivedServicesSubstanceabuseprev { get; set; }
            public int ReceivedServicesSubstanceabusetreat { get; set; }
            public int ReceivedServicesEducation { get; set; }
            public int ReceivedServicesBreastfeedinginfo { get; set; }
            public int TrimesterEnrolled { get; set; }
            public int HighRisk { get; set; }
            public int ReceivedDentalServices { get; set; }
            public int LeftBeforeBirth { get; set; }
            public int TransitionedWithServices { get; set; }
            public int EnrolledChild { get; set; }
            public int IsPreg { get; set; }
            public bool Pregnantmotherenrolled { get; set; }
            public int Pregnantmotherprimaryinsurance { get; set; }
            public string Pregnantmotherprimaryinsurancenotes { get; set; }
            public int totalhousehold { get; set; }

            //Female pregency parent2
            public int ReceivedServicesPrenatal1 { get; set; }
            public int ReceivedServicesPostpartum1 { get; set; }
            public int ReceivedServicesMentalHealth1 { get; set; }
            public int ReceivedServicesSubstanceabuseprev1 { get; set; }
            public int ReceivedServicesSubstanceabusetreat1 { get; set; }
            public int ReceivedServicesEducation1 { get; set; }
            public int ReceivedServicesBreastfeedinginfo1 { get; set; }
            public int TrimesterEnrolled1 { get; set; }
            public int HighRisk1 { get; set; }
            public int ReceivedDentalServices1 { get; set; }
            public int LeftBeforeBirth1 { get; set; }
            public int TransitionedWithServices1 { get; set; }
            public int EnrolledChild1 { get; set; }
            public int IsPreg1 { get; set; }
            public bool PregnantmotherenrolledP1 { get; set; }
            public int Pregnantmotherprimaryinsurance1 { get; set; }
            public string Pregnantmotherprimaryinsurancenotes1 { get; set; }
            public List<calculateincome> Income1 { get; set; }
            public List<calculateincome1> Income2 { get; set; }
            public List<Programdetail> AvailableProgram { get; set; }
            public List<Programdetail> SelectedProgram { get; set; }
            public PostedProgram PostedPostedPrograms { get; set; }
            public List<SelectPoints.CustomQuestion> CustomQues = new List<SelectPoints.CustomQuestion>();
            public Screening _Screening { get; set; }
            public List<HrCenterInfo> HrcenterList = new List<HrCenterInfo>();
            public List<AgencyStaff> staffList = new List<AgencyStaff>();
            public List<SchoolDistrict> SchoolList = new List<SchoolDistrict>();
            public DataTable customscreening { get; set; }
            //added on 12-26-2016
            public List<Childcustomscreening> _childscreenings { get; set; }
            public List<CustomScreeningAllowed> _CustomScreeningAlloweds { get; set; }


            public class NationalityInfo
            {
                public string NationId { get; set; }
                public string Name { get; set; }
            }
            public class PrimarylangInfo
            {
                public string LangId { get; set; }
                public string Name { get; set; }
            }
            public class RaceInfo
            {
                public string RaceId { get; set; }
                public string Name { get; set; }
            }
            public class RaceSubCategory
            {
                public string RaceSubCategoryID { get; set; }
                public string Name { get; set; }
            }
            public class Relationship
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class phone
            {
                public int HouseHoldid { get; set; }
                public int EmergencyId { get; set; }
                public int PhoneId { get; set; }
                public string PhoneType { get; set; }
                public string PhoneNo { get; set; }
                public bool IsPrimary { get; set; }
                public string Notes { get; set; }
                public bool IsSms { get; set; }
            }
            public class Parentphone1
            {
                public int HouseHoldid { get; set; }
                public int Parents { get; set; }
                public int PPhoneId { get; set; }
                public string phonenoP { get; set; }
                public string PhoneTypeP { get; set; }
                public bool StateP { get; set; }
                public bool SmsP { get; set; }
                public string notesP { get; set; }
            }
            public class Parentphone2
            {
                public int HouseHoldid { get; set; }
                public int Parents1 { get; set; }
                public int PPhoneId1 { get; set; }
                public string phonenoP1 { get; set; }
                public string PhoneTypeP1 { get; set; }
                public bool StateP1 { get; set; }
                public bool SmsP1 { get; set; }
                public string notesP1 { get; set; }
            }
            public class calculateincome
            {
                public int newincomeid { get; set; }
                public int Income { get; set; }
                public string IncomeSource1 { get; set; }
                public string IncomeSource2 { get; set; }
                public string IncomeSource3 { get; set; }
                public string IncomeSource4 { get; set; }
                //public string IncomeIDp1 { get; set; }
                public decimal? AmountVocher1 { get; set; }
                public decimal? AmountVocher2 { get; set; }
                public decimal? AmountVocher3 { get; set; }
                public decimal? AmountVocher4 { get; set; }
                public HttpPostedFileBase SalaryAvatar1 { get; set; }
                public string SalaryAvatarFilename1 { get; set; }
                public string SalaryAvatarFileExtension1 { get; set; }
                public byte[] SalaryAvatar1bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar2 { get; set; }
                public string SalaryAvatarFilename2 { get; set; }
                public string SalaryAvatarFileExtension2 { get; set; }
                public byte[] SalaryAvatar2bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar3 { get; set; }
                public string SalaryAvatarFilename3 { get; set; }
                public string SalaryAvatarFileExtension3 { get; set; }
                public byte[] SalaryAvatar3bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar4 { get; set; }
                public string SalaryAvatarFilename4 { get; set; }
                public string SalaryAvatarFileExtension4 { get; set; }
                public byte[] SalaryAvatar4bytes { get; set; }
                public HttpPostedFileBase NoIncomeAvatar { get; set; }
                public string NoIncomeFilename4 { get; set; }
                public string NoIncomeFileExtension4 { get; set; }
                public byte[] NoIncomeAvatarbytes { get; set; }
                public int? Payfrequency { get; set; }
                public int? Working { get; set; }
                public decimal? IncomeCalculated { get; set; }
                public string Attachmentlink1 { get; set; }
                public string Attachmentlink2 { get; set; }
                public string Attachmentlink3 { get; set; }
                public string Attachmentlink4 { get; set; }
                public string Attachmentlink5 { get; set; }
                public bool incomePaper1 { get; set; }
                public bool incomePaper2 { get; set; }
                public bool incomePaper3 { get; set; }
                public bool incomePaper4 { get; set; }
                public bool noincomepaper { get; set; }
                public int docsstorage { get; set; }

            }
            public class calculateincome1
            {
                public int IncomeID { get; set; }
                public int Income { get; set; }
                public string IncomeSource1 { get; set; }
                public string IncomeSource2 { get; set; }
                public string IncomeSource3 { get; set; }
                public string IncomeSource4 { get; set; }
                public decimal? AmountVocher1 { get; set; }
                public decimal? AmountVocher2 { get; set; }
                public decimal? AmountVocher3 { get; set; }
                public decimal? AmountVocher4 { get; set; }
                public HttpPostedFileBase SalaryAvatar1 { get; set; }
                public string SalaryAvatarFilename1 { get; set; }
                public string SalaryAvatarFileExtension1 { get; set; }
                public byte[] SalaryAvatar1bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar2 { get; set; }
                public string SalaryAvatarFilename2 { get; set; }
                public string SalaryAvatarFileExtension2 { get; set; }
                public byte[] SalaryAvatar2bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar3 { get; set; }
                public string SalaryAvatarFilename3 { get; set; }
                public string SalaryAvatarFileExtension3 { get; set; }
                public byte[] SalaryAvatar3bytes { get; set; }
                public HttpPostedFileBase SalaryAvatar4 { get; set; }
                public string SalaryAvatarFilename4 { get; set; }
                public string SalaryAvatarFileExtension4 { get; set; }
                public byte[] SalaryAvatar4bytes { get; set; }
                public HttpPostedFileBase NoIncomeAvatar { get; set; }
                public string NoIncomeFilename4 { get; set; }
                public string NoIncomeFileExtension4 { get; set; }
                public byte[] NoIncomeAvatarbytes { get; set; }
                public int? Payfrequency { get; set; }
                public int? Working { get; set; }
                public decimal? IncomeCalculated { get; set; }
                public string Attachmentlink1 { get; set; }
                public string Attachmentlink2 { get; set; }
                public string Attachmentlink3 { get; set; }
                public string Attachmentlink4 { get; set; }
                public string Attachmentlink5 { get; set; }
                public bool incomePaper1 { get; set; }
                public bool incomePaper2 { get; set; }
                public bool incomePaper3 { get; set; }
                public bool incomePaper4 { get; set; }
                public bool noincomepaper { get; set; }
                public int docsstorage { get; set; }
            }
            public class Programdetail
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                public string ReferenceId { get; set; }
            }
            public class PostedProgram
            {
                public string[] ProgramID { get; set; }
            }
            public class Center
            {
                public string[] CenterID { get; set; }
            }

            public class Qualifier
            {
                public string Name { get; set; }
                public string Dob { get; set; }
                public string Programtype { get; set; }
                public int householdid { get; set; }
                public int clientid { get; set; }
                public int Programid { get; set; }
                public string PovertyPercentage { get; set; }
                public string ClientType { get; set; }
                public string Selectionpoint { get; set; }
                public bool IsAccepted { get; set; }


            }
            public class Applicationnotes
            {
                public string Name { get; set; }
                public string notes { get; set; }
                public string CreatedOn { get; set; }
            }

            public class ImmunizationRecord
            {
                public int ImmunizationId { get; set; }
                public int ImmunizationmasterId { get; set; }
                public string Dose { get; set; }
                public string Dose1 { get; set; }
                public bool Preempt1 { get; set; }
                public bool Exempt1 { get; set; }
                public string Dose2 { get; set; }
                public bool Preempt2 { get; set; }
                public bool Exempt2 { get; set; }
                public string Dose3 { get; set; }
                public bool Preempt3 { get; set; }
                public bool Exempt3 { get; set; }
                public string Dose4 { get; set; }
                public bool Preempt4 { get; set; }
                public bool Exempt4 { get; set; }
                public string Dose5 { get; set; }
                public bool Preempt5 { get; set; }
                public bool Exempt5 { get; set; }
            }
            public class Povertymodel
            {
                public string Percentage1 { get; set; }
                public string Percentage2 { get; set; }
                public string Amount1 { get; set; }
                public string Amount2 { get; set; }
                public string ChildIncome { get; set; }
                public string PovertyCalculated { get; set; }
                public string Totalinhousehold { get; set; }
            }


            //Ehs Program
            public string EhsChildBirthWt { get; set; }
            public string EhsChildBorn { get; set; }
            public int EhsChildProblm { get; set; }
            public string EhsChildProblmExplain { get; set; }
            public string EHSAllergy { get; set; }
            public int EHSEpiPen { get; set; }
            public string EhsChildLength { get; set; }
            public string EhsMedication { get; set; }
            //Added by santosh
            public string EHSmpplan { get; set; }
            public string EHSmpplancomment { get; set; }
            //End
            public string EhsMedicationName { get; set; }
            public string EhsDosage { get; set; }
            public string EhsComment { get; set; }
            public string EhsChildDentalCare { get; set; }
            public string EhsDentalExam { get; set; }
            public string EhsRecentDentalExam { get; set; }
            public string EhsChildNeedDentalTreatment { get; set; }
            public string EhsChildRecievedDentalTreatment { get; set; }
            public string EhsChildRecievedDentalInfo { get; set; }
            public string EhsChildDiet { get; set; }
            public string EhsChildFood { get; set; }

            //Commented by akansha on 16Dec2016
            //public string EhsPersistentNausea { get; set; }
            //public string EhsPersistentDiarrhea { get; set; }
            //public string EhsPersistentConstipation { get; set; }
            //public string EhsDramaticWeight { get; set; }
            //public string EhsRecentSurgery { get; set; }
            //public string EhsRecentHospitalization { get; set; }
            //public string EhsChangeinAppetite { get; set; }

            public string EhsAnemia { get; set; }
          //  public string EhsFoodAllergies { get; set; }
            public string EhsBreastfeed { get; set; }
           // public string EhsTakebottle { get; set; }
            public string EhsDrinkfromcup { get; set; }
            public string EhsFeedherself { get; set; }
            public string Ehspureedfoods { get; set; }
            public string Ehsfingerfoods { get; set; }
           // public string Ehsspoon { get; set; }
          //  public string Ehschewanything { get; set; }
            public string Ehsputfoods { get; set; }
            public string Ehschildbottle { get; set; }
            //public string Ehsfeedingtube { get; set; }
            public string EhsVitamins { get; set; }
            public string Ehsvitaminsupplement { get; set; }
            public string Ehsprescribed { get; set; }
            public string Ehsironfortified { get; set; }
           // public int EhschildThin { get; set; }
            public string Ehsnutritionalconcerns { get; set; }
            public string Ehsstove { get; set; }
            public string Ehsbabyfoods { get; set; }
            public string Ehsrunout { get; set; }
            //public string EhschildTrouble { get; set; }
            public string Ehschildsnacks { get; set; }
            public string EhsFavoritefoods { get; set; }
            public string EhsOtherConditions { get; set; }



            //Hs Program
            public string HsChildBirthWt { get; set; }
            public string HsChildBorn { get; set; }
            public int HsChildProblm { get; set; }
            public string HsChildProblmExplain { get; set; }
            public string HsChildLength { get; set; }
            public string HsMedication { get; set; }
        //Added by santosh
            public string HSmpplan { get; set; }
            public string HSmpplanComment { get; set; }
        //end
            public string HsMedicationName { get; set; }
            public string HsDosage { get; set; }
            public string HsComment { get; set; }
            public string HsChildDentalCare { get; set; }
            public string HsDentalExam { get; set; }
            public string HsRecentDentalExam { get; set; }
            public string HsChildNeedDentalTreatment { get; set; }
            public string HsChildRecievedDentalTreatment { get; set; }
            public string HsChildRecievedDentalInfo { get; set; }
            public string HsChildDiet { get; set; }
            public string HsChildFood { get; set; }
            public string HsPersistentNausea { get; set; }
            public string HsPersistentDiarrhea { get; set; }
            public string HsPersistentConstipation { get; set; }
            public string HsDramaticWeight { get; set; }
            public string HsRecentSurgery { get; set; }
            public string HsRecentHospitalization { get; set; }
            public string HsChangeinAppetite { get; set; }
            public string HsElevatedblood { get; set; }
            public string HsAnemia { get; set; }
            public string HsFoodAllergies { get; set; }
            public string HsBreastfeed { get; set; }
            public string HsTakebottle { get; set; }
            public string HsDrinkfromcup { get; set; }
            public string HsFeedherself { get; set; }
            public string Hspureedfoods { get; set; }
            public string Hsfingerfoods { get; set; }
            public string Hsspoon { get; set; }
            public string Hschewanything { get; set; }
            public string Hsputfoods { get; set; }
            public string Hschildbottle { get; set; }
            public string Hsfeedingtube { get; set; }
            public string HsVitamins { get; set; }
            public string Hsvitaminsupplement { get; set; }
            public string Hsprescribed { get; set; }
            public string Hsironfortified { get; set; }
            public int HschildThin { get; set; }
            public string Hsnutritionalconcerns { get; set; }
            public string Hsstove { get; set; }
            public string Hsbabyfoods { get; set; }
            public string Hsrunout { get; set; }
            public string HschildTrouble { get; set; }
            public string Hschildsnacks { get; set; }
            public string HsFavoritefoods { get; set; }
            public string HsOtherConditions { get; set; }

        //Changes

            //Changes
            public string ChildBirthWt { get; set; }
            public string ChildBorn { get; set; }
            public int ChildProblm { get; set; }
            public string ChildProblmExplain { get; set; }
            public string ChildLength { get; set; }
            public string Medication { get; set; }
            public string MedicationName { get; set; }
            public string Dosage { get; set; }
            public string Comment { get; set; }
            public string ChildDentalCare { get; set; }
            public string DentalExam { get; set; }
            public string RecentDentalExam { get; set; }
            public string ChildNeedDentalTreatment { get; set; }
            public string ChildRecievedDentalTreatment { get; set; }
            public string ChildRecievedDentalInfo { get; set; }
            public string ChildDiet { get; set; }
            public string ChildFood { get; set; }
            public string SurgeryType { get; set; }
            public string RecentTypeHospital { get; set; }
            public string SpecialDiet { get; set; }
            public string NonFoodItems { get; set; }

            public bool WICNutrition { get; set; }
            public bool FoodStamps { get; set; }
            public bool NoNutritionProg { get; set; }

            public string NutritionalConcernDesc { get; set; }
            public string NutritionalConcern { get; set; }
            public string ChildSpecialDiet { get; set; }
            public string PersistentNausea { get; set; }
            public string PersistentDiarrhea { get; set; }
            public string PersistentConstipation { get; set; }
            public string DramaticWeight { get; set; }
            public string RecentSurgery { get; set; }
            public string RecentHospitalization { get; set; }
            public string ChangeinAppetite { get; set; }
            public string Elevatedblood { get; set; }
            public string Anemia { get; set; }
            public string FoodAllergies { get; set; }
            public string Breastfeed { get; set; }
            public string Takebottle { get; set; }
            public string Drinkfromcup { get; set; }
            public string Feedherself { get; set; }
            public string pureedfoods { get; set; }
            public string fingerfoods { get; set; }
            public string spoon { get; set; }
            public string chewanything { get; set; }
            public string putfoods { get; set; }
            public string childbottle { get; set; }
            public string feedingtube { get; set; }
            public string Vitamins { get; set; }
            public string vitaminsupplement { get; set; }
            public string prescribed { get; set; }
            public string ironfortified { get; set; }
            public string childThin { get; set; }
            public bool nutritionalconcerns { get; set; }
            public bool stove { get; set; }
            public bool babyfoods { get; set; }
            public bool runout { get; set; }
            public string childTrouble { get; set; }
            public string childsnacks { get; set; }
            public string Favoritefoods { get; set; }
            public string OtherConditions { get; set; }
            public string MilkComment { get; set; }
            public string ChildFeed { get; set; }
            public string ChildFormula { get; set; }
            public string ChildHungry { get; set; }
            public string ChildFeedCereal { get; set; }
            public string ChildFeedMarshfood { get; set; }
            public string ChildFeedChopedfood { get; set; }
            public string ChildFingerFEDFood { get; set; }
            public string ChildReferalCriteria { get; set; }
            public string ChildFruitJuice { get; set; }
            public string ChildFruitJuicevitaminc { get; set; }

            public string ChildFingerFood { get; set; }
            public string ChildFedJuice { get; set; }
            public string ChildWater { get; set; }
        //Changes on 11 Aug2016
            public string EhsCHConditions { get; set; }
            public string EhsChronicHealthConditions { get; set; }
            public string HsChilddrinkcondition { get; set; }
            public string ChronicHealthConditions { get; set; }
            public string HsOthermedicalConditions { get; set; }
            public string HsDiagonoseOtherConditions { get; set; }
            public string FoodPantory { get; set; }
            public string EHSBabyOrMotherProblems { get; set; }
        //Changes 16Aug2016
            //HS comment properties if yes selected Added 08082016 
            public string HSBabyOrMotherProblems { get; set; }
            public string HSChildMedication { get; set; }
            public string HSPreventativeDentalCare { get; set; }
            public string HSProfessionalDentalExam { get; set; }
            public string HSNeedingDentalTreatment { get; set; }
            public string HSChildReceivedDentalTreatment { get; set; }
            //
            public class Childhealthnutrition
            {
                public string Id { get; set; }
                public string MasterId { get; set; }
                public string Description { get; set; }
                public string Programid { get; set; }
                public string Questionid { get; set; }
            }
            public List<Childhealthnutrition> _Childhealthnutrition = new List<Childhealthnutrition>();
          //  public List<Childhealthnutrition> _Childhealthnutrition = new List<Childhealthnutrition>();
            //Pm , Healthhistory,nutrition properties



            public string _Pregnantmotherpmservices { get; set; }
            public string _Pregnantmotherproblem { get; set; }
            public string _Pregnantmotherpmservices1 { get; set; }
            public string _Pregnantmotherproblem1 { get; set; }
            public string _ChildDirectBloodRelativeEhs { get; set; }
            public string _ChildDiagnosedConditionsEhs { get; set; }
            public string _ChildChronicHealthConditionsEhs { get; set; }
            public string _ChildChronicHealthConditions1Ehs { get; set; }
            public string _ChildChronicHealthConditions2Ehs { get; set; }
            public string _ChildMedicalTreatmentEhs { get; set; }
            public string _ChildChildVitaminSupplement { get; set; }
            public string _ChildDiet { get; set; }
            public string _ChildDrink { get; set; }
            public string _ChildDirectBloodRelativeHs { get; set; }
            public string _ChildDiagnosedConditionsHs { get; set; }
            public string _ChildMedicalTreatmentHs { get; set; }
            public string _ChildChronicHealthConditionsHs { get; set; }
            public string _childprogrefid { get; set; }
        //End


        //End
        //PM Questions Parent 1
            public Int32 PMQuestionID { get; set; }
            public Int32 PMQuestionID1 { get; set; }
            public List<PMService> AvailableService { get; set; }
            public List<PMService> SelectedService { get; set; }
            public PostedPMService PostedPostedService { get; set; }

            public List<PMProblems> AvailablePrblms { get; set; }
            public List<PMProblems> SelectedPrblms { get; set; }
            public PostedPMProblems PostedPostedPrblms { get; set; }

            public string PMPrblmsPosted { get; set; }
            public string PMServicePosted { get; set; }

            public class PMService
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedPMService
            {
                public string[] PMServiceID { get; set; }
                public string[] PMServiceID1 { get; set; }
            }
            public class PMProblems
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedPMProblems
            {
                public string[] PMPrblmID { get; set; }
                public string[] PMPrblmID1 { get; set; }
            }
            public string PMVisitDoc { get; set; }
            public int PMProblem { get; set; }
            public string PMOtherIssues { get; set; }
            public List<PMConditionsInfo> PMCondtnList = new List<PMConditionsInfo>();
            public class PMConditionsInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public string PMConditions { get; set; }
            public string PMCondtnDesc { get; set; }
            public int PMRisk { get; set; }
            public int PMDentalExam { get; set; }
            public string PMDentalExamDate { get; set; }
            public int PMNeedDental { get; set; }
            public int PMRecieveDental { get; set; }

        
        //End
        //PM Conditions Parent2
            //PM Questions Parent 1
            public List<PMService1> AvailableService1 { get; set; }
            public List<PMService1> SelectedService1 { get; set; }
            public PostedPMService1 PostedPostedService1 { get; set; }

            public List<PMProblems1> AvailablePrblms1 { get; set; }
            public List<PMProblems1> SelectedPrblms1 { get; set; }
            public PostedPMProblems1 PostedPostedPrblms1 { get; set; }

            public class PMService1
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedPMService1
            {
                public string[] PMServiceID { get; set; }
            }
            public class PMProblems1
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedPMProblems1
            {
                public string[] PMPrblmID { get; set; }
            }
            public string PMVisitDoc1 { get; set; }
            public int PMProblem1 { get; set; }
            public string PMOtherIssues1 { get; set; }
            public List<PMConditionsInfo1> PMCondtnList1 = new List<PMConditionsInfo1>();
            public class PMConditionsInfo1
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public string PMConditions1 { get; set; }
            public string PMCondtnDesc1 { get; set; }
            public bool PMRisk1 { get; set; }
            public int PMDentalExam1 { get; set; }
            public string PMDentalExamDate1 { get; set; }
            public int PMNeedDental1 { get; set; }
            public int PMRecieveDental1 { get; set; }

        

        //End


            //child nutrition comment Properties if yes selected Added 05082016 
            public string NauseaorVomitingcomment { get; set; }
            public string DiarrheaComment { get; set; }
            public string Constipationcomment { get; set; }
            public string DramaticWeightchangecomment { get; set; }
            public string RecentSurgerycomment { get; set; }
            public string RecentHospitalizationComment { get; set; }
            public string SpecialDietComment { get; set; }
            public string FoodAllergiesComment { get; set; }
            public string NutritionAlconcernsComment { get; set; }
            public string NutritionProgramsComment { get; set; }
            public string ChewingorSwallowingcomment { get; set; }
            public string SpoonorForkComment { get; set; }
            public string SpecialFeedingComment { get; set; }
            public string ThinkYourChildComment { get; set; }
            public string BottleComment { get; set; }
            public string EatOrChewComment { get; set; }
            public string ChildAppetiteComment { get; set; }
            //End

        //Pm Questions

            public List<PMproblemandservices> _PMproblem = new List<PMproblemandservices>();
            public List<PMproblemandservices> _PMservices = new List<PMproblemandservices>();
            public class PMproblemandservices
            {
                public string Id { get; set; }
                public string MasterId { get; set; }
                public string Description { get; set; }
                public string Parentid { get; set; }
            }
        //End
            public List<CustomQuestion1> CustomQues1 = new List<CustomQuestion1>();
            public class CustomQuestion1
            {
                public int CQID { get; set; }
                public string ProgramType { get; set; }
                public string QuesYes { get; set; }
                public string Question { get; set; }
                public string QuesNo { get; set; }
                public string point { get; set; }
                public string totalcustompoint { get; set; }

            }
            //Added by akansha on 16Dec2016 for EHS Nutrition Question
            public string EhsPersistentNausea { get; set; }
            public string EhsPersistentDiarrhea { get; set; }
            public string EhsPersistentConstipation { get; set; }
            public string EhsConstipationcomment { get; set; }
            public string EhsNauseaorVomitingcomment { get; set; }
            public string EhsDiarrheaComment { get; set; }
            public string EhsDramaticWeightchangecomment { get; set; }
            public string EhsRecentSurgery { get; set; }
            public string EhsRecentSurgerycomment { get; set; }
            public string EhsRecentHospitalization { get; set; }
            public string EhsChildVitaminSupplment { get; set; }
            public string EhsRecentHospitalizationComment { get; set; }
            public string EhsChildSpecialDiet { get; set; }
            public string EhsSpecialDietComment { get; set; }
            public string EhsFoodAllergies { get; set; }
            public string EhsFoodAllergiesComment { get; set; }
            public string EhsNutritionalConcern { get; set; }
            public string EhsNutritionAlconcernsComment { get; set; }
            public string EhsFoodPantory { get; set; }
            public string EhschildTrouble { get; set; }
            public string EhsChewingorSwallowingcomment { get; set; }
            public string Ehsspoon { get; set; }
            public string EhsSpoonorForkComment { get; set; }
            public string Ehsfeedingtube { get; set; }
            public string EhsSpecialFeedingComment { get; set; }
            public int EhschildThin { get; set; }
            public string EhsTakebottle { get; set; }
            public string EhsBottleComment { get; set; }
            public string Ehschewanything { get; set; }
            public string EhsEatOrChewComment { get; set; }
            public string EhsNonFoodItems { get; set; }
            public string EhsChangeinAppetite { get; set; }
            public string EhsChildHungry { get; set; }
            public string EhsMilkComment { get; set; }
            public string EhsChildFruitJuicevitaminc { get; set; }
            public string EhsChildWater { get; set; }
            public bool EhsNA { get; set; }
            public bool EhsBreakfast { get; set; }
            public bool EhsDinner { get; set; }
            public bool EhsLunch { get; set; }
            public bool EhsSnack { get; set; }
            public string EhsRestrictFood { get; set; }
            public string EhsRestrictFoodComment { get; set; }









            //End


            //Changes
            public string ChildReferenceProgramID { get; set; }
            public int HealthQuesId { get; set; }
            public int NutritionQuesId { get; set; }
            public string ChildPostedDisease { get; set; }
            public string ChildPostedDiagnosedDisease { get; set; }
            public string ChildPostedMedicalDiagnosedDisease { get; set; }
            public string ChildPostedRecieveTreatmentDisease { get; set; }
            public string ChildPostedVitamin { get; set; }
            public string ChildPostedDietFull { get; set; }
            public string ChildPostedDrink { get; set; }
            public string ChildPostedEHS { get; set; }
            public string ChildPostedEHSMedical { get; set; }
            public string foodpantry { get; set; }
        //end
            public List<ChildReferalCriteriaInfo> CReferalCriteriaList = new List<ChildReferalCriteriaInfo>();
            public List<ChildFeedCerealInfo> CFeedCerealList = new List<ChildFeedCerealInfo>();
            public List<ChildFeedInfo> CFeedList = new List<ChildFeedInfo>();
            public List<ChildFormulaInfo> CFormulaList = new List<ChildFormulaInfo>();
            public List<ChildHungryInfo> ChungryList = new List<ChildHungryInfo>();
            public List<ChildDietInfo> dietList = new List<ChildDietInfo>();
            public List<ChildFoodInfo> foodList = new List<ChildFoodInfo>();

            public List<ChildDirectBloodRelative> AvailableDisease { get; set; }
            public List<ChildDirectBloodRelative> SelectedDisease { get; set; }
            public PostedDisease PostedPostedDisease { get; set; }

            public List<ChildDiagnosedDisease> AvailableDiagnosedDisease { get; set; }
            public List<ChildDiagnosedDisease> SelectedDiagnosedDisease { get; set; }
            public PostedDiagnosedDisease PostedPostedDiagnosedDisease { get; set; }
        //Changes
            //public List<ChildDiagnosedDisease> AvailableMedicalDiagnosedDisease { get; set; }
            //public List<ChildDiagnosedDisease> SelectedMedicalDiagnosedDisease { get; set; }
            public PostedDiagnosedDisease PostedPostedMedicalDiagnosedDisease { get; set; }
            public PostedDiagnosedDisease PostedPostedRecieveTreatmentDisease { get; set; }
        //End
            public List<ChildDental> AvailableDental { get; set; }
            public List<ChildDental> SelectedDental { get; set; }
            public PostedDentalTreatment PostedPostedDental { get; set; }
            //Added by Akansha on 12Dec2016
            public string ChildVitaminSupplment { get; set; }
            public string ChildVitaminSupplmentComment { get; set; }
            public string RestrictFood { get; set; }
            public string RestrictFoodComment { get; set; }
            public string ChildProfessionalDentalExam { get; set; }
            public string EhsDramaticWeight { get; set; }
            //End
            public List<ChildEHS> AvailableEHS { get; set; }
            public List<ChildEHS> SelectedEHS { get; set; }
            public PostedChildEHS PostedPostedEHS { get; set; }
            public PostedChildEHS PostedPostedMedicalEHS { get; set; }


            public List<ChildDrink> AvailableChildDrink { get; set; }
            public List<ChildDrink> SelectedChildDrink { get; set; }
            public PostedChildDrink PostedPostedChildDrink { get; set; }

        //Changes
            public List<ChildDietFull> AvailableChildDietFull { get; set; }
            public List<ChildDietFull> SelectedChildDietFull { get; set; }
            public PostedChildDiet PostedPostedChildDietFull { get; set; }

            public List<ChildVitamin> AvailableChildVitamin { get; set; }
            public List<ChildVitamin> SelectedChildVitamin { get; set; }
            public PostedChildVitamin PostedPostedChildVitamin { get; set; }

            public List<HSOtherCondition> OtherData = new List<HSOtherCondition>();
           public class HSOtherCondition
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
            public class ChildVitamin
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedChildVitamin
            {
                public string[] CDietInfoID { get; set; }
            }
            public class ChildDietFull
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }

            public class PostedChildDiet
            {
                public string[] CDietInfoID { get; set; }
            }


            public bool Breakfast { get; set; }
            public bool Lunch { get; set; }
            public bool Snack { get; set; }
            public bool Dinner { get; set; }
            public bool NA { get; set; }
           // public bool Specialdiet { get; set; }
            public bool Childdoesnoteat { get; set; }
            public class ChildDrink
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedChildDrink
            {
                public string[] CDrinkID { get; set; }
            }
        //Changes
            public class ChildHungryInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class ChildFeedInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class ChildReferalCriteriaInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
        
            public class ChildFeedCerealInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class ChildFormulaInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class ChildDietInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
            public class ChildFoodInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }

            public class ChildDirectBloodRelative
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedDisease
            {
                public string[] DiseaseID { get; set; }
            }
            public class ChildDiagnosedDisease
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedDiagnosedDisease
            {
                public string[] DiagnoseDiseaseID { get; set; }
                public string[] MedicalDiagnoseDiseaseID { get; set; }
                public string[] ChronicHealthConditionsID { get; set; }
            }

            public class ChildDental
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedDentalTreatment
            {
                public string[] DentalTreatmentID { get; set; }
            }
            public class ChildEHS
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public bool IsSelected { get; set; }
                //  public string ReferenceId { get; set; }
            }
            public class PostedChildEHS
            {
                public string[] ChildEHSID { get; set; }
            }


            public class NurseScreening
            {
                public string Screeningid { get; set; }
                public string Screeningname { get; set; }
                public string  Missingcount { get; set; }
               
            }
            public class  clients
            {
                public string Screeningid { get; set; }
                public string clientid { get; set; }
                public string classid { get; set; }
                public string centerid { get; set; }
                public string name { get; set; }
                public string Screeningname { get; set; }
                public string ScreeningDate { get; set; }
                public string Status { get; set; }
                public string Notes { get; set; }
                public string Exception { get; set; }
                public List<ScreeningStatus> _ScreeningStatus { get; set; }
            }
            public class ScreeningStatus
            {
                public string Optionid { get; set; }
                public string Optionname { get; set; }
            }



            public class Childcustomscreening
            {
                public string QuestionID { get; set; }
                public string Value { get; set; }
                public string QuestionAcronym { get; set; }
                public string optionid { get; set; }
                public string ScreeningDate { get; set; }
                public string Screeningid { get; set; }
                public string Status { get; set; }
                public string DocumentName { get; set; }
                public string DocumentExtension { get; set; }
                public string Documentdata { get; set; }
                public string pdfpath { get; set; }
            }
    }


    public class ScreeningMatrix
    {
        public List<List<string>> Screenings { get; set; }
        public List<ClassRoom> Classroom { get; set; }
        public List<Roster> ClientsClassroom { get; set; }
    }


}
