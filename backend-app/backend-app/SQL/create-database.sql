--USE master;
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
    product_id INT,
	content VARBINARY(MAX),
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Chats (
    id INT IDENTITY(1,1) PRIMARY KEY,
	product_id INT,
	customer_id INT,
	is_active BIT NOT NULL,
	last_updated DATETIME DEFAULT GETDATE(),
	created DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Messages (
    id INT IDENTITY(1,1) PRIMARY KEY,
    chat_id INT,
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

ALTER TABLE Chats
ADD CONSTRAINT FK_Chats_Products
FOREIGN KEY (product_id) REFERENCES Products(id);
GO

ALTER TABLE Chats
ADD CONSTRAINT FK_Chats_Customers
FOREIGN KEY (customer_id) REFERENCES Users(id);
GO

ALTER TABLE Messages
ADD CONSTRAINT FK_Chats_Messages
FOREIGN KEY (chat_id) REFERENCES Chats(id);
GO

ALTER TABLE Reviews
ADD CONSTRAINT FK_Reviews_Products
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

ALTER TABLE Chats
ADD CONSTRAINT DF_Chats_is_active DEFAULT 1 FOR is_active;
GO

ALTER TABLE Deliveries
ADD CONSTRAINT DF_delivery_is_active DEFAULT 1 FOR is_active;
GO
