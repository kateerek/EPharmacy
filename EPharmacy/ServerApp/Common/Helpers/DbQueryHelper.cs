using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Common.Helpers
{
    static public class DbQueryHelper
    {
        static public async Task<R> RunWithTransactionAsync<R>(Func<Task<R>> func, DbContext context) where R : class
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = await func();
                    transaction.Commit();
                    return result;
                } catch(Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        static public async Task RunWithTransactionAsync(Func<Task> func, DbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await func();
                    transaction.Commit();
                } catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
