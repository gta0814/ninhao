create table User(
	UserName	varchar(255)	PRIMARY KEY,
	LastName	varchar(255),
	FirstName	varchar(255),
	Address		varchar(255),
	Phone		int,
	SocialAppID	int,
	ContactUserName		varchar(255)
)

create table Trip(
	TripID		int		PRIMARY KEY,
	LocationFrom	varchar(255),
	LocationTo		varchar(255),
	LeaveTime		smalldatetime
) 
