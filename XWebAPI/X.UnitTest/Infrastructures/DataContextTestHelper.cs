using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace X.UnitTest.Infrastructures
{
    public class DataContextTestHelper
    {
        private readonly DbContextHelper _dbContextHelper;

        public DataContextTestHelper(DbContextHelper dbContextHelper)
        {
            _dbContextHelper = dbContextHelper;
        }

        public void Add(object obj)
        {
            try
            {
                using (var dataContext = _dbContextHelper.CreateDataContext())
                {
                    dataContext.Add(obj);
                    dataContext.SaveChanges();
                };
            }
            catch (DbUpdateException e)
            {
                var message = e.Entries.FirstOrDefault().ToString();
                throw new Exception($"{e.Message}\n{message}");
            }
        }

        public void AddRange(IEnumerable<object> obj)
        {
            try
            {
                using (var dataContext = _dbContextHelper.CreateDataContext())
                {
                    dataContext.AddRange(obj);
                    dataContext.SaveChanges();
                };
            }
            catch (DbUpdateException e)
            {
                var message = e.Entries.FirstOrDefault().ToString();
                throw new Exception($"{e.Message}\n{message}");
            }
        }
    }
}
