﻿using Arriba.Serialization;

namespace Arriba.Composition
{
    public static class ArribaServices
    {
        public static void Initialize()
        {
            ArribaConvert.Assign(new NewtonsoftJsonArribaConvert());
        }
    }
}
