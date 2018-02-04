using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine
{
    public interface IService: IDisposable
    {
        void Start();

        void Stop();
    }
}
