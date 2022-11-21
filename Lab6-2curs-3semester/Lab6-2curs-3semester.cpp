#include <iostream>
#include <windows.h>
#include <intrin.h>
#include <comdef.h>
#include <Mmsystem.h>
#include <stdio.h>
#include <conio.h>

#define DIV 1024
#define WIDTH 7


bool shiftPressed = false;
bool altPressed = false;
bool f8Pressed = false;

bool keyLocked = false;


void UpdateLock() {
    if (shiftPressed && altPressed && f8Pressed) {
        keyLocked = !keyLocked;
        if (keyLocked) {
            std::cout << "Клавіша 7 заблокована" << std::endl;
        }
        else {
            std::cout << "Клавіша 7 розблокована" << std::endl;
        }
    }
}

LRESULT CALLBACK LowLevelKeyboardProc(int nCode, WPARAM wParam, LPARAM lParam) {
    if (nCode == HC_ACTION) {

        KBDLLHOOKSTRUCT* details = (KBDLLHOOKSTRUCT*)lParam;
        //std::cout << details->vkCode << std::endl;

        switch (wParam) {
        case WM_KEYDOWN:
        case WM_SYSKEYDOWN:
            switch (details->vkCode) {
            case VK_RSHIFT:
                if (!shiftPressed) {
                    shiftPressed = true;
                }
                break;
            case VK_LMENU:
                if (!altPressed && shiftPressed) {
                    altPressed = true;
                }
                break;
            case 0x77:
                if (!f8Pressed && altPressed && shiftPressed) {
                    f8Pressed = true;
                }
                break;
            }
            break;
        case WM_KEYUP:
        case WM_SYSKEYUP:
            switch (details->vkCode) {
            case VK_RSHIFT:
                shiftPressed = false;
                break;
            case VK_LMENU:
                altPressed = false;
                break; 
            case 0x77:
                f8Pressed = false;
                break;
            }
            break;
        }
        UpdateLock();
        if (wParam == WM_KEYDOWN) {
            if (details->vkCode == 0x37) {
                if (keyLocked) {
                    return 1;
                }
            }
        }
    }
    return CallNextHookEx(NULL, nCode, wParam, lParam);
}




int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);




    std::cout << "Завдання 2 \n\n";

    HHOOK hook = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardProc, 0, 0);
    MSG msg;
    while (GetMessage(&msg, NULL, NULL, NULL) > 0) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    UnhookWindowsHookEx(hook);
    return 1;
}



