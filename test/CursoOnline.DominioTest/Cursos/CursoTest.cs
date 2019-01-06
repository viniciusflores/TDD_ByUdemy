using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest
{
     public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private string _nome;
        private string _descricao;
        private double _cargaHoraria;
        private PublicoAlvo _publicoAlvo;
        private double _valor;
        

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado!");
            var faker = new Faker();
            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(80, 900);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(250, 1490.25);
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado!");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Objeto anonimo
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };
            
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidaNomeCursoInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome do curso invalido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horária dop curso inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoValorInvalido(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor do curso inválido");
        }
    }



    

}
