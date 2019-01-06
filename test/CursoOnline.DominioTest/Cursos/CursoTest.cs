
using Bogus;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest
{
    /*
     * Criar um curso com nome, carga horária, público alvo e valor.
     * As opções para o público alvo são: Estudante, Universitário, Empregado e Empreendedor.
     * Todos os campos de curso são obrigatórios.
     * - NOVO: Curso deve ter uma descrição
     */

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
            // _output.WriteLine($"nome: " + _nome);
            // _output.WriteLine($"descricao: " + _descricao);
            // _output.WriteLine($"cargaHoraria: " + _cargaHoraria);
            // _output.WriteLine($"valor: " + _valor);

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
                Valor = _valor,
                Descricao = _descricao
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
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome do curso invalido");
            
            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária dop curso inválida");

            if (valor < 1)
                throw new ArgumentException("Valor do curso inválido");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }

}
