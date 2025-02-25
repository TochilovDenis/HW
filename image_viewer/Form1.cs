using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Настройка PictureBox для масштабирования изображения
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                       
            // Подписываемся на события нажатия кнопок
            button1.Click += BtnSimple_Click;
            button2.Click += BtnSafe_Click;
        }

        private void BtnSimple_Click(object sender, EventArgs e)
        {
            // Простой способ: прямая загрузка без проверок
            pictureBox1.Load(Clipboard.GetText());
            webBrowser1.Navigate(Clipboard.GetText());
        }

        private void BtnSafe_Click(object sender, EventArgs e)
        {
            // Безопасный способ: с проверками и обработкой ошибок
            try
            {
                string url = Clipboard.GetText();

                if (!string.IsNullOrEmpty(url) &&
                    (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                     url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                {
                    pictureBox1.Load(url);
                    webBrowser1.Navigate(url);
                }
                else
                {
                    MessageBox.Show("В буфере обмена нет корректного URL адреса");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}");
            }
        }
    }
}