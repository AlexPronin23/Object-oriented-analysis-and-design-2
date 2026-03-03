using System;
using System.Windows.Forms;

namespace BakeryApp
{
    public partial class Form1 : Form
    {
        // ==================== 📦 МОДЕЛИ ====================

        public enum BreadType
        {
            Baguette,
            Croissant,
            Brioche,
            Sourdough
        }

        // Простой класс без абстракции
        public class Bread
        {
            public string Name { get; set; }
            public string Style { get; set; }  // "French", "American", "Russian"...

            public string Bake() => $"🔥 Выпекаем {Name} ({Style} стиль)...";
            public string Wrap() => $"📦 Заворачиваем {Name} в крафт...";
            public string GetDescription() => $"🥐 {Style} {Name}";
        }


        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbBakeryStyle.SelectedIndex = 0;
            cmbBreadType.SelectedIndex = 0;
            Log("👋 Добро пожаловать!\r\n");
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string style = cmbBakeryStyle.SelectedItem?.ToString();
                string typeName = cmbBreadType.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(style) || string.IsNullOrEmpty(typeName))
                {
                    MessageBox.Show("Выберите все параметры!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Enum.TryParse<BreadType>(typeName, out var breadType))
                {
                    throw new ArgumentException($"Неверный тип: {typeName}");
                }

                Log($"📋 Заказ: {style} {typeName}");
                Log(new string('-', 40));

                // 🔴 🔴 🔴 ПРОБЛЕМА ЗДЕСЬ: ОГРОМНЫЙ SWITCH ДЛЯ СОЗДАНИЯ 🔴 🔴 🔴
                Bread bread = CreateBreadWithoutPattern(style, breadType);

                Log(bread.Bake());
                Log(bread.Wrap());

                lblResult.Text = $"✅ Готово: {bread.GetDescription()}";
                Log($"✨ Результат: {bread.GetDescription()}\r\n");
            }
            catch (Exception ex)
            {
                Log($"❌ Ошибка: {ex.Message}");
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔴 ЭТОТ МЕТОД НАРУШАЕТ OPEN/CLOSED PRINCIPLE
        // Добавил новую пекарню? ПРИДИ И ПОПРАВЬ ЭТОТ МЕТОД!
        private Bread CreateBreadWithoutPattern(string style, BreadType type)
        {
            Bread bread = new Bread();

            // 🔴 Уровень 1: выбираем стиль
            if (style == "French")
            {
                bread.Style = "French";
                // 🔴 Уровень 2: внутри стиля — ещё один switch по типу
                switch (type)
                {
                    case BreadType.Baguette:
                        bread.Name = "Baguette";
                        // 🔴 Французская специфика
                        break;
                    case BreadType.Croissant:
                        bread.Name = "Croissant";
                        // 🔴 Французская специфика
                        break;
                    case BreadType.Brioche:
                        bread.Name = "Brioche";
                        break;
                    case BreadType.Sourdough:
                        bread.Name = "Sourdough";
                        break;
                }
            }
            else if (style == "American")
            {
                bread.Style = "American";
                switch (type)
                {
                    case BreadType.Baguette:
                        bread.Name = "Baguette";
                        // 🔴 Американская специфика
                        break;
                    case BreadType.Croissant:
                        bread.Name = "Croissant";
                        break;
                    case BreadType.Brioche:
                        bread.Name = "Brioche";
                        break;
                    case BreadType.Sourdough:
                        bread.Name = "Sourdough";
                        break;
                }
            }
            else
            {
                throw new ArgumentException($"Неизвестный стиль: {style}");
            }

            return bread;
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => Log(message)));
                return;
            }
            txtLog.AppendText(message + "\r\n");
            txtLog.ScrollToCaret();
        }
    }
}