using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BLL.desconecta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            string cnpj = textBox1.Text.Trim();

            if (BLL.getCliente(cnpj))
            {
                textBox2.Text = Cliente.getNome();
                List<string[]> vendas = BLL.getVendasCliente(cnpj);

                decimal totalTon = 0;
                decimal totalVal = 0;

                foreach (var venda in vendas)
                {
                    listBox1.Items.Add(venda[0]); 
                    listBox2.Items.Add(venda[1]); 

                    decimal valorVenda;
                    if (decimal.TryParse(venda[2], out valorVenda))
                    {
                        listBox3.Items.Add(valorVenda.ToString("C"));
                        totalVal += valorVenda;
                    }
                    decimal toneladasVenda;
                    if (decimal.TryParse(venda[1], out toneladasVenda))
                    {
                        totalTon += toneladasVenda;
                    }
                }

                textBox3.Text = totalTon.ToString("N2");
                textBox4.Text = totalVal.ToString("C");
            }
            else
            {
                MessageBox.Show(Erro.getMsg());
            }

        }
    }
}
