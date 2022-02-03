using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
    public interface IFileFormatReader
    {
        string readData();
    }
}
