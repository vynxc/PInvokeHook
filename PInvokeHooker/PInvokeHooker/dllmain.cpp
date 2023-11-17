#include <windows.h>
#include "dllmain.h"
#include <iostream>
#include <thread>

//extern LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

extern "C" {

	typedef int (*PresentDelegate)(IDXGISwapChain* pSwapChain, int syncInterval, int flags);

	PresentDelegate callbackFunc;


	__declspec(dllexport) void SetPresent(PresentDelegate callback) {

		callbackFunc = callback;
	}

	typedef void (*RenderDelegate)();

	RenderDelegate callbackRender;
	__declspec(dllexport) void SetRenderDraw(RenderDelegate callback) {

		callbackRender = callback;
	}

	typedef LRESULT(*WndProcDelegate)(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

	WndProcDelegate callbackWndProc;
	__declspec(dllexport) void SetWndProcHandler(WndProcDelegate callback) {

		callbackWndProc = callback;
	}

	LRESULT __stdcall WndProc(const HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {

		if (true && callbackWndProc(hWnd, uMsg, wParam, lParam))
			return true;

		return CallWindowProc(oWndProc, hWnd, uMsg, wParam, lParam);
	}

	bool init = false;
	HRESULT __stdcall hkPresent(IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags) {
		std::cout << "CPP: hkPresent";
		if (!init)
		{
			if (SUCCEEDED(pSwapChain->GetDevice(__uuidof(ID3D11Device), (void**)&pDevice)))
			{
				pDevice->GetImmediateContext(&pContext);
				DXGI_SWAP_CHAIN_DESC sd;
				pSwapChain->GetDesc(&sd);
				window = sd.OutputWindow;
				ID3D11Texture2D* pBackBuffer;
				pSwapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);
				pDevice->CreateRenderTargetView(pBackBuffer, NULL, &mainRenderTargetView);
				pBackBuffer->Release();
				oWndProc = (WNDPROC)SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)WndProc);
				//InitImGui();
				init = true;
				std::cout << "CPP: Init Complete";
			}
			else
				return oPresent(pSwapChain, SyncInterval, Flags);
		}


		if (callbackFunc != NULL)
			callbackFunc(pSwapChain, SyncInterval, Flags);

		pContext->OMSetRenderTargets(1, &mainRenderTargetView, NULL);
		callbackRender();
		return oPresent(pSwapChain, SyncInterval, Flags);
	}



	bool init_kiero() {
		if (kiero::init(kiero::RenderType::D3D11) == kiero::Status::Success) {
			kiero::bind(8, (void**)&oPresent, hkPresent);
			std::cout << "CPP: Init_kiero Complete";
			return true;
		}
		std::cout << "CPP: Init_kiero failed";
		return false;
	}

	__declspec(dllexport) void Init() {
		init_kiero();
	}

	__declspec(dllexport) HWND GetHWND() {

		return window;
	}

	__declspec(dllexport) ID3D11Device* GetDevice() {

		return pDevice;
	}
	__declspec(dllexport) ID3D11DeviceContext* GetContext() {

		return pContext;
	}
}