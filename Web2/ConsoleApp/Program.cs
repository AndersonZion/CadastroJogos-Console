

using System.Text.RegularExpressions;

namespace ConsoleApp;


class Program
{

    #region Configuração dos textos

    //WriteColor("This is my message with new color with red", ("{message}", ConsoleColor.Red), ("{with}", ConsoleColor.Blue));
    private static void WriteColor(string str, params (string substring, ConsoleColor color)[] colors)
    {
        var words = Regex.Split(str, @"( )");

        foreach (var word in words)
        {
            (string substring, ConsoleColor color) cl = colors.FirstOrDefault(x => x.substring.Equals("{" + word + "}"));
            if (cl.substring != null)
            {
                Console.ForegroundColor = cl.color;
                Console.Write(cl.substring.Substring(1, cl.substring.Length - 2));
                Console.ResetColor();
            }
            else
            {
                Console.Write(word);
            }
        }
    }
    public static void InserirTexto(string texto, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(texto);
        Console.ResetColor();
    }
    public static int LerDadosInteiros(String inicie)
    {
        Console.Write(inicie);
        var numInput = Console.ReadLine();
        int cleanNum = 0;
        while (!int.TryParse(numInput, out cleanNum))
        {
            InserirTexto("Esta não é uma entrada válida. Por favor insira um valor valido : ", ConsoleColor.Red);
            numInput = Console.ReadLine();

        }
        return Convert.ToInt32(numInput);
    }

    //TODO: Refatorar essa parte
    public static TipoGenero LerDadosGenero(String inicie)
    {
        Console.Write(inicie);
        var numInput = Console.ReadLine();
        int cleanNum = 0;
        while (!int.TryParse(numInput, out cleanNum))
        {
            WriteColor("Está não é uma entrada válida. Por favor insira um valor valido : ",
            ("{insira}", ConsoleColor.Red),
            ("{valor}", ConsoleColor.Red),
            ("{valido}", ConsoleColor.Red));
            numInput = Console.ReadLine();

        }

        return (TipoGenero)Convert.ToInt32(numInput);
    }

    public static TipoConsole LerDadosConsole(String inicie)
    {
        Console.Write(inicie);
        var numInput = Console.ReadLine();
        int cleanNum = 0;
        while (!int.TryParse(numInput, out cleanNum))
        {
            InserirTexto("Esta não é uma entrada válida. Por favor insira um valor valido : ", ConsoleColor.Red);
            numInput = Console.ReadLine();
        }

        return (TipoConsole)Convert.ToInt32(numInput);
    }

    #endregion
    static int ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Controle de jogos ===");
        Console.WriteLine("Selecione uma opcao: ");
        Console.WriteLine("[1] Cadastrar um jogo");
        Console.WriteLine("[2] Excluir um jogo");
        Console.WriteLine("[3] Alterar um jogo");
        Console.WriteLine("[4] Localizar um jogo por nome");
        Console.WriteLine("[5] Listar os jogos por Genero");
        Console.WriteLine("[6] Listar os jogos por Console");
        Console.WriteLine("[7] Listar todos os jogos");
        Console.WriteLine("[9] Sair");
        // Console.Write("Opcao: ");
        // int op = Convert.ToInt32(Console.ReadLine());

