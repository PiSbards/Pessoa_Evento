using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CadastroPessoas
{
    internal class funcoes
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string celular { get; set; }
        public string cpf { get; set; }
        public string cidade { get; set; }
        public string dataNascimento { get; set; }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\TDS\2° Periodo\PA - Prof.Emerson\CadastroPessoas\CadastroPessoas\Database1.mdf"";Integrated Security=True");

        public List<funcoes> listaPessoa()
        {
            List<funcoes> li = new List<funcoes>();
            string sql = "SELECT * FROM Pessoa";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                funcoes pes = new funcoes();
                pes.Id = (int)dr["id"];
                pes.nome = dr["nome"].ToString();
                pes.celular = dr["celular"].ToString();
                pes.cidade = dr["cidade"].ToString();
                pes.cpf = dr["cpf"].ToString();
                pes.dataNascimento = dr["dataNascimento"].ToString();
                li.Add(pes);
            }
            return li;
        }

        public void Inserir(string nome, string celular, string cidade, string cpf, string dataNascimento)
        {
            string sql = "INSERT INTO Pessoa(nome, celular, cidade, cpf, dataNascimento) VALUES('"+nome+"', '"+celular+"', '"+cidade+"', '"+cpf+"','"+dataNascimento+"')";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Pessoa WHERE Id = '" + Id + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                celular = dr["celular"].ToString();
                cidade = dr["cidade"].ToString();
                cpf = dr["cpf"].ToString();
                dataNascimento = dr["dataNascimento"].ToString();
            }
            con.Close();
        }

        public void Atualizar(int Id, string nome, string celular, string cidade, string cpf, string dataNascimento)
        {
            string sql = "UPDATE Pessoa SET nome='" + nome + "',celular='" + celular + "',cidade='"+cidade+"',cpf='"+cpf+"',dataNascimento='"+dataNascimento+"' WHERE Id='" + Id + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(int Id)
        {
            string sql = "DELETE FROM Pessoa WHERE Id= '" + Id + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool RegistroRepetido(string nome, string celular, string cidade, string cpf, string dataNascimento)
        {
            string sql = "SELECT * FROM Pessoa WHERE nome='" +nome+ "'AND celular='"+celular+"'AND cidade='"+cidade+"'AND cpf='"+cpf+"'AND dataNascimento='"+dataNascimento+"'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}