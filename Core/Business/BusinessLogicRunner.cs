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

        public static dynamic RunnerStatmentOptional(bool statment, dynamic t1, dynamic t2)
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

        public async static Task RunnerStatmentOptionalAsync(bool statment, Task t1, Task t2)
        {
            if (statment)
            {
                await t1;
            }

            else
            {
                await t2;
            }
        }
        public async static Task<T> RunnerStatmentOptionalAsync<T>(bool statment, Task<T> t1, Task<T> t2) where T : class
        {
            if (statment)
            {
                return await t1;
            }

            else
            {
                return await t2;
            }
        }





    }
}
