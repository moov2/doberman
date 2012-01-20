using System;
using System.Collections.Generic;
using Doberman.Model;

namespace Doberman.Checks
{
    public interface ICheck
    {
        DobermanResult Execute();
    }
}
