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
            Cliente.setCNPJ(textBox1.Text.Trim());

            BLL.validaCNPJ();
            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg());
                return;
            }

            if (!BLL.getCliente(Cliente.getCNPJ()))
            {
                MessageBox.Show("Cliente não encontrado!");
                return;
            }

            textBox2.Text = Cliente.getNome();

            var vendas = BLL.getVendasCliente(Cliente.getCNPJ());

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            decimal totalTon = 0;
            decimal totalVal = 0;

            foreach (var venda in vendas)
            {
                listBox1.Items.Add(venda[0]); // Data
                listBox2.Items.Add(venda[1]); // Toneladas
                listBox3.Items.Add(venda[2]); // Valor

                decimal ton;
                if (decimal.TryParse(venda[1], out ton))
                    totalTon += ton;

                decimal val;
                if (decimal.TryParse(venda[2], out val))
                    totalVal += val;
            }

            textBox3.Text = totalTon.ToString("N2");
            textBox4.Text = totalVal.ToString("C");
        }
    }
}
