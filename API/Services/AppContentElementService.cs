using API.Messages;
using Core;
using Core.Models;
using Core.ViewModels;

namespace API.Services
{
    public class AppContentElementService : ServiceBase<AppContentElement, AppContentElementView>
    {
        public AppContentElementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<ApiAppContentElementView> GetTreeStructure()
        {
            return new()
            {
                Children = [
                    new () {
                        Text = "text-01"
                    },
                    new () {
                        Text = "text-02",
                        Children = [
                            new () {
                                Title= "title-02",
                                Text = "text-02-01"
                            }]
                    }]
            };
        }
    }
}
