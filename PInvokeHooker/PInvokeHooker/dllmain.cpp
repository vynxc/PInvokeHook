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

	uint64_t find_pattern(const char* module, const char* pattern)
	{
		uint64_t moduleAdress = (uint64_t)GetModuleHandleA(module);
		if (!moduleAdress)
			return 0;

		static auto patternToByte = [](const char* pattern)
			{
				auto       bytes = std::vector<int>{};
				const auto start = const_cast<char*>(pattern);
				const auto end = const_cast<char*>(pattern) + strlen(pattern);

				for (auto current = start; current < end; ++current)
				{
					if (*current == '?')
					{
						++current;
						if (*current == '?')
							++current;
						bytes.push_back(-1);
					}
					else { bytes.push_back(strtoul(current, &current, 16)); }
				}
				return bytes;
			};

		const auto dosHeader = (PIMAGE_DOS_HEADER)moduleAdress;
		const auto ntHeaders = (PIMAGE_NT_HEADERS)((std::uint8_t*)moduleAdress + dosHeader->e_lfanew);

		const auto sizeOfImage = ntHeaders->OptionalHeader.SizeOfImage;
		auto       patternBytes = patternToByte(pattern);
		const auto scanBytes = reinterpret_cast<std::uint8_t*>(moduleAdress);

		const auto s = patternBytes.size();
		const auto d = patternBytes.data();

		for (auto i = 0ul; i < sizeOfImage - s; ++i)
		{
			bool found = true;
			for (auto j = 0ul; j < s; ++j)
			{
				if (scanBytes[i + j] != d[j] && d[j] != -1)
				{
					found = false;
					break;
				}
			}
			if (found) { return reinterpret_cast<uintptr_t>(&scanBytes[i]); }
		}
		return NULL;
	}

	bool init_kiero() {
		if (kiero::init(kiero::RenderType::D3D11) == kiero::Status::Success) {
			kiero::bind(8, (void**)&oPresent, hkPresent);
			return true;
		}
		return false;
	}
	bool steam = false;
	bool discord = false;
	__declspec(dllexport) void Init() {
		if (steam) {
			auto present_hk_sig = find_pattern("GameOverlayRenderer64.dll", "48 89 6C 24 ? 48 89 74 24 ? 41 56 48 83 EC ? 41 8B E8");
			auto create_hk_sig = find_pattern("GameOverlayRenderer64.dll", "48 89 5C 24 ? 57 48 83 EC ? 33 C0");

			__int64(__fastcall * CreateHook)(
				unsigned __int64 pFuncAddress,
				__int64 pDetourFuncAddress,
				unsigned __int64* pOriginalFuncAddressOut,
				int a4);

			CreateHook = (decltype(CreateHook))create_hk_sig;
			CreateHook(present_hk_sig, (__int64)&hkPresent, (unsigned __int64*)&oPresent, 1);
		}
		else if (discord) {
			uint64_t addr = (uint64_t)(GetModuleHandleA("DiscordHook64.dll")) + 0xE9090;
			Present* discord_present = (Present*)addr;
			oPresent = *discord_present;
			_InterlockedExchangePointer((volatile PVOID*)addr, hkPresent);
		}
		else {
			init_kiero();
		}

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