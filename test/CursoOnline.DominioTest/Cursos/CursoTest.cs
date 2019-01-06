using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.DominioTest
{
    /*
     * Criar um curso com nome, carga horaria, ublico alvo e valor.
     * As opções para o publico alvo são: Estudante, Universitário, Empregado e Empreendedor
     * Todos os campos de curso são obrigatórios.
     */

    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            //Objeto anonimo
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double) 80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double) 950
            };
            
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidaNomeCursoInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).ComMensagem("Nome do curso invalido");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).ComMensagem("Carga horária dop curso inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void ValidarCursoValorInvalido(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido)).ComMensagem("Valor do curso inválido");
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
