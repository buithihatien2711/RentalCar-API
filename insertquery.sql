﻿SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (18, N'Nguyễn Lương Bằng', 1, 1)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (19, N'Nguyễn Tất Thành', 1, 2)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (20, N'Nguyễn Sinh Sắc', 1, 2)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (21, N'Hoàng Thị Loan', 1, 3)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (22, N'Âu Cơ', 1, 3)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (23, N'Nam Cao', 2, 3)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (24, N'Hùng Vương', 7, 3)
INSERT [dbo].[Locations] ([Id], [Address], [WardId], [UserId]) VALUES (25, N'Trưng Nữ Vương', 8, 3)
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (1, N'TOYOTA COROLLA CROSS G 2020', N'Cross corolla xe gầm cao nhập khẩu hoàn toàn 100%', NULL, 4, N'1234', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:03:50.3237193' AS DateTime2), CAST(N'2022-11-17T00:03:50.3237222' AS DateTime2), 1, 1, 1, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 18, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (2, N'MITSUBISHI XPANDER 2021', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'2345', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:07:26.2484235' AS DateTime2), CAST(N'2022-11-17T00:07:26.2484295' AS DateTime2), 2, 1, 2, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 19, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (3, N'MAZDA 3 2019', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'6666', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:10:12.4312032' AS DateTime2), CAST(N'2022-11-17T00:10:12.4312068' AS DateTime2), 3, 2, 2, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 20, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (4, N'MITSUBISHI ATTRAGE 2020', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'7777', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:13:04.0880637' AS DateTime2), CAST(N'2022-11-17T00:13:04.0880685' AS DateTime2), 4, 4, 3, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 21, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (5, N'HONDA CITY 2019', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'8888', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:14:35.7195425' AS DateTime2), CAST(N'2022-11-17T00:14:35.7195508' AS DateTime2), 5, 3, 4, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 22, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (6, N'KIA SOLUTO 2019', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'9999', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:15:43.5103122' AS DateTime2), CAST(N'2022-11-17T00:15:43.5103159' AS DateTime2), 6, 1, 3, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 22, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (7, N'MAZDA 2 2016', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'4444', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:16:37.8858428' AS DateTime2), CAST(N'2022-11-17T00:16:37.8858455' AS DateTime2), 7, 4, 5, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 23, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (8, N'VINFAST LUX A 2.0 2020', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'3333', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:18:59.6556673' AS DateTime2), CAST(N'2022-11-17T00:18:59.6556732' AS DateTime2), 8, 1, 6, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 24, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (9, N'MAZDA 3 2017', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'2222', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:20:14.8666789' AS DateTime2), CAST(N'2022-11-17T00:20:14.8666831' AS DateTime2), 9, 2, 7, 2017, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 25, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (10, N'HONDA CIVIC 2020', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'1111', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:21:06.9074011' AS DateTime2), CAST(N'2022-11-17T00:21:06.9074048' AS DateTime2), 10, 3, 8, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 25, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (11, N'KIA CERATO 2017', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'765', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:22:00.6944110' AS DateTime2), CAST(N'2022-11-17T00:22:00.6944151' AS DateTime2), 11, 4, 8, 2020, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 25, 0)
INSERT [dbo].[Cars] ([Id], [Name], [Description], [Color], [Capacity], [Plate_number], [Cost], [CreatedAt], [UpdateAt], [CarModelId], [StatusID], [UserId], [YearManufacture], [FuelConsumption], [Rule], [NumberStar], [TransmissionID], [FuelTypeID], [LocationId], [NumberTrip]) VALUES (12, N'CHEVROLET CRUZE 2017', N'Giao xe sạch.Nếu trả xe dơ tính phí phụ thu rửa xe 100k', NULL, 4, N'345', CAST(500.00 AS Decimal(18, 2)), CAST(N'2022-11-17T00:23:42.5319886' AS DateTime2), CAST(N'2022-11-17T00:23:42.5319957' AS DateTime2), 12, 4, 9, 2017, 10, N'Không sử dụng xe thuê vào mục đích phi pháp, trái pháp luật', CAST(0.00 AS Decimal(18, 2)), 1, 1, 25, 0)
SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Licenses] ON 

INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (1, N'1234567', N'Sái Vũ Anh ', CAST(N'1975-09-06T22:00:42.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 1)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (3, N'2222222', N'Hàng Hồ Bắc ', CAST(N'1965-04-16T13:37:05.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 2)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (7, N'3333333', N'Cù Phong Châu ', CAST(N'1990-06-17T14:11:50.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 3)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (10, N'4444444', N'Vạn Thanh Ðạo ', CAST(N'1984-08-12T23:55:51.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 4)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (11, N'5555555', N'Lều Thắng Lợi ', CAST(N'1983-12-26T12:19:10.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 5)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (14, N'6666666', N'Mạc Quang Minh ', CAST(N'1983-12-26T12:19:10.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 6)
INSERT [dbo].[Licenses] ([Id], [Number], [Name], [DateOfBirth], [Image], [UserId]) VALUES (28, N'7777777', N'Đống Chiêu Phong ', CAST(N'1988-02-05T21:28:00.0000000' AS DateTime2), N'https://daylaixehanoi.vn/wp-content/uploads/2021/11/mau-bang-lai-xe-o-to-moi-2.jpg', 7)
SET IDENTITY_INSERT [dbo].[Licenses] OFF
GO
SET IDENTITY_INSERT [dbo].[CarReviews] ON 

INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (3, N'Dịch vụ tốt , giá hợp lý , chủ xe nhiệt tình!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 20, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (4, N'Chủ xe thân thiên', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 21, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (5, N'Good', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 23, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (6, N'Tốt', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 24, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (7, N'Xe chạy êm', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 25, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (8, N'chủ xe nhiệt tình, vui vẻ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 26, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (9, N'Quá xịn', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 27, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (10, N'Dịch vụ tốt , giá hợp lý , chủ xe nhiệt tình!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 28, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (11, N'Anh chủ xe nhiệt tình, thân thiện, lần sau nhất định sẽ thuê anh tiếp.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 29, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (12, N'Chủ xe nhiệt tình

', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 30, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (13, N'chủ xe vui vẻ, thân thiện', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 31, 1)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (14, N'xe tốt, ít hao xăng, chủ xe dễ thương', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 32, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (15, N'xe đi ok, chủ xe thân thiện, good.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 33, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (16, N'Rất tốt', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 34, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (17, N'Xe sạch đẹp, Chủ xe dễ thương nhiệt tình chuyên nghiệp...', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 35, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (18, N'Anh Chủ nhiệt tình. Xe chạy tốt.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 36, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (19, N'Chủ xe vui tính , dễ gần. Xe rất tốt, lợi xăng. Tôi sẽ ửng hộ lần sau! Năm mới phát tài!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 37, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (20, N'chủ xe dễ thương, xe sạch sẽ. chạy ok', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 38, 2)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (21, N'Chủ xe nhiệt tình, dễ thương', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 39, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (22, N'Chủ xe vui vẻ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 40, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (23, N'chủ xe rất thân thiện và nhiệt tình, rất hài lòng về chuyến đi', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 41, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (24, N'A thân thiện. Nhiệt tình', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 42, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (25, N'chủ xe thân thiện nhiệt tình,xe đẹp sạch sẽ chạy vọt lại tiết kiệm xăng', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 43, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (26, N'ok . chu xe rat good', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 44, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (27, N'Xe mới, phuộc tốt, chạy êm. Chủ xe vui vẻ nhiệt tình', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 45, 3)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (28, N'Chu xe Nhiệt tình và dễ thương.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 46, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (29, N'Anh chủ thoải mái vui tính, xe chạy rất thích và tiết kiệm xăng, đồ chơi đầy đủ giúp ngồi thoải mái. Sẽ tiếp tục ủng hộ và giới thiệu bạn bè cho anh.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 47, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (30, N'Chủ xe vui vẻ, nhiệt tình, giao xe đúng hẹn. Lần sau sẽ thuê tiếp :)', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 48, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (31, N'xe ngon, chủ dễ thương. sẽ giới thiệu cho bạn bè, và nhất định sẽ thuê lần sau.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 49, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (32, N'Good xe dep, em.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 50, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (33, N'Anh chủ xe vui vẻ , nhiệt tình . Xe chạy rất tốt . Ít hao xăng', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 51, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (34, N'chủ xe vui ve hoa đồng và xe rat đep ...hen a 1 ngay nao gan nhất', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 52, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (35, N'xe mới. chủ xe vui vẻ. good', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 53, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (36, N'xe mới, chạy êm, chủ nhiệt tình, thoải mái. 5* cho chuyến chuyến xe đầu tiên của khách thuê và chủ cho thuê.

', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 54, 4)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (37, N'Anh, chị chủ nhiệt tình, vui vẻ sẽ ủng hộ anh, chị những lầm tiếp theo', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 55, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (38, N'Chủ xe chuyên nghiệp,tận tâm với khách hàng.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 56, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (39, N'Chủ xe thân thiện, tư vấn sử dụng xe rất nhiệt tình. Tôi rất hài long.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 57, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (40, N'Xe chạy êm, tiết kiệm xăng, anh chị chủ nhiệt tình!!!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 58, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (41, N'Xe chạy êm, anh chị chủ xe nhiệt. Sẽ ủng hộ dài dài!!!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 59, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (42, N'all ok', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 60, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (43, N'Chủ xe nhiệt tình thân thiện. Xe chạy ổn.

', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 61, 5)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (44, N'Xe tốt, chủ tốt! Mọi người ủng hộ anh chị chủ xe nha!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 62, 6)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (45, N'Anh chị chủ dễ thương, nhiệt tình. Xe chạy êm, tiết kiệm xăng.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 63, 6)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (46, N'Anh chị chủ vui vẻ, thoải mái. Nếu có lần tới sẽ thuê xe nhà anh chị tiếp', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 64, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (47, N'tuyệt vời', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 65, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (48, N'Chủ xe ok, xe nhỏ dễ đi', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 66, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (49, N'Anh chị chủ xe dễ thương, rõ ràng và hướng dẫn cụ thể khi giao xe. xe chạy đầm, êm và ít hao xăng', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 67, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (50, N'Anh chị chủ rất vui vẻ, nhiệt tình. Xe đi rất thoả mái, máy tốt. Sẽ book lại xe anh chị.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 68, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (51, N'xe tốt, chủ xe thân thiện, nhiệt tình, sẽ còn thuê tiếp', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 69, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (52, N'Xe mới, chạy ngon, anh chủ rất lịch thiệp. Trời mưa gió vô tình va chạm người lớn tuổi làm tróc sơn nhưng anh chủ bỏ qua không tính tiền. Cảm ơn Anh', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 70, 7)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (53, N'Xe OK, chủ xe thân thiện.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 71, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (54, N'Chủ xe vui vẻ nhiệt tình. Xe có xước chút xíu mà ko bắt đền mình :)) Xe chạy tốt.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 72, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (55, N'Hai vk chủ xe phải nói là siêu dễ thương luôn. Xe y như mô tả chạy tiết kiệm nhiên liệu cực kỳ. Mọi người nên thuê nha.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 73, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (56, N'anh chị chủ xe hỗ trợ nhiệt tình, xe chạy tốt, sạch sẽ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 74, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (57, N'anh chị chủ xe nhiệt tình, thủ tục nhanh gọn, thuận lợi cho đôi bên, xe nên thuê nha các bạn', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 75, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (58, N'Xe chạy ok, chủ xe nhiệt tình, vui vẻ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 76, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (59, N'Anh chị chủ xe dễ thương, nhiệt tình chu đáo. thủ tục nhanh gọn có xe đi liền. Xe mượt rộng rãi thoải mái. lần sau tiếp tục thuê. Tuyệt vời nha anh em', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 77, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (60, N'Bạn chủ xe quá tuyệt vời, sẽ tiếp tục ủng hộ.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 78, 8)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (61, N'xe sạch đẹp, chạy ngon. chủ xe lịch sự, thân thiện', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 79, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (62, N'Anh chị chủ xe nhiệt tình, vui vẻ Xe chạy êm, hoạt động ổn định', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 80, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (63, N'Xe chạy bốc, khá êm, Anh Chị chủ xe vui vẻ nhiệt tình, sẽ ủng hộ lần sau tiếp.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 21, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (64, N'Xe : mới , sạch sẽ, chạy mướt >>>>> rất hài lòng Chủ xe : thân thiện hỗ trợ khách hàng nhiệt tình, linh động >>>>> rất hài lòng >>>>>>>> Lần sau sẽ ủng hộ tiếp', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 22, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (65, N'Xe sạch sẽ, chị chủ xe vui vẻ, nhiệt tình Đánh giá 5 sao', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 23, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (66, N'chủ xe thân thiện', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 24, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (67, N'xe sạch, chạy êm hai bạn chủ xe chu đáo nhiệt tình', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 25, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (68, N'Xe tốt, chủ xe nhiệt tình vui vẻ!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 26, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (69, N'Anh chị chủ rất nhiệt tình và thân thiện !', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 27, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (70, N'chủ xe rất nhiệt tình', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 28, 9)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (71, N'Xe tốt, chủ xe thân thiện!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 29, 10)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (72, N'Anh chị chủ xe nhiệt tình Xe mới, tiết kiệm xăng Giá cả hợp lý', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 30, 10)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (73, N'Chủ xe thoải mái nhiệt tình. Hỗ trợ khách nhanh chóng. Xe nhỏ gọn, tiết kiệm xăng, còn mới, full option.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 31, 11)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (74, N'2 vợ chồng chủ xe nhiệt tình. Xe êm, và cực kì tiết kiệm xăng', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 32, 11)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (75, N'Vợ chồng chủ xe rất thân thiện nhiệt tình Xe chạy êm, tiết kiệm xăng lắm Nói chung là trên cả tuyệt vời ạ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 33, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (76, N'chu xe than thien nhiet tinh, se tiep tuc ung ho a chi!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 34, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (77, N'xe tốt, chạy êm tiết kiệm xăng. chủ xe rất vui vẻ, nhiệt tình, chỉ có đánh giá 5 sao là đúng nhất. Nhất định sẽ ủng hộ lần sau. cảm ơn 2 bạn', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 35, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (78, N'anh chị chủ xe nhiệt tình, thân thiện, đúng giờ, sẽ ủng hộ anh chị thêm nữa.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 36, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (79, N'Tài xế đúng giờ, nhiệt tình, vui vẻ.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 37, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (80, N'Anh chị chủ xe rất thân thiện, nhiệt tình, dễ chịu. Nếu cần lần sau mình sẽ tiếp tục ủng hộ', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 38, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (81, N'Chủ xe nhiệt tình, thân thiện. Xe tốt. Lần sau sẽ tiếp tục ủng hộ.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 39, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (82, N'Xe đi ngon, sạch sẽ. 2 vợ chồng chủ dễ thương! nhất định sẽ book tiếp cho lần sau!', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 40, 12)
INSERT [dbo].[CarReviews] ([Id], [Content], [Rating], [CreatedAt], [UpdateAt], [UserId], [CarId]) VALUES (83, N'vui vẻ và nhiệt tình.lần sau có đi đâu tôi sẽ thuê xe của hai bạn.', 5, CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), CAST(N'2022-11-16T22:10:58.9323576' AS DateTime2), 41, 12)
SET IDENTITY_INSERT [dbo].[CarReviews] OFF
GO
SET IDENTITY_INSERT [dbo].[CarImages] ON 

INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (1, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/toyota_corolla_cross_g_2020/p/g/2020/09/04/14/HzJQdYBEGFgF-RPo7usFKw.jpg', 1)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (2, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/toyota_corolla_cross_g_2020/p/g/2020/09/04/14/XY20mdHZNXb2aK3NGJjx1g.jpg', 1)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (3, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mitsubishi_xpander_2021/p/g/2021/09/04/14/IHR8B2cdUB_ZREAreVKJlQ.jpg', 2)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (4, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mitsubishi_xpander_2021/p/g/2021/09/04/14/kHuIaGnaqPXRehFLOMtiCA.jpg', 2)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (5, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_3_2019/p/g/2021/04/04/13/4OuVBTyPDbNGdcjuKfqe7A.jpg', 3)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (6, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_3_2019/p/g/2021/04/04/13/nVfdnOxt2J37YcCsTrsf4A.jpg', 3)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (7, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mitsubishi_attrage_2020/p/g/2021/07/13/12/PiKa_FrqZuCVXovCxy5jtQ.jpg', 4)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (8, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mitsubishi_attrage_2020/p/g/2021/07/13/12/ZFH6T__LNpn-ccPpvWt6pw.jpg', 4)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (9, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/honda_city_2019/p/g/2021/07/21/18/4raaxDVyMw5JAcI5Rbjhsw.jpg', 5)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (10, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/honda_city_2019/p/g/2021/07/21/18/wcAIb_mRe-Jxaa1EGG28LQ.jpg', 5)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (11, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/kia_soluto_2019/p/g/2020/09/27/06/n8vdfiOHLU8fCPjr6WMYew.jpg', 6)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (12, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/kia_soluto_2019/p/g/2020/09/27/06/KWacSZDBApPLQ_ypzEYw1g.jpg', 6)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (13, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_2_2016/p/g/2022/09/18/15/bwAcKXK3rQ1n3AE7bhZCiw.jpg', 7)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (14, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_2_2016/p/g/2022/09/18/15/K8jFjcjPVepoz26p0eXR0w.jpg', 7)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (15, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/vinfast_lux_a_2.0_2020/p/g/2021/03/14/10/640_dI3iLXIw2ua_tuV3SQ.jpg', 8)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (16, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/vinfast_lux_a_2.0_2020/p/g/2021/03/14/10/JyjRfsspM0vbm3muHlmhng.jpg', 8)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (17, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_3_2017/p/g/2022/07/25/15/jOCbONimyefObFrOTduilw.jpg', 9)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (18, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/mazda_3_2017/p/g/2022/07/25/15/KH8A5Q4s4O5WiihC-B6CKg.jpg', 9)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (19, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/honda_civic_2020/p/g/2022/06/13/23/Wb2sIADyEz7MmmXYJXERbw.jpg', 10)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (20, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/honda_civic_2020/p/g/2022/06/13/23/zauaCFiqbnSWb8C_SJ2YiA.jpg', 10)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (21, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/kia_cerato_2017/p/g/2022/03/18/14/1NlQjIoCVuO3NuYGW8P0cw.jpg', 11)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (22, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/kia_cerato_2017/p/g/2019/07/07/12/3BXr1dJe2cBwNIEGJIQRvQ.jpg', 11)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (23, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/chevrolet_cruze_2017/p/g/2022/05/27/13/ySLmDCpnbR5elRF963sp1w.jpg', 12)
INSERT [dbo].[CarImages] ([Id], [Path], [CarId]) VALUES (24, N'https://n1-pstg.mioto.vn/cho_thue_xe_o_to_tu_lai_thue_xe_du_lich_hochiminh/chevrolet_cruze_2017/p/g/2022/05/27/13/sDEuXm_FNDe9Oc4nFxS-vw.jpg', 12)
SET IDENTITY_INSERT [dbo].[CarImages] OFF