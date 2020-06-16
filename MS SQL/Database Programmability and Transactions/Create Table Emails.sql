CREATE TRIGGER tr_LogsChanges ON Accounts 
FOR UPDATE
AS
INSERT INTO NotificationEmails(Recipient, [Subject], Body)
SELECT i.Id, CONCAT('Balance change for account: ', i.Id,
			 CONCAT('On', FORMAT(GETDATE(), 'MMM dd yyyy hh:mm'), 'your balance was changed from', d.Balance, 'to', i.AccountHolderId, '.')
FROM inserted i
JOIN deleted d
ON i.Id = d.Id
