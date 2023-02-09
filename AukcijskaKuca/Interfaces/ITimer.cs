using System;

namespace AukcijskaKuca.Interfaces
{
    public interface ITimer
    {
        Action StartTimer { get; set; }
    }
}