        int op = LerDadosInteiros("Opcao: ");
        return op;
    }

    static void Main(string[] args)
    {
        JogoRepositorios jogoRepositorios = new JogoRepositorios();

        List<Jogo> lista = new List<Jogo>();

        int id = 0;
        Jogo jogo;

        int op = 0; //valor da operação que o usuário era realizar
        while (op != 9)
        {
            op = ShowMenu();
            Console.Clear();
            switch (op)
            {
                case 1: //inserir

                    jogo = new Jogo();
                    jogo.Id = jogoRepositorios.LocalizarUltimoId();
                    Console.WriteLine("Id: " + jogo.Id);
                    Console.Write("Nome: ");
                    jogo.Nome = Console.ReadLine();

                    Console.Write("Descricao: ");

                    jogo.Descricao = Console.ReadLine();
                    jogo.Genero = LerDadosGenero("Informe o Genero Acao [0], Aventura [1], Casual [2], Puzze [3], Estrategia [4], Outro [5]: ");
                    jogo.Console = LerDadosConsole("Informe o Console PS4 [0], PS5 [1], Switch [2], Xbox 360 [3], Xbox One [4], PC [5], Outro [6]: ");

                    if (jogoRepositorios.Inserir(jogo))
                    {
                        InserirTexto("Jogo alterado!!!!", ConsoleColor.Green);
                    }
                    else
                    {
                        InserirTexto("Jogo nao alterado!!!!", ConsoleColor.Red);
                    }

                    Console.ReadKey();
                    break;
                case 2: //excluir
                    Console.WriteLine("Excluir jogo");
                    id = LerDadosInteiros("Informe o id do jogo: ");

                    if (jogoRepositorios.Excluir(id))
                    {
                        InserirTexto("Jogo excluido!!!!", ConsoleColor.Green);
                    }
                    else
                    {
                        InserirTexto("Jogo nao excluido!!!!", ConsoleColor.Red);
                    }
                    Console.ReadKey();
                    break;
                case 3: //Alterar
                    Console.WriteLine("Alterar um jogo");
                    jogo = new Jogo();

                    jogo.Id = LerDadosInteiros("Id: ");
                    Console.Write("Nome: ");
                    jogo.Nome = Console.ReadLine();

                    Console.Write("Descricao: ");
                    jogo.Descricao = Console.ReadLine();

                    jogo.Genero = LerDadosGenero("Informe o Genero Acao [0], Aventura [1], Casual [2], Puzze [3], Estrategia [4], Outro [5]: ");
                    jogo.Console = LerDadosConsole("Informe o Console PS4 [0], PS5 [1], Switch [2], Xbox 360 [3], Xbox One [4], PC [5], Outro [6]: ");

                    if (jogoRepositorios.Alterar(jogo))
                    {
                        InserirTexto("Jogo alterado!!!!", ConsoleColor.Green);
                    }
                    else
                    {
                        InserirTexto("Jogo nao alterado!!!!", ConsoleColor.Red);
                    }
                    Console.ReadKey();
                    break;
                case 4: //Localizar por nome
                    Console.WriteLine("Localizar jogos");
                    Console.Write("Informe o nome do jogo: ");
                    string? nomejogo = Console.ReadLine();
                    lista = jogoRepositorios.Localizar(nomejogo);

                    foreach (var j in lista)
                    {
                        Console.Write("Id: " + j.Id);
                        Console.Write(" - Nome: " + j.Nome);
                        Console.Write(" - Descricao: " + j.Descricao);
                        Console.Write(" - Genero: " + j.Genero);
                        Console.WriteLine(" - Console: " + j.Console);
                    }
                    InserirTexto("Aperte qualquer tecla para continuar", ConsoleColor.Blue);
                    Console.ReadKey();
                    break;
                case 5: //Listar Gênero
                    Console.WriteLine("Listar todos os jogos por genero");
                    Console.Write("Informe o Genero Acao [0], Aventura [1], Casual [2], Puzze [3], Estrategia [4], Outro [5]: ");
                    TipoGenero genero = (TipoGenero)Convert.ToInt32(Console.ReadLine());
                    lista = jogoRepositorios.ListarPorGenero(genero);

                    foreach (var j in lista)
                    {
                        Console.Write("Id: " + j.Id);
                        Console.Write(" - Nome: " + j.Nome);
                        Console.Write(" - Descricao: " + j.Descricao);
                        Console.Write(" - Genero: " + j.Genero);
                        Console.WriteLine(" - Console: " + j.Console);
                    }
                    InserirTexto("Aperte qualquer tecla para continuar", ConsoleColor.Blue);
                    Console.ReadKey();
                    break;
                case 6: //Listar Console
                    Console.WriteLine("Listar todos os jogos por console");
                    Console.Write("Informe o Console PS4 [0], PS5 [1], Switch [2], Xbox 360 [3], Xbox One [4], PC [5], Outro [6]: ");
                    TipoConsole console = (TipoConsole)Convert.ToInt32(Console.ReadLine());
                    lista = jogoRepositorios.ListarPorConsole(console);

                    foreach (var j in lista)
                    {
                        Console.Write("Id: " + j.Id);
                        Console.Write(" - Nome: " + j.Nome);
                        Console.Write(" - Descricao: " + j.Descricao);
                        Console.Write(" - Genero: " + j.Genero);
                        Console.WriteLine(" - Console: " + j.Console);
                    }
                    InserirTexto("Aperte qualquer tecla para continuar", ConsoleColor.Blue);

                    Console.ReadKey();
                    break;
                case 7: //Listar todos os jogos
                    Console.WriteLine("Listar todos os jogos");
                    var result = jogoRepositorios.jogos;
                    if (result.Count() > 0)
                    {
                        foreach (var j in jogoRepositorios.jogos)
                        {
                            Console.Write("Id: " + j.Id);
                            Console.Write(" - Nome: " + j.Nome);
                            Console.Write(" - Descricao: " + j.Descricao);
                            Console.Write(" - Genero: " + j.Genero);
                            Console.WriteLine(" - Console: " + j.Console);
                        }

                    }
                    else
                    {
                        InserirTexto("Não existe dados no banco.", ConsoleColor.Red);
                    }

                    InserirTexto("Aperte qualquer tecla para continuar", ConsoleColor.Blue);
                    Console.ReadKey();
                    break;
            }
        }
    }
}


