using StyleShell.Assembly;
using StyleShell.Security;
using StyleShell.Bash;
using Newtonsoft.Json;
using System.Data;

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
            // Inicia o módulo principal do StyleShell
            StyleCore.Core();
        }

        /// <summary>
        /// A classe compactadora do metodo estático Core(), antes chamado de StyleCore()
        /// </summary>
        public static class StyleCore {
            // Variável determinada a armazenar avisos de todo o programa
            public static List<string> UserWarnings = [];

            /// <summary>
            /// Aqui é a função dedicada a inicializar os módulos e dizer se devem ser inicializados, módulos que oferecem ao usuário a experiência de inicialização inicial
            /// </summary>
            public static void Core() {
                try {
                    // Define variáveis iniciais do aplicativo
                    List<string>? UserShellHistory = LoadShellHistory();

                    // Cria e inicializa uma ConsoleThread para a animação inicial
                    Thread ConsoleSplash = new(Konsole.HitLoadingSplash);
                    ConsoleSplash.Start();

                    // Define a cor padrão de várias outputs do StyleShell
                    Konsole.LoadKonsoleColors();

                    // Finaliza a ConsoleThread de carregamento e espera um pouco
                    Konsole.AbortLoadingText = true;
                    ConsoleSplash.Join();

                    // Imprime um textinho
                    Console.WriteLine("miau :3");
                } catch (Exception e) {
                    // Captura e gerência o erro de forma segura
                    SecurityManager.Catch(e, "Ponto de partida principal do StyleShell");
                }
            }

            /// <summary>
            /// Carrega o conteúdo do arquivo de histórico de comandos relativo ao usuário
            /// </summary>
            /// <returns>Em casso de erro, um valor nulo, ou em caso de sucesso, uma Lista de strings</returns>
            private static List<string>? LoadShellHistory() {
                try {
                    // Define a variável de carregamento e tenta carregar
                    string? LoadedFile = File.ReadAllText(ProjectStorage.DefaultFile.History);

                    // Organiza a variável para conseguir ser usada
                    if (LoadedFile != null) {
                        // Deserializa o conteúdo do arquivo
                        List<string>? Converted = JsonConvert.DeserializeObject<List<string>>(LoadedFile);

                        // Verifica se esse valor pode ser retornado
                        if (Converted != null) return Converted;
                        else throw new NoNullAllowedException(); // Cria uma nova exceção para indicar erro
                    } else { throw new NullReferenceException(); } // Causa uma exceção para indicar erro

                } catch (Exception e) {
                    // Captura todo o conteúdo do erro ocorrido
                    SecurityManager.Catch(e, "Carregador no módulo principal com a finalidade de carregar o HistoryFile", false);

                    // Retorna um valor nulo para emitir o erro
                    return null;
                }
            }

        }


    }
}