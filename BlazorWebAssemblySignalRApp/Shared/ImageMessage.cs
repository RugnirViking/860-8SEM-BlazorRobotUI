using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWebAssemblySignalRApp.Shared
{
    class ImageMessage
    {
        public byte[] ImageBinary { get; set; }
        public string ImageHeaders { get; set; }
    }
}
