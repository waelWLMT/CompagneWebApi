using CampaignPubTest.MockData.WebApiMock;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Dtos;
using WebApi.Helpers;
using Xunit;
using IAuthenticationService = WebApi.Helpers.IAuthenticationService;

namespace CampaignPubTest.WebApiTests.ContorllerTests
{
    
    public class AutenticationControllerTest
    {      
        [Theory]
        [InlineData("admin", "test1")]
        [InlineData("wael", "xxxx")]
        public void AuthenticateTest(string userName, string password)
        {

            // arrange
            var identity = new AuthenticateRequest()
            {
                Username = userName,
                Password = password
            };           
            var mockAuthService = new Mock<IAuthenticationService>();

            mockAuthService.Setup(
                service => 
                service.Authenticate(identity)
                ); 

            var authController = new AuthenticationController(mockAuthService.Object);
                        
            // Act
            var actionResult =  authController.Authenticate(identity);

            // Assert
            if (userName.Equals("admin") && password.Equals("test1"))
            {
                mockAuthService.Setup(
                    service => 
                    service.Authenticate(identity))
                    .Returns(
                    AuthenticationMockData.GetAdminIdentity(identity.Username, identity.Password)
                    );

                authController = new AuthenticationController(mockAuthService.Object);

                // Act
                actionResult = authController.Authenticate(identity);

                var result = actionResult as OkObjectResult;
                var model = result.Value as AuthenticateResponse;               
               
                Assert.IsType<OkObjectResult>(actionResult);
                Assert.NotNull(result);                
                Assert.NotNull(model.User);
                Assert.Equal(15, model.User.Menus.Count);
            }
            else
            {
                var result = actionResult as BadRequestObjectResult;
                var message = result.Value.ToString();
                var expected = new 
                {
                    message = "Username or password is incorrect"
                }.ToString();

                Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal(expected, message);
            }

            


        }

    }
}
