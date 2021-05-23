using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DIO.Bank
{
	class Program
	{
		static string usuario;
		static string senha;
		static int id;

		static List<Conta> listContas = new List<Conta>();
		static void Main(string[] args)
		{

			Console.WriteLine("Informe o Nome do úsuario para acessar as opções ?");
			usuario = Console.ReadLine();
		

			Console.WriteLine("Informe a Senha de acesso ?");
			senha = Console.ReadLine();

			if (login(usuario, senha))
			{
				menu_principal();
			}

			else
			{
				Console.WriteLine("Senha ou úsuario invalida !!!");
			}


		
		}

		private static void menu_principal()
        {
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarContas();
						break;
					case "2":
						Console.WriteLine(InserirConta());
						break;
					case "3":
						Console.WriteLine(Transferir());
						break;
					case "4":
						Console.WriteLine(Sacar());
						break;
					case "5":
				     	Console.WriteLine(Depositar());
						break;
					case "C":
						Console.Clear();
						break;

					default:
						Console.WriteLine("Opção digitada não existe !");
						break;
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
		}

		public static bool login(string usuario, string senha)
		{

			if (usuario == "bank" && senha == "123456")
				return true;
			else
				return false;

		}


		private static bool validar_currency(string valor)
        {
			return Regex.IsMatch(valor, @"^[0-9,.]");
		}

		private static string Depositar()
		{
			Console.Write("Digite o número da conta: ");
			string valor = Console.ReadLine();

			// valida valor digitado e retorna a mensagem abaixo se digitado letra
			if (char.IsLetter(Convert.ToChar(valor)))
			{
				return "\n Letra não é valido para número de conta,\n Operação cancelada !";
			}

			int indiceConta = int.Parse(valor);

			// condição para tratar quando número de conta que não existe
			if (indiceConta > id - 1)
            {
				return "\n Número de Conta não existe !";
            }

			Console.Write("Digite o valor a ser depositado: ");
			valor = Console.ReadLine();
			// valida o valor digitado e só aceita número, ponto e virgula
			if (validar_currency(valor) == false)
			{
				return "\n Valor de deposito digitado é invalido,\n Operação cancelada !";
			}
            double valorDeposito = double.Parse(valor);

            listContas[indiceConta].Depositar(valorDeposito);

			return "\n Deposito realizado com Sucesso !";
		}

		private static string Sacar()
		{
			Console.Write("Digite o número da conta: ");
			string valor = Console.ReadLine();

			// valida valor digitado e retorna a mensagem abaixo se digitado letra
			if (char.IsLetter(Convert.ToChar(valor)))
			{
				return "\n Letra não é valido para número de conta,\n Operação cancelada !";
			}
            int indiceConta = int.Parse(valor);

            // condição para tratar quando número de conta que não existe
            if (indiceConta > id - 1)
            {
	           return "\n Número de Conta não existe !";
            }

            Console.Write("Digite o valor a ser sacado: ");
			valor = Console.ReadLine();
			// valida o valor digitado e só aceita número, ponto e virgula
			if (validar_currency(valor) == false)
			{
				return "\n Valor de saque digitado é invalido,\n Operação cancelada !";
			}
			double valorSaque = double.Parse(valor);

            listContas[indiceConta].Sacar(valorSaque);

			return "\n Operação realizada com Sucesso !";
		}

		private static string Transferir()
		{
			Console.Write("Digite o número da conta de origem: ");
			string valor = Console.ReadLine();

			// valida valor digitado e retorna a mensagem abaixo se digitado letra
			if (char.IsLetter(Convert.ToChar(valor)))
			{
				return "\n Não é permitido digitar letra nesse campo,\n Operação cancelada !";
			}
            int indiceContaOrigem = int.Parse(valor);

            Console.Write("Digite o número da conta de destino: ");
			int indiceContaDestino = int.Parse(Console.ReadLine());


             // condição para tratar quando número de conta que não existe
            if (indiceContaDestino > id - 1)
            {
              	return "\n Número de Conta não existe !";
            }

            Console.Write("Digite o valor a ser transferido: ");
			valor = Console.ReadLine();
			// valida o valor digitado e só aceita número, ponto e virgula
			if (validar_currency(valor) == false)
			{
				return "\n Valor a ser transferido é invalido,\n Operação cancelada !";
			}
			double valorTransferencia = double.Parse(valor);

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);

			return "\n Operação realizada com Sucesso !";
		}

		private static string InserirConta()
		{
			Console.WriteLine("Inserir nova conta");

			Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");

			string valor = Console.ReadLine();

			// valida valor digitado e retorna a mensagem abaixo se digitado um valor diferente de 1 e 2
			if (valor != "1" && valor != "2")
            {
				return "\n Valor informa digitado não é valido para tipo de conta,\n Operação cancelada !";
            }
            int entradaTipoConta = int.Parse(valor);

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();



			Console.Write("Digite o saldo inicial: ");
			valor = Console.ReadLine();
			// valida o valor digitado e só aceita número, ponto e virgula
			if (validar_currency(valor) == false)
			{
				return "\n Saldo inicial digitado é invalido,\n Operação cancelada !";
			}
			double entradaSaldo = double.Parse(valor);

			Console.Write("Digite o crédito: ");
			valor = Console.ReadLine();
			// valida o valor digitado e só aceita número, ponto e virgula
			if (validar_currency(valor) == false)
			{
				return "\n Crédito digitado é invalido,\n Operação cancelada !";
			}
            double entradaCredito = double.Parse(valor);

			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
		    id += 1;

			return "Operação realizada com Sucesso !";
		}

		private static void ListarContas()
		{
			Console.WriteLine("Listar contas");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("Numero Conta: {0} - ", i);
				Console.WriteLine(conta);
			}
		}

		private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Bank a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar contas");
			Console.WriteLine("2- Inserir nova conta");
			Console.WriteLine("3- Transferir");
			Console.WriteLine("4- Sacar");
			Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
	}
}
