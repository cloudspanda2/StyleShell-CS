using StyleShell.Assembly;

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
        }

        /// <summary>
        /// Aqui é a função dedicada a inicializar os módulos e dizer se devem ser inicializados, módulos que oferecem ao usuário a experiência de inicialização inicial
        /// </summary>
        public static void FirstRunModule() {
            try {
                // Verifica o caminho relativo a pasta de configurações, a ".styleshell"
                if (File.Exists(ProjectStorage.Root + "default.json")) return;

            } catch (Exception e) {

            }
        }

    }
}