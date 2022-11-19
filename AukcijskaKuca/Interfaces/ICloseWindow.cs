using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukcijskaKuca.Interfaces
{
    internal interface ICloseWindow
    {
        Action CloseWindow { get; set; }
    }
}
