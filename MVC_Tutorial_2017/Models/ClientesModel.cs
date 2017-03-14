using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace MVC_Tutorial_2017.Models
{
    public class ClientesModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome do cliente")]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "O nome é muito pequeno")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Tem de indicar a morada do cliente")]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "Morada muito pequena")]
        public string morada { get; set; }

        [Required(ErrorMessage = "Tem de indicar o código postal do cliente")]
        [StringLength(8)]
        [MinLength(7, ErrorMessage = "O código postal é muito pequeno")]
        [Display(Name = "Código Postal")]
        public string cp { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public string telefone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Tem de indicar a data de nascimento do cliente")]
        public DateTime data_nascimento { get; set; }
    }

    public class ClientesBD
    {

        public List<ClientesModel> lista()
        {
            string sql = "SELECT * FROM Clientes";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<ClientesModel> lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {

                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.data_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<ClientesModel> lista(string nome)
        {
            string sql = "SELECT * FROM Clientes WHERE nome=@nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<ClientesModel> lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.data_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<ClientesModel> lista(int id)
        {
            string sql = "SELECT * FROM Clientes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<ClientesModel> lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.data_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public int nrRegistos()
        {
            string sql = @"SELECT count(*) from clientes";
            return BD.Instance.executaScalar(sql);
        }
        public List<ClientesModel> listaPagina(int nPagina,int registosPorPagina)
        {
            string sql = @"SELECT * FROM (select row_number() over (order by nome) as rownum, *
                            FROM Clientes) AS p WHERE rownum>=@primeiro AND rownum<=@ultimo";

            int primeiro = (nPagina-1) * registosPorPagina;
            int ultimo = primeiro + registosPorPagina;
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@primeiro",SqlDbType=SqlDbType.Int,Value=primeiro },
                new SqlParameter() {ParameterName="@ultimo",SqlDbType=SqlDbType.Int,Value=ultimo },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<ClientesModel> lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[1].ToString());
                novo.nome = dados[2].ToString();
                novo.morada = dados[3].ToString();
                novo.cp = dados[4].ToString();
                novo.email = dados[5].ToString();
                novo.telefone = dados[6].ToString();
                novo.data_nascimento = DateTime.Parse(dados[7].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public int adicionarCliente(ClientesModel novo)
        {
            string sql = "INSERT INTO Clientes(nome,morada,cp,email,telefone,data_nascimento) VALUES ";
            sql += " (@nome,@morada,@cp,@email,@telefone,@data);SELECT cast(scope_identity() as int);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=novo.nome },
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=novo.morada },
                new SqlParameter() {ParameterName="@cp",SqlDbType=SqlDbType.VarChar,Value=novo.cp },
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=novo.email },
                new SqlParameter() {ParameterName="@telefone",SqlDbType=SqlDbType.VarChar,Value=novo.telefone },
                new SqlParameter() {ParameterName="@data",SqlDbType=SqlDbType.Date,Value=novo.data_nascimento },
            };
            int id = (int)BD.Instance.executaScalar(sql, parametros);
            return id;
        }
        public void atualizarCliente(ClientesModel cliente)
        {
            string sql = "UPDATE Clientes SET nome=@nome,morada=@morada,cp=@cp,";
            sql += "email=@email,telefone=@telefone,data_nascimento=@data ";
            sql += "WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=cliente.nome },
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=cliente.morada },
                new SqlParameter() {ParameterName="@cp",SqlDbType=SqlDbType.VarChar,Value=cliente.cp },
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=cliente.email },
                new SqlParameter() {ParameterName="@telefone",SqlDbType=SqlDbType.VarChar,Value=cliente.telefone },
                new SqlParameter() {ParameterName="@data",SqlDbType=SqlDbType.Date,Value=cliente.data_nascimento },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=cliente.id },
            };
            BD.Instance.executaComando(sql, parametros);
            return;
        }
        public void removerCliente(int id)
        {

            string sql = "DELETE FROM Clientes WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=id }
            };
            BD.Instance.executaComando(sql, parametros);
        }
        public List<ClientesModel> pesquisa(string nome)
        {
            string sql = "SELECT * FROM Clientes WHERE nome like @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=System.Data.SqlDbType.VarChar,Value="%" + (string)nome + "%" }
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<ClientesModel> lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {
                ClientesModel novo = new ClientesModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.morada = dados[2].ToString();
                novo.cp = dados[3].ToString();
                novo.email = dados[4].ToString();
                novo.telefone = dados[5].ToString();
                novo.data_nascimento = DateTime.Parse(dados[6].ToString());
                lista.Add(novo);
            }

            return lista;
        }
    }
}