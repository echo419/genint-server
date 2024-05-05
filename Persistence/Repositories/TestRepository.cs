using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Core.Models;
using Core.Repositories;


namespace Persistence.Repositories
{

    public class TestRepository : ITestRepository
    {

        public IEnumerable<Test> GetAll()
        {

            List<Test> response = new List<Test>();

            for (int i = 0; i < 10; i++)
            {

                Test test = new Test();
                test.StringValue01 = "string01_" + i.ToString();
                test.StringValue02 = "string02_" + i.ToString();
                test.StringValue03 = "string03_" + i.ToString();
                test.StringValue04 = "string04_" + i.ToString();
                test.StringValue05 = "string05_" + i.ToString();

                response.Add(test);
            }



            return response;

        }

        public Task<IEnumerable<Test>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}