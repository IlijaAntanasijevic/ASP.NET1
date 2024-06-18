USE [Booking2]
GO
/****** Object:  Trigger [dbo].[trg_CalculateTotalPrice]    Script Date: 6/19/2024 1:08:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER TRIGGER [dbo].[trg_CalculateTotalPrice] 
ON [dbo].[Bookings]
AFTER INSERT, UPDATE
AS 
BEGIN
    SET NOCOUNT ON;

    UPDATE b
    SET b.TotalPrice = 
        CASE 
            WHEN DATEDIFF(DAY, i.CheckIn, i.CheckOut) = 0 THEN a.Price 
            ELSE (DATEDIFF(DAY, i.CheckIn, i.CheckOut)) * a.Price 
        END
    FROM Bookings b
    INNER JOIN Inserted i ON b.Id = i.Id
    INNER JOIN Apartments a ON i.ApartmentId = a.Id;
END;
