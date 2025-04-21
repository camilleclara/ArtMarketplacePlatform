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
(1, 'PROCESSING', null, false),
(1, 'PROCESSING', null, true),
(2, 'SHIPPED', null, false);

INSERT INTO REVIEWS (product_id, customer_id, content, fromArtisan, is_active, score ) values 
( 3, 2, 'Beautiful', 0, 1, 5), ( 3, 2, 'Thank you :)', 1, 1, 5),  (1, 1, 'Whimsical!', 0, 1, 4);

