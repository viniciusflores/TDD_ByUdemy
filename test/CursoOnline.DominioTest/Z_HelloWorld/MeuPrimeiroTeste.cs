using Xunit;

namespace CursoOnline.DominioTest
{
    public class MeuPrimeiroTeste
    {
        // Melhorando a descri��o do nome do teste no relat�rio de testes.
        // [Fact(DisplayName = "Esse � o nome do Teste()")]

        // A anota��o Fact foi comentada para n�o ser executada a todo momento.
        // [Fact]
        public void DeveAsVariaveisTerOMesmoValor()
        {
            // Arrange
            var variavel1 = 1;

            //Action
            var variavel2 =  variavel1;

            //Assert
            Assert.Equal(variavel1, variavel2);
        }
    }
}
