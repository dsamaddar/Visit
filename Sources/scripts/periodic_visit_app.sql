
/*fetch data from mHRM for user credentials to store in tblVisitUsers*/
select i.EmployeeID,i.EmployeeName,i.UserID,e.Password,i.OfficialDesignation,i.DeptName,i.CurrentSupervisor,i.Email
from vwEmpInfo i inner join tblEmployeeInfo e on i.EmployeeID = e.EmployeeID 
where e.isActive =1 and e.IncludedInPayroll = 1;

GO

USE CoreDB;

/* Table definition */
/* Clear All visit tables
drop table tblVisitDocuments;
drop table tblVisitRelatedParty;
drop table tblVisitContacts;
drop table tblVisitAgreementSummary;
drop table tblVisitPaymentStatus;
drop table tblWeightMatrix;
drop table tblVisitReports;
*/
GO
-- select distinct Department from tblVisitUsers;
-- drop table tblVisitUsers
create table tblVisitUsers(
EmployeeID nvarchar(50) primary key,
EmployeeName nvarchar(100),
UserID nvarchar(50) unique,
Password nvarchar(50),
Designation nvarchar(50),
Department nvarchar(50),
CurrentSupervisor nvarchar(50),
Email nvarchar(50),
DigitalSignature nvarchar(50)
);

GO

-- exec spListEmployees;
create proc spListEmployees
as
begin
	select EmployeeID,EmployeeName from tblVisitUsers where Department IN ('CRM','Consumer - Home Loan','Corporate & SME','Recovery','Branch in charge')
	UNION ALL
	select EmployeeID,EmployeeName from tblVisitUsers where EmployeeID IN ('EMP-00000272')
	order by EmployeeName;
end

GO
-- select * from tblVisitReports where ReportID='20240214121854';
-- drop table tblVisitReports
create table tblVisitReports(
ReportID nvarchar(50) primary key,
AgreementID nvarchar(50),
CustomerID nvarchar(50),
BorrowerName nvarchar(100),
RegisteredOffice nvarchar(500),
FactoryAddress nvarchar(500),
ProductType nvarchar(100),
PurposeOfLoan nvarchar(100),
Security nvarchar(200),
AsOnDate nvarchar(50),
BusinessNature nvarchar(50),
PaymentBehaviorID int,
PaymentBehavior nvarchar(100),
PaymentBehaviorScore numeric(5,2) default 0,
CurrentStock nvarchar(50),
LiabilityPosition nvarchar(50),
Warehouse nvarchar(500),
ElaboratePurpose nvarchar(500),
CapacityUtilizationID int,
CapacityUtilization nvarchar(100),
CapacityUtilizationScore numeric(5,2) default 0,
BusinessExpansionID int,
BusinessExpansion nvarchar(100),
BusinessExpansionScore numeric(5,2) default 0,
TurnoverGrowthID int,
TurnoverGrowth nvarchar(100),
TurnoverGrowthScore numeric(5,2) default 0,
Remarks nvarchar(500),
IsSubmitted bit default 0,
SubmissionDate datetime,
IsApproved bit default 0,
IsRejected bit default 0,
ApproverRemarks nvarchar(500),
ApproverID nvarchar(50),
ApprovalDate datetime,
FinalScore numeric(5,2) default 0,
EntryBy nvarchar(50),
EntryDate datetime default getdate(),
LastUpdate datetime
);

GO

