-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE promote
@Semester int , @StudyName varchar(10) , @exit INT OUTPUT
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @studyId int = (SELECT IdStudy from Studies where Name=@StudyName);
	declare @liczba int = (SELECT count(*) from Student s join Enrollment e on e.IdEnrollment=s.IdEnrollment where e.Semester=@Semester+1 and e.IdStudy=@studyId);
	if (@liczba is null)
		begin
			INSERT INTO Enrollment VALUES ((Select max(IdEnrollment)+1 from Enrollment),@Semester+1,@studyId,GETDATE());
		end
		declare @maxIdEn int = (SELECT IdEnrollment FROM Enrollment WHERE IdStudy= @studyId AND Semester = @Semester + 1);
	UPDATE Student SET IdEnrollment = @maxIdEn+1 WHERE IdEnrollment in (SELECT IdEnrollment FROM Enrollment WHERE Semester = @Semester AND IdStudy = @studyId);
Set @exit = (select max(IdEnrollment) from Enrollment);
END
GO
