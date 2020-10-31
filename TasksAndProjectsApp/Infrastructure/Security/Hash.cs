using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAndProjectsApp.Infrastructure.Security
{
    public static class Hash
    {
        public static byte[] HashString(string value)
        {
            byte[] hash = System.Text.Encoding.ASCII.GetBytes(value);
            return new System.Security.Cryptography.SHA256Managed().ComputeHash(hash);
        }
    }
}
