using StyleShell.Assembly.Config;
using StyleShell.Security;

namespace StyleShell.Bash {

    public class SConsole {
        // Relacionas a função de texto de carregamento
        public static bool LoadingTextAnimationDisabled = false;
        private readonly static int AnimationDelay = 100;

        /// <summary>
        /// Define o texto animado de carregamento na mesma hora que o usuário executa o programa
        /// </summary>
        public static void HitLoadingSplash() {
            try {
                // Cria uma lista para determinar os passos do While
                List<string> LoadingCharacterStep = ["⠁ ", "⠄ ", "⡀ ", "⢀ ", " ⢀", " ⠠", " ⠈", "⠈ "];

                // Limpa o Console para evitar qualquer interferência e escreve uma base sólida
                Console.Write("BWAAA :3");
                Console.Clear();

                // Após a chamada da função, fica desativo até o sinalizador ser acionado
                while (!LoadingTextAnimationDisabled) {
                    // Itera e atualiza o texto da tela
                    foreach (var step in LoadingCharacterStep) {
                        // Define o conteúdo
                        Console.SetCursorPosition(0, 0);
                        Console.Write($"[{step}](0 / 1) Carregando StyleShell");

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

        /// <summary>
        /// Trata uma cor e traduz ela para uma cor relativa ao conteúdo de ConsoleColor
        /// </summary>
        /// <param name="needs">A cor necessária para executar essa ação</param>
        /// <param name="ConsoleDefaultPrimaryColorReset">Apenas quando a cor primária necessita ser selecionada, para outras, utilizamos com essa opção explicitamente false</param>
        /// <returns></returns>
        public static ConsoleColor DispatchColor(string? needs, bool ConsoleDefaultPrimaryColorReset = false) {
            // Verifica se oque o usuário quer, é redefinir a cor padrão do console
            if (ConsoleDefaultPrimaryColorReset) {
                // Primeiro, consegue as configurações do projeto
                IDefaultsModel? Settings = Defaults.Get();

                // Determina a cor primária
                if (Settings?.SpecifiedColors?.PrimaryColor != null) {
                    // Define a cor padrão primária do terminal
                    Console.ForegroundColor = DispatchColor(Settings?.SpecifiedColors?.PrimaryColor);

                    // Retorna o valor definido como a cor primária do terminal
                    return Console.ForegroundColor;
                } else { return Console.ForegroundColor; } // Retorna a cor atualmente definida no terminal, para não ocorrer erros.
            } else {
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
                if (FinalOutput != null) {
                    return (ConsoleColor)FinalOutput; // Retorna o conteúdo de DispatchColor
                } else {
                    // Registra um erro para ser dito ao usário e retorna branco
                    Program.StyleCore.UserWarnings.Add($"Cor inválida mencionada em pedido: {needs}");
                    return ConsoleColor.White;
                }
            }
        }
    }

}