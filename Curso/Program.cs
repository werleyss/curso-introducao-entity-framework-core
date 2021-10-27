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
            ConsultarDados();
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