-- drop table tblVisitRelatedParty
create table tblVisitRelatedParty(
RelatedPartyID int identity(1,1) primary key,
ReportID nvarchar(50) foreign key references tblVisitReports(ReportID),
CustomerID nvarchar(50),
Role nvarchar(50),
RelatedPerson nvarchar(100),
Relation nvarchar(100),
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO

-- drop table tblVisitContacts
Create table tblVisitContacts(
ContactID int identity(1,1) primary key,
ReportID nvarchar(50) foreign key references tblVisitReports(ReportID),
ContactPerson nvarchar(100),
Relation nvarchar(100),
ContactNo nvarchar(100),
VisitingOfficialID nvarchar(50) foreign key references tblVisitUsers(EmployeeID),
VisitingDate date,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO

-- drop table tblVisitDocuments
Create table tblVisitDocuments(
DocumentID int identity(1,1) primary key,
ReportID nvarchar(50) foreign key references tblVisitReports(ReportID),
DocumentName nvarchar(100),
DocumentType nvarchar(50),
Attachment nvarchar(50),
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO
-- drop table tblVisitAgreementSummary
create table tblVisitAgreementSummary(
AgrSummaryID int identity(1,1) primary key,
ReportID nvarchar(50) foreign key references tblVisitReports(ReportID),
AgreementID nvarchar(50),
DisbursementDt date,
DisbursementAmt numeric(18,2),
Tenor int,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO
-- drop table tblVisitPaymentStatus
create table tblVisitPaymentStatus(
AgrSummaryID int identity(1,1) primary key,
ReportID nvarchar(50) foreign key references tblVisitReports(ReportID),
AgreementID nvarchar(50),
InstallmentDue int,
InstallmentPaid int,
OutstandingAmnt numeric(18,2),
OverdueNo int,
OverdueAmnt numeric(18,2),
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO

-- select * from tblWeightMatrix
-- drop table tblWeightMatrix
create table tblWeightMatrix(
WeightMatrixID int identity(1,1),
WeightItem nvarchar(100),
BusinessArea nvarchar(100),
BusinessNature nvarchar(100),
WeightValue numeric(5,2),
VOrder int
);

GO

-- select dbo.fnGetWeightItemByID(1)
create function fnGetWeightItemByID(@WeightMatrixID int)
returns nvarchar(100)
as
begin
	declare @WeightItem nvarchar(100) set @WeightItem = '';

	select @WeightItem = WeightItem from tblWeightMatrix where WeightMatrixID = @WeightMatrixID

	return isnull(@WeightItem,'');
end

GO
-- select dbo.fnGetWeightScoreByID(1)
-- drop function fnGetWeightScoreByID
create function fnGetWeightScoreByID(@WeightMatrixID int)
returns numeric(5,2)
as
begin
	declare @WeightValue numeric(5,2) set @WeightValue = 0;

	select @WeightValue = WeightValue from tblWeightMatrix where WeightMatrixID = @WeightMatrixID

	return isnull(@WeightValue,0);
end

GO

-- exec spListWeightValues 'CapacityUtilization','Manufacturing'
create proc spListWeightValues
@BusinessArea nvarchar(100),
@BusinessNature nvarchar(100)
as
begin
	Select WeightMatrixID,WeightItem from tblWeightMatrix where BusinessArea = @BusinessArea and 
	BusinessNature = @BusinessNature
	order by VOrder;
end

GO
--Payment Behavior
insert into tblWeightMatrix(WeightItem,BusinessArea,BusinessNature,WeightValue,VOrder)
values('Regular with no OD/delay','PaymentBehavior','Manufacturing',1,1),
('Regular with occational delay','PaymentBehavior','Manufacturing',1,2),
('Regular with occational OD','PaymentBehavior','Manufacturing',1,3),
('Irregular Payment with below 2 OD','PaymentBehavior','Manufacturing',1,4),
('Irregular Payment with above 2 OD','PaymentBehavior','Manufacturing',1,5),
('Regular with no OD/delay','PaymentBehavior','Service',1,1),
('Regular with occational delay','PaymentBehavior','Service',1,2),
('Regular with occational OD','PaymentBehavior','Service',1,3),
('Irregular Payment with below 2 OD','PaymentBehavior','Service',1,4),
('Irregular Payment with above 2 OD','PaymentBehavior','Service',1,5),
('Regular with no OD/delay','PaymentBehavior','Trading',1,1),
('Regular with occational delay','PaymentBehavior','Trading',1,2),
('Regular with occational OD','PaymentBehavior','Trading',1,3),
('Irregular Payment with below 2 OD','PaymentBehavior','Trading',1,4),
('Irregular Payment with above 2 OD','PaymentBehavior','Trading',1,5);

GO
--Turnover Growth
GO
insert into tblWeightMatrix(WeightItem,BusinessArea,BusinessNature,WeightValue,VOrder)
values('2-5%','TurnoverGrowth','Manufacturing',1,1),
('6-10%','TurnoverGrowth','Manufacturing',1,2),
('11-15%','TurnoverGrowth','Manufacturing',1,3),
('16-20%','TurnoverGrowth','Manufacturing',1,4),
('> 20%','TurnoverGrowth','Manufacturing',1,5),
('(-) < 5%','TurnoverGrowth','Manufacturing',1,6),
('(-) 6-10%','TurnoverGrowth','Manufacturing',1,7),
('(-) 11-20%','TurnoverGrowth','Manufacturing',1,8),
('(-) 21-30%','TurnoverGrowth','Manufacturing',1,9),
('(-) > 30%','TurnoverGrowth','Manufacturing',1,10),
('2-5%','TurnoverGrowth','Service',1,1),
('6-10%','TurnoverGrowth','Service',1,2),
('11-15%','TurnoverGrowth','Service',1,3),
('16-20%','TurnoverGrowth','Service',1,4),
('> 20%','TurnoverGrowth','Service',1,5),
('(-) < 5%','TurnoverGrowth','Service',1,6),
('(-) 6-10%','TurnoverGrowth','Service',1,7),
('(-) 11-20%','TurnoverGrowth','Service',1,8),
('(-) 21-30%','TurnoverGrowth','Service',1,9),
('(-) > 30%','TurnoverGrowth','Service',1,10),
('2-5%','TurnoverGrowth','Trading',1,1),
('6-10%','TurnoverGrowth','Trading',1,2),
('11-15%','TurnoverGrowth','Trading',1,3),
('16-20%','TurnoverGrowth','Trading',1,4),
('> 20%','TurnoverGrowth','Trading',1,5),
('(-) < 5%','TurnoverGrowth','Trading',1,6),
('(-) 6-10%','TurnoverGrowth','Trading',1,7),
('(-) 11-20%','TurnoverGrowth','Trading',1,8),
('(-) 21-30%','TurnoverGrowth','Trading',1,9),
('(-) > 30%','TurnoverGrowth','Trading',1,10);
GO
--Capacity Utilization
insert into tblWeightMatrix(WeightItem,BusinessArea,BusinessNature,WeightValue,VOrder)
values('< 30%','CapacityUtilization','Manufacturing',1,1),
('31-40%','CapacityUtilization','Manufacturing',1,2),
('41-50%','CapacityUtilization','Manufacturing',1,3),
('51-60%','CapacityUtilization','Manufacturing',1,4),
('61-70%','CapacityUtilization','Manufacturing',1,5),
('71-80%','CapacityUtilization','Manufacturing',1,6),
(' > 80%','CapacityUtilization','Manufacturing',1,7),
('Premise Shutdown','CapacityUtilization','Manufacturing',1,8),
('Not Applicable','CapacityUtilization','Service',0,1),
('Premise Shutdown','CapacityUtilization','Service',0,2),
('Not Applicable','CapacityUtilization','Trading',0,1),
('Premise Shutdown','CapacityUtilization','Trading',0,2);
GO
--Business Expansion
insert into tblWeightMatrix(WeightItem,BusinessArea,BusinessNature,WeightValue,VOrder)
values('Capacity Expansion','BusinessExpansion','Manufacturing',1,1),
('New Product Line','BusinessExpansion','Manufacturing',1,2),
('Business Point/Outlet','BusinessExpansion','Manufacturing',1,3),
('Factory Area Expansion','BusinessExpansion','Manufacturing',1,4),
('Not Applicable','BusinessExpansion','Manufacturing',1,5),
('Capacity Expansion','BusinessExpansion','Service',1,1),
('New Product Line','BusinessExpansion','Service',1,2),
('Business Point/Outlet','BusinessExpansion','Service',1,3),
('Factory Area Expansion','BusinessExpansion','Service',1,4),
('Not Applicable','BusinessExpansion','Service',1,5),
('Capacity Expansion','BusinessExpansion','Trading',1,1),
('New Product Line','BusinessExpansion','Trading',1,2),
('Business Point/Outlet','BusinessExpansion','Trading',1,3),
('Factory Area Expansion','BusinessExpansion','Trading',1,4),
('Not Applicable','BusinessExpansion','Trading',1,5);

GO
--Liability Position

GO

/* Function Definition*/

-- select dbo.fnGetAgreementSecurity('14101221036-0-0')
create function fnGetAgreementSecurity(@AgreementID nvarchar(50))
returns nvarchar(500)
as
begin
	Declare @agrSecurity as nvarchar(500) set @agrSecurity = '';
	Declare @SecurityType as nvarchar(50) set @SecurityType = '';
	Declare @SecurityValue as nvarchar(50) set @SecurityValue = '';

	DECLARE db_cursor CURSOR FOR
	select SecurityType,SecurityValue from AgreementSecurity where AgreementID = @AgreementID
	and SecurityType in ('Advance EMI','Cash Security', 'Registered Mortgage', 'TDR Lien')
	order by SecurityType;

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @SecurityType,@SecurityValue 

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		  set @agrSecurity += @SecurityType + ' : ' + FORMAT(convert(numeric,@SecurityValue), 'N', 'en-us')  + ' | ';

		  FETCH NEXT FROM db_cursor INTO @SecurityType,@SecurityValue 
	END 

	CLOSE db_cursor  
	DEALLOCATE db_cursor

	return isnull(@agrSecurity,'');
end

GO

create function fnGetCustomerAddressByType(@CustomerID nvarchar(50),@AddressType nvarchar(50))
returns nvarchar(500)
begin
	Declare @CustAddress as nvarchar(500) set @CustAddress = 'N\A';
	select @CustAddress = ISNULL(Address,'') + ', '+ isnull(District,'') + '-' + isnull(PostalCode,'') + ', ' + isnull(Country,'')
	from CustomerAddress where CustomerID = @CustomerID and AddressType = @AddressType;

	return ISNULL(@CustAddress,'N\A');
end

GO

create function fnGetOrgAddress(@CustomerID nvarchar(50))
returns nvarchar(500)
begin
	Declare @OrgAddress as nvarchar(500) set @OrgAddress = 'N\A';
	select @OrgAddress = ISNULL(OrgAddress,'') + ', '+ isnull(OrgAddressDistrict,'') + '-' + isnull(OrgAddressPostalCode,'') + ', ' + isnull(OrgAddressCountry,'') + ', ' + ISNULL(OrgContactNo,'')
	from CustomerGeneral where CustomerID = @CustomerID;

	return ISNULL(@OrgAddress,'N\A');
end

GO

-- select dbo.fnGetCustomerAddressByType('000895','Registered Office')

GO

/* Stored Procedure Definition*/

-- exec spGetRelatedParty '14101221036-0-0'
-- exec spGetRelatedParty'14600201014-0-0'
create proc spGetRelatedParty
@AgreementID nvarchar(50)
as
begin
	select SecurityID as CustomerID,SecurityType as Role,Ref1 as RelatedPerson,Ref2 as Relation
	from AgreementSecurity where AgreementID = @AgreementID and NatureOfSecurity in (
	'Personal Guarantee','Corporate Guarantee'
	);
end

GO

-- exec spGetAgreementInfoByID '14101221036-0-0'
-- exec spGetAgreementInfoByID '14400231265-0-0'
-- exec spGetAgreementInfoByID '14201221072-0-0'
-- exec spGetAgreementInfoByID '14600201014-0-0'
-- exec spGetAgreementInfoByID '14101221028-0-0'
create proc spGetAgreementInfoByID
@AgreementID nvarchar(50)
as
begin
	Declare @CustomerID as nvarchar(50) set @CustomerID = '';
	Declare @AgreementNo as nvarchar(50) set @AgreementNo = '';
	Declare @CustomerName as nvarchar(200) set @CustomerName = '';
	Declare @Address as nvarchar(200) set @Address = '';
	Declare @BranchName as nvarchar(50) set @BranchName = '';
	Declare @ProductName as nvarchar(50) set @ProductName = '';
	Declare @LoanCategory as nvarchar(50) set @LoanCategory = '';
	Declare @FinanceAmount as numeric(18,2) set @FinanceAmount = 0;
	Declare @RegisteredOffice as nvarchar(500) set @RegisteredOffice = '';
	Declare @FactoryAddress as nvarchar(500) set @FactoryAddress = '';
	Declare @DisbursementAmnt as numeric(18,2) set @DisbursementAmnt = 0;
	Declare @DisbursementDt as date
	Declare @Tenure as nvarchar(50) set @Tenure = '';
	Declare @Security as nvarchar(500) set @Security = '';
	Declare @InstallmentDue as nvarchar(50) set @InstallmentDue = '';
	Declare @InstallmentPaid as nvarchar(50) set @InstallmentPaid = '';
	Declare @OutstandingAmnt as nvarchar(50) set @OutstandingAmnt = '';
	Declare @OverdueAmnt as nvarchar(50) set @OverdueAmnt = '';
	Declare @AsOnDate as nvarchar(50) set @AsOnDate = GETDATE();

	select @CustomerID = CustomerID,@AgreementNo=AgreementNo,@BranchName=BranchName,@ProductName = ProductName,@LoanCategory = ISNULL(LoanPurpose,'N\A'),
	@DisbursementDt=ExecutionDate,@DisbursementAmnt= FinanceAmount,@Tenure = FinancePeriod
	from Agreement where AgreementID = @AgreementID

	Select @CustomerName = CustomerName from CustomerGeneral Where CustomerID=@CustomerID;
	Select @RegisteredOffice = dbo.fnGetOrgAddress(@CustomerID);
	Select @FactoryAddress = dbo.fnGetCustomerAddressByType(@CustomerID,'Factory Address');

	select @Security = dbo.fnGetAgreementSecurity(@AgreementID);
	select @InstallmentDue = dbo.getNoOfODInstallment(@AgreementID,GETDATE());
	select @InstallmentPaid = dbo.fnGetNoOfPaidInstallment(@AgreementID,@DisbursementDt,GETDATE());
	select @OutstandingAmnt = [dbo].[GetOutstandingAmount](@AgreementID,0,GETDATE())
	select @OverdueAmnt = [dbo].[fnGetOverDueAmount](@AgreementID,@DisbursementDt,GETDATE());

	select @AgreementID as AgreementID,@AgreementNo as AgreementNo,@CustomerID as CustomerID, @CustomerName as CustomerName,
	@RegisteredOffice as RegisteredOffice,@FactoryAddress as FactoryAddress,@BranchName as BranchName, 
	@ProductName as ProductName,@LoanCategory as LoanCategory,ISNULL(convert(nvarchar,@DisbursementDt,106),'') as DisbursementDt,
	ISNULL(@DisbursementAmnt,'') as DisbursementAmnt,@Tenure as Tenure, @Security as 'Security', @InstallmentDue as InstallmentDue,
	@InstallmentPaid as InstallmentPaid, @OutstandingAmnt as OutstandingAmnt, @OverdueAmnt as OverdueAmnt,
	FORMAT(convert(date,@AsOnDate),'dd-MMM-yyyy') as AsOnDate
end

GO
-- exec spGetAgrSummaryByAgrNo '14101221036'
create proc spGetAgrSummaryByAgrNo
@AgreementNo nvarchar(50)
as
begin
	select AgreementID,convert(nvarchar,ExecutionDate,106) as DisbursementDt,FinanceAmount as DisbursementAmt,FinancePeriod as Tenor 
	from Agreement where AgreementNo=@AgreementNo
end

GO

-- select dbo.fnGetNoOfODueInstallment('14101221028-0-0','02/13/2024')
create function fnGetNoOfODueInstallment(@AgreementId as nvarchar(100), @CutOffDate datetime) returns int       
as        
begin  
	declare @NoOfODInstallment nvarchar(100)
 --declare @AgreementId nvarchar(100), @CutOffDate datetime
 --set @AgreementId = '0120000218401-0-0'
 --set @CutOffDate = getdate()
 
 select @NoOfODInstallment = count(InstallmentNo)
    from PaymentSchedule
    where AgreementID = @AgreementID and InstallmentDate <= @CutOffDate
    and InstallmentNo > 0
    and (ApplicationStatus in ('Due','Paid','Receivable')
	and ISNULL(PaymentDate,InstallmentDate) <= @CutOffDate)
   
 return  @NoOfODInstallment         
end

GO
-- exec spGetAgrPaymentStatusByAgrNo '14101221036'
create proc spGetAgrPaymentStatusByAgrNo
@AgreementNo nvarchar(50)
as
begin
	select AgreementID,convert(int,dbo.fnGetNoOfODueInstallment(AgreementID,GETDATE())) as 'InstallmentDue',
	convert(int,dbo.fnGetNoOfPaidInstallment(AgreementID,ExecutionDate,GETDATE())) as 'InstallmentPaid',
	dbo.GetOutstandingAmount(AgreementID,0,GETDATE()) as 'OutstandingAmnt',
	convert(int,dbo.GetOverDueInstallment(AgreementID,GETDATE())) as 'OverdueNo',
	dbo.fnGetOverDueAmount(AgreementID,ExecutionDate,GETDATE()) as 'OverdueAmnt'
	from Agreement where AgreementNo=@AgreementNo
end

GO

create proc spCheckVisitUserLogin
@UserID nvarchar(50),
@Password nvarchar(50)
as
begin
	Select * from tblVisitUsers Where UserID=@UserID and Password=@Password;
end

GO
-- drop proc spGenVisitReportID
Create proc spGenVisitReportID
as
begin
	SELECT FORMAT (getdate(), 'yyyyMMddhhmmss') as ReportID
end

GO

-- exec spDeleteVisitReport '20240229121524'
create proc spDeleteVisitReport
@ReportID nvarchar(50)
as
begin
	delete from tblVisitPaymentStatus where ReportID = @ReportID;
	delete from tblVisitAgreementSummary where ReportID = @ReportID;
	delete from tblVisitContacts where ReportID = @ReportID;
	delete from tblVisitDocuments where ReportID = @ReportID;
	delete from tblVisitRelatedParty where ReportID = @ReportID;
	delete from tblVisitReports where ReportID = @ReportID;
end

GO

-- exec spGetReportDetails 20240526045638
create proc spGetReportDetails
@ReportID nvarchar(50)
as
begin
	
	Declare @visitDate as date

	if exists(select * from tblVisitContacts where ReportID = @ReportID)
	begin
		select @visitDate = max(visitingDate) from tblVisitContacts where ReportID = @ReportID;
	end
	else
		set @visitDate = GETDATE();

	select AgreementID,CustomerID,BorrowerName,RegisteredOffice,FactoryAddress,ProductType,ISNULL(PurposeOfLoan,'') as PurposeOfLoan,ISNULL(Security,'') Security,
	FORMAT(convert(date,ISNULL(AsOnDate,GETDATE())),'dd-MMM-yyyy') as AsOnDate,BusinessNature,PaymentBehaviorID,ISNULL(PaymentBehavior,'') PaymentBehavior,
	isnull(PaymentBehaviorScore,0) PaymentBehaviorScore,
	ISNULL(CurrentStock,'') CurrentStock,ISNULL(r.LiabilityPosition,'') LiabilityPosition,ISNULL(Warehouse,'') Warehouse,ISNULL(Remarks,'') Remarks,
	CapacityUtilizationID,ISNULL(CapacityUtilization,'') CapacityUtilization,ISNULL(CapacityUtilizationScore,0) CapacityUtilizationScore,
	BusinessExpansionID,ISNULL(BusinessExpansion,'') BusinessExpansion,ISNULL(BusinessExpansionScore,0) BusinessExpansionScore,
	TurnoverGrowthID,ISNULL(TurnoverGrowth,'') TurnoverGrowth,ISNULL(TurnoverGrowthScore,0) TurnoverGrowthScore,
	u.EmployeeName,u.Designation,u.Department,FORMAT(ISNULL(@visitDate,r.EntryDate),'dd-MMM-yyyy') as SubmissionDate,
	ISNULL(FinalScore,0) FinalScore,r.IsApproved,r.IsRejected,ISNULL(r.ApproverRemarks,'') ApproverRemarks,
	ISNULL(x.EmployeeName,'') as Supervisor,ISNULL(x.Designation,'') as SupervisorDesignation, 
	ISNULL(x.Department,'') as SupervisorDepartment, 
	CONVERT(nvarchar,ISNULL(r.ApprovalDate,GETDATE()),106) ApprovalDate,
	ISNULL(u.DigitalSignature,'na.jpg') imgVisitBy,
	ISNULL(x.DigitalSignature,'na.jpg') imgApprovedBy
	from tblVisitReports r 
	inner join tblVisitUsers u on r.EntryBy = u.EmployeeID
	left join tblVisitUsers x on r.ApproverID = x.EmployeeID
	where r.ReportID = @ReportID;
end

GO

-- exec spGetVisitReports 'EMP-00000001'
create proc spGetVisitReports
@EmployeeID nvarchar(50)
as
begin
	select r.ReportID,r.AgreementID,r.ProductType,r.BorrowerName,u.EmployeeName as VisitedBy,
	CASE WHEN r.IsApproved = 1 THEN 'Approved' WHEN r.IsRejected =1 THEN 'Rejected' ELSE 'Pending' END as VisitStatus,
	convert(nvarchar,r.EntryDate,106) as VisitDate 
	from tblVisitReports r inner join tblVisitUsers u on r.EntryBy = u.EmployeeID
	where r.EntryBy = @EmployeeID and r.IsSubmitted=1
	order by VisitDate desc;
end

GO

create proc spPendingVisits
@EmployeeID nvarchar(50)
as
begin
	select r.ReportID,r.AgreementID,r.ProductType,r.BorrowerName,u.EmployeeName as VisitedBy,
	CASE WHEN r.IsApproved = 1 THEN 'Approved' WHEN r.IsRejected =1 THEN 'Rejected' ELSE 'Pending' END as VisitStatus,
	convert(nvarchar,r.EntryDate,106) as VisitDate 
	from tblVisitReports r inner join tblVisitUsers u on r.EntryBy = u.EmployeeID
	where r.EntryBy = @EmployeeID and r.IsSubmitted=0
	order by VisitDate desc;
end

GO

-- select * from tblVisitReports
create proc spGetWaitingApprovalVisits
@EmployeeID nvarchar(50)
as
begin
	select r.ReportID,r.AgreementID,r.ProductType,r.BorrowerName,u.EmployeeName as VisitedBy,
	CASE WHEN r.IsApproved = 1 THEN 'Approved' WHEN r.IsRejected =1 THEN 'Rejected' ELSE 'Pending' END as VisitStatus,
	convert(nvarchar,r.EntryDate,106) as VisitDate 
	from tblVisitReports r inner join tblVisitUsers u on r.EntryBy = u.EmployeeID
	where r.ApproverID = @EmployeeID and r.IsSubmitted=1 and r.IsApproved=0 and r.IsRejected=0
	order by VisitDate desc;
end

GO

create proc spGiveApproval
@ReportID nvarchar(50),
@ApproverRemarks nvarchar(500),
@ApprovalStatus nvarchar(50)
as
begin
	Declare @IsApproved as bit set @IsApproved = 0;
	Declare @IsRejected as bit set @IsRejected = 0;

	if @ApprovalStatus = 'Approved'
		begin
			set @IsApproved = 1;
			set @IsRejected = 0;
		end
	else
		begin
			set @IsApproved = 0;
			set @IsRejected = 1;
		end

	Update tblVisitReports set ApproverRemarks = @ApproverRemarks, IsApproved=@IsApproved,IsRejected = @IsRejected,
	ApprovalDate= GETDATE()
	where ReportID = @ReportID;
end

GO

-- exec spFindVisitReports 'a'
create proc spFindVisitReports
@AgreementID nvarchar(50),
@StartDate date,
@EndDate date
as
begin
	select r.ReportID,r.AgreementID,r.ProductType,r.BorrowerName,u.EmployeeName as VisitedBy,
	CASE WHEN r.IsApproved = 1 THEN 'Approved' WHEN r.IsRejected =1 THEN 'Rejected' ELSE 'Pending' END as VisitStatus,
	convert(nvarchar,r.EntryDate,106) as VisitDate 
	from tblVisitReports r inner join tblVisitUsers u on r.EntryBy = u.EmployeeID
	where (r.AgreementID like '%'+ @AgreementID + '%'
	or r.BorrowerName like '%'+ @AgreementID + '%'
	or r.ProductType like '%'+ @AgreementID + '%'
	or r.BusinessNature like '%'+ @AgreementID + '%'
	or r.ReportID like '%'+ @AgreementID +'%'
	) and r.IsSubmitted = 1 and r.SubmissionDate between @StartDate and @EndDate
	order by SubmissionDate desc;
end

GO

-- exec spGetVisitRelatedPartyByReportID '20231207015220'
create proc spGetVisitRelatedPartyByReportID
@ReportID nvarchar(50)
as
begin
	Select RelatedPartyID,ReportID,CustomerID,Role,RelatedPerson,Relation 
	from tblVisitRelatedParty where ReportID = @ReportID
end

GO

create proc spGetVisitAgrSummaryByReportID
@ReportID nvarchar(50)
as
begin
	select AgreementID,FORMAT(DisbursementDt,'dd-MMM-yyyy') as DisbursementDt,DisbursementAmt,Tenor 
	from tblVisitAgreementSummary where ReportID = @ReportID;
end

GO

create proc spGetVisitPaymentStatusByReportID
@ReportID nvarchar(50)
as
begin
	select AgreementID,InstallmentDue,InstallmentPaid,OutstandingAmnt,OverdueNo,OverdueAmnt 
	from tblVisitPaymentStatus where ReportID = @ReportID;
end

GO

-- exec spGetContactsByReportID '20240209061209'
create proc spGetContactsByReportID
@ReportID nvarchar(50)
as
begin
	select ContactID,ReportID,ContactPerson,Relation,ContactNo,VisitingOfficialID,u.EmployeeName as VisitingOfficial,
	convert(nvarchar,VisitingDate,106) VisitingDate,EntryBy,EntryDate 
	from tblVisitContacts c inner join tblVisitUsers u on c.VisitingOfficialID = u.EmployeeID
	where ReportID = @ReportID
end

GO

Create proc spGetDocumentsByReportID
@ReportID nvarchar(50)
as
begin
	Select DocumentID,ReportID,DocumentName,DocumentType,Attachment,EntryBy,EntryDate
	from tblVisitDocuments where ReportID = @ReportID;
end

GO

create proc spInsertVisitReport
@ReportID nvarchar(50),
@AgreementID nvarchar(50),
@BusinessNature nvarchar(50),
@PaymentBehaviorID int,
@CurrentStock nvarchar(50),
@LiabilityPosition nvarchar(50),
@Warehouse nvarchar(500),
@Remarks nvarchar(500),
@CapacityUtilizationID int,
@BusinessExpansionID int,
@TurnoverGrowthID int,
@AgrSummaryList nvarchar(4000),
@PaymentStatusList nvarchar(4000),
@RelatedPartyList nvarchar(4000),
@ContactList nvarchar(4000),
@DocumentList nvarchar(4000),
@IsSubmitted bit,
@EntryBy nvarchar(50)
as
begin
	Declare @CustomerID as nvarchar(50) set @CustomerID = '';
	Declare @ProductType as nvarchar(100) set @ProductType = '';
	Declare @BorrowerName as nvarchar(100) set @BorrowerName = '';
	Declare @FactoryAddress as nvarchar(500) set @FactoryAddress = '';
	Declare @PurposeOfLoan as nvarchar(100) set @PurposeOfLoan = '';
	Declare @RegisteredOffice as nvarchar(500) set @RegisteredOffice = '';
	Declare @Security as nvarchar(500) set @Security = '';
	Declare @AsOnDate as nvarchar(50) set @AsOnDate = GETDATE();
	Declare @SubmissionDate as datetime
	Declare @CurrentSupervisor as nvarchar(50) set @CurrentSupervisor = '';

begin tran

	select @CustomerID = CustomerID,@ProductType = ProductName,@PurposeOfLoan = ISNULL(LoanPurpose,'') from Agreement where AgreementID = @AgreementID

	Select @BorrowerName = CustomerName from CustomerGeneral Where CustomerID=@CustomerID;
	Select @RegisteredOffice = dbo.fnGetOrgAddress(@CustomerID);
	Select @FactoryAddress = dbo.fnGetCustomerAddressByType(@CustomerID,'Factory Address');
	select @Security = dbo.fnGetAgreementSecurity(@AgreementID);
	select @CurrentSupervisor = CurrentSupervisor from tblVisitUsers where EmployeeID = @EntryBy;
	set @CurrentSupervisor = ISNULL(@CurrentSupervisor,@EntryBy);

	if @IsSubmitted = 1
	begin
		set @SubmissionDate = GETDATE()
	end

	if exists (select * from tblVisitReports where ReportID = @ReportID)
	begin

		Update tblVisitReports set BusinessNature=@BusinessNature,PaymentBehaviorID = @PaymentBehaviorID, CurrentStock = @CurrentStock,LiabilityPosition=@LiabilityPosition,
		Warehouse = @Warehouse,Remarks= @Remarks,CapacityUtilizationID = @CapacityUtilizationID, BusinessExpansionID = @BusinessExpansionID,
		TurnoverGrowthID = @TurnoverGrowthID,IsSubmitted= @IsSubmitted,
		SubmissionDate = GETDATE(),LastUpdate=GETDATE()
		where ReportID = @ReportID;

		delete from tblVisitAgreementSummary where ReportID = @ReportID;
		exec spInsertMultipleAgrSummary @ReportID,@AgrSummaryList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		delete from tblVisitPaymentStatus where ReportID = @ReportID;
		exec spInsertMultiplePaymentStatus @ReportID,@PaymentStatusList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		delete from tblVisitRelatedParty where ReportID = @ReportID;
		exec spInsertMultipleRelatedParty @ReportID,@RelatedPartyList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		delete from tblVisitContacts where ReportID = @ReportID;
		exec spInsertMultipleContacts @ReportID,@ContactList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		delete from tblVisitDocuments where ReportID = @ReportID;
		exec spInsertMultipleDocuments @ReportID,@DocumentList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
	end
	else
	begin
		insert into tblVisitReports(ReportID,AgreementID,CustomerID,ProductType,BorrowerName,RegisteredOffice,FactoryAddress,PurposeOfLoan,
		Security,AsOnDate,BusinessNature,PaymentBehaviorID,CurrentStock,LiabilityPosition,Warehouse,Remarks,CapacityUtilizationID,BusinessExpansionID,
		TurnoverGrowthID,IsSubmitted,SubmissionDate,EntryBy,LastUpdate)
		values(@ReportID,@AgreementID,@CustomerID,@ProductType,@BorrowerName,@RegisteredOffice,@FactoryAddress,ISNULL(@PurposeOfLoan,''),
		@Security,@AsOnDate,@BusinessNature,@PaymentBehaviorID,@CurrentStock,@LiabilityPosition,@Warehouse,@Remarks,@CapacityUtilizationID,@BusinessExpansionID,
		@TurnoverGrowthID,@IsSubmitted,GETDATE(),@EntryBy,GETDATE())
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		exec spInsertMultipleAgrSummary @ReportID,@AgrSummaryList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		exec spInsertMultiplePaymentStatus @ReportID,@PaymentStatusList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		exec spInsertMultipleRelatedParty @ReportID,@RelatedPartyList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		exec spInsertMultipleContacts @ReportID,@ContactList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER

		exec spInsertMultipleDocuments @ReportID,@DocumentList,@EntryBy
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
	end

	if @IsSubmitted = 1
	begin
		Update tblVisitReports set 
		PaymentBehavior=dbo.fnGetWeightItemByID(@PaymentBehaviorID),
		PaymentBehaviorScore = dbo.fnGetWeightScoreByID(@PaymentBehaviorID),
		CapacityUtilization=dbo.fnGetWeightItemByID(@CapacityUtilizationID),
		CapacityUtilizationScore = dbo.fnGetWeightScoreByID(@CapacityUtilizationID),
		BusinessExpansion=dbo.fnGetWeightItemByID(@BusinessExpansionID),
		BusinessExpansionScore = dbo.fnGetWeightScoreByID(@BusinessExpansionID),
		TurnoverGrowth=dbo.fnGetWeightItemByID(@TurnoverGrowthID),
		TurnoverGrowthScore = dbo.fnGetWeightScoreByID(@TurnoverGrowthID),
		FinalScore = dbo.fnGetWeightScoreByID(@PaymentBehaviorID) + dbo.fnGetWeightScoreByID(@CapacityUtilizationID) +
						+ dbo.fnGetWeightScoreByID(@BusinessExpansionID) + dbo.fnGetWeightScoreByID(@TurnoverGrowthID),
		ApproverID = @CurrentSupervisor
		where ReportID = @ReportID;
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

--exec spInsertMultipleAgrSummary '20231213050708','14101221036-0-0~24 Oct 2022~70,000,000.00~12~|14101221036-1-0~07 Nov 2022~50,000,000.00~12~|14101221036-2-0~26 Dec 2022~30,000,000.00~12~|','dsamaddar'
create proc spInsertMultipleAgrSummary
@ReportID nvarchar(50),
@AgrSummaryList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin
	Declare @Index as int
	Declare @CurrentData as nvarchar(4000)
	Declare @RestData as nvarchar(4000)
	Declare @RestPortion as nvarchar(4000)

	Declare @AgreementID as nvarchar(100) set @AgreementID = ''
	Declare @DisbursementDt as date
	Declare @DisbursementAmt as numeric(18,2) set @DisbursementAmt = 0;
	Declare @Tenor as int set @Tenor = 0;
	
begin tran

	set @RestData=@AgrSummaryList
	while @RestData<>''
	begin
		set @Index=CHARINDEX('|',@RestData)
		set @CurrentData=substring(@RestData,1,@Index-1)
		set @RestData=substring(@RestData,@Index+1,len(@RestData))		
		
		set @RestPortion=@CurrentData
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @AgreementID=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @DisbursementDt= convert(date, substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))
		
		set @Index=CHARINDEX('~',@RestPortion)
		print  substring(@RestPortion,1,@Index-1);
		set @DisbursementAmt= convert(numeric(18,2), substring(@RestPortion,1,@Index-1));
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @Tenor= convert(int,substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		
		insert into tblVisitAgreementSummary(ReportID,AgreementID,DisbursementDt,DisbursementAmt,Tenor,EntryBy)
		Values(@ReportID,@AgreementID,@DisbursementDt,@DisbursementAmt,@Tenor,@EntryBy)
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		set @AgreementID = '';
		set @DisbursementAmt = 0;
		set @Tenor = 0;
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end


GO

create proc spInsertMultiplePaymentStatus
@ReportID nvarchar(50),
@PaymentStatusList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin
	Declare @Index as int
	Declare @CurrentData as nvarchar(4000)
	Declare @RestData as nvarchar(4000)
	Declare @RestPortion as nvarchar(4000)

	Declare @AgreementID as nvarchar(100) set @AgreementID = '';
	Declare @InstallmentDue as int set @InstallmentDue = 0;
	Declare @InstallmentPaid as int set @InstallmentPaid = 0;
	Declare @OutstandingAmnt as numeric(18,2) set @OutstandingAmnt = 0;
	Declare @OverdueNo as int set @OverdueNo = 0;
	Declare @OverdueAmnt as numeric(18,2) set @OverdueAmnt = 0;
	
begin tran

	set @RestData=@PaymentStatusList
	while @RestData<>''
	begin
		set @Index=CHARINDEX('|',@RestData)
		set @CurrentData=substring(@RestData,1,@Index-1)
		set @RestData=substring(@RestData,@Index+1,len(@RestData))		
		
		set @RestPortion=@CurrentData
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @AgreementID=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @InstallmentDue=convert(int,substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @InstallmentPaid=convert(int,substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @OutstandingAmnt=convert(numeric(18,2),substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @OverdueNo=convert(int,substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @OverdueAmnt=convert(numeric(18,2),substring(@RestPortion,1,@Index-1))
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		
		insert into tblVisitPaymentStatus(ReportID,AgreementID,InstallmentDue,InstallmentPaid,OutstandingAmnt,OverdueNo,OverdueAmnt,EntryBy)
		Values(@ReportID,@AgreementID,@InstallmentDue,@InstallmentPaid,@OutstandingAmnt,@OverdueNo,@OverdueAmnt,@EntryBy)
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		set @AgreementID = '';
		set @InstallmentDue = 0;
		set @InstallmentPaid = 0;
		set @OutstandingAmnt = 0;
		set @OverdueNo = 0;
		set @OverdueAmnt = 0;
		
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end


GO

create proc spInsertMultipleRelatedParty
@ReportID nvarchar(50),
@RelatedPartyList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin
	Declare @Index as int
	Declare @CurrentData as nvarchar(4000)
	Declare @RestData as nvarchar(4000)
	Declare @RestPortion as nvarchar(4000)

	Declare @CustomerID as nvarchar(100)
	Declare @Role as nvarchar(100)
	Declare @RelatedPerson as nvarchar(100)
	Declare @Relation as nvarchar(100)
	
begin tran

	set @RestData=@RelatedPartyList
	while @RestData<>''
	begin
		set @Index=CHARINDEX('|',@RestData)
		set @CurrentData=substring(@RestData,1,@Index-1)
		set @RestData=substring(@RestData,@Index+1,len(@RestData))		
		
		set @RestPortion=@CurrentData
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @CustomerID=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @Role=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @RelatedPerson=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @Relation=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		
		insert into tblVisitRelatedParty(ReportID,CustomerID,Role,RelatedPerson,Relation,EntryBy)
		Values(@ReportID,@CustomerID,@Role,@RelatedPerson,@Relation,@EntryBy)
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		set @CustomerID = '';
		set @Role = '';
		set @RelatedPerson = '';
		set @Relation = '';
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end


GO

create proc spInsertMultipleContacts
@ReportID nvarchar(50),
@ContactList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin
	Declare @Index as int
	Declare @CurrentData as nvarchar(4000)
	Declare @RestData as nvarchar(4000)
	Declare @RestPortion as nvarchar(4000)

	Declare @ContactPerson as nvarchar(100)
	Declare @Relation as nvarchar(100)
	Declare @ContactNo as nvarchar(100)
	Declare @VisitingOfficialID as nvarchar(100)
	Declare @VisitingDate as nvarchar(100)
	
begin tran

	set @RestData=@ContactList
	while @RestData<>''
	begin
		set @Index=CHARINDEX('|',@RestData)
		set @CurrentData=substring(@RestData,1,@Index-1)
		set @RestData=substring(@RestData,@Index+1,len(@RestData))		
		
		set @RestPortion=@CurrentData
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @ContactPerson=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @Relation=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @ContactNo=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @VisitingOfficialID=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		set @VisitingDate=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		insert into tblVisitContacts(ReportID,ContactPerson,Relation,ContactNo,VisitingOfficialID,VisitingDate,EntryBy)
		Values(@ReportID,@ContactPerson,@Relation,@ContactNo,@VisitingOfficialID,@VisitingDate,@EntryBy)
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		set @ContactPerson = '';
		set @Relation = '';
		set @ContactNo = '';
		set @VisitingOfficialID = '';
		set @VisitingDate = '';
						
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

create proc spInsertMultipleDocuments
@ReportID nvarchar(50),
@DocumentList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin
	Declare @Index as int
	Declare @CurrentData as nvarchar(4000)
	Declare @RestData as nvarchar(4000)
	Declare @RestPortion as nvarchar(4000)

	Declare @DocumentName as nvarchar(100)
	Declare @DocumentType as nvarchar(100)
	Declare @Attachment as nvarchar(100)
	
begin tran

	set @RestData=@DocumentList
	while @RestData<>''
	begin
		set @Index=CHARINDEX('|',@RestData)
		set @CurrentData=substring(@RestData,1,@Index-1)
		set @RestData=substring(@RestData,@Index+1,len(@RestData))		
		
		set @RestPortion=@CurrentData
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @DocumentName=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))	
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @DocumentType=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))
		
		set @Index=CHARINDEX('~',@RestPortion)		
		set @Attachment=substring(@RestPortion,1,@Index-1)
		set @RestPortion=substring(@RestPortion,@Index+1,len(@RestPortion))

		set @Index=CHARINDEX('~',@RestPortion)		
		
		insert into tblVisitDocuments(ReportID,DocumentName,DocumentType,Attachment,EntryBy)
		Values(@ReportID,@DocumentName,@DocumentType,@Attachment,@EntryBy)
		IF (@@ERROR <> 0) GOTO ERR_HANDLER
		
		set @DocumentName = '';
		set @DocumentType = '';
		set @Attachment = '';
	end

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end


GO

Create function fnGetVisitUserNameByID(@EmployeeID nvarchar(50))
returns nvarchar(100)
as
begin
	Declare @VisitUserName as nvarchar(100) set @VisitUserName = '';
	select @VisitUserName = EmployeeName from tblVisitUsers where EmployeeID = @EmployeeID;

	return isnull(@VisitUserName,'')
end

GO

create function fnGetVisitUserEmailByID(@EmployeeID nvarchar(50))
returns nvarchar(100)
as
begin
	Declare @VisitUserEmail as nvarchar(100) set @VisitUserEmail = '';
	select @VisitUserEmail = Email from tblVisitUsers where EmployeeID = @EmployeeID;

	return isnull(@VisitUserEmail,'')
end

GO

-- select * from tblVisitReports where ReportID = '20240229121524'
-- exec spGetVisitMailInfo '20240229121524','Submitted'
create proc spGetVisitMailInfo
@ReportID nvarchar(50),
@ApplicationStatus nvarchar(50)
as
begin

	Declare @AgreementID as nvarchar(50) Set @AgreementID = '';
	Declare @BorrowerName as nvarchar(200) Set @BorrowerName = '';
	Declare @ProductType as nvarchar(200) Set @ProductType = '';
	Declare @SubmittedBy as nvarchar(50) set @SubmittedBy = '';
	Declare @Remarks as nvarchar(50) set @Remarks = '';
	Declare @ApproverRemarks as nvarchar(50) set @ApproverRemarks = '';
	Declare @SubmissionDate as datetime;
	Declare @UserEmail as nvarchar(50) set @UserEmail = '';
	Declare @ApproverEmail as nvarchar(50) set @ApproverEmail = '';
	
	Declare @MailBody as nvarchar(4000) Set @MailBody = '';
	Declare @MailSubject as nvarchar(200) Set @MailSubject = '';
	Declare @MailTo as nvarchar(50) Set @MailTo = '';
	Declare @MailFrom as nvarchar(50) Set @MailFrom ='';
	Declare @MailCC as nvarchar(50) Set @MailCC = '';
	Declare @MailBCC as nvarchar(50) Set @MailBCC = 'dsamaddar@meridianfinancebd.com';
	Declare @SoftwareLink  as nvarchar(500) Set @SoftwareLink = 'http://web.meridianfinancebd.com:4411/visit/frmLogin.aspx'

	select @AgreementID=AgreementID,@BorrowerName = BorrowerName,@ProductType = ProductType,
	@SubmittedBy = dbo.fnGetVisitUserNameByID(EntryBy), @SubmissionDate = isnull(SubmissionDate,LastUpdate), @Remarks = ISNULL(Remarks,''),
	@UserEmail = dbo.fnGetVisitUserEmailByID(EntryBy), @ApproverEmail = dbo.fnGetVisitUserEmailByID(ApproverID),
	@ApproverRemarks = isnull(ApproverRemarks,'')
	from tblVisitReports where ReportID = @ReportID and IsSubmitted = 1;

	Set @MailBody = '
	<html>
	<body>
	<table border=''1'' width=''100%''>
	<tr>
		<th>AgreementID</th>
		<th>BorrowerName</th>
		<th>Product</th>
		<th>Visited By</th>
		<th>Submission Date</th>
		<th>Remarks</th>
		<th>Status</th>
		<th>Approver Remarks</th>
	</tr>
	<tr>
		<td>' + @AgreementID + '</td>
		<td>' + @BorrowerName + '</td>
		<td>' + @ProductType + '</td>
		<td>' + @SubmittedBy + '</td>
		<td>' + convert(nvarchar,@SubmissionDate) + '</td>
		<td>' + @Remarks + '</td>
		<td>' + @ApplicationStatus + '</td>
		<td>' + @ApproverRemarks + '</td>
	</tr>
	<tr>
		<td colspan=''2''>Software Link</td>
		<td colspan=''6''><a href='+@SoftwareLink+'>Link</a> </td>
	</tr>
	</table>
	</body>
	</html>';

	if @ApplicationStatus = 'Submitted'
	begin
		Set @MailSubject = 'Periodic Visit : Request From ('+@SubmittedBy+') : Need Approval'
		Select @MailFrom = @UserEmail
		Select @MailTo = @ApproverEmail
	end
	else if @ApplicationStatus = 'Rejected'
	begin
		Set @MailSubject = 'Periodic Visit : Report ID ('+@ReportID+') : Rejected'
		Select @MailFrom = @ApproverEmail
		Select @MailTo = @UserEmail
	end
	else if @ApplicationStatus = 'Approved'
	begin
		Set @MailSubject = 'Periodic Visit : Report ID ('+@ReportID+') : Approved'
		Select @MailFrom = @ApproverEmail
		Select @MailTo = @UserEmail
	end

	Select @MailSubject as 'MailSubject',@MailBody as 'MailBody' ,Case When @MailFrom='' then 'divit@meridianfinancebd.com' else @MailFrom end  as 'MailFrom',
	Case When @MailTo='' then 'divit@meridianfinancebd.com' else @MailTo end as 'MailTo',
	Case When @MailCC='' then 'divit@meridianfinancebd.com' else @MailCC end as 'MailCC',
	@MailBCC as 'MailBCC'
end

GO