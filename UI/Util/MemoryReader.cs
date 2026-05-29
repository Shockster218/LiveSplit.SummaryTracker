using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class MemoryReader : IDisposable
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(
        uint dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    private const uint PROCESS_VM_READ = 0x0010;
    private const uint PROCESS_QUERY_INFORMATION = 0x0400;

    private IntPtr _processHandle;

    public Process Process { get; }

    public MemoryReader(string processName)
    {
        var processes = Process.GetProcessesByName(processName);

        if (processes.Length == 0)
            throw new Exception($"Process '{processName}' not found.");

        Process = processes[0];

        _processHandle = OpenProcess(
            PROCESS_VM_READ | PROCESS_QUERY_INFORMATION,
            false,
            Process.Id);

        if (_processHandle == IntPtr.Zero)
            throw new Exception("Failed to open process.");
    }

    public float ReadAddressFloat(string address)
    {
        address = address.Trim();

        if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            address = address.Substring(2);

        long addr = long.Parse(
            address,
            System.Globalization.NumberStyles.HexNumber);

        IntPtr ptr = (IntPtr)addr;

        byte[] buffer = new byte[4];

        if (!ReadProcessMemory(
            _processHandle,
            ptr,
            buffer,
            buffer.Length,
            out int bytesRead) || bytesRead != 4)
        {
            throw new Exception("Failed to read memory.");
        }

        return BitConverter.ToSingle(buffer, 0);
    }

    public int ReadAddressInt(string address)
    {
        address = address.Trim();

        if (address.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            address = address.Substring(2);

        long addr = long.Parse(
            address,
            System.Globalization.NumberStyles.HexNumber);

        IntPtr ptr = (IntPtr)addr;

        byte[] buffer = new byte[4];

        if (!ReadProcessMemory(
            _processHandle,
            ptr,
            buffer,
            buffer.Length,
            out int bytesRead) || bytesRead != 4)
        {
            throw new Exception("Failed to read memory.");
        }

        return BitConverter.ToInt32(buffer, 0);
    }

    public byte[] ReadBytes(IntPtr address, int size)
    {
        byte[] buffer = new byte[size];

        ReadProcessMemory(
            _processHandle,
            address,
            buffer,
            size,
            out _);

        return buffer;
    }

    public void Dispose()
    {
        if (_processHandle != IntPtr.Zero)
        {
            CloseHandle(_processHandle);
            _processHandle = IntPtr.Zero;
        }
    }
}