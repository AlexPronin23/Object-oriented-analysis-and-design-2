import tkinter as tk
from tkinter import messagebox

# У нас нет классов Handler, ManagerHandler и т.д.
# Вся логика находится внутри приложения.

class ApprovalAppNoPattern:
    def __init__(self, root):
        self.root = root
        self.root.title("Согласование БЕЗ паттерна")
        self.root.geometry("600x500")

        # --- Элементы интерфейса (те же самые) ---
        
        header = tk.Label(root, text="Система согласования расходов", font=("Arial", 16, "bold"))
        header.pack(pady=10)

        input_frame = tk.Frame(root)
        input_frame.pack(pady=10)

        tk.Label(input_frame, text="Сумма заявки ($):", font=("Arial", 12)).pack(side=tk.LEFT, padx=5)
        self.amount_entry = tk.Entry(input_frame, font=("Arial", 12), width=15)
        self.amount_entry.pack(side=tk.LEFT, padx=5)
        
        submit_btn = tk.Button(input_frame, text="Отправить на согласование", 
                               command=self.submit_request, bg="#4CAF50", fg="white", font=("Arial", 10, "bold"))
        submit_btn.pack(side=tk.LEFT, padx=10)

        tk.Frame(root, height=2, bd=1, relief=tk.SUNKEN).pack(fill=tk.X, padx=5, pady=5)

        status_label = tk.Label(root, text="Статус обработки:", font=("Arial", 12, "bold"))
        status_label.pack(anchor=tk.W, padx=10)

        self.log_text = tk.Text(root, height=12, width=70, state=tk.DISABLED, font=("Consolas", 10))
        self.log_text.pack(padx=10, pady=5)

        legend_frame = tk.Frame(root)
        legend_frame.pack(pady=5)
        tk.Label(legend_frame, text="Лимиты:", font=("Arial", 10, "underline")).pack()
        tk.Label(legend_frame, text="Менеджер: $1000 | Директор: $5000 | VP: $10000", font=("Arial", 9), fg="gray").pack()

    def add_log(self, message):
        """Такой же метод для вывода текста"""
        self.log_text.config(state=tk.NORMAL)
        self.log_text.insert(tk.END, message + "\n")
        
        if "Одобрено" in message:
            self.log_text.tag_add("OK", "end-2l", "end-1l")
            self.log_text.tag_config("OK", foreground="green")
        elif "Отказ" in message or "отклонена" in message:
            self.log_text.tag_add("ERR", "end-2l", "end-1l")
            self.log_text.tag_config("ERR", foreground="red")
            
        self.log_text.see(tk.END)
        self.log_text.config(state=tk.DISABLED)
        self.root.update_idletasks()

    def approve_expense(self, amount):
        """
        ВОТ ГЛАВНОЕ ОТЛИЧИЕ:
        Вся бизнес-логика зашита здесь в виде жестких условий.
        Нет объектов, нет цепочки. Только данные и условия.
        """
        self.add_log(f"🚀 Начало обработки суммы: ${amount}")
        
        # Уровень 1: Менеджер
        if amount <= 1000:
            self.add_log(f"✅ Менеджер (Иван Иванов): Одобрено (${amount})")
            return f"Одобрено Менеджером: ${amount}"
        
        # Уровень 2: Директор
        elif amount <= 5000:
            self.add_log(f"❌ Менеджер: Превышен лимит ($1000). Передача...")
            self.add_log(f"✅ Директор (Петр Петров): Одобрено (${amount})")
            return f"Одобрено Директором: ${amount}"
        
        # Уровень 3: Вице-президент
        elif amount <= 10000:
            self.add_log(f"❌ Менеджер: Превышен лимит ($1000). Передача...")
            self.add_log(f"❌ Директор: Превышен лимит ($5000). Передача...")
            self.add_log(f"✅ Вице-президент (Анна Сидорова): Одобрено (${amount})")
            return f"Одобрено VP: ${amount}"
        
        # Отказ
        else:
            self.add_log(f"❌ Менеджер: Превышен лимит.")
            self.add_log(f"❌ Директор: Превышен лимит.")
            self.add_log(f"❌ Вице-президент: Превышен лимит ($10000).")
            self.add_log(f"🏁 ИТОГ: Заявка отклонена.")
            return "Заявка отклонена: Требуется совет директоров."

    def submit_request(self):
        try:
            amount_str = self.amount_entry.get()
            if not amount_str:
                raise ValueError
            amount = int(amount_str)
            if amount < 0:
                raise ValueError
        except ValueError:
            messagebox.showerror("Ошибка", "Введите корректную положительную сумму!")
            return

        self.log_text.config(state=tk.NORMAL)
        self.log_text.delete(1.0, tk.END)
        self.log_text.config(state=tk.DISABLED)

        # Вызываем монолитный метод
        result = self.approve_expense(amount)
        
        self.add_log("-" * 40)
        self.add_log(f"🏁 ФИНАЛЬНЫЙ РЕЗУЛЬТАТ: {result}")

if __name__ == "__main__":
    root = tk.Tk()
    app = ApprovalAppNoPattern(root)
    root.mainloop()