﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
    }
}
