using StyleShell.Assembly;
using System.Text;

namespace StyleShell.Security {

    /// <summary>
    /// Classe dedicada ao gerênciamento de erros e exceções do StyleShell
    /// </summary>
    public sealed class SecurityManager {
        private static Exception? CatchException = null;

        /// <summary>
        /// Metodo estático dedicado a inicialização da criação de relatórios de erro do StyleShell
        /// </summary>
        /// <param name="exception">Modelo genérico de qualquer tipo derivado de uma exceção, serve para tratamento void-side</param>
        /// <param name="location">A localização mais explícita do código onde isso ocorreu, ou seja, um local mais claro de onde ocorreu</param>
        /// <returns>Não retorna nenhum valor, porém, causa uma exceção também, em qualquer erro, fazendo StyleShell fechar</returns>
        public static void Catch(Exception exception, string? location) {
            // Define o FileStream do arquivo para poder fechar
            FileStream? JournalFile = null;

            try {
                // Chama a função de verificar a integridade da estrutura de arquivo, e caso necessário, repara
                if (Integrity.CheckDirectoryIntegrity()) {
                    // Variáveis locais de informações
                    string? StackTrace = "Not Implemented", vLocation = "Undefined", CatchInnerType, ProcessArch, SuperProcess;
                    string CatchDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    // Primeiro, consegue o tipo de erro
                    if (exception.GetType().Name != null) CatchInnerType = exception.GetType().Name;
                    else CatchInnerType = "[G]UnknownError";

                    // Segundo, determina se o local relativo de ocorrência é valido para essa Catch
                    if (location != null) vLocation = location;

                    // Terceiro, determina a StackTrace do erro para o Catch e lógica de organizar ele
                    if (exception.StackTrace != null) {
                        // Define a variável de StackTrace não formatada
                        StackTrace = exception.StackTrace;

                        // Define a variável de StackTrace a ser formatada, e itera em cima dessa StackTrace separada
                        string[] FormattedStackTrace = exception.StackTrace.Split(Environment.NewLine); string FinalStackTrace = "";
                        foreach (string line in FormattedStackTrace) {
                            if (line == FormattedStackTrace[FormattedStackTrace.Length - 1]) FinalStackTrace += "// " + line;
                            else FinalStackTrace += "// " + line + Environment.NewLine;
                        } 

                        // Para finalizar tudo com chave de ouro, atualiza a varivel final
                        StackTrace = FinalStackTrace;
                    }

                    // Quarto, analisa e determina a arquitetura do processo
                    if (Environment.Is64BitProcess) ProcessArch = "x64";
                    else ProcessArch = "x32";

                    // Quinto, determina se o processo tem previlégios de administrador
                    if (Environment.IsPrivilegedProcess) SuperProcess = "Sim";
                    else SuperProcess = "Não";

                    // Determina o conteúdo do arquivo a ser escrito
                    string CatchContent = $"""
                    // ===#================== Erro StyleShell {CatchInnerType} ==================#== //
                    // ---
                    // Nome do Sistema Opercinal     {Environment.MachineName}
                    // Arquitetura do processo       {ProcessArch}
                    // Administrador // Root         {SuperProcess}
                    // Sistema Operacional           {Environment.OSVersion}
                    // Data da ocorrência            {CatchDate}
                    // PID do StyleShell             {Environment.ProcessId}
                    // Linha de comando              {Environment.CommandLine}
                    // Tipo de exceção               {CatchInnerType}
                    // Diretório raíz                {Environment.ProcessPath}
                    // Uso de CPU                    {Environment.CpuUsage}
                    // ---
                    // ===#================== StackTrace {CatchInnerType} ==================#== //
                    // ---
                    {StackTrace}
                    // ---
                    // ===#================== Erro StyleShell {CatchInnerType} ==================#== //
                    """;

                    // Verifica a existência da categoria de erros específica
                    string JournalFileCategory = Path.Combine(ProjectStorage.DefaultDirectory.Journal, $"{CatchInnerType}");
                    if (!Directory.Exists(JournalFileCategory)) Directory.CreateDirectory(JournalFileCategory);

                    // Cria o novo arquivo
                    string JournalFilePath = Path.Combine(ProjectStorage.DefaultDirectory.Journal, $"{CatchInnerType}", $"ERROR_{DateTime.Now:ddMMyyyyHHmmssfffff}.catch");
                    JournalFile = File.Create(JournalFilePath);

                    // Tenta escrever no novo arquivo e fecha
                    JournalFile.Write(Encoding.UTF8.GetBytes(CatchContent));
                    JournalFile.Close();
                } else throw new DirectoryNotFoundException();
            } catch (Exception e) { CatchException = e; } finally {
                // Verifica se o arquivo está aberto para evitar qualquer vazamento de informações
                JournalFile?.Close();

                // Agora de forma segura finaliza o programa, por ser o comportamento padrão dele
                Integrity.FinalizeDestinguer(CatchException ?? exception);
            } 
        }


    }

}