/*Script to insert status booking*/

INSERT INTO [dbo].[BookingStatus]
           ([Name]
           ,[IsActive])
     VALUES ('Pendiente',	1),
			('Confirmado',	1),
			('Cancelado por Restaurante',	1),
			('Cancelado por Comensal',	1)