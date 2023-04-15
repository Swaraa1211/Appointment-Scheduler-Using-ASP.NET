CREATE TABLE Appointments(
	AppointmentId INT PRIMARY KEY IDENTITY(1,1),
	Title VARCHAR(30),
	Description VARCHAR(30),
	StartTime DATETIME,
	EndTime DATETIME,
	Location VARCHAR(30)

	)

SELECT * FROM Appointments

INSERT INTO Appointments (Title, Description, StartTime, EndTime, Location)
VALUES ('Brain Storming', 'Discussion about project', '2023-04-13 10:00:00', '2023-04-13 11:00:00', 'Conference Room'),
       ('Game Plan', 'Review of marketing strategy', '2023-04-13 14:30:00', '2023-04-13 16:00:00', 'Board Room'),
       ('Team Training', 'Training session for new hires', '2023-04-14 09:00:00', '2023-04-14 11:00:00', 'Training Room');
