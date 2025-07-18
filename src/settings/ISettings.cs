using Newtonsoft.Json;

namespace StyleShell.Assembly.Config {

    /// <summary>
    /// Modelo de configurações do arquivo JSON de configurações do StyleShell, adaptado para retro-compatibilidade
    /// </summary>
    public class IDefaultsModel {
        /// <summary>
        /// A mensagem que será mostrada na inicialização do StyleShell.
        /// </summary>
        /// <value>O valor que será utilizado como conteúdo da mensagem de carregamento</value>
        [JsonProperty("Message", NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public string? BootMessage { get; set; }

        /// <summary>
        /// Conteúdo relativo a sobreposição de nome do processo atual, ou seja, substitui o nome padrão do StyleShell.
        /// </summary>
        /// <value>O valor para ser utilizado como uma sobreposição ao nome padrão do StyleShell</value>
        [JsonProperty("ShellTitle", NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string? ShellOverlayType { get; set; }

        /// <summary>
        /// Nome de usuário a ser utilizado como uma sobreposição a qualquer nome de usuário do computador, oferecendo flexibilidade a oque for aparecer no Console.
        /// </summary>
        /// <value>O valor para ser utilizado como um nome de usuário no Console, afeta apenas visualmente o conteúdo.</value>
        [JsonProperty("UserName", NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        public string? OverlayedUserName { get; set; }

        /// <summary>
        /// O valor relativo as cores utilizadas no Console do StyleShell
        /// </summary>
        /// <value>Um objeto representando a classe ColorRelative</value>
        [JsonProperty("ConsoleColors", NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public IColorRelative? SpecifiedColors { get; set; }
    }

    /// <summary>
    /// Classe que herda de IDefaultModel no sentido de utilização, serve para representar campos de cor StyleShell
    /// </summary>
    public class IColorRelative {
        /// <summary>
        /// Valor diretamente ligado ao nome da classe, relativo a cor primária (textos padrões do terminal)
        /// </summary>
        /// <value>Um valor dinstinguível em linha de código, e traduzível para System.ConsoleColor</value>
        [JsonProperty("PrimaryTextColor", NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public string? PrimaryColor { get; set; }

        /// <summary>
        /// Valor também diretamente ligado ao nome da classe, relativo a cor secundária (textos de interface do StyleShell)
        /// </summary>
        /// <value>Um valor dintinguível em linha de código, e traduzível para System.ConsoleColor</value>
        [JsonProperty("SecondaryTextColor", NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string? SecondaryColor { get; set; }

        /// <summary>
        /// Outro valor também ligado diretamente ao nome da classe, relativo a cor terciária (cores do auto-completar, histórico, etc)
        /// </summary>
        /// <value>Um valor distinguível em linha de código, e traduzível para System.ConsoleColor</value>
        [JsonProperty("TertiaryTextColor", NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        public string? TertiaryColor { get; set; }

        /// <summary>
        /// Um valor não diretamente ligado ao nome da classe, porém, ele define se todos os textos do terminal devem aparecer em "bold", aplica-se a todos os itens dessa classe
        /// </summary>
        /// <value>O valor representando um valor booleano, determina se esse recurso está ativado ou não</value>
        [JsonProperty("UsesBoldOutput", NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public bool? BoldOutput { get; set; }
        
        /// <summary>
        /// Um valor diretamente ligado ao da classe, ele define a cor de mensagens de erro, lembrando que essa cor tenta substituir qualquer outro output de programa
        /// </summary>
        /// <value>Um valor destinguível por código, e associável a System.ConsoleColor</value>
        [JsonProperty("ErrorTextColor", NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public string? ErrorColor { get; set; }
    }

}
