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
CREATE PROCEDURE spAvgPricePerNight 
	-- Add the parameters for the stored procedure here
	@month smallint

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
DROP TABLE temp_L

declare @d1 date, @d2 date;
set @d1 = '2000-01-01';
set @d2 = '2050-12-31';

;with cte(n) as
(
	SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 0))
	FROM master..spt_values t1 
	CROSS JOIN master..spt_values t2
), 
cte2(n, [date]) as
(
	SELECT n, DATEADD(DAY, n - 1, @d1)
	FROM cte
	WHERE n <= DATEDIFF(DAY, @d1, @d2) + 1
)

select v.flatId, CONVERT(varchar(7), d.[date]) AS year_month,COUNT(1) AS TotalDays, f.city ,f.price AS pricePerNight ,COUNT(1)*f.price AS totalPrice
INTO temp_L
from Flats_L_2022 f inner join Vacations_L_2022 v on v.flatId = f.id join cte2 d on  d.[date] between v.startDate and v.endDate
where MONTH(d.[date]) = @month
group by flatId, CONVERT(varchar(7), d.[date]), f.city ,f.price

select (SUM(totalPrice)/SUM(TotalDays)) AS AvgPricePerNight ,city
from [temp_L]
GROUP BY city

END
GO


--check procedure
EXEC spAvgPricePerNight 2