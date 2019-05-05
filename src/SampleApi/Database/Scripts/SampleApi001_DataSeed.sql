INSERT [dbo].[WP_Schools] ([Id], [Name])
VALUES
    ('61452046-ba63-47ba-9a9e-1f0dcc10ce1a', N'School A'),
    ('5107904c-1249-43ea-b34f-7bc4c9ba3008', N'B Univercity'),
    ('57ef3c7e-ff9e-4aba-a22c-9f92d71ec856', N'College C')
GO

INSERT [dbo].[WP_Students] ([Id], [Name], [SchoolId], [CreatedTime])
VALUES
    ('dc1dae81-35e9-42a6-8f44-499cb81c03cf', N'Chris', '61452046-ba63-47ba-9a9e-1f0dcc10ce1a', '2019-02-03T12:34:56Z'),
    ('1d0bc120-caea-4b7e-a6c3-f0f9e508f754', N'Tom', '61452046-ba63-47ba-9a9e-1f0dcc10ce1a', '2019-04-01T14:12:33Z'),
    ('d4d3a267-6c20-4ba9-9193-6624299c0de1', N'Ada', '5107904c-1249-43ea-b34f-7bc4c9ba3008', '2019-05-02T10:02:07Z')
GO
