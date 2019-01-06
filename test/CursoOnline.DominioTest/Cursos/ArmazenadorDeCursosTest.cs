using Bogus;
using CursoOnline.Dominio.Cursos;
using Moq;
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
                publicoAlvoId = 1,
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
            Curso curso = new Curso(cursoDto.nome, cursoDto.descricao, cursoDto.cargaHoraria, PublicoAlvo.Estudante, cursoDto.valor);

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
