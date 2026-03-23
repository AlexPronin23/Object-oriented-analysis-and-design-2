
#include <windows.h>
#include <string>
#include <memory>

#pragma comment(lib, "comctl32.lib")


// Реализация: Платёжные шлюзы
class PaymentGateway {
public:
    virtual ~PaymentGateway() = default;
    virtual bool process(double amount) = 0;
    virtual const char* name() const = 0;
    virtual double fee() const = 0;
};

class Stripe : public PaymentGateway {
public:
    bool process(double amount) override { return true; }
    const char* name() const override { return "Stripe"; }
    double fee() const override { return 2.9; }
};

class PayPal : public PaymentGateway {
public:
    bool process(double amount) override { return true; }
    const char* name() const override { return "PayPal"; }
    double fee() const override { return 3.4; }
};

class Bank : public PaymentGateway {
public:
    bool process(double amount) override { return true; }
    const char* name() const override { return "Банк"; }
    double fee() const override { return 1.5; }
};

// Абстракция: Способы оплаты
class PaymentMethod {
protected:
    std::shared_ptr<PaymentGateway> gw;
public:
    PaymentMethod(std::shared_ptr<PaymentGateway> g) : gw(g) {}
    virtual ~PaymentMethod() = default;
    virtual bool pay(double amount) = 0;
    virtual const char* type() const = 0;

    // Расчёт комиссии
    double calcFee(double amount) {
        return amount * gw->fee() / 100.0;
    }

    // Геттер для доступа к шлюзу извне
    std::shared_ptr<PaymentGateway> getGateway() const { return gw; }
};

// Оплата картой
class CardPay : public PaymentMethod {
public:
    CardPay(std::shared_ptr<PaymentGateway> g) : PaymentMethod(g) {}
    bool pay(double amount) override {
        return gw->process(amount + calcFee(amount));
    }
    const char* type() const override { return "Карта"; }
};

// Оплата криптовалютой
class CryptoPay : public PaymentMethod {
public:
    CryptoPay(std::shared_ptr<PaymentGateway> g) : PaymentMethod(g) {}
    bool pay(double amount) override {
        return gw->process(amount);
    }
    const char* type() const override { return "Крипта"; }
};


HWND hWnd, hComboGW, hComboMethod, hEditAmount, hTxtResult;
std::shared_ptr<PaymentGateway> gateways[3] = {
    std::make_shared<Stripe>(),
    std::make_shared<PayPal>(),
    std::make_shared<Bank>()
};
std::unique_ptr<PaymentMethod> payment;
int gwIdx = 0, methodIdx = 0;


// Безопасная конвертация строки в число
double safeStod(const wchar_t* wstr, double def = 100.0) {
    try {
        std::string str(wstr, wstr + wcslen(wstr));
        if (str.empty()) return def;
        double val = std::stod(str);
        return (val > 0) ? val : def;
    }
    catch (...) { return def; }
}

// Обновить текущий метод оплаты при смене настроек
void updatePayment() {
    if (methodIdx == 0)
        payment = std::make_unique<CardPay>(gateways[gwIdx]);
    else
        payment = std::make_unique<CryptoPay>(gateways[gwIdx]);
}

// Обработать платёж
void doPayment() {
    wchar_t buf[50];
    GetWindowTextW(hEditAmount, buf, 50);
    double amount = safeStod(buf);
    double fee = payment->calcFee(amount);

    char result[256];
    if (payment->pay(amount)) {
        sprintf_s(result,
            "УСПЕХ: $%.2f + комиссия $%.2f через %s/%s",
            amount, fee,
            payment->type(),
            payment->getGateway()->name());
    }
    else {
        sprintf_s(result, "ОШИБКА ПЛАТЕЖА!");
    }

    SetWindowTextA(hTxtResult, result);
}



LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wp, LPARAM lp) {
    if (msg == WM_CREATE) return 0;

    if (msg == WM_COMMAND) {
        switch (LOWORD(wp)) {
        case 1: // Смена платёжного шлюза
            if (HIWORD(wp) == CBN_SELCHANGE) {
                gwIdx = (int)SendMessageA(hComboGW, CB_GETCURSEL, 0, 0);
                updatePayment();
            }
            break;

        case 2: // Смена способа оплаты
            if (HIWORD(wp) == CBN_SELCHANGE) {
                methodIdx = (int)SendMessageA(hComboMethod, CB_GETCURSEL, 0, 0);
                updatePayment();
            }
            break;

        case 10: // Кнопка ОПЛАТИТЬ
            doPayment();
            break;

        case 11: // Кнопка ВОЗВРАТ
            SetWindowTextA(hTxtResult, "Возврат средств обработан");
            break;
        }
    }

    if (msg == WM_DESTROY) PostQuitMessage(0);
    return DefWindowProcA(hWnd, msg, wp, lp);
}


