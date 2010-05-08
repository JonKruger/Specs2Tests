using System;

namespace Specs2Tests
{
    public interface IClipboard
    {
        string GetData();
        void SetData(string data);
    }
}