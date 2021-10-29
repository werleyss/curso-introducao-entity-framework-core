using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            // verificar se existe migrations pendentes 
            /*var existe = db.Database.GetPendingMigrations().Any();
            if(existe)
            {
                // 
            }*/

            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            // ConsultarPedidoCarregmentoAdiantamento();
            //AtualizarDados();
            RemoverDados();
        }

        private static void RemoverDados()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.Find(2);

            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Detached;

            db.SaveChanges();

        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(1);

            var cliente = new
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente desconectado",
                Telefone = "7966666"
            };

            db.Attach(cliente);
            //db.Entry(cliente).State = EntityState.Modified;
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            db.SaveChanges();
        }

        public static void ConsultarPedidoCarregmentoAdiantamento()
        {
            using var db = new Data.ApplicationContext();

            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        public static void CadastrarPedido()
        {
           
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();

            var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();

            var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Cliente Teste",
                CEP = "12345678",
                Cidade = "Palmas",
                Estado = "TO",
                Telefone = ""
            };

            using var db = new Data.ApplicationContext();

            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();

            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}
