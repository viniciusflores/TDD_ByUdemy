using CursoOnline.Dominio.Cursos;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursos
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                nome = "Curso A",
                descricao = "Descricao do Curso A",
                cargaHoraria = 80,
                publicoAlvoId = 1,
                valor = 850.00
            };

            // o mock simula a utilização de banco de dados.
            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            // para instanciar o serviço de dominio, foi passado o mock.object  (obj final)
            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

            // passando o cursoDto para o metodo de armazenar
            armazenadorDeCurso.Armazenar(cursoDto);

            // verificando se o mock foi chamado (verify diferente de assert, valida o comportamento do sistema, e não um dado especifico
            cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
        }

    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {

        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(cursoDto.nome, cursoDto.descricao, cursoDto.cargaHoraria, PublicoAlvo.Estudante, cursoDto.valor);

            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public double cargaHoraria { get; set; }
        public int publicoAlvoId { get; set; }
        public double valor { get; set; }
    }

}
