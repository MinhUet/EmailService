using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BVMinh.EmailService.DL.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>
    {
        public ApplicationRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public async Task<Application> FindByAppCode(string appCode)
        {
            return await _collection.AsQueryable().Where(app => app.ApplicationCode == appCode).FirstAsync();
        }
    }
}
