using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

class BaseClassWithFinalizer : IDisposable
{
    // To detect redundant calls
    private bool _disposedValue;

    // If the object gets garbage collected this finalizer (destructor) gets called and frees the unmanaged resources
     ~BaseClassWithFinalizer() => Dispose(false);

    // Instantiate a SafeHandle instance.
    private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

    // Public implementation of Dispose pattern callable by consumers.
    //  This permits the users to manually free both managed and unmanaged
    //  resources unlike the finalizer which has to do it only for unmanaged
    //  resources because in that situation managed resources get automatically
    //  disposed by the garbage collector on it's multiple cycle sweep
    public void Dispose()    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _safeHandle.Dispose();  // Dispose managed state (managed objects)
            }
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }
}