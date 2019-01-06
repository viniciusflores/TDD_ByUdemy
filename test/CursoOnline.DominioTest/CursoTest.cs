using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
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

        /*
        [Fact]
        public void ValidaNomeCursoVazio()
        {
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(string.Empty, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));
        }

        [Fact]
        public void ValidaNomeCursoNulo()
        {
            var cursoEsperado = new
            {
                Nome = "",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(null, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));
        }*/

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

            var msg = Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;

            Assert.Equal("Nome do curso invalido", msg);
        }

        /*
        [Fact]
        public void ValidarCargaHorariaInvalida()
        {
            var cursoEsperado = new
            {
                Nome = "Informatica Basica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, 0, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));
        }
        */

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

            var msg = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;

            Assert.Equal("Carga horária dop curso inválida", msg);
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

            var msg = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido)).Message;

            Assert.Equal("Valor do curso inválido", msg);
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
            // if (nome == string.Empty)
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
