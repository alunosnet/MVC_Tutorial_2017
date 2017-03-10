using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Tutorial_2017.Models
{
    public class EntradaSaidaModel
    {
        public int id { get; set; }
        public int id_quarto { get; set; }
        public int id_cliente { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime data_entrada { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime data_saida { get; set; }
        [DataType(DataType.Currency)]
        public decimal valor_pago { get; set; }
    }
    public class EntradaSaidaBD
    {

        public List<EntradaSaidaModel> listaTodos()
        {
            string sql = "SELECT * FROM entradasaida";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            foreach (DataRow dados in registos.Rows)
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                novo.data_saida = DateTime.Parse(dados[4].ToString());
                novo.valor_pago = Decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<EntradaSaidaModel> listaSaidas()
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida not null";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            foreach (DataRow dados in registos.Rows)
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                novo.data_saida = DateTime.Parse(dados[4].ToString());
                novo.valor_pago = Decimal.Parse(dados[5].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public List<EntradaSaidaModel> listaOcupados()
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida is null";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            foreach (DataRow dados in registos.Rows)
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                DateTime data;
                if (DateTime.TryParse(dados[4].ToString(), out data))
                    novo.data_saida = data;
                decimal valor_pago;
                if (Decimal.TryParse(dados[5].ToString(), out valor_pago))
                    novo.valor_pago = valor_pago;
                lista.Add(novo);
            }

            return lista;
        }
        public List<EntradaSaidaModel> listaOcupados(int id)
        {
            string sql = "SELECT * FROM entradasaida WHERE data_saida is null and id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<EntradaSaidaModel> lista = new List<EntradaSaidaModel>();

            foreach (DataRow dados in registos.Rows)
            {
                EntradaSaidaModel novo = new EntradaSaidaModel();
                novo.id = int.Parse(dados[0].ToString());
                novo.id_quarto = int.Parse(dados[1].ToString());
                novo.id_cliente = int.Parse(dados[2].ToString());
                novo.data_entrada = DateTime.Parse(dados[3].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        //entrada
        public void registarEntrada(EntradaSaidaModel novo)
        {
            string sql = "INSERT INTO entradasaida (id_quarto,id_cliente,data_entrada) VALUES ";
            sql += "(@id_quarto,@id_cliente,@data_entrada)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id_quarto",SqlDbType=SqlDbType.Int,Value=novo.id_quarto },
                new SqlParameter() {ParameterName="@id_cliente",SqlDbType=SqlDbType.Int,Value=novo.id_cliente },
                new SqlParameter() {ParameterName="@data_entrada",SqlDbType=SqlDbType.Date,Value=novo.data_entrada },
            };
            BD.Instance.executaComando(sql, parametros);

            parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=novo.id_quarto },
            };
            //atualizar estado do quarto
            sql = "UPDATE quartos SET estado='false' WHERE nr=@nr";
            BD.Instance.executaComando(sql, parametros);
        }
        //saida
        public void registarSaida(EntradaSaidaModel novo)
        {
            string sql = "UPDATE entradasaida set data_saida=@data_saida,valor_pago=@valor_pago WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@data_saida",SqlDbType=SqlDbType.Date,Value=novo.data_saida },
                new SqlParameter() {ParameterName="@valor_pago",SqlDbType=SqlDbType.Decimal,Value=novo.valor_pago },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=novo.id },
            };
            BD.Instance.executaComando(sql, parametros);
            //atualizar estado do quarto
            sql = "UPDATE quartos SET estado='true' WHERE nr=@nr";
            parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=novo.id_quarto },
            };
            BD.Instance.executaComando(sql, parametros);
        }
    }
}