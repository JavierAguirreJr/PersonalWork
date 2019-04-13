USE master
GO

IF EXISTS(SELECT * FROM sysdatabases WHERE Name='HotelReservation')
DROP DATABASE HotelReservation
GO

CREATE DATABASE HotelReservation
GO

USE HotelReservation
GO

CREATE TABLE CustomerInformation(
CustomerInfoID INT IDENTITY (1,1) PRIMARY KEY,
CustomerName varchar(30) not null,
Lastname varchar(30) not null,
Email varchar (45) not null,
Phone varchar (15) not null
)

CREATE TABLE CustomerReservation(
ReservationID INT IDENTITY(1,1) PRIMARY KEY,
TotalRooms INT not null,
Arrival DATE not null,
Departure DATE not null,
CustomerInfoID INT FOREIGN KEY REFERENCES CustomerInformation(CustomerInfoID)
)

CREATE TABLE Guest(
GuestID INT IDENTITY (1,1) PRIMARY KEY,
FirstName varchar(30) not null,
LastName varchar(30) not null,
AgeOfGuest tinyint not null,
ReservationID INT FOREIGN KEY REFERENCES CustomerReservation(ReservationID)
)

CREATE TABLE Billing(
BillingID INT IDENTITY (1,1) PRIMARY KEY,
Total money not null,
TaxAmount decimal not null,
ReservationID INT FOREIGN KEY REFERENCES CustomerReservation(ReservationID)
)

CREATE TABLE AddOns(
AddOnID INT IDENTITY (1,1) PRIMARY KEY,
[Description] varchar (80),
)

CREATE TABLE AddOnsRates(
AddOnRatesID INT IDENTITY(1,1) PRIMARY KEY,
Rates decimal not null,
StartRate decimal not null,
EndRate decimal not null,
AddOnID INT FOREIGN KEY REFERENCES AddOns(AddOnID)
)

CREATE TABLE TotalBreakdown(
TotalID INT IDENTITY (1,1) PRIMARY KEY,
RoomTotal INT not null,
AddOnesID INT FOREIGN KEY REFERENCES AddOns(AddOnID)
)

CREATE TABLE RoomType(
RoomTypeID INT IDENTITY (1,1) PRIMARY KEY,
RoomDescription varchar(80)
)

CREATE TABLE RoomRates(
RatesID INT IDENTITY (1,1) PRIMARY KEY,
StartRate decimal not null,
EndRate decimal not null,
RateForRoom decimal not null,
RoomTypeID INT FOREIGN KEY REFERENCES RoomType(RoomTypeID)
)

CREATE TABLE RoomInformation(
RoomID INT IDENTITY (1,1) PRIMARY KEY,
[Floor] INT not null,
RoomNumber tinyint not null,
Occupancy tinyint not null,
RoomTypeID INT FOREIGN KEY REFERENCES RoomType(RoomTypeID)
)

CREATE TABLE Amenities(
AmenititesID INT IDENTITY (1,1) PRIMARY KEY,
[Description] varchar (80) not null
)

CREATE TABLE RoomAmenities(
RoomAmenitiesID INT IDENTITY (1,1) PRIMARY KEY,
RoomID INT FOREIGN KEY REFERENCES RoomInformation(RoomID),
AmenitiesID INT FOREIGN KEY REFERENCES Amenities(AmenititesID)
)

CREATE TABLE RoomReserved(
RoomReservedID INT IDENTITY (1,1) PRIMARY KEY,
ReservationID INT FOREIGN KEY REFERENCES CustomerReservation(ReservationID),
RoomID INT FOREIGN KEY REFERENCES RoomInformation(RoomID)
)

CREATE TABLE Promos(
PromoID INT IDENTITY (1,1) PRIMARY KEY,
DiscountFlat NUMERIC not null,
DiscountPercentile INT not null,
PromoStart INT not null,
PromoEnd INT not null,
ReservationID INT FOREIGN KEY REFERENCES CustomerReservation(ReservationID)
)