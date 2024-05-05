using API.Messages;
using Core;
using Core.Models;
using Core.ViewModels;
using Persistence.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace API.Services
{
    public class AppContentElementService : ServiceBase<AppContentElement, AppContentElementView>
    {



        public AppContentElementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        private ApiAppContentElementView mock()
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

        public async Task<ApiAppContentElementView> GetTreeStructure()
        {

            ApiAppContentElementView root = new()
            {
                Title = "Table of Contents"
            };

            //IEnumerable<AppContentElement> rootChildren = await _unitOfWork.AppContentElements.FindAsync(a => a.Parent == null);
            IEnumerable<AppContentElement> allElements = await _unitOfWork.AppContentElements.GetAllAsync();

            foreach (var item in allElements)
            {
                if (item.Parent == null)
                {
                    root.Children.Add(getSubTree(item));
                }
            }

            //return mock();
            return root;
        }

        private ApiAppContentElementView getSubTree(AppContentElement appContentElement)
        {
            ApiAppContentElementView response = new()
            {
                Title = appContentElement.Title,
                Text = appContentElement.Text,
                Icon = appContentElement.Icon,

            };

            if (appContentElement.Children != null)
            {
                foreach (var item in appContentElement.Children)
                {
                    response.Children.Add(getSubTree(item));
                };
            }

            return response;
        }
    }
}
