using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestorCliente
{
    public class Program
    {
            [Serializable]
        struct Cliente 
        {
            public string nome;
            public string email;
            public string cpf;
        }


        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem =1, Adicionar, Remover, Sair }
        static void Main(string[] args)
        {

            Carregar();

            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Gestor de Clientes - Bem Vindo");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int op = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)op;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                         
                }
                Console.Clear();

            }

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente: ");
            Console.WriteLine("Qual o nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do Cliente: ");
            cliente.cpf = Console.ReadLine();

            
            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro Concluido, aperte enter para sair");
            Console.ReadLine();
        }

        static void Listagem()
        {

            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("======================================");
                    i++;
                }


            } 
            else
            {
                Console.WriteLine("Nenhum Cliente cadastrado.");
            }
                Console.WriteLine("Aperte ENTER para sair");
                Console.ReadLine();

        }


        static void Remover ()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que deseja remover");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id  < clientes.Count)
            {

                clientes.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("Id digitado é invalido, tente novamente!");
                Console.ReadLine();
            }

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();
            
            
            
            enconder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }
            catch(Exception e) 
            {

                clientes = new List<Cliente>();

            }

            stream.Close();
        }
    }
}
