using StyleShell.Security;
using Newtonsoft.Json;

namespace StyleShell.Assembly.Config {

    /// <summary>
    /// Funções básicas, como a de carregar as configurações do StyleShell em um objeto específico e salvar as configurações modificadas
    /// </summary>
    public sealed class Defaults {

        /// <summary>
        /// Uma função para apenas receber o valor das configurações do StyleShell
        /// </summary>
        /// <returns>O valor de configurações escrito no JSON do StyleShell</returns>
        /// <exception cref="JsonSerializationException"></exception>
        /// <exception cref="JsonException"></exception>
        public static IDefaultsModel? Get() {
            try {
                // Tenta carregar o JSON em sua localização padrão
                if (File.Exists(ProjectStorage.DefaultFile.Settings)) {
                    // Tenta ler o conteúdodo arquivo JSON
                    string? JSONBruteContent = File.ReadAllText(ProjectStorage.DefaultFile.Settings);

                    // Verifica o conteúdo do arquivo
                    if (!string.IsNullOrEmpty(JSONBruteContent)) {
                        // Tenta carregar o conteúdo do arquivo
                        IDefaultsModel? Settings = JsonConvert.DeserializeObject<IDefaultsModel>(JSONBruteContent);

                        // Verifica se Settings não é null e continua
                        if (Settings != null) {
                            // Retorna as configurações para o usuário
                            return Settings;
                        } else { throw new JsonSerializationException(); } // Alega um problema de tratamento do JSON
                    } else {
                        File.Delete(ProjectStorage.DefaultFile.Settings); // Deleta o arquivo para ele ser criado novamente
                        throw new JsonException(); // Alega que o arquivo está corrompido
                    }
                } else {
                    // Define o conteúdo a ser serializado e utilizado no novo arquivo
                    IDefaultsModel EmptyModel = new();

                    // Tenta criar um arquivo JSON vazio para configurar tudo e escreve o básicox
                    File.AppendAllText(ProjectStorage.DefaultFile.Settings, JsonConvert.SerializeObject(EmptyModel));

                    // Re-calla essa função para evitar duplas chamadas no código
                    return Get();
                }
            } catch (Exception e) {
                // Reporta para o sistema informações do erro
                SecurityManager.Catch(e, "Função pública estática de obter configurações do StyleShell");
                // Retorna um valor nulo como base
                return null;
            }
        }

        /// <summary>
        /// Função com a utilidade de definir o conteúdo do arquivo de configurações do StyleShell de forma eficiente e segura, apaga o arquivo antigo e escreve um novo arquivo de configurações com informações atualizadas, ótimo para a questão de retro-compatibilidade
        /// </summary>
        /// <param name="parse">Argumento com as configurações modificadas do StyleShell</param>
        /// <param name="recall">Argumento utilizado no lado da função, determina se deve houver tentativas (por favor, não modificar esse argumento)</param>
        /// <returns>Retorna o valor de configurações presente no arquivo de configurações JSON do StyleShell, porém, após as modificações dessa função, ótimo para modiciar variáveis antigas de forma direta</returns>
        public static IDefaultsModel? Save(IDefaultsModel? parse, bool recall = false) {
            try {
                // Verifica a existência do arquivo de configurações
                if (File.Exists(ProjectStorage.DefaultFile.Settings)) {
                    // Verifica se o conteúdo passado não é diretamente um null
                    if (parse != null) {
                        // Tenta serializar o conteúdo passado por argumentos
                        string ConfigFileContent = JsonConvert.SerializeObject(parse);

                        // Para finalizar, tenta escrever o conteúdo da variavel anterior no arquivo
                        File.Delete(ProjectStorage.DefaultFile.Settings); // Deleta o arquivo, para garantir um limpo
                        File.AppendAllText(ProjectStorage.DefaultFile.Settings, ConfigFileContent);

                        // E logo assim, retorna o conteúdo explicitamente da forma em que ele foi gravado
                        return Get();
                    } else { throw new ArgumentNullException(nameof(parse)); } // Alega que a base é nula, causando uma exceção
                } else {
                    if (!recall) { Get(); return Save(parse, true); } // Apenas chama novmente a mesma função se isso já não tiver sido feito
                    else throw new FileNotFoundException(); // Alega um corrompimento, caso já seja feito e o erro continue, por segurança
                }
            } catch (Exception e) {
                // Envia para o tratamento de erros do sistema, essa exceção
                SecurityManager.Catch(e, $"Função pública e estática de salvar configurações -> Carrega (parse='{parse}');");
                // Retorna nulo para indicar falha
                return null;
            }
        }
    }

}