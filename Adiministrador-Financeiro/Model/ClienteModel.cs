﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adiministrador.Model
{
    public class ClienteModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id {  get; set; }
        public string Name {  get; set; }
    }
}
