using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;

namespace NomesNotas3
{
    class Program
    {
        private const int NOTA_MINIMA = 0;
        private const int NOTA_MAXIMA = 20;
        private const double NOTA_APROVACAO = NOTA_MAXIMA / 2.0;

        private static List<Aluno> listaAlunos = new List<Aluno>();
        private static List<Aluno> listaAprovados = new List<Aluno>();
        private static List<Aluno> listaReprovados = new List<Aluno>();
        private static List<Aluno> listaAlunoAcimaMedia = new List<Aluno>();
        private static List<Aluno> listaAlunoAbaixoMedia = new List<Aluno>();

        static void Main(string[] args)
        {
            int totalAlunos = 0;
            double somaNotas = 0;
             double media = 0;

            int opcao;
            do
            {
                opcao = Menu();
                
                switch (opcao)
                {
                    case 1:
                        IntroduzirAlunos();
                        break;
                    case 2:
                        MostraListaAlunos();
                        break;
                    case 3:
                        NotaAprovacao();
                        break;
                    case 4:
                        MostraListaAprovados();
                        break;
                    case 5:
                        MostraListaReprovados();
                        break;
                    case 6:
                        media = CalculaMedia(ref totalAlunos, ref somaNotas);
                        Console.WriteLine($"A média da turma é de: {media}");
                        Console.ReadKey();
                        break;
                    case 7:
                        CalculaMedia(ref totalAlunos, ref somaNotas);
                        foreach (var aluno in listaAlunos)
                        {
                            if (aluno.Nota > media)
                            {
                                Console.WriteLine($"{aluno.Nome}: {aluno.Nota} valores");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 8:
                        foreach (var aluno in listaAlunoAbaixoMedia)
                        {
                            Console.WriteLine($"{aluno.Nome}: {aluno.Nota} valores");
                        }
                        Console.ReadKey();
                        break;
                    case 9:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Por Favor introduza um número válido");
                        break;
                }
            } while (opcao !=9);
        }

        private static double CalculaMedia(ref int totalAlunos, ref double somaNotas)
        {
            double media = 0;
            Console.Clear();
            foreach (var aluno in listaAlunos)
            {
                totalAlunos++;
                somaNotas += aluno.Nota;
            }
            media = somaNotas / totalAlunos;
            return media;
        }

        private static void MostraListaReprovados()
        {
            Console.Clear();
            foreach (var aluno in listaReprovados)
            { 
                Console.WriteLine($" {aluno.Nome}: {aluno.Nota} valores");   
            }
            Console.ReadKey();
        }

        private static void MostraListaAprovados()
        {
            Console.Clear();
            foreach (var aluno in listaAprovados)
            {               
                Console.WriteLine($"{aluno.Nome}: {aluno.Nota} valores");                
            }
            Console.ReadKey();
        }

        private static void NotaAprovacao()
        {
            Console.Clear();
            Console.WriteLine($"Nota mínima de aprovação: {NOTA_APROVACAO}");
            Console.ReadKey();
        }

        private static void MostraListaAlunos()
        {
            string notaQuantitativa = "";
            Console.Clear();
            int melhorNota = int.MinValue;
            int piorNota = int.MaxValue;
            foreach (var aluno in listaAlunos)
            {
                for (int i = 0; i < aluno.Nota; i++)
                {
                    Console.Write("+");
                }

                if (aluno.Nota < 6) notaQuantitativa = "Muito Fraco";
                else if (aluno.Nota < 10) notaQuantitativa = "Fraco";
                else if (aluno.Nota < 14) notaQuantitativa = "Razoável";
                else if (aluno.Nota < 16) notaQuantitativa = "Bom";
                else if (aluno.Nota < 18) notaQuantitativa = "Muito Bom";
                else notaQuantitativa = "Excelente";

                Console.WriteLine($"{aluno.Nome}: {aluno.Nota} valores {notaQuantitativa}");
                if(aluno.Nota<piorNota)
                {
                    piorNota = aluno.Nota;
                }
                if (aluno.Nota>melhorNota)
                {
                    melhorNota = aluno.Nota;
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Melhor nota: {melhorNota}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Pior nota: {piorNota}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        private static void IntroduzirAlunos()
        {
            Console.Clear();
            Aluno aluno = new Aluno();
            Console.WriteLine("Introduza o nome do aluno");
            aluno.Nome = Console.ReadLine();
            NotaAlunos(aluno);
            listaAlunos.Add(aluno);
            

        }

        private static void NotaAlunos(Aluno aluno)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Introduza a nota do aluno");
                aluno.Nota = int.Parse(Console.ReadLine());
                if (aluno.Nota < NOTA_APROVACAO)
                {
                    listaReprovados.Add(aluno);
                }
                else
                {
                    listaAprovados.Add(aluno);
                }

            } while (aluno.Nota < NOTA_MINIMA || aluno.Nota > NOTA_MAXIMA);
        }

        private static int Menu()
        {
            Console.Clear();
            Console.WriteLine("MENU");
            Console.WriteLine("1. Introduzir um aluno");
            Console.WriteLine("2. Ver lista de alunos");
            Console.WriteLine("3. Nota mínima para aprovação");
            Console.WriteLine("4. Ver lista dos aprovados");
            Console.WriteLine("5. Ver lista dos reprovados");
            Console.WriteLine("6. Média da turma");
            Console.WriteLine("7. Lista de alunos acima da média");
            Console.WriteLine("8. Lista de alunos abaixo da média");
            Console.WriteLine("9. Sair do programa");

            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Introduza um valor válido");
                return 999;
            }        
        }
    }
}
