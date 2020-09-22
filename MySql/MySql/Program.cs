using System;
using System.Collections;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

/*
 Criado por Samuel dos Santos Alencar
     */

namespace MySql
{

    /// <summary>
    /// Essa classe tera os comando basicos de SqlServer como Insert, Update, Delete e Select
    /// </summary>
    public class Comand
    {
        /// <summary>
        /// Retorna um valor bool (true, false) informando se foi ou não cadastrado os dados no banco
        /// </summary>
        /// 
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer "INSERT INTO tabela (coluna1,coluna2,coluna3...) VALUES ('valor1, valor2, valor3...)" 
        /// </param>
        /// 
        /// <param name="RouteConnect">
        /// Rota de conexão com o banco de dados
        /// </param>
        /// <returns></returns>
        public static bool Insert(string comando, string RouteConnect)
        {
            if (comando.Substring(0, 6).ToUpper() == "INSERT")
            {
                //ponte de conexão
                MySqlConnection PonteDeConexao = new MySqlConnection(RouteConnect);

                //ponte de comando SqlServer
                MySqlCommand PonteDeComando = new MySqlCommand(comando, PonteDeConexao);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    PonteDeConexao.Open();
                    PonteDeComando.ExecuteNonQuery();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    PonteDeConexao.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retorna um valor bool (true, false) informando se houve ou não alteração no dados no banco
        /// </summary>
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer "UPDATE tabela SET alteração WHERE condição" 
        /// </param>
        /// 
        /// <param name="RouteConnect">
        /// Rota de conexão com o banco de dados
        /// </param>
        /// <returns></returns>
        public static bool Update(string comando, string RouteConnect)
        {

            if (comando.Substring(0, 6).ToUpper() == "UPDATE")
            {

                //ponte de conexão
                MySqlConnection conn = new MySqlConnection(RouteConnect);

                //ponte de comando SqlServer
                MySqlCommand com = new MySqlCommand(comando, conn);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    conn.Open();
                    com.ExecuteNonQuery();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    conn.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retorna um valor bool (true, false) informando se houve ou não alteração no dados no banco
        /// </summary>
        /// <param name="comando"> 
        /// O comando tem que seguir o padrão SqlServer "DELETE tabela WHERE condição" sé não informar condição deleta todos os dados da tabela 
        /// </param>
        /// 
        /// <param name="RouteConnect">
        /// Rota de conexão com o banco de dados
        /// </param>
        /// <returns></returns>
        public static bool Delete(string comando, string RouteConnect)
        {
            if (comando.Substring(0, 6).ToUpper() == "DELETE")
            {
                //ponte de conexão
                MySqlConnection conn = new MySqlConnection(RouteConnect);

                //ponte de comando SqlServer
                MySqlCommand com = new MySqlCommand(comando, conn);

                //tentando executar o comando SqlServer
                try
                {
                    //abrindo o banco de dados
                    conn.Open();
                    com.ExecuteNonQuery();

                    //retorna o valor de verdadeiro informando que execução do comando SqlServer foi um sucesso
                    return true;
                }
                catch
                {
                    //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                    return false;
                }
                finally
                {
                    //fechando o banco de dados
                    conn.Close();
                }
            }
            else
            {
                //retorna o valor de falso informando que houve um erro na execução do comando SqlServer
                return false;
            }
        }

        /// <summary>
        /// Retorna o valor da pesquisa no banco de Dados "SELECT colunas FROM tabela WHERE condição"
        /// </summary>
        public class Select
        {

            /// <summary>
            /// Retorna a pesquisa em um valor string (retorna apenas 1 valor)
            /// </summary>
            /// <param name="comando">
            /// Comando de pesquisa SqlServe "SELECT colunas FROM tabela WHERE condição"
            /// </param>
            /// <param name="campo">
            /// Campo a ser retornado (nome da coluna em que irar se tira os dados)
            /// </param>
            /// <param name="RouteConnect">
            /// Rota de conexão com o banco de dados
            /// </param>
            /// <returns></returns>
            public static string StringFormat(string comando, string campo, string RouteConnect)
            {
                //ponte de conexão
                MySqlConnection conexao = new MySqlConnection(RouteConnect);

                //string que vai registra o dado guardado no SQL
                string dado = "";

                //ponte de comando em SQL
                MySqlCommand comandos = new MySqlCommand(comando, conexao);

                try
                {
                    //abrindo banco
                    conexao.Open();

                    //ponte de leitura de bando SQL
                    MySqlDataReader dr = comandos.ExecuteReader();

                    //continua enquanto tiver linha para se ler
                    while (dr.Read())
                    {
                        dado = dr[campo].ToString();
                    }


                }
                catch (Exception)
                {
                    //não faz nada caso não retorne nenhuma informação
                }
                finally
                {
                    //feixando banco
                    conexao.Close();
                }
                return dado;
            }

            /// <summary>
            ///  Retorna a pesquisa em um formato DataTable (ideal para DataGrid) retorna todos os campos (colunas) da tabela 
            /// </summary>
            /// <param name="comando">
            /// Comando de pesquisa SqlServe "SELECT colunas FROM tabela WHERE condição"
            /// </param>
            /// <param name="RouteConnect">
            /// Rota de conexão com o banco de dados
            /// </param>
            /// <returns></returns>
            public static DataTable DataTableFormat(string comando, string RouteConnect)
            {
                //ponte de conexão
                MySqlConnection conexao = new MySqlConnection(RouteConnect);

                //ponte de comando Sql que adapita os dados para uma tabela
                MySqlDataAdapter da = new MySqlDataAdapter(comando, conexao);

                //objeto do tipo tabela que ira arquivar os valores da tabela lida no SqlServer
                DataTable dt = new DataTable();

                try
                {
                    //arquivando os dados na tabela
                    da.Fill(dt);
                }
                catch
                {
                    //não faz nada caso não retorne nenhuma informação
                }

                return dt;
            }

            /// <summary>
            /// Retorna a pesquisa em um formato ArryaList
            /// </summary>
            /// <param name="comando">
            /// Comando de pesquisa SqlServe "SELECT colunas FROM tabela WHERE condição"
            /// </param>
            /// <param name="campos">
            /// Campos a ser retornado o valor sera retornado na ordem que foi apresentado os nomes dentro do vetor
            /// </param>
            /// <param name="RouteConnect">
            /// Rota de conexão com o banco de dados
            /// </param>
            /// <returns></returns>
            public static ArrayList ArryaListFormat(string comando, string[] campos, string RouteConnect)
            {

                //ponte de conexão
                MySqlConnection conexao = new MySqlConnection(RouteConnect);

                //responsavel por armazenar os dados que forem puxados do banco
                ArrayList texto = new ArrayList();


                try
                {

                    //ponte de comando SQP
                    MySqlCommand comandos = new MySqlCommand(comando, conexao);

                    //Abrindo o Banco
                    conexao.Open();

                    MySqlDataReader dr = comandos.ExecuteReader();
                    //ira continuar ate toda a table ser lida
                    while (dr.Read())
                    {
                        //ira continuar ate todos os campos da linha serem lidos
                        for (int i = 0; i != campos.Length; i++)
                        {
                            //adicionando o item a ArrayList
                            texto.Add(dr[campos[i]].ToString());
                        }
                    }

                }
                catch (Exception)
                {
                    //não faz nada caso não retorne nenhuma informação
                }
                finally
                {
                    //fechando o banco
                    conexao.Close();
                }
                return texto;
            }

        }
    }


    /// <summary>
    /// Essa classe tem o foco de organizar e documentar os endereços do seu banco de dados (com o padrão SqlServer)
    /// </summary>
    public class Connection
    {

        //ponte de leitura e escrita
        private static StreamReader sr;
        private static StreamWriter sw;

        //responsavel por criar as pastas onde seram guardados os documentos de gerenciamento do banco de dados (com a rota do banco de dados)
        private void PastaDataBase()
        {
            //pasta data e dentro dela tera a pasta DataBase
            DirectoryInfo di = new DirectoryInfo(@"Data");
            di.CreateSubdirectory(@"DataBase");
        }

        /// <summary>
        /// Lê o ficheiro na pasta ...\Data\DataBase com a rota de conexão do banco de dados (o ficheiro tem que ser cirado mediante o comando "RegisterBD")
        /// </summary>
        /// <param name="DataBase">
        /// Nome da DataBase que ira ser pesquisada na documentação do sistema (tenha certeza de que o banco já esta registrado no programa)
        /// </param>
        /// <returns></returns>
        public static string Route(string DataBase)//(#BD)
        {
            //variavel que atribuindo endereço do banco de dados
            string local = "";

            //verifica a existencia do ficheiro se não existir acusa o erro
            if (File.Exists(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
            {
                using (sr = File.OpenText(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
                {
                    //caso já tenha cadastro no arquivo txt ele ira ler o endereço
                    local = sr.ReadLine();
                }
            }

            //retorna o endereço do banco de dados
            return local;
        }

        /// <summary>
        /// Escreve o ficheiro na pasta ...\Data\DataBase com a rota de conexão do banco de dados
        /// </summary>
        /// <param name="instancia">
        /// Nome da Instancia do Banco de Dados a ser conectado
        /// </param>
        /// 
        /// <param name="DataBase">
        /// Nome DataBase a ser conectada (sera usada para organizar o documento com a rota do banco de dados)
        /// </param>
        /// 
        /// <param name="user">
        /// Nome do USER a se conectar no banco de dados
        /// </param>
        /// 
        /// <param name="password">
        /// Senha do USER a se conectar no banco de dados
        /// </param>
        /// <returns></returns>
        public static bool RegisterBD(/*Adapitar par conexão com banco de dados MySql*/)
        {
            //se o usuario passa o valor vazio converte para nulo
            user = user == "" ? null : user;
            //se o usuario passa o valor vazio converte para nulo
            password = password == "" ? null : password;

            //se o cliente pasar um valor nulo/vazio ou informa que e local informa que o endereço do banco e localhost
            if (instancia == "" || instancia == "localhost" || instancia == null)
                instancia = @"localhost\SQLEXPRESS";

            //endereço do banco e da DataBase
            string banco = @"Data Source = " + instancia;

            banco += DataBase != "" || DataBase != null ? "; Initial Catalog = " + DataBase : "";

            //Caso o não se de senha ou usuarioa vai se entender que e um caso de Integrated Securyt caso o contrario sera cadastrado
            banco += user == null && password == null ? @"; Integrated Security = True" : "; USER ID = " + user + "; PASSWORD = " + password + " ;";

            //Caso não passe o usuario e senha
            //Data Source = instancia; Initial Catalog = database; Integrated Security = True;

            //Caso passe o usuario e senha
            //Data Source = instancia; Initial Catalog = database; USER ID = usuario; PASSWORD = senha;


            //testa primeiro a conecxão se ela for realizada com sucesso ela salva no ficheiro se não não faz auteração
            bool resposta = ConnectionTest(banco);

            if (resposta == true)
            {
                try
                {

                    //se existir com esse nome ele sera sobreescrevido
                    using (sw = File.CreateText(@"Data\Database\Connect_from_" + DataBase.ToUpper() + ".txt"))
                        //escrevendo as informações do banco de dados no ficheiro
                        sw.Write(banco);

                }
                catch (System.Exception)
                {
                    //se o ficheiro estiver aberto em segundo plano ou algo do genero retorna falso
                    return false;
                }
            }

            return resposta;

        }

        /// <summary>
        /// retorna um valor bool (true, false) infromando se a conexão e valida ou não
        /// </summary>
        /// <param name="Route">
        /// Rota de conexão com o banco de dados "DATA SOURCE = instancia; INITIAL CATALOG = database; USER ID = usuario; PASSWORD = senha;"
        /// </param>
        /// <returns></returns>
        public static bool ConnectionTest(string Route)
        {

            bool resposta;

            //CONECTANDO AO BANCO DE DADOS
            MySqlConnection connect = new MySqlConnection(Route);
            try
            {
                //abrindo o banco de dados para testar a conecção
                connect.Open();
                //quando o banco abri e tiver sucesso registra a resposta como verdadeira a conexão com o banco
                resposta = true;

            }
            catch
            {
                //quando o banco abri e tiver erro registra a resposta como falsa a conexão com o banco
                resposta = false;
            }
            finally
            {
                //fecha o banco apois o sucesso ou erro da execução do codigo
                connect.Close();
            }
            //retorna o valor da resposta
            return resposta;
        }

    }
}
