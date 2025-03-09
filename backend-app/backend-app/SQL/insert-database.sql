USE MarketPlace;
GO

INSERT INTO Users (login, hashed_password, salt, first_name, last_name, is_active, user_type) VALUES 
    ('camcam', 'EE1D043DE283E12CD10A', 'Sunday', 'Camille', 'Berrier-Plater Syberg', 1,  'ADMIN'),
	('customer', 'EE1D043DE283E12CD10A', 'Sunday', 'Henri', 'Dupont', 1,  'CUSTOMER'),
	('deliverypartner', 'EE1D043DE283E12CD10A', 'Sunday', 'Gérard', 'Agediss', 1,  'DELIVERYPARTNER'),
	('artisan', 'EE1D043DE283E12CD10A', 'Sunday', 'Charles', 'Dupont', 1,  'ARTISAN');
GO

INSERT INTO Products ( artisan_id, name, description, price, category) VALUES
	(4, 'Whale painting', 'Acrylic painting of a blue whale under water, by Scott Highlander', 400.50, 'PAINTING'),
	(4, 'Pottery Kit', 'The perfect getting started kit for pottery enthousiasts ', 20.50, 'POTTERY'),
	(4, 'Topaze necklace', 'A beautiful topaze chocker necklace', 50.50, 'JEWELS');
