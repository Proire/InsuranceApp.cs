CREATE PROCEDURE CheckPolicyExists
    @CustomerID INT,
    @SchemeID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the policy already exists
    IF EXISTS (
        SELECT 1
        FROM Policies
        WHERE CustomerID = @CustomerID
          AND SchemeID = @SchemeID
    )
    BEGIN
        -- Raise an error if the policy exists
        RAISERROR ('Policy with the specified Scheme already exists for this customer', 16, 1);
    END
END;

CREATE TRIGGER trg_UpdatePolicyStatusOnFirstPayment
ON Payments
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Update the PolicyStatus to 'Active' for the policy related to the first payment
    UPDATE Policies
    SET PolicyStatus = 'Active'
    FROM Policies p
    INNER JOIN inserted i ON p.PolicyID = i.PolicyID
    WHERE p.PolicyStatus = 'Inactive'
      AND (SELECT COUNT(*) FROM Payments WHERE PolicyID = p.PolicyID) = 1;
END;

CREATE PROCEDURE ValidatePayment
    @CustomerID INT,
    @PolicyID INT,
    @Amount FLOAT,
    @PaymentDate DATETIME
AS
BEGIN
    -- Check if CustomerID exists in the Customer table
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerID = @CustomerID)
    BEGIN
        RAISERROR('Invalid CustomerID.', 16, 1);
        RETURN;
    END

    -- Check if PolicyID exists in the Policy table
    IF NOT EXISTS (SELECT 1 FROM Policies WHERE PolicyID = @PolicyID)
    BEGIN
        RAISERROR('Invalid PolicyID.', 16, 1);
        RETURN;
    END

    -- Check if Amount is positive
    IF @Amount <= 0
    BEGIN
        RAISERROR('Amount must be greater than zero.', 16, 1);
        RETURN;
    END

    -- Check if PaymentDate is not in the future
    IF @PaymentDate > GETDATE()
    BEGIN
        RAISERROR('PaymentDate cannot be in the future.', 16, 1);
        RETURN;
    END

    -- Validate if policy has been paid more than once in a month from the issuance date
    DECLARE @PolicyIssuanceDate DATE;
    DECLARE @PaymentsDone INT;
	DECLARE @PaymentsTobeDone INT;
	DECLARE @MaturityPeriod INT;	

    -- Get the issuance date of the policy
    SELECT @PolicyIssuanceDate = DateIssued, @MaturityPeriod = MaturityPeriod
    FROM Policies
    WHERE PolicyID = @PolicyID;

	SELECT @PaymentsDone = COUNT(*)
    FROM Payments
    WHERE PolicyID = @PolicyID;

	SET @PaymentsTobeDone = DATEDIFF(MONTH, @PolicyIssuanceDate, @PaymentDate);

    IF @PaymentsTobeDone <= @PaymentsDone
    BEGIN
        RAISERROR('Payment already done for this month', 16, 1);
        RETURN;
    END

	if @MaturityPeriod * 12 <= @PaymentsDone 
	BEGIN
        RAISERROR('All Payments for this policy are already done', 16, 1);
        RETURN;
    END
END;

