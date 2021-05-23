using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Informix_Avaya_Synonyms
{
    class Program
    {
        static void Main(string[] args)
        {

            // Aqui é definido o nome que aparecerá na barra de título do App Console
            string titulo = "CSU - Integration Informix Avaya CMS Synonyms - Versão ";

            //Aqui é definida a variável que armazena a versão do App
            string versao = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

            //Essa variável armazena uma nova linha
            string linha = Environment.NewLine;

            string rotina = "Conecta com o SGBD Informix...";
            Console.Title = string.Concat(titulo, versao);

            Console.WriteLine(rotina);
            Console.WriteLine("...");
            Console.WriteLine(linha);


            Console.WriteLine("Conectando no SGBD Informix...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui configuramos a conexão com o Informix Database através de DSN, o que facilita a conexão por OdbcConnection
            OdbcConnection conn = new OdbcConnection(@"DSN=seuDSN;UID=seu_usuario_cms;PWD=sua_senha_cms;"); // Aqui vc define seu DSN, seu usuário CMS e senha CMS
            conn.Open(); //Abre a conexão

            Console.WriteLine("Conectado com sucesso!");
            Console.WriteLine("...");
            Console.WriteLine(linha);


            Console.WriteLine("Formatando consulta SQL");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Consulta SQL na tabela synonyms
            string sSQL = string.Concat("SELECT * FROM synonyms");

            Console.WriteLine("Formatação concluída!");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            OdbcCommand cmd = new OdbcCommand(sSQL, conn);

            Console.WriteLine("Executando consulta SQL");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui executamos a consulta SQL
            OdbcDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("Consulta executada com sucesso!");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            Console.WriteLine("Criando arquivo de texto...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui definimos a variável x que manipula arquivos txt via stream
            StreamWriter x;

            //Aqui criamos o arquivo de texto que receberá os dados do Informix
            x = File.CreateText(@"C:\CMS\informixsyn.txt");

            Console.WriteLine("Gravando registros da consulta no arquivo...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui, gravamos os dados do cabeçalho do arquivo (que são os nomes dos campos da tabela synonyms)
            x.Write("acd_no;item_type;item_name;value;descr;standard");

            //Aqui quebramos a linha para que os próximos dados sejam escritos na linha seguinte
            x.Write(linha);

            //Aqui: enquando a variável dr (Data Reader) estiver lendo os dados:
            while (dr.Read())
            {
                //Aqui escrevemos no arquivo txt os dados do campo "acd_no" da linha atual
                x.Write(string.Concat(dr["acd_no"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "item_type" da linha atual
                x.Write(string.Concat(dr["item_type"].ToString().Trim(), ';'));// Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "item_name" da linha atual
                x.Write(string.Concat(dr["item_name"].ToString().Trim(), ';'));// Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "value" da linha atual
                x.Write(string.Concat(dr["value"].ToString().Trim(), ';'));// Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "value" da linha atual
                x.Write(string.Concat(dr["descr"].ToString().Trim(), ';'));// Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "standard" da linha atual
                x.Write(string.Concat(dr["standard"].ToString()));// Aqui não colocamos o ponto e vírgula no final, porque é o final da linha atual

                //Aqui quebramos a linha para que os próximos dados sejam escritos na linha seguinte
                x.Write(linha);

            }

            Console.WriteLine("Registros gravados com sucesso...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            Console.WriteLine("Salvando e Fechando o Arquivo...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui fechamos o arquivo txt
            x.Close();

            Console.WriteLine("Fechando a conexão com o Informix...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui fechamos o objeto Data Reader
            dr.Close();

            //Aqui fechamos a conexão com o Avaya Informix Database
            conn.Close();

        }
    }
}
