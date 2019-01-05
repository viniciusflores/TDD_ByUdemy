using System;
using Xunit;

namespace CursoOnline.DominioTest
{
    public class MeuPrimeiroTeste
    {
        // Melhorando a descrição do nome do teste no relatório de testes.
        // [Fact(DisplayName = "Esse é o nome do Teste()")]
        [Fact]
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
