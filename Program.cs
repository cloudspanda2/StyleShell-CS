using StyleShell.Debug;

namespace StyleShell {

    /// <summary>
    /// Ponto de partida do programa inteiro, aqui é o carregador principal de todos os módulos do StyleShell
    /// </summary>
    public class Program {

        /// <summary>
        /// Função chamada quando o programa é executado, seja pelo Console ou seja por um aplicativo independente
        /// </summary>
        /// <param name="args">Argumentos passsados na chamada da linha de comando do StyleShell</param>
        public static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            FirstRunModule();
        }

        /// <summary>
        /// Aqui é a função dedicada a inicializar os módulos e dizer se devem ser inicializados, módulos que oferecem ao usuário a experiência de inicialização inicial
        /// </summary>
        public static void FirstRunModule() {
            try {
                Console.WriteLine("causando uma crash no meme aq, divindo 0 por 0 :3");
                int cu = 0;
                int x = 0 / cu;
            } catch (Exception e) {
                SecurityManager.Catch(e, "Ponto de partida (Program.Main())");
            }
        }

    }
}