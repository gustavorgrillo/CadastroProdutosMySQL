using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using MySql.Data.MySqlClient;

namespace CadastroProdutosMySQL
{
    public partial class Form1 : Form
    {

        private MySqlConnection conexao;
        private string data_source = "datasource=localhost;username=root;password=;database=db_projeto";

        //tive que criar uma viariavel tipo anulável para aceitar tanto anulaveis quanto inteiros


        private int ?id_contato_selecionado = null;
        public Form1()
        {
            InitializeComponent();

            //carregar_contatos();

            lst_produtos.View = View.Details;
            lst_produtos.LabelEdit = true;
            lst_produtos.AllowColumnReorder = true;
            lst_produtos.FullRowSelect = true;
            lst_produtos.GridLines = true;

            lst_produtos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lst_produtos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lst_produtos.Columns.Add("Fornecedores", 100, HorizontalAlignment.Left); 
            lst_produtos.Columns.Add("Unidade", 100, HorizontalAlignment.Left);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                conexao = new MySqlConnection(data_source);

                conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conexao;


                if (id_contato_selecionado == null)
                {
                    cmd.CommandText = "INSERT INTO produtos (nome, fornecedor, unidade)" +
                                  "VALUES " +
                                  "(@nome, @fornecedor, @unidade)";


                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text);
                    cmd.Parameters.AddWithValue("@unidade", txtUnidade.Text);

                    
                    


                    if (txtNome.Text != string.Empty)
                    {

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Produto inserido com sucesso! ",
             "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        MessageBox.Show("Preencha todos os campos!");
                    }

                }
                else
                {
                    cmd.CommandText = "UPDATE produtos SET nome=@nome, fornecedor=@fornecedor, unidade=@unidade WHERE id=@id ";

                    

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text);
                    cmd.Parameters.AddWithValue("@unidade", txtUnidade.Text);
                    cmd.Parameters.AddWithValue("@id", id_contato_selecionado);

                    

                    

                    DialogResult result = MessageBox.Show("Deseja atualizar o cadastro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();

                              MessageBox.Show("Produto atualizado com sucesso! ",
                                              "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // a caixa será fechada e nada será feito
                    }

                }

                id_contato_selecionado = null;

                txtNome.Text = string.Empty;
                txtFornecedor.Text = "";
                txtUnidade.Text = "";



 

            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Erro" + ex.Number + "ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);


            } catch (Exception ex) 
            
            { 
            
              
                MessageBox.Show("Erro Ocorreu: " + ex.Message,
             "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);





            }
            finally
            {
                conexao.Close();

                // para a conexão fechar caso de algum erro no programa ou ocorra tudo certo.
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            try 
            {


                conexao = new MySqlConnection(data_source);

                conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conexao;

                cmd.CommandText = "SELECT * FROM produtos WHERE nome LIKE @q OR fornecedor LIKE @q";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@q", "%" + txt_Buscar.Text + "%");



                MySqlDataReader reader = cmd.ExecuteReader();

                lst_produtos.Items.Clear();

                //limpar e percorrer o select, seguindo as posições 0,1,2,3 (campos da tabela)

                while(reader.Read()) 
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),

                    };
                    var linha_listview = new ListViewItem(row);

                    lst_produtos.Items.Add(linha_listview);

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Erro" + ex.Number + "ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            catch (Exception ex)

            {


                MessageBox.Show("Erro Ocorreu: " + ex.Message,
             "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                conexao.Close();

                // para a conexão fechar caso de algum erro no programa ou ocorra tudo certo.
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                conexao = new MySqlConnection(data_source);

                conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conexao;

                if (id_contato_selecionado == null);
            {
                cmd.CommandText = "DELETE FROM produtos  WHERE  id= @id ";



                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text);
                cmd.Parameters.AddWithValue("@unidade", txtUnidade.Text);
                cmd.Parameters.AddWithValue("@id", id_contato_selecionado);



                    if (id_contato_selecionado == null)
                    {
                        MessageBox.Show("Selecione um produto primeiro!");

                    }
                    else
                    {

                        DialogResult result = MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Produto excluído com sucesso! ",
                                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // programa fecha e nada acontece.
                        }

                    }

                }

                

            id_contato_selecionado = null;

            txtNome.Text = string.Empty;
            txtFornecedor.Text = "";
            txtUnidade.Text = "";
        
        }
         catch (MySqlException ex)
            {

                MessageBox.Show("Erro" + ex.Number + "ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);


            } catch (Exception ex) 
            
            { 
            
              
                MessageBox.Show("Erro Ocorreu: " + ex.Message,
             "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);





            }
            finally
            {
                conexao.Close();

    // para a conexão fechar caso de algum erro no programa ou ocorra tudo certo.
        }
   }

        private void lst_produtos_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            ListView.SelectedListViewItemCollection itens_selecionados = lst_produtos.SelectedItems;

            foreach(ListViewItem item in itens_selecionados)
            {
               
                //tive que converter o id para inteiro, pois essa propriedade serve para int

                id_contato_selecionado = Convert.ToInt32(item.SubItems[0].Text);

                txtNome.Text = item.SubItems[1].Text;
                txtFornecedor.Text = item.SubItems[2].Text;
                txtUnidade.Text = item.SubItems[3].Text;

                //MessageBox.Show("o produto selecionado é " + id_contato_selecionado);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            id_contato_selecionado = null;

            txtNome.Text = string.Empty;
            txtFornecedor.Text = "";
            txtUnidade.Text = "";

            txtNome.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
