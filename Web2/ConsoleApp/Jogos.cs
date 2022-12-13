namespace ConsoleApp
{

    public class Jogo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public TipoConsole? Console { get; set; }
        public TipoGenero? Genero { get; set; }

        public Jogo()
        {
            this.Id = 1;
            this.Nome = "";
            this.Descricao = "";
            this.Genero = TipoGenero.Outro;
            this.Console = TipoConsole.Outro;
        }
        public Jogo(int id, string nome, string descricao, TipoConsole console, TipoGenero genero)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Console = console;
            this.Genero = genero;
        }

    }
}
