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

CREATE PROCEDURE spInsertFlat_L 
	-- Add the parameters for the stored procedure here
	@city nvarchar (30),
    @address nvarchar (30),
    @price float,
    @numberOfRooms smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	 Insert INTO [Flats_L_2022] ([city], [address] ,[price], [numberOfRooms]) Values (@city , @address, @price, @numberOfRooms)
END
GO

--check procedure
--exec spInsertFlat_L 'London','ADD','100','3'
--select * from Flats_L_2022