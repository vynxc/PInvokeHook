#pragma once

#include <Windows.h>
#include <d3d11.h>
#include <dxgi.h>
#include "kiero/kiero.h"
#include <vector>

using Present = HRESULT(__stdcall*)(IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags);
using WNDPROC = LRESULT(CALLBACK*)(HWND, UINT, WPARAM, LPARAM);
using PTR = uintptr_t;

ID3D11Device* pDevice;
ID3D11DeviceContext* pContext;
ID3D11RenderTargetView* mainRenderTargetView;
Present oPresent;
HWND window;
WNDPROC oWndProc;

#pragma region Statics
static HINSTANCE g_hInstance = NULL;

#pragma endregion

