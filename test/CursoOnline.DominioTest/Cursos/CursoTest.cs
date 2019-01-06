using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest
{
    /*
     * Criar um curso com nome, carga horaria, ublico alvo e valor.
     * As opções para o publico alvo são: Estudante, Universitário, Empregado e Empreendedor
     * Todos os campos de curso são obrigatórios.
     */

    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private string _nome;
        private double _cargaHoraria;
        private PublicoAlvo _publicoAlvo;
        private double _valor;


        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado!");
            _nome = "Informatica Basica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
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
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };
            
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidaNomeCursoInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome do curso invalido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() => new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor)).ComMensagem("Carga horária dop curso inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoValorInvalido(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() => new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido)).ComMensagem("Valor do curso inválido");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitário,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome do curso invalido");
            
            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária dop curso inválida");

            if (valor < 1)
                throw new ArgumentException("Valor do curso inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }

}
