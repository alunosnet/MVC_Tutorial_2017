using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace MVC_Tutorial_2017.Models
{
    public class UtilizadoresModel
    {
        [Required(ErrorMessage ="Campo nome tem de ser preenchido")]
        [StringLength(50)]
        [MinLength(2,ErrorMessage ="Nome muito pequeno")]
        public string nome { get; set; }
        [Display(Name ="Palavra passe")]
        [MinLength(5, ErrorMessage = "Palavra passe muito pequena")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirme a sua palavra passe")]
        [Compare("password",ErrorMessage ="Palavras passe não são iguais")]
        public string confirmaPassword { get; set; }
        public int perfil { get; set; }

        public bool estado { get; set; }
    }

    public class UtilizadoresBD
    {
        //create
        public void adicionarUtilizadores(UtilizadoresModel novo)
        {
            string sql = @"INSERT INTO Utilizadores(nome,password,perfil,estado)
                        VALUES (@nome,HASHBYTES('SHA2_512',@password),@perfil,@estado)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=novo.nome},
                new SqlParameter(){ParameterName="@password",
                    SqlDbType =SqlDbType.VarChar,Value=novo.password},
                 new SqlParameter(){ParameterName="@perfil",
                    SqlDbType =SqlDbType.Int,Value=novo.perfil},
                 new SqlParameter(){ParameterName="@estado",
                    SqlDbType =SqlDbType.Int,Value=novo.estado},
            };
            BD.Instance.executaComando(sql, parametros);
        }
        //read
        public List<UtilizadoresModel> lista()
        {
            string sql = "SELECT * FROM utilizadores";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();
            foreach(DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                novo.nome = data[0].ToString();
                novo.password = data[1].ToString();
                novo.perfil = int.Parse(data[2].ToString());
                novo.estado = bool.Parse(data[3].ToString());
                lista.Add(novo);
            }
            return lista;
        }
        public List<UtilizadoresModel> lista(string nome)
        {
            string sql = "SELECT * FROM utilizadores WHERE nome like @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value="%"+nome+"%"}
            };
            DataTable registos = BD.Instance.devolveConsulta(sql,parametros);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();
            foreach (DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                novo.nome = data[0].ToString();
                novo.password = data[1].ToString();
                novo.perfil = int.Parse(data[2].ToString());
                novo.estado = bool.Parse(data[3].ToString());
                lista.Add(novo);
            }
            return lista;
        }
        //update
        //delete
    }
}