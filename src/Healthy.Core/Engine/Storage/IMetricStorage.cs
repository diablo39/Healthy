using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Core.Engine.Storage
{
    public interface IMetricStorage
    {
        void SaveValue(string name, decimal value, DateTime moment);
    }
}
