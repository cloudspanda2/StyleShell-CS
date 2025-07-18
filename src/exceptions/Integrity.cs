using StyleShell.Assembly;

namespace StyleShell.Security {

    /// <summary>
    /// Classe dedicada a veirificação de integridade do sistema de armazenamento do StyleShell
    /// </summary>
    public sealed class Integrity {

        /// <summary>
        /// Serve para determinar o código de erro que deve finalizar o programa a partir da exceção, essa é uma função estática dedicada para realizar isso
        /// </summary>
        /// <param name="exc">O tipo genérico System.Exception, aqui sera determinado a exceção que deve finalizar o programa, e consequentemente o código de erro.</param>
        public static void FinalizeDestinguer(Exception exc) {
            switch (exc.InnerException) {
                case IOException: Environment.Exit((int)ErrorCodes.StyleShellIOError); break; // Erros de IO do StyleShell
                default: Environment.Exit((int)ErrorCodes.UnknownError); break; // Qualquer outros erros
            }
        }

        /// <summary>
        /// Função de verifica a estrutura em pastas do StyleShell e repara qualquer pasta em falta, dessa forma garantindo o bom funcionamento de tudo
        /// </summary>
        /// <returns>Um valor verdadeiro para quando a verificação for bem execuatada, junto com os reparos ou não, e false de correr algum erro em qualquer reparo</returns>
        public static bool CheckDirectoryIntegrity() {
            try {
                // Cria uma lista contendo todos os caminhos a serem investigados
                List<string> Paths = [
                    ProjectStorage.Root,
                    ProjectStorage.DefaultDirectory.Journal,
                    ProjectStorage.DefaultDirectory.Profiles
                ];

                // Verifica dinâmicamente item por item contendo na lista Paths
                foreach (var path in Paths) {
                    // Tenta criar o objeto, caso ele seja inexistente
                    if (!Path.Exists(path)) {
                        // Tenta criar a pasta no sistema de arquivos
                        Directory.CreateDirectory(path);

                        // Verifica se ela existe, caso não, uma exceção é lançada por ser erro de IO
                        if (!Path.Exists(path)) throw new DirectoryNotFoundException();
                    }
                }

                // Aqui retorna verdadeiro, caso todas as verificações estivessem corretas
                return true;

                // No bloco catch, ele finaliza o programa com o código de erro consistente
            } catch { return false; }
        }

    }

}