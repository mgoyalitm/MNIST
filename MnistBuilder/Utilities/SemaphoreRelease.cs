using System;
using System.Collections.Generic;
using System.Text;

namespace MNIST.Utilities;

public class SemaphoreRelease(SemaphoreSlim semaphore) : IDisposable
{
    private readonly SemaphoreSlim _semaphore = semaphore;

    public void Dispose()
    {
        _semaphore.Release();
    }
}
