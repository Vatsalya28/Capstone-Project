--Employee Table
CREATE TABLE Employee (
EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
FirstName VARCHAR(50),
LastName VARCHAR(50),
DateOfBirth DATE,
Gender VARCHAR(50),
Email VARCHAR(100),
PhoneNumber VARCHAR(15),
Address VARCHAR(255),
Position VARCHAR(50),
JoiningDate DATE,
TerminationDate DATE NULL
)

select * from Payroll
-- Payroll Table
CREATE TABLE Payroll (
    PayrollID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT FOREIGN KEY REFERENCES Employee(EmployeeID),
    PayPeriodStartDate DATE,
    PayPeriodEndDate DATE,
    BasicSalary Float,
    OvertimePay Float,
    Deductions Float,
    NetSalary Float
)

-- Tax Table
CREATE TABLE Tax (
    TaxID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT FOREIGN KEY REFERENCES Employee(EmployeeID),
    TaxYear INT,
    TaxableIncome Float,
    TaxAmount Float
)

-- FinancialRecord Table
CREATE TABLE FinancialRecord (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT FOREIGN KEY REFERENCES Employee(EmployeeID),
    RecordDate DATE,
    Description VARCHAR(255),
    Amount Float,
    RecordType VARCHAR(50)
)



-----insert data
--Employee Table
INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate)
VALUES 
('Lionel', 'Messi', '1987-06-24', 'Male', 'lionel.messi@gmail.com', '1234567890', '123 Main St, Cityville', 'Manager', '2022-01-01', NULL),
('Cristiano', 'Ronaldo', '1985-02-05', 'Male', 'cristiano.ronaldo@yahoo.com', '9876543210', '456 Oak St, Townsville', 'Developer', '2022-01-01', NULL),
('Neymar', 'Jr', '1992-02-05', 'Male', 'neymar.jr@gmail.com', '5551234567', '789 Pine St, Villagetown', 'Developer', '2022-01-01', NULL),
('Kylian', 'Mbappe', '1998-12-20', 'Male', 'kylian.mbappe@yahoo.com', '7778889999', '101 Cedar St, Hamletsville', 'Analyst', '2022-01-01', NULL),
('Sergio', 'Ramos', '1986-03-30', 'Male', 'sergio.ramos@yahoo.com', '3332221111', '202 Elm St, Suburbia', 'Analyst', '2022-01-01', NULL),
('Kevin', 'De Bruyne', '1991-06-28', 'Male', 'kevin.debruyne@outlook.com', '4445556666', '303 Maple St, Countryside', 'Senior Analyst', '2022-01-01', NULL),
('Virgil', 'van Dijk', '1991-07-08', 'Male', 'virgil.vandijk@gmail.com', '9990001111', '505 Birch St, Outskirts', 'Sales Representative', '2022-01-01', NULL),
('Mohamed', 'Salah', '1992-06-15', 'Male', 'mohamed.salah@gmail.com', '2223334444', '707 Pine St, Riverside', 'Junior Developer', '2022-01-01', NULL),
('Robert', 'Lewandowski', '1988-08-21', 'Male', 'robert.lewandowski@gmail.com', '1112223333', '909 Cedar St, Lakeside', 'Junior Developer', '2022-01-01', NULL),
('Marta', 'Silva', '1986-02-19', 'Female', 'marta.silva@gmail.com', '3334445555', '606 Maple St, Lakeside', 'Analyst', '2022-01-01', NULL),
('Ada', 'Hegerberg', '1995-07-10', 'Female', 'ada.hegerberg@gmail.com', '5556667777', '808 Birch St, Hillside', 'Developer', '2022-01-01', NULL),
('Sam', 'Kerr', '1993-09-10', 'Female', 'sam.kerr@gmail.com', '7778889999', '909 Pine St, Mountainside', 'Senior Manager', '2022-01-01', NULL)

--Payroll Table

INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary)
VALUES
(1, '2023-01-01', '2023-01-15', 5000.00, 250.00, 150.00, 5100.00),
(2, '2023-01-01', '2023-01-15', 4500.00, 200.00, 120.00, 4580.00),
(3, '2023-01-01', '2023-01-15', 4800.00, 300.00, 180.00, 4920.00),
(4, '2023-01-01', '2023-01-15', 5500.00, 150.00, 100.00, 5450.00),
(5, '2023-01-01', '2023-01-15', 5200.00, 180.00, 130.00, 5250.00),
(6, '2023-01-01', '2023-01-15', 4900.00, 250.00, 160.00, 4990.00),
(7, '2023-01-01', '2023-01-15', 4700.00, 200.00, 110.00, 4790.00),
(8, '2023-01-01', '2023-01-15', 5300.00, 300.00, 200.00, 5400.00),
(9, '2023-01-01', '2023-01-15', 5000.00, 180.00, 120.00, 5060.00),
(10, '2023-01-01', '2023-01-15', 4800.00, 220.00, 140.00, 4880.00)

---Tax Table
INSERT INTO Tax (EmployeeID, TaxYear, TaxableIncome, TaxAmount)
VALUES
(1, 2023, 60000.00, 12000.00),
(2, 2023, 55000.00, 11000.00),
(3, 2023, 58000.00, 11600.00),
(4, 2023, 65000.00, 13000.00),
(5, 2023, 62000.00, 12400.00),
(6, 2023, 59000.00, 11800.00),
(7, 2023, 57000.00, 11400.00),
(8, 2023, 63000.00, 12600.00),
(9, 2023, 60000.00, 12000.00),
(10, 2023, 58000.00, 11600.00)

---Financial Record Table
INSERT INTO FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType)
VALUES
(1, '2023-02-02', 'Travel Reimbursement', 75.00, 'Income'),
(2, '2023-02-05', 'Office Supplies', 50.00, 'Expense'),
(3, '2023-02-10', 'Bonus Deduction', 100.00, 'Deduction'),
(4, '2023-02-12', 'Business Trip Allowance', 120.00, 'Income'),
(5, '2023-02-15', 'Sick Leave Deduction', 40.00, 'Deduction'),
(6, '2023-02-18', 'Project Bonus', 250.00, 'Income'),
(7, '2023-02-20', 'Meal Allowance', 30.00, 'Income'),
(8, '2023-02-22', 'Computer Equipment Purchase', 500.00, 'Expense'),
(9, '2023-02-25', 'Overtime Pay', 180.00, 'Income'),
(10, '2023-02-28', 'Health Checkup Expense', 120.00, 'Expense')


----
SELECT * FROM Employee
SELECT * FROM Payroll
SELECT * FROM Tax
SELECT * FROM FinancialRecord

-- CalculateAge()
SELECT e.FirstName,e.LastName,DATEDIFF(YEAR,e.DateOfBirth,GETDATE()) as Age from Employee e;

---GetEmployeeById
DECLARE @EmployeeID INT
set @EmployeeID=2
select * from Employee where EmployeeID = @EmployeeID

---GetAllEmployees
select * from Employee

---GetPayrollByID
DECLARE @PayrollID INT 
Set @PayrollID= 1
SELECT *FROM Payroll WHERE PayrollID = @PayrollID

---GetPayrollsForEmployee
DECLARE @EID INT
SET @EID = 1
SELECT *FROM Payroll WHERE EmployeeID = @EID

----GetPayrollsForPeriod
DECLARE @StartDate DATE 
SET @StartDate= '2023-01-01'
DECLARE @EndDate DATE 
SET @EndDate = '2023-01-15'
SELECT *FROM Payroll
WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate

----GetTaxById
DECLARE @TaxID INT = 1
SELECT *FROM Tax WHERE TaxID = @TaxID


---GetTaxesForEmployee
DECLARE @EmpID INT 
SET @EmpID= 1 
SELECT *FROM Tax WHERE EmployeeID = @EmployeeID
---