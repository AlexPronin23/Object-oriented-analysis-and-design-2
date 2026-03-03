using System;
using System.Windows.Forms;

namespace BakeryApp
{
    public partial class Form1 : Form
    {
        // ==================== 📦 МОДЕЛИ ====================

        // Типы выпечки
        public enum BreadType
        {
            Baguette,
            Croissant,
            Brioche,
            Sourdough
        }

        // Абстрактный продукт
        public abstract class Bread
        {
            public string Name { get; protected set; }
            public string Style { get; protected set; }

            public virtual string Bake()
            {
                return $"🔥 Выпекаем {Name} ({Style} стиль)...";
            }

            public virtual string Wrap()
            {
                return $"📦 Заворачиваем {Name} в крафт...";
            }

            public string GetDescription() => $"🥐 {Style} {Name}";
        }

        // 🇫🇷 Французские стили
        public class FrenchBaguette : Bread
        {
            public FrenchBaguette() { Name = "Baguette"; Style = "French"; }
        }
        public class FrenchCroissant : Bread
        {
            public FrenchCroissant() { Name = "Croissant"; Style = "French"; }
            public override string Wrap()
            {
                return "📦 Заворачиваем круассан в фирменную бумагу с логотипом...";
            }
        }
        public class FrenchBrioche : Bread
        {
            public FrenchBrioche() { Name = "Brioche"; Style = "French"; }
        }
        public class FrenchSourdough : Bread
        {
            public FrenchSourdough() { Name = "Sourdough"; Style = "French"; }
        }

        // 🇺 Американские стили
        public class AmericanBaguette : Bread
        {
            public AmericanBaguette() { Name = "Baguette"; Style = "American"; }
            public override string Bake()
            {
                return "🔥 Выпекаем американский багет (более мягкий)...";
            }
        }
        public class AmericanCroissant : Bread
        {
            public AmericanCroissant() { Name = "Croissant"; Style = "American"; }
        }
        public class AmericanBrioche : Bread
        {
            public AmericanBrioche() { Name = "Brioche"; Style = "American"; }
        }
        public class AmericanSourdough : Bread
        {
            public AmericanSourdough() { Name = "Sourdough"; Style = "American"; }
        }


        // ==================== 🏭 ФАБРИКИ ====================

        // Абстрактная фабрика (Factory Method)
        public abstract class Bakery
        {
            public Bread OrderBread(BreadType type, Action<string> log)
            {
                Bread bread = CreateBread(type);
                log(bread.Bake());
                log(bread.Wrap());
                return bread;
            }

            protected abstract Bread CreateBread(BreadType type);
        }

        // Французская пекарня
        public class FrenchBakery : Bakery
        {
            protected override Bread CreateBread(BreadType type)
            {
                switch (type)
                {
                    case BreadType.Baguette:
                        return new FrenchBaguette();
                    case BreadType.Croissant:
                        return new FrenchCroissant();
                    case BreadType.Brioche:
                        return new FrenchBrioche();
                    case BreadType.Sourdough:
                        return new FrenchSourdough();
                    default:
                        throw new ArgumentException($"Неизвестный тип: {type}");
                }
            }
        }

        // Американская пекарня
        public class AmericanBakery : Bakery
        {
            protected override Bread CreateBread(BreadType type)
            {
                switch (type)
                {
                    case BreadType.Baguette:
                        return new AmericanBaguette();
                    case BreadType.Croissant:
                        return new AmericanCroissant();
                    case BreadType.Brioche:
                        return new AmericanBrioche();
                    case BreadType.Sourdough:
                        return new AmericanSourdough();
                    default:
                        throw new ArgumentException($"Неизвестный тип: {type}");
                }
            }
        }


        // ==================== 🎮 UI ЛОГИКА ====================

        private Bakery _currentBakery;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbBakeryStyle.SelectedIndex = 0;
            cmbBreadType.SelectedIndex = 0;
            Log("👋 Добро пожаловать в пекарню!\r\nВыберите стиль и тип выпечки.\r\n");
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

                // Выбираем фабрику
                if (style == "French")
                    _currentBakery = new FrenchBakery();
                else if (style == "American")
                    _currentBakery = new AmericanBakery();
                else
                    throw new ArgumentException($"Неизвестный стиль: {style}");

                Log($"📋 Заказ: {style} {typeName}");
                Log(new string('-', 40));

                // Выполняем заказ через фабрику
                Bread bread = _currentBakery.OrderBread(breadType, Log);

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