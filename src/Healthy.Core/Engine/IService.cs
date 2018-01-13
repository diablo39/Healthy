using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine
{
    internal interface IService: IDisposable
    {
        void Start();

        void Stop();
    }
}
