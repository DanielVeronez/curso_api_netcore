using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É possível Mapear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "Teste",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            //Model to Entity
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.Password, model.Password);
            Assert.Equal(dtoToEntity.CreateAt, model.CreateAt);
            Assert.Equal(dtoToEntity.UpdateAt, model.UpdateAt);
        }

        [Fact(DisplayName = "É possível Mapear de UserEntity para UserDTO")]
        public void E_Possivel_Mapear_UserEntity_UserDto()
        {

            var dtoToEntity = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Password = "Teste",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            //Entity to Model
            var model = Mapper.Map<UserDto>(dtoToEntity);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.CreateAt, model.CreateAt);
        }

        [Fact(DisplayName = "É possível Mapear Lista de UserEntity para UserDTO")]
        public void E_Possivel_Mapear_Lista_UserEntity_UserDto()
        {
            var listaEntity = new List<UserEntity>();
            for (int i = 0; i < 10; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                };

                listaEntity.Add(item);
            }

            //Entity to Model
            var lista = Mapper.Map<List<UserDto>>(listaEntity);
            Assert.True(lista.Count == listaEntity.Count);
            for (int i = 0; i < lista.Count; i++)
            {
                Assert.Equal(lista[i].Id, listaEntity[i].Id);
                Assert.Equal(lista[i].Name, listaEntity[i].Name);
                Assert.Equal(lista[i].Email, listaEntity[i].Email);
                Assert.Equal(lista[i].CreateAt, listaEntity[i].CreateAt);
            }
        }
    }
}
