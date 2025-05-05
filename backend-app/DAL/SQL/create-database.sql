--USE Master;
--GO

--ALTER DATABASE MarketPlace SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--GO

--DROP DATABASE MarketPlace;
--GO

CREATE DATABASE MarketPlace;
GO

USE MarketPlace;
GO

CREATE TABLE Users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    login VARCHAR(255),
	hashed_password VARCHAR(255),
	salt VARCHAR(255),
	first_name VARCHAR(255),
	last_name VARCHAR(50),
	is_active BIT NOT NULL,
	user_type varchar(255),
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Products (
    id INT IDENTITY(1,1) PRIMARY KEY,
	artisan_id INT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
	price FLOAT,
	category VARCHAR(255),
	is_active BIT NOT NULL,
	is_available BIT NOT NULL,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    artisan_id INT,
    customer_id INT,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Item_Orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    quantity INT, 
    order_id INT,
    product_id INT,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Deliveries (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
	partner_id INT,
	is_active BIT NOT NULL,
    deli_status varchar(255),
    estimated_date DATE,
	delivery_date DATETIME,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

-- Les images seront réellement supprimées
CREATE TABLE ProductImages (
    id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(255),
    product_id INT,
	content VARBINARY(MAX),
	mime_type VARCHAR(255),
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO


CREATE TABLE Messages (
    id INT IDENTITY(1,1) PRIMARY KEY,
	msg_from_id INT,
	msg_to_id INT,
    product_id INT,
	content VARCHAR(255),
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Reviews (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT,
    customer_id INT,
	content VARCHAR(255),
	fromArtisan BIT,
	is_active BIT NOT NULL,
	score INT,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE DeliveryArtisanPartnerships (
    id INT IDENTITY(1,1) PRIMARY KEY,
    delivery_partner_id INT,
    artisan_id INT,
    is_active BIT NOT NULL,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE Users
ADD CONSTRAINT UQ_Users_logins UNIQUE (login);
GO 

ALTER TABLE Deliveries
ADD CONSTRAINT FK_Deliveries_Partner
FOREIGN KEY (partner_id) REFERENCES Users(id);
GO

ALTER TABLE Products
ADD CONSTRAINT FK_Products_Artisans
FOREIGN KEY (artisan_id) REFERENCES Users(id);
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Artisans
FOREIGN KEY (artisan_id) REFERENCES Users(id);
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers
FOREIGN KEY (customer_id) REFERENCES Users(id);
GO

ALTER TABLE Item_Orders
ADD CONSTRAINT FK_ItemOrders_Orders
FOREIGN KEY (order_id) REFERENCES Orders(id);
GO

ALTER TABLE Item_Orders
ADD CONSTRAINT FK_ItemOrders_Products
FOREIGN KEY (product_id) REFERENCES Products(id);
GO

ALTER TABLE Deliveries
ADD CONSTRAINT FK_Deliveries_Orders
FOREIGN KEY (order_id) REFERENCES Orders(id);
GO

ALTER TABLE ProductImages
ADD CONSTRAINT FK_ProductImages_Products
FOREIGN KEY (product_id) REFERENCES Products(id);
GO

ALTER TABLE Reviews
ADD CONSTRAINT FK_Reviews_Products
FOREIGN KEY (product_id) REFERENCES Products(id);
GO

ALTER TABLE Messages
ADD CONSTRAINT FK_Message_From
FOREIGN KEY (msg_from_id) REFERENCES Users(id);
GO

ALTER TABLE Messages
ADD CONSTRAINT FK_Message_To
FOREIGN KEY (msg_to_id) REFERENCES Users(id);
GO

ALTER TABLE Messages
ADD CONSTRAINT FK_Message_Product
FOREIGN KEY (product_id) REFERENCES Products(id);
GO

ALTER TABLE Reviews
ADD CONSTRAINT FK_Reviews_Customers
FOREIGN KEY (customer_id) REFERENCES Users(id);
GO

ALTER TABLE DeliveryArtisanPartnerships
ADD CONSTRAINT FK_DeliveryArtisanPartnerships_Artisans
FOREIGN KEY (artisan_id) REFERENCES Users(id);
GO

ALTER TABLE DeliveryArtisanPartnerships
ADD CONSTRAINT FK_DeliveryArtisanPartnerships_Partner
FOREIGN KEY (delivery_partner_id) REFERENCES Users(id);
GO

ALTER TABLE Users
ADD CONSTRAINT DF_Users_is_active DEFAULT 1 FOR is_active;
GO

ALTER TABLE Products
ADD CONSTRAINT DF_Products_is_active DEFAULT 1 FOR is_active;
GO

ALTER TABLE Products
ADD CONSTRAINT DF_Products_is_available DEFAULT 1 FOR is_available;
GO

ALTER TABLE DeliveryArtisanPartnerships
ADD CONSTRAINT DF_DAP_is_active DEFAULT 1 FOR is_active;
GO
ALTER TABLE Reviews
ADD CONSTRAINT DF_Reviews_is_active DEFAULT 1 FOR is_active;
GO


ALTER TABLE Deliveries
ADD CONSTRAINT DF_delivery_is_active DEFAULT 1 FOR is_active;
GO
USE MarketPlace;
GO
DECLARE @tableName NVARCHAR(128);
DECLARE @sql NVARCHAR(MAX);

DECLARE table_cursor CURSOR FOR
SELECT name
FROM sys.tables
WHERE type = 'U'; -- Only user tables

OPEN table_cursor;

FETCH NEXT FROM table_cursor INTO @tableName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = '
    CREATE TRIGGER trg_Update_' + @tableName + '
    ON ' + @tableName + '
    AFTER UPDATE
    AS
    BEGIN
        -- Update the last_updated field for the modified row
        UPDATE ' + @tableName + '
        SET last_updated = GETDATE()
        WHERE id IN (SELECT id FROM inserted);
    END';
    
    -- Check if trigger already exists, then create it
    EXEC sp_executesql @sql;
    
    FETCH NEXT FROM table_cursor INTO @tableName;
END;

CLOSE table_cursor;
DEALLOCATE table_cursor;
USE MarketPlace;
GO

INSERT INTO Users (login, hashed_password, salt, first_name, last_name, is_active, user_type) VALUES 
    ('camcam', 'EE1D043DE283E12CD10A', 'Sunday', 'Camille', 'Berrier-Plater Syberg', 1,  'ADMIN'),
	('customer', 'EE1D043DE283E12CD10A', 'Sunday', 'Henri', 'Dupont', 1,  'CUSTOMER'),
	('deliverypartner', 'EE1D043DE283E12CD10A', 'Sunday', 'G�rard', 'Agediss', 1,  'DELIVERYPARTNER'),
	('artisan', 'EE1D043DE283E12CD10A', 'Sunday', 'Charles', 'Dupont', 1,  'ARTISAN'),
	('artisan2', 'EE1D043DE283E12CD10A', 'Sunday', 'Vincent', 'Van Gogh', 1,  'ARTISAN'),
	('artisan3', 'EE1D043DE283E12CD10A', 'Sunday', 'Claude', 'Monet', 1,  'ARTISAN');
GO

INSERT INTO Products ( artisan_id, name, description, price, category) VALUES
	(4, 'Whale painting', 'Acrylic painting of a blue whale under water, by Scott Highlander', 400.50, 'PAINTING'),
	(4, 'Pottery Kit', 'The perfect getting started kit for pottery enthousiasts ', 20.50, 'POTTERY'),
	(4, 'Topaze necklace', 'A beautiful topaze chocker necklace', 50.50, 'JEWELS'),
	(5, 'Tournesols', 'Sunflowers, la peinture connue', 5000000, 'PAINTING'),
	(6, 'Nénuphars', 'Peinture impressioniste de Nénuphars', 560.50, 'PAINTING');
GO

INSERT INTO Orders ( artisan_id, customer_id) VALUES
(4, 1),
(4, 2);

INSERT INTO Item_Orders (quantity, order_id, product_id) VALUES
(3,1,1),
(1,1,2),
(6,2,3);

INSERT INTO Deliveries (order_id, deli_status, estimated_date, is_active) VALUES
(1, 'PROCESSING', null, 0),
(1, 'PROCESSING', null, 1),
(2, 'SHIPPED', null, 0);

INSERT INTO REVIEWS (product_id, customer_id, content, fromArtisan, is_active, score ) values 
( 3, 2, 'Beautiful', 0, 1, 5), ( 3, 2, 'Thank you :)', 1, 1, 5),  (1, 1, 'Whimsical!', 0, 1, 4);

INSERT INTO Messages(msg_from_id, msg_to_id, content, product_id) values
(2, 4, 'Hello, could I have more information about this product?', 1),
(4, 2, 'Hi, thank you for your interest, what would you like to know? Dimensions are 10x50', 1),
(1, 4, 'Hi, whats up ?', null);