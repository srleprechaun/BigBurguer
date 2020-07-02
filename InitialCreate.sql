-- INSERT ORDER STATUS
INSERT INTO dbo.OrderStatuses VALUES ('OrderReceived');
INSERT INTO dbo.OrderStatuses VALUES ('InProgress');
INSERT INTO dbo.OrderStatuses VALUES ('Ready');
INSERT INTO dbo.OrderStatuses VALUES ('Delivered');

-- INSERT PAYMENT METHOD
INSERT INTO dbo.PaymentMethod VALUES ('CreditCard');
INSERT INTO dbo.PaymentMethod VALUES ('DebitCard');
INSERT INTO dbo.PaymentMethod VALUES ('Cash');
INSERT INTO dbo.PaymentMethod VALUES ('Ticket');

-- INSERT ROLE 
INSERT INTO dbo.Role VALUES ('Admin');
INSERT INTO dbo.Role VALUES ('Customer');
INSERT INTO dbo.Role VALUES ('Employee');
