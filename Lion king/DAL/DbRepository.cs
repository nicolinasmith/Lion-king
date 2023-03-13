using Lion_king.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion_king.DAL
{
    public class DbRepository
    {

        private readonly string _connectionString;

        public DbRepository()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<DbRepository>().Build();
            _connectionString = config.GetConnectionString("develop");

        }

        //public async Task<Animal> GetAnimalById()
        //{
        //    string stmt = "select * from film where animal_id@id";
        //    await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        //    await using var command = dataSource.CreateCommand(stmt);
        //    command.Parameters.AddWithValue("id", id);
        //    await using var reader = await command.ExecuteReaderAsync();

        //    Animal animal = new Animal();

        //    while (await reader.ReadAsync())
        //    {
        //        animal = new Animal()
        //        {
        //            Animal_id = reader.GetInt32(0),
        //            Name = (string)reader["name"]
        //        };
        //    }

        //    return animal;
        //}

        #region Hämta data
        // hämta djur efter sökfunktion
        public async Task<IEnumerable<Animal>> GetAnimalByName(Animal animals)
        {
            List<Animal> animal = new List<Animal>();

            string stmt = "select * from animal where name = @name";
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);

            await using var command = dataSource.CreateCommand(stmt);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                animals = new Animal()
                {
                    Animal_id = (int)reader["animal_id"],
                    Name = (string)reader["name"],

                    Species = new()
                    {
                        Common_name = (string)reader["common_name"],
                        Latin_name = (string)reader["latin_name"],
                        Species_id = (int)reader["species_id"],

                        Class = new()
                        {
                            Class_id = (int)reader["class_id"],
                            Class_name = (string)reader["class_name"]
                        }
                    }
                };
                animal.Add(animals);
            }
            return animal;
        }


        //hämta lista på djur
        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            List<Animal> animals = new List<Animal>();

            string stmt = "select * from animal join species on species.species_id = animal.species_id join class on class.class_id = species.class_id";
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);

            await using var command = dataSource.CreateCommand(stmt);
            await using var reader = await command.ExecuteReaderAsync();
            Animal animal = new Animal();

            while (await reader.ReadAsync())
            {
                animal = new Animal()
                {
                    Animal_id = (int)reader["animal_id"],
                    Name = (string)reader["name"],

                    Species = new()
                    {
                        Common_name = (string)reader["common_name"],
                        Latin_name = (string)reader["latin_name"],
                        Species_id = (int)reader["species_id"],

                        Class = new()
                        {
                            Class_id = (int)reader["class_id"],
                            Class_name = (string)reader["class_name"]
                        }
                    }
                };
                animals.Add(animal);
            }
            return animals;
        }

        //hämta lista på klass
        public async Task<IEnumerable<Class>> GetClass()
        {
            List<Class> classes = new List<Class>();

            string stmt = "select * from class";
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);

            await using var command = dataSource.CreateCommand(stmt);
            await using var reader = await command.ExecuteReaderAsync();
            Class classs = new Class();

            while (await reader.ReadAsync())
            {
                classs = new Class()
                {
                    Class_id = (int)reader["class_id"],
                    Class_name = (string)reader["class_name"]
                };
                classes.Add(classs);
            }
            return classes;
        }

        //hämta art
        public async Task<IEnumerable<Species>> GetSpecies()
        {
            List<Species> species = new List<Species>();

            string stmt = "select * from species";
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);

            await using var command = dataSource.CreateCommand(stmt);
            await using var reader = await command.ExecuteReaderAsync();
            Species specie = new Species();

            while (await reader.ReadAsync())
            {
                specie = new Species()
                {
                    Species_id = (int)reader["species_id"],
                    Common_name = (string)reader["common_name"],
                    Latin_name = (string)reader["latin_name"]
                };
                species.Add(specie);
            }
            return species;
        }
        #endregion


        #region Lägg till
        //lägg till djur
        public async Task<Animal> AddAnimal(Animal animal)
        {
            try
            {
                string stmt = $"insert into animal(name, species_id) values (@name, @species)";
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var command = dataSource.CreateCommand(stmt);

                command.Parameters.AddWithValue("name", animal.Name);
                command.Parameters.AddWithValue("species", animal.Species.Species_id);
                await command.ExecuteNonQueryAsync();

                return animal;
            }
            catch (PostgresException ex)
            {
                string mess = "Det blev fel i databasen. Prova igen.";
                string errorCode = ex.SqlState;

                switch (errorCode)
                {
                    case PostgresErrorCodes.StringDataRightTruncation:
                        mess = "Namnet är för långt. Max 25 tecken.";
                        break;
                    //case PostgresErrorCodes.UniqueViolation:
                    //    mess = "Namnet på kategorin är inte unikt.";
                    //    break;
                    default:
                        break;
                }

                throw new Exception(mess, ex);
            }
        }

        //lägg till klass
        public async Task<Class> AddClass(Class newClass)
        {
            try
            {
                string stmt = $"insert into class(class_name) values (@name)";
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var command = dataSource.CreateCommand(stmt);

                command.Parameters.AddWithValue("name", newClass.Class_name);
                await command.ExecuteNonQueryAsync();

                return newClass;
            }
            catch (PostgresException ex) 
            {
                string mess = "Det blev fel i databasen. Prova igen.";
                string errorCode = ex.SqlState;

                switch (errorCode)
                {
                    case PostgresErrorCodes.StringDataRightTruncation:
                        mess = "Namnet är för långt. Max 25 tecken.";
                        break;
                    case PostgresErrorCodes.UniqueViolation:
                        mess = "Namnet på klassen finns redan. Det måste vara unikt.";
                        break;
                    default:
                        break;
                }

                throw new Exception(mess, ex);
            }
        }

        //lägg till art
        public async Task<Species> AddSpecies(Species specie)
        {
            try
            {
                string stmt = $"insert into species(common_name, latin_name, class_id) values (@name, @latin, @class)";
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var command = dataSource.CreateCommand(stmt);

                command.Parameters.AddWithValue("name", specie.Common_name);
                command.Parameters.AddWithValue("latin", specie.Latin_name);
                command.Parameters.AddWithValue("class", specie.Class.Class_id);
                await command.ExecuteNonQueryAsync();

                return specie;
            }
            catch (PostgresException ex)
            {
                string mess = "Det blev fel i databasen. Prova igen.";
                string errorCode = ex.SqlState;

                switch (errorCode)
                {
                    case PostgresErrorCodes.StringDataRightTruncation:
                        mess = "Namnet är för långt. Max 25 tecken.";
                        break;
                    case PostgresErrorCodes.UniqueViolation:
                        mess = "Namnet på arten finns redan. Det måste vara unikt.";
                        break;
                    default:
                        break;
                }

                throw new Exception(mess, ex);
            }
        }

        public async Task<Class> DeleteClass(Class classs)
        {
            try
            {
                // hur formulerar man detta?
                string stmt = $"delete from class where class_id = {classs.Class_id}";
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var command = dataSource.CreateCommand(stmt);

                command.Parameters.AddWithValue("class_id", classs.Class_id);
                await command.ExecuteNonQueryAsync();

                return classs;
            }
            catch (PostgresException ex)
            {
                string mess = "Det blev fel i databasen. Prova igen.";
                string errorCode = ex.SqlState;

                switch (errorCode)
                {
                    case PostgresErrorCodes.ForeignKeyViolation:
                        mess = "Du kan inte ta bort klassen då den innehåller arter.";
                        break;
                    case PostgresErrorCodes.UniqueViolation:
                        mess = "Namnet på kategorin är inte unikt.";
                        break;
                    default:
                        break;
                }

                throw new Exception(mess, ex);
            }
        }

        public async Task<Species> DeleteSpecies(Species specie)
        {
            try
            {
                // hur formulerar man detta?
                string stmt = $"delete from class where species_id = {specie.Species_id}";
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var command = dataSource.CreateCommand(stmt);

                command.Parameters.AddWithValue("species_id", specie.Species_id);
                await command.ExecuteNonQueryAsync();

                return specie;
            }
            catch (PostgresException ex)
            {
                string mess = "Det blev fel i databasen. Prova igen.";
                string errorCode = ex.SqlState;

                switch (errorCode)
                {
                    case PostgresErrorCodes.ForeignKeyViolation:
                        mess = "Du kan inte ta bort klassen då den innehåller arter.";
                        break;
                    case PostgresErrorCodes.UniqueViolation:
                        mess = "Namnet på kategorin är inte unikt.";
                        break;
                    default:
                        break;
                }

                throw new Exception(mess, ex);
            }
        }

        #endregion


        #region Hantera nullvärden

        private static T? ConvertFromDbVal<T>(object obj)
        {
            // generisk metod för att konvertera från databasvärdet 
            // C#-uttryck som kan ta emot vad som helst - objekt!

            if (obj == null || obj == DBNull.Value)
            {
                return default;
            }
            return (T)obj;

        }

        private static object ConvertToDbVal<T>(object obj)
        {
            // till databasvärde ifrån vilken typ som helst

            if (obj == null || obj == string.Empty)
            {
                return DBNull.Value;
            }
            return (T)obj;
        } 
        #endregion
    }
}
