using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursosTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

        public ArmazenadorDeCursosTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                nome = fake.Random.Words(),
                descricao = fake.Lorem.Paragraph(),
                cargaHoraria = fake.Random.Double(20,200),
                publicoAlvo = "Estudante",
                valor = fake.Random.Double(500,2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);

        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.nome &&
                     c.Descricao == _cursoDto.descricao &&
                     c.CargaHoraria == _cursoDto.cargaHoraria &&
                     c.Valor == _cursoDto.valor
                    )
            ));
        }

        [Fact]
        public void ValidaPublicoAlvoInformado()
        {
            var publicoAlvoInvalido = "Médico";
            _cursoDto.publicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto)).ComMensagem("Público Alvo Inválido");
        }

    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
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
            Enum.TryParse(typeof(PublicoAlvo), cursoDto.publicoAlvo, out var publicoAlvo);
            if (publicoAlvo == null)
                throw new ArgumentException("Público Alvo Inválido");

            Curso curso = new Curso(cursoDto.nome, cursoDto.descricao, cursoDto.cargaHoraria, (PublicoAlvo) publicoAlvo, cursoDto.valor);

            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public double cargaHoraria { get; set; }
        public string publicoAlvo { get; set; }
        public double valor { get; set; }
    }

}
