namespace StyleShell.Assembly {

    /// <summary>
    /// Todas as enumerações de erros do StyleShell
    /// </summary>
    public enum ErrorCodes {
        UnknownError = 001,
        StyleShellIOError = 005
    }

    /// <summary>
    /// Informações relacionadas a essa versão do StyleShell C#
    /// </summary>
    public sealed class ProjectAssembly {
        // Versão do StyleShell relativa
        public static List<string> Version { get; } = ["2.0.0", "active-development"];

        // Publicador dessa versão do StyleShell
        public static string Author { get; } = "Luno Henrique";

        // Ultima data de atualização desse lançamento
        public static string ReleaseDate { get; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // TODO: como isso é uma active developmet, mudar isso depois
    }

    /// <summary>
    /// Opções relacionadas a questão de armazenamento do projeto em si
    /// </summary>
    public sealed class ProjectStorage {
        /// <summary>
        /// Caminho relativo ao armazenamento local do StyleShell
        /// </summary>
        public static string Root { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".styleshell");

        /// <summary>
        /// Informações fixas sobre arquivos padrões de armazenamento do StyleShell, aqui sendo em específico apenas arquivos
        /// </summary>
        public sealed class DefaultFile {
            /// <summary>
            /// Local padrão de inserção do arquivo de configurações do StyleShell
            /// </summary>
            public static string Settings { get; } = Path.Combine(Root, "defaults.json");
        }

        /// <summary>
        /// Informações fixas sobre locais padrões de armazenamento do StyleShell, sendo esssa sessão em específica dedicada a apenas pastas
        /// </summary>
        public sealed class DefaultDirectory {
            /// <summary>
            /// Pasta relativa a arquivos de registro temporários do StyleShell, como relatórios de erro e entre várias outras coisas "temporárias", porém, mais normalmente armazena logs
            /// </summary>
            public static string Journal { get; } = Path.Combine(Root, "journal");

            /// <summary>
            /// Pasta relacionadas aos perfis de execução do StyleShell, que podem ser escolhidos a vontade pelo usuário.
            /// </summary>
            public static string Profiles { get; } = Path.Combine(Root, "profiles");
        }
    }

}