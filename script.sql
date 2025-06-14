
CREATE TABLE Estudiante
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100),
	Edad INT,
	Carrera VARCHAR(100)
)
GO

CREATE PROCEDURE SP_AgregarEstudiante
@Nombre VARCHAR(100),
@Edad INT,
@Carrera VARCHAR(100)
AS
BEGIN
	INSERT INTO Estudiante(Nombre, Edad, Carrera)
	VALUES(@Nombre, @Edad, @Carrera)
END

EXEC SP_AgregarEstudiante 'Manuel', 35, 'TI'

GO

CREATE PROCEDURE SP_ObtenerEstudiantes
AS
BEGIN
	SELECT * FROM Estudiante
END

EXEC SP_ObtenerEstudiantes

GO

CREATE PROCEDURE SP_ObtenerEstudiantePorId
@Id INT
AS
BEGIN
	SELECT * FROM Estudiante WHERE Id = @Id
END

EXEC SP_ObtenerEstudiantePorId 1

GO


CREATE PROCEDURE SP_ActualizarEstudiante
@Id INT,
@Nombre VARCHAR(100),
@Edad INT,
@Carrera VARCHAR(100)
AS
BEGIN
	UPDATE Estudiante 
			SET Nombre = @Nombre,
				Edad = @Edad,
				Carrera = @Carrera
	WHERE Id = @Id

END

EXEC SP_ActualizarEstudiante 1, 'Alexis', 33, 'Tecnologia'

GO

CREATE PROCEDURE SP_EliminarEstudiante
@Id INT
AS
BEGIN
	DELETE FROM Estudiante WHERE Id = @Id
END

EXEC SP_EliminarEstudiante 2