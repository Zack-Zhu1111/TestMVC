using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using TestMVC.Domain.ViewModels;
using System.Configuration;
using TestMVC.Data.Repositories;
using TestMVC.Provider;

namespace TestMVC.Controllers
{
    [RoutePrefix("api/BaseAPI")]
    public class BaseAPIController : ApiController
    {

        private readonly IInformationProvider _informationProvider;
       
        public BaseAPIController(IInformationProvider informationProvider)
        {
            _informationProvider = informationProvider;
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<List<InformationViewModel>> GetAllUser()
        {
            //if (LoginID == 20162068)
            //{
                var Users = await _informationProvider.GetAllUserList();
                return Users;
            //}
            //else
            //{
            //    return new List<InformationViewModel>();
            //}

        }

        [HttpGet]
        [Route("GetUser/{id:int}")]
        public async Task<InformationViewModel> GetUser(int id)
        {
            var user = await _informationProvider.GetUser(Convert.ToString(id));
            return user;
        }
    }
}