void createUI(HWND hWnd) {
    
    HFONT font = CreateFontA(14, 0, 0, 0, FW_NORMAL, 0, 0, 0,
        RUSSIAN_CHARSET, 0, 0, 0, 0, "Segoe UI");


    CreateWindowA("STATIC", "Платёжный шлюз:", WS_CHILD | WS_VISIBLE,
        20, 20, 120, 20, hWnd, 0, 0, 0);

    hComboGW = CreateWindowA("COMBOBOX", "",
        WS_CHILD | WS_VISIBLE | CBS_DROPDOWNLIST,
        140, 18, 180, 100, hWnd, (HMENU)1, nullptr, nullptr);
    SendMessageA(hComboGW, CB_ADDSTRING, 0, (LPARAM)"Stripe (2.9%)");
    SendMessageA(hComboGW, CB_ADDSTRING, 0, (LPARAM)"PayPal (3.4%)");
    SendMessageA(hComboGW, CB_ADDSTRING, 0, (LPARAM)"Банк (1.5%)");
    SendMessageA(hComboGW, CB_SETCURSEL, 0, 0);

    
    CreateWindowA("STATIC", "Способ оплаты:", WS_CHILD | WS_VISIBLE,
        20, 60, 120, 20, hWnd, 0, 0, 0);

    hComboMethod = CreateWindowA("COMBOBOX", "",
        WS_CHILD | WS_VISIBLE | CBS_DROPDOWNLIST,
        140, 58, 180, 100, hWnd, (HMENU)2, nullptr, nullptr);
    SendMessageA(hComboMethod, CB_ADDSTRING, 0, (LPARAM)"💳 Банковская карта");
    SendMessageA(hComboMethod, CB_ADDSTRING, 0, (LPARAM)"₿ Криптовалюта");
    SendMessageA(hComboMethod, CB_SETCURSEL, 0, 0);

    
    CreateWindowA("STATIC", "Сумма ($):", WS_CHILD | WS_VISIBLE,
        20, 100, 80, 20, hWnd, 0, 0, 0);

    hEditAmount = CreateWindowA("EDIT", "100",
        WS_CHILD | WS_VISIBLE | WS_BORDER | ES_NUMBER,
        100, 98, 100, 25, hWnd, (HMENU)3, 0, 0);

    CreateWindowA("BUTTON", "💳 ОПЛАТИТЬ",
        WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        20, 140, 120, 35, hWnd, (HMENU)10, 0, 0);

    CreateWindowA("BUTTON", "🔄 ВОЗВРАТ",
        WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        150, 140, 120, 35, hWnd, (HMENU)11, 0, 0);

    hTxtResult = CreateWindowA("STATIC", "",
        WS_CHILD | WS_VISIBLE | SS_CENTER,
        20, 190, 400, 30, hWnd, 0, 0, 0);

   
    for (HWND ctrl : {hComboGW, hComboMethod, hEditAmount, hTxtResult})
        SendMessageA(ctrl, WM_SETFONT, (WPARAM)font, TRUE);

    updatePayment();
}



int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst, LPSTR lpCmd, int nShow) {
    // Регистрация класса окна
    WNDCLASSEXA wc = {};
    wc.cbSize = sizeof(wc);
    wc.style = CS_HREDRAW | CS_VREDRAW;
    wc.lpfnWndProc = WndProc;
    wc.hInstance = hInst;
    wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wc.lpszClassName = "BridgeApp";
    wc.hIcon = nullptr;
    wc.hIconSm = nullptr;

    RegisterClassExA(&wc);

    // Создание главного окна
    hWnd = CreateWindowExA(0, "BridgeApp",
        "💳 Система оплаты (Паттерн МОСТ)",
        WS_OVERLAPPEDWINDOW & ~WS_THICKFRAME,
        100, 100, 500, 320, 0, 0, hInst, 0);

    if (!hWnd) return 0;

    ShowWindow(hWnd, nShow);
    UpdateWindow(hWnd);
    createUI(hWnd);

    // Главный цикл сообщений
    MSG msg;
    while (GetMessage(&msg, 0, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    return (int)msg.wParam;
}