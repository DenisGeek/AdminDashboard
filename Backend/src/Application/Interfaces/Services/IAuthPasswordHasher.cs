﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public interface IAuthPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}