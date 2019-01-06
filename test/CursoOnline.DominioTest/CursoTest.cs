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
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }

}
