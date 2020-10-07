using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implamentations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repositorio = new UserImplementation(context);
                UserEntity _entity = new UserEntity()
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName(),
                    Password = "teste"
                };

                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Email, _registroCriado.Email);
                Assert.Equal(_entity.Name, _registroCriado.Name);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Email, _registroAtualizado.Email);
                Assert.Equal(_entity.Name, _registroAtualizado.Name);
                Assert.NotNull(_registroAtualizado.UpdateAt);

                var _registroReExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroReExiste);

                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Email, _registroSelecionado.Email);
                Assert.Equal(_registroAtualizado.Name, _registroSelecionado.Name);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _registroEncontrado = await _repositorio.FindByLogin(_registroSelecionado.Email);
                Assert.NotNull(_registroEncontrado);
                Assert.Equal(_registroEncontrado.Email, _registroSelecionado.Email);

                var _registroEncontradoPorId = await _repositorio.FindById(_registroSelecionado.Id);
                Assert.NotNull(_registroEncontradoPorId);
                Assert.Equal(_registroEncontradoPorId.Id, _registroSelecionado.Id);

                var _registroRemovido = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_registroRemovido);
            }
        }
    }
}
