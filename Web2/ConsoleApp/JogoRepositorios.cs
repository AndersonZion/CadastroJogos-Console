using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class JogoRepositorios
    {
        public List<Jogo> jogos { get; set; }
        public JogoRepositorios()
        {
            jogos = new List<Jogo>();
        }

        public bool Inserir(Jogo jogo)
        {
            bool resultado = true;
            try
            {
                var j = jogos.Find(x => x.Id == jogo.Id);
                if (j == null)
                {
                    jogos.Add(jogo);
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }
            return resultado;
        }
        public bool Alterar(Jogo jogo)
        {
            bool resultado = false;
            var j = jogos.Find(x => x.Id == jogo.Id);
            if (j != null)
            {
                j.Nome = jogo.Nome;
                j.Descricao = jogo.Descricao;
                j.Genero = jogo.Genero;
                j.Console = jogo.Console;
                resultado = true;
            }
            return resultado;
        }
        public bool Excluir(int id)
        {
            bool resultado = false;
            var j = jogos.Find(x => x.Id == id);
            if (j != null)
            {
                resultado = jogos.Remove(j);
            }
            return resultado;
        }
        public List<Jogo> Localizar(string nome)
        {
            List<Jogo> lj = jogos.FindAll(x => x.Nome.Contains(nome.ToUpper()));
            return lj;
        }

        public List<Jogo> ListarPorGenero(TipoGenero genero)
        {
            List<Jogo> lj = jogos.FindAll(x => x.Genero.Equals(genero));
            return lj;
        }

        public List<Jogo> ListarPorConsole(TipoConsole console)
        {
            List<Jogo> lj = jogos.FindAll(x => x.Console.Equals(console));
            return lj;
        }
        public int LocalizarUltimoId()
        {
            List<Jogo> resultlist = jogos;

            if (resultlist.Count <= 0)
            {
                return 1;
            }
            else
            {
                var last = resultlist.Last();
                return last.Id + 1;
            }
        }


    }

}