﻿using System;
namespace ENTIDADES
{
    [Serializable]
    public class Venta
    {
        public string idVenta { get; set; }
        public Usuario usuario { get; set; }
        public string nombreCliente { get; set; }
        public double montoPago { get; set; }
        public double montoCambio { get; set; }
        public double montoTotal { get; set; }
        public string FechaVenta { get; set; }

    }
}