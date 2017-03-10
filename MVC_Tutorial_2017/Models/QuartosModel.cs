using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace MVC_Tutorial_2017.Models
{
    public class QuartosModel
    {
        [Key]
        public int nr { get; set; }

        [Required(ErrorMessage = "Deve indicar o piso do quarto")]
        public int piso { get; set; }

        [Required(ErrorMessage = "Deve indicar a lotação")]
        public int lotacao { get; set; }

        [Required(ErrorMessage = "Deve indicar o estado do quarto")]
        public bool estado { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Deve indicar o preço por dia do quarto")]
        public decimal custo_dia { get; set; }

        public int idCliente { get; set; }
    }
    public class QuartosBD
    {      

        public List<QuartosModel> lista()
        {
            string sql = "SELECT * FROM Quartos";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<QuartosModel> lista = new List<QuartosModel>();

            foreach (DataRow dados in registos.Rows)
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente = -1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<QuartosModel> lista(int nr)
        {
            string sql = "SELECT * FROM Quartos WHERE nr=@nr";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=nr },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql,parametros);
            
            List<QuartosModel> lista = new List<QuartosModel>();
            foreach (DataRow dados in registos.Rows)
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente = -1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<QuartosModel> listaVazios()
        {
            string sql = "SELECT * FROM Quartos WHERE estado='True'";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<QuartosModel> lista = new List<QuartosModel>();

            foreach (DataRow dados in registos.Rows)
            {
                QuartosModel novo = new QuartosModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.piso = int.Parse(dados[1].ToString());
                novo.lotacao = int.Parse(dados[2].ToString());
                novo.estado = bool.Parse(dados[3].ToString());
                int id_cliente = -1;
                int.TryParse(dados[4].ToString(), out id_cliente);
                novo.idCliente = id_cliente;
                novo.custo_dia = decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public void adicionarQuarto(QuartosModel novo)
        {
            string sql = "INSERT INTO quartos(piso,lotacao,estado,custo_dia) VALUES";
            sql += " (@piso,@lotacao,@estado,@custo_dia)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@piso",SqlDbType=SqlDbType.VarChar,Value=novo.piso },
                new SqlParameter() {ParameterName="@lotacao",SqlDbType=SqlDbType.VarChar,Value=novo.lotacao },
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=novo.estado },
                new SqlParameter() {ParameterName="@custo_dia",SqlDbType=SqlDbType.Int,Value=novo.custo_dia },
            };
            BD.Instance.executaComando(sql, parametros);
        }
        public void removerQuarto(int nr)
        {
            string sql = "DELETE FROM quartos WHERE nr=@nr";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=System.Data.SqlDbType.Int,Value=nr }
            };
            BD.Instance.executaComando(sql, parametros);
        }
        public void atualizarQuarto(QuartosModel quarto)
        {
            string sql = "UPDATE Quartos SET piso=@piso,lotacao=@lotacao,estado=@estado,custo_dia=@custo_dia ";
            sql += " WHERE nr=@nr";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@piso",SqlDbType=SqlDbType.VarChar,Value=quarto.piso },
                new SqlParameter() {ParameterName="@lotacao",SqlDbType=SqlDbType.VarChar,Value=quarto.lotacao },
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=quarto.estado },
                new SqlParameter() {ParameterName="@custo_dia",SqlDbType=SqlDbType.Int,Value=quarto.custo_dia },
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=quarto.nr },
            };
            BD.Instance.executaComando(sql, parametros);
        }
    }
}