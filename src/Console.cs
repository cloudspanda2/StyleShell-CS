using StyleShell.Assembly.Config;
using StyleShell.Security;

namespace StyleShell.Bash {

    /// <summary>
    /// Utilidades para a classe Konsole
    /// </summary>
    public class KonsoleUtils {

        /// <summary>
        /// Trata uma cor e traduz ela para uma cor relativa ao conteúdo de ConsoleColor
        /// </summary>
        /// <param name="needs">A cor necessária para executar essa ação</param>
        /// <returns></returns>
        public static ConsoleColor DispatchColor(string? needs) {
            // Tunuleia as opções e define a cor do Console
            ConsoleColor? FinalOutput = (needs) switch {
                // Cores base
                "Black" => ConsoleColor.Black,
                "Yellow" => ConsoleColor.Yellow,
                "Blue" => ConsoleColor.Blue,
                "Green" => ConsoleColor.Green,
                "Red" => ConsoleColor.Red,
                "Magenta" => ConsoleColor.Magenta,
                "Cyan" => ConsoleColor.Cyan,
                "White" => ConsoleColor.White,
                "Gray" => ConsoleColor.Gray,

                // Tonalidades escuras
                "DarkCyan" => ConsoleColor.DarkCyan,
                "DarkBlue" => ConsoleColor.DarkBlue,
                "DarkGray" => ConsoleColor.DarkGray,
                "DarkGreen" => ConsoleColor.DarkGreen,
                "DarkMagenta" => ConsoleColor.DarkMagenta,
                "DarkRed" => ConsoleColor.DarkRed,
                "DarkYellow" => ConsoleColor.DarkYellow,

                // Opção padrão
                _ => null,
            };

            // Retorna o valor
            if (FinalOutput != null) return (ConsoleColor)FinalOutput; // Retorna a cor final
            else {
                // Registra um erro para ser dito ao usário e retorna branco
                Program.StyleCore.UserWarnings.Add($"Cor inválida mencionada em pedido: {needs}");
                return ConsoleColor.White;
            }
        }
        
    }

    /// <summary>
    /// A classe principal de Konsole, dividida em duas partes, com a segunda sendo KonsoleUtils
    /// </summary>
    public static class Konsole {
        // Relacionas a função de texto de carregamento
        public static bool AbortLoadingText = false;
        private readonly static int AnimationDelay = 100;

        /// <summary>
        /// Define o texto animado de carregamento na mesma hora que o usuário executa o programa
        /// </summary>
        public static void HitLoadingSplash() {
            // Cria uma lista para determinar os passos do While
            List<string> LoadingCharacterStep = ["⠁ ", "⠄ ", "⡀ ", "⢀ ", " ⢀", " ⠠", " ⠈", "⠈ "];

            // Limpa o Console para evitar qualquer interferência e escreve uma base sólida
            Console.Write("");
            Console.Clear();

            try {
                // Após a chamada da função, fica desativo até o sinalizador ser acionado
                while (!AbortLoadingText) {
                    // Itera e atualiza o texto da tela
                    foreach (var step in LoadingCharacterStep) {
                        // Define o conteúdo
                        Console.SetCursorPosition(0, 0);
                        Console.Write($"[{step}] O StyleShell está carregando :3");

                        // Aguarda um pouco para atualizar
                        Thread.Sleep(AnimationDelay);
                    }
                }

                // Limpa novamente o terminal para evitar conflitos de saída e texto
                Console.Clear();
            } catch (Exception e) {
                // Alega o erro que ocorreu durante o funcionamento
                SecurityManager.Catch(e, "Texto de carregamento animado incial do StyleShell");
            }
        }

        // Relaciondas a cores
        public static ConsoleColor PrimaryColor;
        public static ConsoleColor SecondaryColor;
        public static ConsoleColor TerciaryColor;
        public static ConsoleColor ErrorColor;
        public static bool BorderStyleShell; // \x1b[1m

        public static void LoadKonsoleColors() {
            try {
                // Consegue as configurações
                IDefaultsModel? Settings = Defaults.Get();

                // Define a cor principal do Console
                if (Settings?.SpecifiedColors?.PrimaryColor != null) {
                    // Define a cor específica através do metodo de reconhecimento de cores
                    PrimaryColor = KonsoleUtils.DispatchColor(Settings!.SpecifiedColors!.PrimaryColor);

                    // Define como a cor primária principal do Console
                    Console.ForegroundColor = PrimaryColor;
                } else { PrimaryColor = ConsoleColor.DarkBlue; Console.ForegroundColor = PrimaryColor; }

                // Define a cor secundária do Console
                if (Settings?.SpecifiedColors?.SecondaryColor != null) {
                    // Define a cor específica por meio da função de reconhecimento de cores
                    SecondaryColor = KonsoleUtils.DispatchColor(Settings!.SpecifiedColors!.SecondaryColor);
                } else { SecondaryColor = ConsoleColor.Cyan; }

                // Define a cor terciária do Console
                if (Settings?.SpecifiedColors?.TertiaryColor != null) {
                    // Define a cor terciária por meio da função de dispachar cores
                    TerciaryColor = KonsoleUtils.DispatchColor(Settings!.SpecifiedColors!.TertiaryColor);
                } else { TerciaryColor = ConsoleColor.Magenta; }

                // Define se o conteúdo do Console deve sair com bordas no StyleShell
                if (Settings?.SpecifiedColors?.BoldOutput != null) {
                    // Define a variável que afirma as letras terem bordas
                    BorderStyleShell = (bool)Settings!.SpecifiedColors!.BoldOutput;

                    // Define tudo no terminal já a partir do contexto atual
                    Console.Write("\x1b[1m");
                } else { BorderStyleShell = false; }

                // Define a cor de erros do Console, para maior adaptabilidade
                if (Settings?.SpecifiedColors?.ErrorColor != null) {
                    // Define a cor de erros através da função de decidir cores
                    ErrorColor = KonsoleUtils.DispatchColor(Settings!.SpecifiedColors!.ErrorColor);
                } else { ErrorColor = ConsoleColor.Red; }
            } catch (Exception e) {
                // Relata o acontecimento para os arquivos de jornal
                SecurityManager.Catch(e, "Função estática do Console de definir cores");
            }
        }
    }

}