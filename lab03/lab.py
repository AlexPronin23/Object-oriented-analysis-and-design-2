import tkinter as tk
from tkinter import messagebox
from abc import ABC, abstractmethod



class Handler(ABC):
    def __init__(self, name, limit):
        self.name = name
        self.limit = limit
        self._next_handler = None
        self.gui_callback = None  # Ссылка на функцию обновления GUI

    def set_next(self, handler):
        self._next_handler = handler
        return handler

    def set_gui_callback(self, callback):
      
        self.gui_callback = callback

        if self._next_handler:
            self._next_handler.set_gui_callback(callback)

    def _log(self, message):
        if self.gui_callback:
            self.gui_callback(message)
        else:
            print(message) # Фоллбэк для консоли, если GUI нет

    @abstractmethod
    def handle(self, amount):
        pass

    def _pass_to_next(self, amount):
        if self._next_handler:
            return self._next_handler.handle(amount)
        else:
            return "Заявка отклонена: Превышен максимальный лимит компании."

class ManagerHandler(Handler):
    def handle(self, amount):
        # Теперь _log доступен, так как он есть в родителе Handler
        self._log(f"Менеджер ({self.name}) получил заявку на ${amount}")
        if amount <= self.limit:
            msg = f"✅ Одобрено Менеджером ({self.name}): ${amount}"
            self._log(msg)
            return msg
        else:
            self._log(f"❌ Менеджер ({self.name}): Слишком много (${amount} > ${self.limit}). Передаю дальше...")
            return self._pass_to_next(amount)

class DirectorHandler(Handler):
    def handle(self, amount):
        self._log(f"Директор ({self.name}) получил заявку на ${amount}")
        if amount <= self.limit:
            msg = f"✅ Одобрено Директором ({self.name}): ${amount}"
            self._log(msg)
            return msg
        else:
            self._log(f"❌ Директор ({self.name}): Слишком много (${amount} > ${self.limit}). Передаю дальше...")
            return self._pass_to_next(amount)

class VicePresidentHandler(Handler):
    def handle(self, amount):
        self._log(f"Вице-президент ({self.name}) получил заявку на ${amount}")
        if amount <= self.limit:
            msg = f"✅ Одобрено Вице-президентом ({self.name}): ${amount}"
            self._log(msg)
            return msg
        else:
            self._log(f"❌ Вице-президент ({self.name}): Отказ. Сумма ${amount} превышает даже мой лимит.")
            return "Заявка отклонена: Требуется совет директоров."




class ApprovalApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Chain of Responsibility Demo")
        self.root.geometry("600x500")

        # Создаем цепочку обработчиков
        self.manager = ManagerHandler("Иван Иванов", 1000)
        self.director = DirectorHandler("Петр Петров", 5000)
        self.vp = VicePresidentHandler("Анна Сидорова", 10000)

       
        self.manager.set_next(self.director).set_next(self.vp)
        
      
        self.manager.set_gui_callback(self.add_log)

      
        
        # Заголовок
        header = tk.Label(root, text="Система согласования расходов", font=("Arial", 16, "bold"))
        header.pack(pady=10)

        # Фрейм ввода
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

        # Легенда
        legend_frame = tk.Frame(root)
        legend_frame.pack(pady=5)
        tk.Label(legend_frame, text="Лимиты:", font=("Arial", 10, "underline")).pack()
        tk.Label(legend_frame, text="Менеджер: $1000 | Директор: $5000 | VP: $10000", font=("Arial", 9), fg="gray").pack()

    def add_log(self, message):
        self.log_text.config(state=tk.NORMAL)
        self.log_text.insert(tk.END, message + "\n")
        

        if "Одобрено" in message:
            self.log_text.tag_add("OK", "end-2l", "end-1l")
            self.log_text.tag_config("OK", foreground="green")
        elif "Отказ" in message or "отклонена" in message:
            self.log_text.tag_add("ERR", "end-2l", "end-1l")
            self.log_text.tag_config("ERR", foreground="red")
        elif "Передаю" in message:
            self.log_text.tag_add("PASS", "end-2l", "end-1l")
            self.log_text.tag_config("PASS", foreground="orange")
            
        self.log_text.see(tk.END)
        self.log_text.config(state=tk.DISABLED)
        self.root.update_idletasks()

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
        
        self.add_log(f"🚀 Новая заявка на сумму: ${amount}")
        self.add_log("-" * 40)

        # Запуск цепочки
        result = self.manager.handle(amount)
        
        self.add_log("-" * 40)
        self.add_log(f"🏁 ИТОГ: {result}")

if __name__ == "__main__":
    root = tk.Tk()
    app = ApprovalApp(root)
    root.mainloop()