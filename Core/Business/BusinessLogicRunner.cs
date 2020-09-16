using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace forgeSampleAPI_DotNetCore.Core.Business
{
    public static class BusinessLogicRunner
    {

        public static T RunnerStatmentOptional<T>(bool statment, T t1, T t2) where T:class
        {
            if (statment)
            {
                return t1;
            }

            else
            {
                return t2;
            }
        }
        

    }
}
