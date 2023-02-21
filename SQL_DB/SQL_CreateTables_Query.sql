CREATE TABLE [Users_L_2022] (
    [firstName] nvarchar (30) NOT NULL ,
	[lastName] nvarchar (30) NOT NULL ,
	[email] nvarchar (30) Primary key check(email like '%@%._%') ,
	[password] nvarchar (30) NOT NULL,
	[isActive] bit default 'true' NOT NULL ,
	[isAdmin] bit default 'false' NOT NULL
)

CREATE TABLE [Flats_L_2022] (
	[id] smallint IDENTITY (1, 1) Primary key,
    [city] nvarchar (30) NOT NULL ,
	[address] nvarchar (30) NOT NULL ,
    [price] float NOT NULL check(price>0),
	[numberOfRooms] smallint NOT NULL check(numberOfRooms>0)
)

CREATE TABLE [Vacations_L_2022] (
	    [id] smallint IDENTITY (1, 1) Primary key,
		[userId] nvarchar (30) REFERENCES [Users_L_2022](email),
		[flatId] smallint REFERENCES [Flats_L_2022](id),
        [startDate] DateTime check(startDate >= getdate()) NOT NULL ,
		[endDate] DateTime NOT NULL,
		check(startDate<=endDate)
)

select * from Vacations_L_2022

select * from Flats_L_2022

select * from Users_L_2022

Delete from Users_L_2022 where email = 'linbm@gmail.com'




--insert into example
select * from Flights_L_2022
Insert INTO Flights_L_2022 ( [from] ,[to], [price]) Values ('israel','NYC','100')
