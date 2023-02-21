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
-- Author:		<Lin>
-- Create date: <18-12-2022>
-- Description:	<Description>
-- =============================================

CREATE PROCEDURE spInsertVacation_L 
	-- Add the parameters for the stored procedure here
	@userId nvarchar (30),
    @flatId int,
    @startDate Datetime,
    @endDate Datetime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	 Insert INTO [Vacations_L_2022] ([userId], [flatId] ,[startDate], [endDate]) Values (@userId, @flatId, @startDate,@endDate)
END
GO

--check procedure
--exec spInsertVacation_L  'linbm@gmail.com', 1, [2022-12-20 00:00:00], [2022-12-23 00:00:00]
--select * from Vacations_L_2022