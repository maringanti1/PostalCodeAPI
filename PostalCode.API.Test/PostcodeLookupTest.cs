using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostalCode.API.Model;
using PostalCode.API.Service;
using PostalCode.API.Service.Common;
using PostalCode.API.Service.Interfaces;
using PostcodeLookup.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PostalCode.API.Test
{
    [TestClass]
    public class PostCodesLookupControllerTests
    {
        private readonly Mock<IPostCodesLookupService> _mockService;
        private readonly PostCodesLookupController _controller;

        public PostCodesLookupControllerTests()
        {
            _mockService = new Mock<IPostCodesLookupService>();
            _controller = new PostCodesLookupController(_mockService.Object);
        }

        [TestMethod]
        public async Task AutocompletePostcode_ValidPartialPostcode_ReturnsSearchResults()
        {
            // Arrange
            string partialPostcode = "AB12";
            var expectedResults = new List<SearchResult>
        {
            new SearchResult { Postcode = "AB12 3CD" },
            new SearchResult { Postcode = "AB12 4EF" }
        };
            _mockService.Setup(s => s.AutocompletePostcode(partialPostcode)).ReturnsAsync(expectedResults);

            // Act
            var result = await _controller.AutocompletePostcode(partialPostcode);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<SearchResult>>));
            var actionResult = result as ActionResult<IEnumerable<SearchResult>>;
            Assert.IsNotNull(actionResult.Value);
            var actualResults = actionResult.Value.ToList();
            Assert.AreEqual(expectedResults.Count, actualResults.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.AreEqual(expectedResults[i].Postcode, actualResults[i].Postcode); 
            }
        }

        [TestMethod]
        public async Task AutocompletePostcode_InvalidPartialPostcode_ThrowsCustomException()
        {
            // Arrange
            string partialPostcode = "AB1";
            _mockService.Setup(s => s.AutocompletePostcode(partialPostcode))
                        .ThrowsAsync(new CustomException((int)HttpStatusCode.BadRequest, "Invalid partial postcode"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _controller.AutocompletePostcode(partialPostcode));
        }

        [TestMethod]
        public async Task LookupPostcodeAsync_Returns_Successful_Result()
        {
            // Arrange
            var postcode = "AB1 2CD";
            var expected = new PostcodeResult { Postcode = postcode };

            _mockService.Setup(x => x.LookupPostcode(postcode)).ReturnsAsync(expected);

            // Act
            var result = await _controller.LookupPostcodeAsync(postcode);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PostcodeResult>));
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public async Task LookupPostcodeAsync_Throws_CustomException()
        {
            // Arrange
            var postcode = "AB1 2CD";
            var exception = new CustomException((int)HttpStatusCode.BadRequest, "Invalid postcode");

            _mockService.Setup(x => x.LookupPostcode(postcode)).ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<CustomException>(() => _controller.LookupPostcodeAsync(postcode));
        }
    }

}
