using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sporty.DAL;
using Sporty.DAL.Models;
using Sporty.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Sporty.Web.Commons.Attributes;

namespace Sporty.Web.API.Controllers
{
    [RoutePrefix("api/user")]
    [ApplicationAuthorize(Roles = "Customer")]
    public class UserController : ApiController
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().Get<ApplicationUserManager>();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult List(GetAccessTokenBindingModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new List<Place> { new Place(), new Place() });
            }
            return BadRequest(ModelState);
        }

        //
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAccessToken(GetAccessTokenBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password"),
                    new KeyValuePair<string, string>( "username", model.Email),
                    new KeyValuePair<string, string> ( "Password", model.Password)
                };
                HttpResponseMessage response;
                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(pairs);
                    response = await client.PostAsync("http://" + Request.RequestUri.Host
                        + ":" + Request.RequestUri.Port
                        + "/Token", content);
                }

                if (response.StatusCode == HttpStatusCode.OK)
                    return Ok(await response.Content.ReadAsAsync<GetAccessTokenViewModel>());
                else
                    return BadRequest();
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                try
                {
                    using (var dbContext = Request.GetOwinContext().Get<SportyDbContext>())
                    {
                        using (var trans = dbContext.Database.BeginTransaction())
                        {
                            var result = await UserManager.CreateAsync(user, model.Password);
                            if (!result.Succeeded)
                            {
                                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors));
                            }

                            result = await UserManager.AddToRoleAsync(user.Id, "Customer");
                            //result = await UserManager.AddToRole
                            if (!result.Succeeded)
                            {
                                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors));
                            }

                            // OK
                            trans.Commit();
                            return Ok();
                        }
                    }
                }
                catch (DbEntityValidationException e)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, e.EntityValidationErrors));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return Ok(user);
        }





        // Test
        [HttpGet]
        [AllowAnonymous]
        [Route("Test")]
        public string Test()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, "Hello");
            //response.Headers.Add("Access-Control-Allow-Origin", "*");
            return "Hello";
        }
    }
}